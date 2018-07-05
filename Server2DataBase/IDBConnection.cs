using NCLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server2DataBase
{
    public interface IDBConnection
    {
        /// <summary>
        /// 执行语句结果(只读)
        /// </summary>
        DataSet TmpDataSet
        {
            get;
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <param name="userId">用户ID</param>
        /// <param name="password">密码</param>
        /// <returns>处理结果</returns>
        IResult AddUser(UserInfo info, string password, int age = 20, string sex = "man");

        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="password">密码</param>
        /// <returns>结果</returns>
        IResult CheckUserInfo(UserInfo info, string password);

        /// <summary>
        /// 是否特权用户
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <returns>结果</returns>
        bool IsPrerogative(UserInfo info);

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        List<string> GetFriend(UserInfo info);

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="info"></param>
        /// <param name="friend"></param>
        /// <returns></returns>
        IResult AddFriend(UserInfo info, string friend);

        /// <summary>
        /// 断开数据库连接
        /// </summary>
        void CloseDatabase();

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="user">数据库账号</param>
        /// <param name="password">密码</param>
        /// <param name="url">数据库地址</param>
        /// <param name="database">数据库名称</param>
        /// <returns>链接结果</returns>
        IResult ConnectDatabase(string user, string password, string url, string database);

        /// <summary>
        /// 执行SQL结构化查询语句
        /// </summary>
        /// <param name="sql">结构化查询语句</param>
        /// <param name="tableTitle">结果标题</param>
        /// <returns>执行结果</returns>
        IResult ExecuteStructuredQueryLanguage(string sql, string tableTitle);


        //version.date.7.5
        /// <summary>
        /// 获取加入日期
        /// </summary>
        /// <param name="recordid"></param>
        /// <returns>获取失败时返回9999-01-01</returns>
        DateTime GetDate(string userid);

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>正常情况下返回大于等于0的数；获取失败时返回-1</returns>
        int GetAge(string userid);

        /// <summary>
        /// 设置年龄
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        IResult SetAge(string userid, int age);

        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>正常情况下返回大于等于0的数；获取失败时返回空字符串</returns>
        string GetSex(string userid);

        /// <summary>
        /// 设置性别
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        IResult SetSex(string userid, string sex);

        /// <summary>
        /// 添加答疑室
        /// </summary>
        /// <param name="roomid">答疑室ID</param>
        /// <param name="count">答疑人数</param>
        /// <returns>处理结果</returns>
        IResult AddRoom(string roomid, int count);

        /// <summary>
        /// 添加答疑室记录
        /// </summary>
        /// <param name="recordid"></param>
        /// <param name="roomid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns>处理结果</returns>
        IResult AddRecord(string recordid, string roomid, DateTime starttime, DateTime endtime);

        /// <summary>
        /// 添加加入关系
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="roomid">答疑室ID</param>
        /// <returns>处理结果</returns>
        IResult AddJoin(string userid, string roomid);

        /// <summary>
        /// 获取开始答疑时间
        /// </summary>
        /// <param name="recordid"></param>
        /// <param name="roomid"></param>
        /// <returns>获取失败时返回9999-01-01</returns>
        DateTime GetStart(string recordid, string roomid);

        /// <summary>
        /// 设置开始答疑时间
        /// </summary>
        /// <param name="start"></param>
        /// <param name="recordid"></param>
        /// <param name="roomid"></param>
        /// <returns></returns>
        IResult SetStart(DateTime start, string recordid, string roomid);

        /// <summary>
        /// 获取结束答疑时间
        /// </summary>
        /// <param name="recordid"></param>
        /// <param name="roomid"></param>
        /// <returns>获取失败时返回9999-01-01</returns>
        DateTime GetEnd(string recordid, string roomid);

        /// <summary>
        /// 设置结束答疑时间
        /// </summary>
        /// <param name="end"></param>
        /// <param name="recordid"></param>
        /// <param name="roomid"></param>
        /// <returns></returns>
        IResult SetEnd(DateTime end, string recordid, string roomid);

        /// <summary>
        /// 获取答疑室记录的用户列表
        /// </summary>
        /// <param name="recordid"></param>
        /// <returns></returns>
        List<string> GetUser(string recordid);

        /// <summary>
        /// 获取答疑室人数
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns>正常情况下返回大于等于0的数；获取失败时返回-1</returns>
        int GetCount(string roomid);

        /// <summary>
        /// 设置答疑室人数
        /// </summary>
        /// <param name="roomid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IResult SetCount(string roomid, int count);
    }
}
