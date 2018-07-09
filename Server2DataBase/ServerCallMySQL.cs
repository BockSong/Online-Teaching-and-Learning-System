using MySql.Data.MySqlClient;
using NCLib;
using System;
using System.Collections.Generic;
using System.Data;

namespace Server2DataBase
{
    public class ServerCallMySQL : IDBConnection
    {

        #region 静态字段

        //user表的sql语句
        private static string checkExist = "select count(*) from user where userid='{0}'";
        private static string checkExist2 = "select count(*) from user where userid='{0}' and password='{1}'";
        private static string addUser = "insert into user values('{0}','{1}','{2}',0,'{3}','{4}','{5}','{6}','{7}')";
        private static string isPrerogative = "select count(*) from user where userid='{0}' and permission=1";
        private static string getfriend = "select friends from user where userid='{0}'";
        private static string setfriend = "update user set friends = '{0}' WHERE userid = '{1}'";
        private static string getdate = "select joindate from user where userid='{0}'";
        private static string getsex = "select sex from user where userid='{0}'";
        private static string setsex = "update user set sex = '{0}' WHERE userid = '{1}'";
        private static string getage = "select age from user where userid='{0}'";
        private static string setage = "update user set age = '{0}' WHERE userid = '{1}'";

        //room表的sql语句
        private static string checkRoomExist = "select count(*) from room where roomid='{0}'";
        private static string addRoom = "insert into room values('{0}','{1}')";
        private static string getcount = "select count from room where roomid='{0}'";
        private static string setcount = "update room set count = '{0}' WHERE roomid = '{1}'";

        //recordid
        private static string getmaxrecordid = "select max(recordid) from record";
        private static string checkRecordExist = "select count(*) from record where recordid='{0}' and roomid='{1}'";
        private static string addRecord = "insert into record (roomid, starttime, endtime) values('{0}','{1}','{2}')";
        private static string getstart = "select starttime from record where recordid='{0}' and roomid='{1}'";
        private static string setstart = "update record set starttime = '{0}' WHERE recordid = '{1}' and roomid='{2}'";
        private static string getend = "select endtime from record where recordid='{0}' and roomid='{1}'";
        private static string setend = "update record set endtime = '{0}' WHERE recordid = '{1}' and roomid='{2}'";


        //joinroom表的操作
        private static string checkJoinExist = "select count(*) from joinroom where userid='{0}' and recordid='{1}'";
        private static string addJoin = "insert into joinroom values('{0}','{1}','{2}','{3}','{4}','{5}')";
        private static string getuser = "select userid from joinroom where roomid='{0}' and recordid='{1}'";
        private static string getscount = "select scount from joinroom where userid='{0}' and recordid='{1}'";
        private static string getuserjointime = "select userjointime from joinroom where userid='{0}' and recordid='{1}'";
        private static string getuserouttime = "select userouttime from joinroom where userid='{0}' and recordid='{1}'";

        #endregion

        private static MySqlConnection sqlConnection;
        private DataSet _tmpDataSet = new DataSet();  //[记录序号][属性序号]
        /// <summary>
        /// 执行语句结果(只读)
        /// </summary>
        public DataSet TmpDataSet
        {
            get
            {
                return _tmpDataSet;
            }
        }

