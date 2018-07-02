using Client2Server;
using NCLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;

namespace ClientTest
{
    class ClientProgram
    {
        static IClientSocket ClientC = new ClientSocket();
        static void Main(string[] args)
        {
            //test2();
            Dictionary<string, string> k = new Dictionary<string, string>();
            k["a"] = 123 + "";
            k["sfd"] = 312 + "";
            kkd e1;
            e1.a = 10;
            e1.b = 10;
            byte[] a = Class1.ObjectToBytes(e1);
            kkd b = (kkd)Class1.BytesToObject(a);

            Console.Read();
        }

        static string LongestCommonPrefix(string[] strs)
        {
            string result = "";
            if (strs.Length == 1)
                return strs[0];
            for (int i = 0; ; i++)
            {
                if (strs[0].Length <= i)
                    return result;
                char temp = strs[0][i];
                for (int j = 1; j < strs.Length; j++)
                {
                    if (strs[j].Length <= i || strs[j][i] != temp)
                        return result;                        
                }
                result += temp;
            }
        }

        static void test1()
        {
            Console.Write("PORT:");
            int port = int.Parse(Console.ReadLine());
            Console.WriteLine(ClientC.Access("127.0.0.1", 9840, port).Info);
            ClientC.Receive(ar => { Console.WriteLine(ar); });
            while (true)
            {
                string cmd = Console.ReadLine();
                if (cmd == "shutdown")
                    break;
                else if (cmd == "login")
                {
                    ClientC.Send(MessageTranslate.EncapsulationInfo(MessageContent.登录, MessageType.请求, "1501", "123"));
                }
                else if (cmd == "login1")
                {
                    ClientC.Send(MessageTranslate.EncapsulationInfo(MessageContent.登录, MessageType.请求, "0000", "123456"));
                }
                else
                    ClientC.Send(cmd);
            }
        }

        static void test2()
        {
            Console.Write("PORT:");
            int port = int.Parse(Console.ReadLine());
            Client.Client client = new Client.Client();
            client.Init("127.0.0.1", 9840, port);
            UserInfo c = client.UserInfo;
            NCAsyncResult aa = (NCAsyncResult)client.BeginLogin(ar =>
            { }
            , "1501", "123", null);
            while (!aa.IsCompleted)
            {
                Console.WriteLine("等待中。。。");
            }
            Console.WriteLine(aa.BaseResult.ToString() + aa.Info);

            client.BeginLogin(ar =>
            {
                var a = (NCAsyncResult)ar;
                Console.WriteLine(a.BaseResult.ToString() + a.Info);
            }
            , "0000", "123456", null);
            Console.Read();
        }
    }
}
