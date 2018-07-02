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
        IResult AddUser(UserInfo info, string password);

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
    }
}
