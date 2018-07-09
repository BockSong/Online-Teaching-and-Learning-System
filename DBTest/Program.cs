using NCLib;
using Server2DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IDBConnection scd = new ServerCallMySQL("root", "123456", "154.8.211.49", "NCDB");
            IResult r = scd.ConnectDatabase();
            #region Shop
            //scd.ExecuteStructuredQueryLanguage(@"select 名称 from 商店 where 商店.地址 LIKE '上海%'","aa");
            //foreach (DataRow theRow in scd.TmpDataSet.Tables["aa"].Rows)
            //{
            //    Console.WriteLine(theRow["名称"]);
            //}
            //scd.ExecuteStructuredQueryLanguage(@"select 名称 from 商店 where 商店.地址 LIKE '北京%'", "aa");
            //foreach (DataRow theRow in scd.TmpDataSet.Tables["aa"].Rows)
            //{
            //    Console.WriteLine(theRow["名称"]);
            //}
            //Console.Read();
            #endregion
            #region NC
            scd.ExecuteStructuredQueryLanguage(@"select recordid from haverecord where roomid=1", "aa");
            List<string> result = new List<string>();
            for (int i = 0; i < scd.TmpDataSet.Tables["aa"].Select().Length; i++)
            {
                result.Add(((scd.TmpDataSet.Tables["aa"].Rows)[i])[0].ToString());
            }
            Console.WriteLine(result[0]);
            //Console.WriteLine(result.Length);
            //Console.WriteLine(scd.TmpDataSet.Tables["aa"].Rows[1][0]); 
            //foreach (DataRow theRow in scd.TmpDataSet.Tables["aa"].Rows)
            //{
            //    Console.WriteLine(theRow["recordid"]);
            //}
            //Console.Read();
            #endregion
            #region check
            //UserInfo userInfo = new ClientUser();
            //userInfo.UserId = "1501";
            //bool k = scd.CheckUserInfo(userInfo);
            //userInfo.UserId = "0000";
            //bool k1 = scd.CheckUserInfo(userInfo);
            //userInfo.UserId = "15021";
            //bool k2 = scd.CheckUserInfo(userInfo);
            //userInfo.UserId = "1501";
            //bool k3 = scd.IsPrerogative(userInfo);
            //userInfo.UserId = "0000";
            //bool k4 = scd.IsPrerogative(userInfo);
            #endregion

        }
    }
}
