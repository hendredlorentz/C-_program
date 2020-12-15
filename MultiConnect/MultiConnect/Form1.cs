using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiConnect
{
    public partial class Form1 : Form
    {
        delegate void AppendStringCallback(string text);
        AppendStringCallback appendStringCallback;
        //接收的端口号
        private int port = 8000;
        private UdpClient udpClient;
        public Form1()
        {
            InitializeComponent();
            appendStringCallback = new AppendStringCallback(AppendString);
        }
        private void AppendString(string text)
        {
            if(richTextBox1.InvokeRequired == true)
            {
                this.Invoke(appendStringCallback, text);
            }
            else
            {
                richTextBox1.AppendText(text + "\r\n");
            }
        }

        private void ReceiveData()
        {
            udpClient = new UdpClient(port);
            IPEndPoint remote = null;
            while (true)
            {
                try
                {
                    byte[] bytes = udpClient.Receive(ref remote);//ref为传地址参数
                    string str = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    AppendString(string.Format("来自{0}:{1}", remote, str));

                }
                catch
                {
                    //退出循环
                    break;
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //开始发送了
        private void buttonSend_Click(object sender, EventArgs e)
        {
            UdpClient myUdpClient = new UdpClient();
            try
            {
                //自动提供IP广播地址
                IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 8000);
                //允许发送与接收广播数据报
                myUdpClient.EnableBroadcast = true;
                //转化为字节数组
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(textBox1.Text);

                //向广播子网发送消息
                myUdpClient.Send(bytes, bytes.Length, iep);
                textBox1.Clear();
                textBox1.Focus();
                
            }
            catch(Exception err)
            {
                //报错当然发送不了
                MessageBox.Show(err.Message, "发送失败");
            }
            finally
            {
                //要关闭通信的
                myUdpClient.Close();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            Thread myThead = new Thread(new ThreadStart(ReceiveData));

            //后台调用
            myThead.IsBackground = true;
            myThead.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            udpClient.Close();
        }
    }
}