        public ServerCallMySQL(string user, string password, string url, string database)
        {
            string connString = String.Format("Database={0};Data Source={1};User Id={2};Password={3};SslMode=none", database, url, user, password);
            sqlConnection = new MySqlConnection(connString);
        }
        /// <summary>
        /// 断开数据库连接
        /// </summary>
        public void CloseDatabase()
        {
            sqlConnection.Close();
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="user">数据库账号</param>
        /// <param name="password">密码</param>
        /// <param name="url">数据库地址</param>
        /// <param name="database">数据库名称</param>
        /// <returns>链接结果</returns>
        public IResult ConnectDatabase()
        {
            try
            {
                sqlConnection.Open();
            }
            catch (Exception e)
            {
                return new Result(baseResult.Faild, e.Message);
            }
            return new Result(baseResult.Successful);
        }
        /// <summary>
        /// 执行SQL结构化查询语句
        /// </summary>
        /// <param name="sql">结构化查询语句</param>
        /// <param name="tableTitle">结果标题</param>
        /// <returns>执行结果</returns>
        public IResult ExecuteStructuredQueryLanguage(string sql, string tableTitle)
        {
            IResult connect = ConnectDatabase();
            if (connect.BaseResult == baseResult.Faild)
                return connect;
            //定义一个数据库操作指令
            MySqlCommand MyCommand = new MySqlCommand(sql, sqlConnection);
            //定义一个数据适配器
            MySqlDataAdapter SelectAdapter = new MySqlDataAdapter();
            //定义数据适配器的操作指令
            SelectAdapter.SelectCommand = MyCommand;
            try
            {
                SelectAdapter.SelectCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return new Result(baseResult.Faild, e.Message);
            }
            //填充数据集
            if (this._tmpDataSet.Tables.Contains(tableTitle))
                this._tmpDataSet.Tables[tableTitle].Clear();
            SelectAdapter.Fill(this._tmpDataSet, tableTitle);
            CloseDatabase();
            return new Result(baseResult.Successful);
        }

        
        //user表操作
        /// <summary>
        /// 验证用户信息(或存在)
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <param name="password">密码</param>
        /// <returns>结果</returns>
        private bool IsExist(UserInfo info, string password = null)
        {
            if (password == null)
            {
                if (ExecuteStructuredQueryLanguage(String.Format(checkExist, info.UserId), "IsExist").BaseResult == baseResult.Faild)
                    return false;
                if (((this._tmpDataSet.Tables["IsExist"].Rows)[0])[0].ToString() == "0")
                    return false;
            }
            else
            {
                if (ExecuteStructuredQueryLanguage(String.Format(checkExist2, info.UserId, password), "IsExist").BaseResult == baseResult.Faild)
                    return false;
                if (((this._tmpDataSet.Tables["IsExist"].Rows)[0])[0].ToString() == "0")
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 验证用户信息(或存在)
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <param name="password">密码</param>
        /// <returns>结果</returns>
        private bool IsExist(string infoid, string password = null)
        {
            if (password == null)
            {
                if (ExecuteStructuredQueryLanguage(String.Format(checkExist, infoid), "IsExist").BaseResult == baseResult.Faild)
                    return false;
                if (((this._tmpDataSet.Tables["IsExist"].Rows)[0])[0].ToString() == "0")
                    return false;
            }
            else
            {
                if (ExecuteStructuredQueryLanguage(String.Format(checkExist2, infoid, password), "IsExist").BaseResult == baseResult.Faild)
                    return false;
                if (((this._tmpDataSet.Tables["IsExist"].Rows)[0])[0].ToString() == "0")
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="password">密码</param>
        /// <returns>结果</returns>
        public IResult CheckUserInfo(UserInfo info, string password)
        {
            if (IsExist(info, password))
                return new Result(baseResult.Successful, "验证成功");
            return new Result(baseResult.Faild, "错误的账号或密码");
        }
        /// <summary>
        /// 是否特权用户
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <returns>结果</returns>
        public bool IsPrerogative(UserInfo info)
        {
            if (ExecuteStructuredQueryLanguage(String.Format(isPrerogative, info.UserId), "IsPrerogative").BaseResult == baseResult.Faild)
                return false;
            if (((this._tmpDataSet.Tables["IsPrerogative"].Rows)[0])[0].ToString() == "0")
                return false;
            return true;
        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <param name="userId">用户ID</param>
        /// <param name="password">密码</param>
        /// <returns>处理结果</returns>
        public IResult AddUser(UserInfo info, string password, int permission = 0, int age = 20, string sex = "man")
        {
            if (IsExist(info))
                return new Result(baseResult.Faild, "已存在用户");
            else
            {
                IResult tmpResult = ExecuteStructuredQueryLanguage(String.Format(addUser, info.UserId, info.NickName, password, permission,
                                                                   "", DateTime.Now.Date, age, sex), "AddUser");
                if (tmpResult.BaseResult == baseResult.Faild)
                    return tmpResult;
            }
            return new Result(baseResult.Successful, "成功");
        }
        /// <summary>
        /// 获取加入日期
        /// </summary>
        /// <param name="recordid"></param>
        /// <returns>获取失败时返回9999-01-01</returns>
        public DateTime GetDate(string userid)
        {
            if (IsExist(userid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(getdate, userid), "GetDate").BaseResult == baseResult.Faild)
                    return new DateTime(9999, 1, 1);
                else
                {
                    return Convert.ToDateTime(((this._tmpDataSet.Tables["GetDate"].Rows)[0])[0]);
                }
            }
            else
                return new DateTime(9999, 1, 1);
        }
        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>正常情况下返回大于等于0的数；获取失败时返回-1</returns>
        public int GetAge(string userid)
        {
            if (IsExist(userid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(getage, userid), "GetAge").BaseResult == baseResult.Faild)
                    return -1;
                else
                    return Convert.ToInt32(((this._tmpDataSet.Tables["GetAge"].Rows)[0])[0]);
            }
            else
                return -1;
        }
        /// <summary>
        /// 设置年龄
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        public IResult SetAge(string userid, int age)
        {
            if (IsExist(userid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(setage, userid, age), "SetCount").BaseResult == baseResult.Faild)
                    return new Result(baseResult.Faild, "操作失败");
                return new Result(baseResult.Successful, "成功");
            }
            else
                return new Result(baseResult.Faild, "用户不存在");
        }
        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>正常情况下返回大于等于0的数；获取失败时返回空字符串</returns>
        public string GetSex(string userid)
        {
            if (IsExist(userid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(getsex, userid), "GetSex").BaseResult == baseResult.Faild)
                    return "";
                else
                    return ((this._tmpDataSet.Tables["GetSex"].Rows)[0])[0].ToString();
            }
            else
                return "";
        }
        /// <summary>
        /// 设置性别
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        public IResult SetSex(string userid, string sex)
        {
            if (IsExist(userid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(setsex, userid, sex), "SetSex").BaseResult == baseResult.Faild)
                    return new Result(baseResult.Faild, "操作失败");
                return new Result(baseResult.Successful, "成功");
            }
            else
                return new Result(baseResult.Faild, "用户不存在");
        }
        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<string> GetFriend(UserInfo info)
        {
            if (IsExist(info))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(getfriend, info.UserId), "GetFriend").BaseResult == baseResult.Faild)
                    return new List<string>();
                else
                {
                    string[] result = ((this._tmpDataSet.Tables["GetFriend"].Rows)[0])[0].ToString().Split(',');
                    if (result[0] == "")
                        return new List<string>();
                    return new List<string>(result);
                }
            }
            else
                return new List<string>();
        }
        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="info"></param>
        /// <param name="friend"></param>
        /// <returns></returns>
        public IResult AddFriend(UserInfo info, string friend)
        {
            if (IsExist(info))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(getfriend, info.UserId), "GetFriend").BaseResult == baseResult.Faild)
                    return new Result(baseResult.Faild, "无效操作");
                else if (IsExist(friend))
                {
                    string result = ((this._tmpDataSet.Tables["GetFriend"].Rows)[0])[0].ToString();
                    List<string> results = new List<string>(result.Split(','));
                    if (results.Contains(friend))
                        return new Result(baseResult.Faild, "已有此好友");
                    if (result == "")
                        result += friend;
                    else
                        result += "," + friend;
                    return ExecuteStructuredQueryLanguage(String.Format(setfriend, result, info.UserId), "SetFriend");
                }
                else
                {
                    return new Result(baseResult.Faild, "无此用户");
                }
            }
            else
                return new Result(baseResult.Faild, "无效操作");
        }
        /// <summary>
        /// 删除用户（特殊处理）
        /// </summary>
        /// <param name="userId"></param>
        public void DeletUser(string userId)
        {
            throw new NotImplementedException();
        }
        
        //room表操作
        /// <summary>
        /// 验证答疑室(或存在)
        /// </summary>
        /// <param name="roomid">答疑室ID</param>
        /// <returns>结果</returns>
        private bool IsRoomExist(string roomid)
        {
            if (ExecuteStructuredQueryLanguage(String.Format(checkRoomExist, roomid), "IsRoomExist").BaseResult == baseResult.Faild)
                return false;
            if (((this._tmpDataSet.Tables["IsRoomExist"].Rows)[0])[0].ToString() == "0")
                return false;
            return true;
        }
        /// <summary>
        /// 添加答疑室
        /// </summary>
        /// <param name="roomid">答疑室ID</param>
        /// <param name="count">答疑人数</param>
        /// <returns>处理结果</returns>
        public IResult AddRoom(string roomid, int count)
        {
            if (IsRoomExist(roomid))
                return new Result(baseResult.Faild, "已存在答疑室");
            else
            {
                IResult tmpResult = ExecuteStructuredQueryLanguage(String.Format(addRoom, roomid, count), "AddRoom");
                if (tmpResult.BaseResult == baseResult.Faild)
                    return tmpResult;

            }
            return new Result(baseResult.Successful, "成功");
        }
        /// <summary>
        /// 获取答疑室人数
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns>正常情况下返回大于等于0的数；获取失败时返回-1</returns>
        public int GetCount(string roomid)
        {
            if (IsRoomExist(roomid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(getcount, roomid), "GetCount").BaseResult == baseResult.Faild)
                    return -1;
                else
                    return Convert.ToInt32(((this._tmpDataSet.Tables["GetCount"].Rows)[0])[0]);
            }
            else
                return -1;
        }
        /// <summary>
        /// 设置答疑室人数
        /// </summary>
        /// <param name="roomid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IResult SetCount(string roomid, int count)
        {
            if (IsRoomExist(roomid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(setcount, roomid, count), "SetCount").BaseResult == baseResult.Faild)
                    return new Result(baseResult.Faild, "操作失败");
                return new Result(baseResult.Successful, "成功");
            }
            else
                return new Result(baseResult.Faild, "答疑室不存在");
        }

        
        //record表操作
        /// <summary>
        /// 验证答疑室记录(或存在)
        /// </summary>
        /// <param name="recordid">答疑室记录ID</param>
        /// <param name="roomid"></param>
        /// <returns>结果</returns>
        private bool IsRecordExist(int recordid, string roomid)
        {
            if (ExecuteStructuredQueryLanguage(String.Format(checkRecordExist, recordid, roomid), "IsRecordExist").BaseResult == baseResult.Faild)
                return false;
            if (((this._tmpDataSet.Tables["IsRecordExist"].Rows)[0])[0].ToString() == "0")
                return false;
            return true;
        }
        /// <summary>
        /// 添加答疑室记录
        /// </summary>
        /// <param name="recordid"></param>
        /// <param name="roomid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns>处理结果</returns>
        public int AddRecord(string roomid, DateTime starttime, DateTime endtime)
        {
            if (IsRoomExist(roomid))
            {
                if (ExecuteStructuredQueryLanguage(getmaxrecordid, "GetMaxRecordid").BaseResult == baseResult.Faild)
                    return -1;
                else
                {
                    int maxrecordid = Convert.ToInt32(((this._tmpDataSet.Tables["GetMaxRecordid"].Rows)[0])[0]) + 1;

                    if (IsRecordExist(maxrecordid, roomid))
                        return -1;
                    else
                    {
                        IResult tmpResult = ExecuteStructuredQueryLanguage(String.Format(addRecord, roomid, starttime, endtime), "AddRecord");
                        if (tmpResult.BaseResult == baseResult.Faild)
                            return -1;
                        return maxrecordid;
                    }
                }

            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 获取开始答疑时间
        /// </summary>
        /// <param name="recordid"></param>
        /// <param name="roomid"></param>
        /// <returns>获取失败时返回9999-01-01</returns>
        public DateTime GetStart(int recordid, string roomid)
        {
            if (IsRecordExist(recordid, roomid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(getstart, recordid, roomid), "GetStart").BaseResult == baseResult.Faild)
                    return new DateTime(9999, 1, 1);
                else
                {
                    return Convert.ToDateTime(((this._tmpDataSet.Tables["GetStart"].Rows)[0])[0]);
                }
            }
            else
                return new DateTime(9999, 1, 1);
        }
        /// <summary>
        /// 设置开始答疑时间
        /// </summary>
        /// <param name="start"></param>
        /// <param name="recordid"></param>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public IResult SetStart(DateTime start, int recordid, string roomid)
        {
            if (IsRecordExist(recordid, roomid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(setstart, start, recordid, roomid), "SetStart").BaseResult == baseResult.Faild)
                    return new Result(baseResult.Faild, "操作失败");
                return new Result(baseResult.Successful, "成功");
            }
            else
                return new Result(baseResult.Faild, "答疑室记录不存在");
        }
        /// <summary>
        /// 获取结束答疑时间
        /// </summary>
        /// <param name="recordid"></param>
        /// <param name="roomid"></param>
        /// <returns>获取失败时返回9999-01-01</returns>
        public DateTime GetEnd(int recordid, string roomid)
        {
            if (IsRecordExist(recordid, roomid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(getend, recordid, roomid), "GetEnd").BaseResult == baseResult.Faild)
                    return new DateTime(9999, 1, 1);
                else
                {
                    return Convert.ToDateTime(((this._tmpDataSet.Tables["GetEnd"].Rows)[0])[0]);
                }
            }
            else
                return new DateTime(9999, 1, 1);
        }
        /// <summary>
        /// 设置结束答疑时间
        /// </summary>
        /// <param name="end"></param>
        /// <param name="recordid"></param>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public IResult SetEnd(DateTime end, int recordid, string roomid)
        {
            if (IsRecordExist(recordid, roomid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(setend, end, recordid, roomid), "SetEnd").BaseResult == baseResult.Faild)
                    return new Result(baseResult.Faild, "操作失败");
                return new Result(baseResult.Successful, "成功");
            }
            else
                return new Result(baseResult.Faild, "答疑室记录不存在");
        }
        
        //joinroom表的操作
        /// <summary>
        /// 验证加入关系(或存在)
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="roomid">答疑室记录ID</param>
        /// <returns>结果</returns>
        private bool IsJoinExist(string userid, int recordid)
        {
            if (ExecuteStructuredQueryLanguage(String.Format(checkJoinExist, userid, recordid), "IsJoinExist").BaseResult == baseResult.Faild)
                return false;
            if (((this._tmpDataSet.Tables["IsJoinExist"].Rows)[0])[0].ToString() == "0")
                return false;
            return true;
        }
        /// <summary>
        /// 添加加入关系
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="roomid">答疑室记录ID</param>
        /// <returns>处理结果</returns>
        public IResult AddJoin(string userid, int recordid, DateTime userjointime, DateTime userouttime, int scount)
        {
            if (IsJoinExist(userid, recordid))
                return new Result(baseResult.Faild, "已存在加入关系");
            else
            {
                IResult tmpResult = ExecuteStructuredQueryLanguage(String.Format(addJoin, userid, userjointime, userouttime, recordid, scount), "AddJoin");
                if (tmpResult.BaseResult == baseResult.Faild)
                    return tmpResult;
            }
            return new Result(baseResult.Successful, "成功");
        }
        /// <summary>
        /// 获取答疑室记录的用户列表
        /// </summary>
        /// <param name="recordid"></param>
        /// <returns></returns>
        public List<string> GetUser(string roomid, int recordid)
        {
            if (IsRoomExist(roomid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(getuser, recordid), "GetUser").BaseResult == baseResult.Faild)
                    return new List<string>();
                else
                {
                    List<string> result = new List<string>();
                    for (int i = 0; i < this._tmpDataSet.Tables["GetUser"].Select().Length; i++)
                    {
                        result.Add(((this._tmpDataSet.Tables["GetUser"].Rows)[i])[0].ToString());
                    }
                    if (result[0] == "")
                        return new List<string>();
                    return result;
                }
            }
            else
                return new List<string>();
        }
        /// <summary>
        /// 获取答疑室记录的对应用户的发言次数
        /// </summary>
        /// <returns></returns>
        public int GetScount(string userid, int recordid)
        {
            if (IsJoinExist(userid, recordid))
                return -1;
            else
            {
                if (ExecuteStructuredQueryLanguage(String.Format(getscount, userid, recordid), "GetScount").BaseResult == baseResult.Faild)
                    return -1;
                else
                {
                    return Convert.ToInt32(((this._tmpDataSet.Tables["GetScount"].Rows)[0])[0]);
                }

            }
        }
        /// <summary>
        /// 获取答疑室记录的对应用户进入时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetUserJoinTime(string userid, int recordid)
        {
            if (IsJoinExist(userid, recordid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(getuserjointime, userid, recordid), "GetUserJoinTime").BaseResult == baseResult.Faild)
                    return new DateTime(9999, 1, 1);
                else
                {
                    return Convert.ToDateTime(((this._tmpDataSet.Tables["GetUserJoinTime"].Rows)[0])[0]);
                }
            }
            else
                return new DateTime(9999, 1, 1);
        }
        /// <summary>
        /// 获取答疑室记录的对应用户退出时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetUserOutTime(string userid, int recordid)
        {
            if (IsJoinExist(userid, recordid))
            {
                if (ExecuteStructuredQueryLanguage(String.Format(getuserouttime, userid, recordid), "GetUserOutTime").BaseResult == baseResult.Faild)
                    return new DateTime(9999, 1, 1);
                else
                {
                    return Convert.ToDateTime(((this._tmpDataSet.Tables["GetUserOutTime"].Rows)[0])[0]);
                }
            }
            else
                return new DateTime(9999, 1, 1);
        }

        
    }
}
