using Client.OMCS;
using Client2Server;
using NCLib;
using OMCS.Passive.Video;
using OMCS.Passive.WhiteBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// 客户端
    /// </summary>
    public class Client : IClientLogic
    {
        private IClientSocket clientSocket;
        private CallOMCS callOMCS;
        /// <summary>
        /// 等待接受响应结果
        /// </summary>
        private bool waitingRespond = false;
        /// <summary>
        /// 是否发送成功
        /// </summary>
        private bool isSendSucceful = true;
        /// <summary>
        /// 锁对象 互斥保证同一时刻只能有一个异步请求在等待响应
        /// </summary>
        private object LOCK;
        /// <summary>
        /// 响应结果
        /// </summary>
        private Dictionary<string, string> respondMessage;
        public User_Client UserInfo;
        public string builder;

        #region 事件集
        /// <summary>
        /// 连接结束事件
        /// </summary>
        public event Action<Result> ConnectEnded;
        /// <summary>
        /// 语音组加入事件传递
        /// </summary>
        public event Action<string> SomeoneJoin;
        /// <summary>
        /// 语音组离开事件传递
        /// </summary>
        public event Action<string> SomeoneExit;
        /// <summary>
        /// 某人请求发言
        /// </summary>
        public event Action<string> SomeoneSpeakInform;
        /// <summary>
        /// 某人被修改发言权限操作
        /// </summary>
        public event someoneMuteHandler onSomeoneMute;
        /// <summary>
        /// 触发异常
        /// </summary>
        public event Action<Exception> OnException;
        #endregion

        public Client()
        {
            clientSocket = new ClientSocket();
            clientSocket.OnException += e => { OnException?.Invoke(e); };
            UserInfo = new User_Client();
            LOCK = new object();
        }

        public void Init(string serverIP, int serverPort, int localPort)
        {
            clientSocket.Access(serverIP, serverPort, localPort, Accept);
        }

        #region 客户端逻辑
        public IResult Login(string id, string password)
        {
            lock (LOCK)
            {
                this.UserInfo = new User_Client(id);
                if (waitingRespond) { throw new Exception("产生过未上锁的异步请求"); }
                this.waitingRespond = true;
                isSendSucceful = true;
                clientSocket.Send(MessageTranslate.EncapsulationInfo(MessageContent.登录, MessageType.请求, id, password), ar => { if (ar == baseResult.Faild.ToString()) isSendSucceful = false; });
                while (waitingRespond) { if (!isSendSucceful) { waitingRespond = false; return new Result(baseResult.Faild, "发送send失败"); } }
                //等待响应
                if (respondMessage["结果"] == baseResult.Successful.ToString())
                {
                    this.UserInfo.UserState = UserState.已登录;
                    UserInfo.IsPrerogative = Boolean.Parse(respondMessage["权限"]);
                    return new Result(baseResult.Successful, respondMessage["权限"]);
                }
                return new Result(baseResult.Faild, respondMessage["权限"]);
            }
        }
        public IAsyncResult BeginLogin(AsyncCallback callback, string id, string password, object state = null)
        {
            var asyncResult = new NCAsyncResult(callback, state);
            var thr = new Thread(() =>
            {
                asyncResult.SetCompleted(Login(id, password));
            });
            thr.IsBackground = true;
            thr.Start();
            return asyncResult;
        }

        public void ConnectOMCS(string serverIP, int serverPort, CameraConnector CameraConnector = null, WhiteBoardConnector WhiteBoardControl = null)
        {
            if (UserInfo.UserState == UserState.未登录) { throw new Exception("未登录用户申请初始化"); }
            if (callOMCS != null)//登录成功 实例CallOMCS
                callOMCS.Dispose();
            if (UserInfo.IsPrerogative)
                callOMCS = new TeacherCallOMCS();
            else
                callOMCS = new StudentCallOMCS();
            callOMCS.Initialize(UserInfo.UserId, "", serverIP, serverPort);
            callOMCS.SetControl(CameraConnector, WhiteBoardControl);
            callOMCS.ConnectEnded += a => { ConnectEnded?.Invoke(a); };
            callOMCS.SomeoneExit += a => { SomeoneExit?.Invoke(a.GetUserId); };
            callOMCS.SomeoneJoin += a => { SomeoneJoin?.Invoke(a.GetUserId); };
        }
        public IAsyncResult BeginConnectOMCS(AsyncCallback callback, string serverIP, int serverPort, CameraConnector CameraConnector = null, WhiteBoardConnector WhiteBoardControl = null, object state = null)
        {
            var asyncResult = new NCAsyncResult(callback, state);
            var thr = new Thread(() =>
            {
                try
                {
                    ConnectOMCS(serverIP, serverPort, CameraConnector, WhiteBoardControl);
                    asyncResult.SetCompleted(baseResult.Successful);
                }
                catch (Exception e)
                {
                    asyncResult.SetCompleted(baseResult.Faild, e.Message);
                }
            });
            thr.IsBackground = true;
            thr.Start();
            return asyncResult;
        }

        public IResult CreateNCRoom(string roomId, string password = "")
        {
            lock (LOCK)
            {
                if (waitingRespond) { throw new Exception("产生过未上锁的异步请求"); }
                this.waitingRespond = true;
                isSendSucceful = true;
                clientSocket.Send(MessageTranslate.EncapsulationInfo(MessageContent.创建答疑室, MessageType.请求, roomId, password), ar => { if (ar == baseResult.Faild.ToString()) isSendSucceful = false; });
                while (waitingRespond) { if (!isSendSucceful) { waitingRespond = false; return new Result(baseResult.Faild, "发送send失败"); } }
                //等待响应
                if (respondMessage["结果"] == baseResult.Successful.ToString())
                {
                    ((TeacherCallOMCS)callOMCS).createRoom(roomId, UserInfo.UserId);
                    UserInfo.RoomID = roomId;
                    return new Result(baseResult.Successful, respondMessage["描述"]);
                }
                return new Result(baseResult.Faild, respondMessage["描述"]);
            }
        }
        public IAsyncResult BeginCreateNCRoom(AsyncCallback callback, string roomId, string password = "", object state = null)
        {
            var asyncResult = new NCAsyncResult(callback, state);
            var thr = new Thread(() =>
            {
                asyncResult.SetCompleted(CreateNCRoom(roomId, password));
            });
            thr.IsBackground = true;
            thr.Start();
            return asyncResult;
        }

        public IResult JoinNCRoom(string roomId, string password = "")
        {
            lock (LOCK)
            {
                if (waitingRespond) { throw new Exception("产生过未上锁的异步请求"); }
                this.waitingRespond = true;
                isSendSucceful = true;
                clientSocket.Send(MessageTranslate.EncapsulationInfo(MessageContent.加入答疑室, MessageType.请求, roomId, password), ar=> { if (ar == baseResult.Faild.ToString()) isSendSucceful = false; });

                while (waitingRespond) { if (!isSendSucceful) { waitingRespond = false; return new Result(baseResult.Faild, "发送send失败"); } }
                //等待响应
                if (respondMessage["结果"] == baseResult.Successful.ToString())
                {
                    builder = respondMessage["创建者"];
                    callOMCS.JoinRoom(roomId, builder);
                    UserInfo.RoomID = roomId;
                    return new Result(baseResult.Successful, respondMessage["描述"]);
                }
                return new Result(baseResult.Faild, respondMessage["描述"]);
            }
        }
        public IAsyncResult BeginJoinNCRoom(AsyncCallback callback, string roomId, string password = "", object state = null)
        {
            var asyncResult = new NCAsyncResult(callback, state);
            var thr = new Thread(() =>
            {
                asyncResult.SetCompleted(JoinNCRoom(roomId, password));
            });
            thr.IsBackground = true;
            thr.Start();
            return asyncResult;
        }

        public void SpeakInform(string roomId)
        {
            clientSocket.Send(MessageTranslate.EncapsulationInfo(MessageContent.发言请求, MessageType.通知, roomId, UserInfo.UserId));
        }

        public IResult ExitNCRoom(string roomId)
        {
            clientSocket.Send(MessageTranslate.EncapsulationInfo(MessageContent.退出答疑室, MessageType.通知, roomId));
            UserInfo.RoomID = null;
            try
            {
                callOMCS.ExitRoom();
            }
            catch(Exception e)
            {
                OnException?.Invoke(e);
                return new Result(baseResult.Faild, e.Message);
            }
            return new Result(baseResult.Successful);
        }

        public IResult MuteUser(string roomId, string guestId, bool isMute)
        {
            if (UserInfo.IsPrerogative == false)
                return new Result(baseResult.Faild, "没有权限");
            clientSocket.Send(MessageTranslate.EncapsulationInfo(MessageContent.静音某人, MessageType.通知, roomId, guestId, isMute.ToString()));
            return new Result(baseResult.Successful);
        }

        private void someoneMute(string roomId, string guestId, bool isMute)
        {
            if(guestId == UserInfo.UserId)
            {
                callOMCS.Mute(isMute);
            }
               
            onSomeoneMute?.Invoke(roomId, guestId, isMute);
        }
        #endregion

        public void Close()
        {
            if (callOMCS != null)
                callOMCS.Dispose();
            if (clientSocket != null)
                clientSocket.DisposeSocket("");
        }

        #region 回调函数
        private void Accept(string remoteEndPoint)
        {
            clientSocket.Receive(Receive);
        }

        private void Receive(string info)
        {
            //分析信息
            Dictionary<string, string> receiveMessage = new Dictionary<string, string>();
            MessageContent messageContent;
            MessageType messageType;
            receiveMessage = MessageTranslate.AnalyseInfo(info, out messageContent, out messageType);
            switch (messageType)
            {
                case MessageType.错误:
                    break;
                case MessageType.响应:
                    respondMessage = receiveMessage;
                    if (waitingRespond)
                        waitingRespond = false;//得到响应 停止等待
                    else
                        throw new Exception("未请求的响应到来");
                    break;
                case MessageType.通知://未封装的
                    if (messageContent == MessageContent.静音自己)
                    {
                        someoneMute(receiveMessage["房间名"], UserInfo.UserId, bool.Parse(receiveMessage["是否静音"]));
                    }
                    else if (messageContent == MessageContent.静音某人)
                    {                                
                        someoneMute(receiveMessage["房间名"], receiveMessage["学号"], bool.Parse(receiveMessage["是否静音"]));
                    }
                    else if (messageContent == MessageContent.发言请求)
                    {
                        SomeoneSpeakInform?.Invoke(receiveMessage["学号"]);
                    }
                    break;
            }
        }
        #endregion
    }
    public delegate void someoneMuteHandler(string roomId, string userId, bool isMute);
}
