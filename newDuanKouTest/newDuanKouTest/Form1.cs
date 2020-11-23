using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newDuanKouTest
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }
        public static bool PortInUse(int port)
        {
            bool inUse = false;
            // 提供有关本地计算机网络连接的信息
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            // 网络终结点表示为IP端口号
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            //遍历循环IP端口
            foreach (IPEndPoint endPoint in ipEndPoints)
            { 
                //如果找到了与参数相同的端口号,那么就说明端口已经在使用了
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }
            // 没有则说明没有使用
            return inUse;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            HttpListener httpListner = new HttpListener();

            // 这边自己进行添加操作进行测试

            httpListner.Prefixes.Add("http://*:8080/");
            httpListner.Prefixes.Add("http://*:80/");
            httpListner.Prefixes.Add("http://*:20/");
            // httpListner.Prefixes.Add("http://*:50/");

            httpListner.Start();

            richTextBox1.AppendText("------检测-------"+"\n");
            richTextBox1.AppendText("Port: 8080 status: "+ (PortInUse(8080) ? "正在使用" : "未使用")+"\n");
            richTextBox1.AppendText("Port: 80 status: " + (PortInUse(80) ? "正在使用" : "未使用") + "\n");
            richTextBox1.AppendText("Port: 20 status: " + (PortInUse(20) ? "正在使用" : "未使用") + "\n");
            richTextBox1.AppendText("Port: 50 status: " + (PortInUse(50) ? "正在使用" : "未使用") + "\n");

            richTextBox1.AppendText("*************" + "\n");

            httpListner.Close();
        }
    }
}
// 还有一个问题就是这个程序必须使用管理员权限进行打开,否则就会异常.