using ClientForm.Properties;
using NCLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForm
{
    public partial class Form1 : Form
    {
        Client.Client client;
        

        #region 事件监听
        private void SomeoneJoin(string userId)
        {
            NCskinListBox.Invoke(new Action<string>(ar =>
            {
                NCskinListBox.Items.Add(new CCWin.SkinControl.SkinListBoxItem(ar));
            }), userId);
        }
        private void SomeoneExit(string userId)
        {
            NCskinListBox.Invoke(new Action<string>(ar =>
            {
                for (int index = 0; index < NCskinListBox.Items.Count; index++)
                {
                    if (NCskinListBox.Items[index].Text == ar)
                    {
                        NCskinListBox.Items.Remove(NCskinListBox.Items[index]);
                        break;
                    }
                }

            }), userId);
        }
        private void SomeoneSpeakInform(string userId)
        {
            Invoke(new Action(()=> 
            {
                if (MessageBox.Show("是否允许" + userId + "发言?", "发言请求", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    client.MuteUser(client.UserInfo.RoomID, userId, false);
                }
                else
                {
                    client.MuteUser(client.UserInfo.RoomID, userId, true);
                }
            }));
        }
        private void OnSomeoneMute(string roomId, string userId, bool isMute)
        {
            if (userId == client.UserInfo.UserId)
            {
                if(isMute == true)
                    MessageBox.Show("你被禁止发言");
                else
                    MessageBox.Show("你可以发言了");
            }           
        }
        private void OnException(Exception e)
        {
            label2.Invoke(new Action(() => { label2.Text = e.Message; }));
        }
        #endregion

        public Form1(Client.Client client)
        {
            InitializeComponent();
            this.client = client;
            client.SomeoneJoin += SomeoneJoin;
            client.SomeoneExit += SomeoneExit;
            client.SomeoneSpeakInform += SomeoneSpeakInform;
            client.onSomeoneMute += OnSomeoneMute;
            client.OnException += OnException;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            int port = rd.Next(1000, 9999);
            label1.Text = "port:" + port;
            client.Init(ConfigurationManager.AppSettings["SERVERIP"], int.Parse(ConfigurationManager.AppSettings["SERVERPORT"]), port);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.BeginLogin(ar =>
            {
                var a = (NCAsyncResult)ar;
                if (a.BaseResult == baseResult.Successful)
                    client.BeginConnectOMCS(iar => { showResult((IResult)iar); Invoke(new Action(() => { label3.Text = "id:0000"; })); }, ConfigurationManager.AppSettings["OMCSIP"], int.Parse(ConfigurationManager.AppSettings["OMCSPORT"]), this.cameraConnector1, this.whiteBoardConnector1);
                                                          
            }
            , "0000", "123456", null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client.BeginLogin(ar =>
            {
                var a = (NCAsyncResult)ar;
                if (a.BaseResult == baseResult.Successful)
                    client.BeginConnectOMCS(iar => { showResult((IResult)iar); Invoke(new Action(() => { label3.Text = "id:1501"; })); }, ConfigurationManager.AppSettings["OMCSIP"], int.Parse(ConfigurationManager.AppSettings["OMCSPORT"]), this.cameraConnector1, this.whiteBoardConnector1);
            }
            , "1501", "123", null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            client.BeginLogin(ar =>
            {
                var a = (NCAsyncResult)ar;
                if (a.BaseResult == baseResult.Successful)
                    client.BeginConnectOMCS(iar => { showResult((IResult)iar); Invoke(new Action(() => { label3.Text = "id:1502"; })); }, ConfigurationManager.AppSettings["OMCSIP"], int.Parse(ConfigurationManager.AppSettings["OMCSPORT"]), this.cameraConnector1, this.whiteBoardConnector1);
            }
            , "1502", "123", null);
        }

        void showResult(IResult result)
        {
            this.BeginInvoke(new Action<IResult>(a=> { label2.Text = a.BaseResult.ToString() + a.Info; }),result);          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            client.BeginCreateNCRoom(ar => 
            {
                var a = (NCAsyncResult)ar; showResult(a);
                if (a.BaseResult == baseResult.Successful)                
                    Invoke(new Action(() => { button8.Text = "静音自己"; label4.Text = "roomid:" + textBox1.Text; }));            }, textBox1.Text, textBox2.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            client.BeginJoinNCRoom(ar => 
            {
                var a = (NCAsyncResult)ar; showResult(a);
                if (a.BaseResult == baseResult.Successful)
                    Invoke(new Action(() => { label4.Text = "roomid:" + textBox1.Text; }));
            }, textBox1.Text, textBox2.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            client.ExitNCRoom(textBox1.Text);
            label4.Text = "roomid:";
            NCskinListBox.Items.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            client.SpeakInform(textBox1.Text);
        }
    }
}
    