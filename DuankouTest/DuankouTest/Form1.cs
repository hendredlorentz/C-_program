using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
namespace DuankouTest
{
    public partial class Form1 : Form
    {
        internal static Boolean IsPortOccuped(Int32 port)
        {
            System.Net.NetworkInformation.IPGlobalProperties iproperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
            System.Net.IPEndPoint[] ipEndPoints = iproperties.GetActiveTcpListeners();
            foreach (var item in ipEndPoints)
            {
                if (item.Port == port)
                {
                    return true;
                }
            }
            return false;
        }
        static List<IpAndPort> IpAndPortList = new List<IpAndPort>();
        static List<string> PortList = new List<string>();

        internal static Boolean IsPortOccupedFun2(Int32 port, IpAndPort m_PortList)
        {
            System.Net.NetworkInformation.IPGlobalProperties iproperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
            System.Net.IPEndPoint[] ipEndPoints = iproperties.GetActiveTcpListeners();

            if (!PortList.Contains(m_PortList.port))
            {
                PortList.Add(m_PortList.port);
                IpAndPortList.Add(m_PortList);
            }
            foreach (var item in ipEndPoints)
            {

                if (item.Port == Convert.ToInt32(m_PortList.port))
                {
                    foreach (IpAndPort ipPort in IpAndPortList)
                    {
                        if (ipPort.ip.Equals(m_PortList.ip) && ipPort.port.Equals(m_PortList.port))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public class IpAndPort
        {
            public string ip;
            public string port;
        }
        public Form1()
        {
            InitializeComponent();
        }
        int[] ports = new int[] { 21, 22, 23, 25, 53, 80, 110, 118, 135, 143, 156, 161, 443, 445, 465, 587, 666, 990, 991, 993, 995, 1080, 1433, 1434, 1984, 2049, 2483, 2484, 3128, 3306, 3389, 4662, 4672, 5222, 5223, 5269, 5432, 5500, 5800, 5900, 8000, 8008, 8080 };
        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            richTextBox1.AppendText("-------开始扫描-------"+"\n");
            foreach(int p in ports)
            {
                Socket scanIt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                do
                {
                    try
                    {
                        scanIt.Bind(new IPEndPoint(IPAddress.Any, rand.Next(65535)));
                        break;
                    }
                    catch
                    {
                        //失败了
                    }
                } while (true);

                try
                {
                    scanIt.Connect(IPAddress.Parse("www.baidu.com"), p);
                    if (scanIt.Connected)
                        richTextBox1.AppendText(((IPEndPoint)scanIt.LocalEndPoint).Address+":"+ ((IPEndPoint)scanIt.LocalEndPoint).Port+" <->"+ ((IPEndPoint)scanIt.RemoteEndPoint).Address +":" + ((IPEndPoint)scanIt.RemoteEndPoint).Port + "\t端口 "+p+ "\t打开."+"\n");
                    //else    // 不会执行
                    //    Console.WriteLine("{0}:{1} <-> {2}:{3} \t端口 {4,5}\t关闭.", ((IPEndPoint)scanSocket.LocalEndPoint).Address, ((IPEndPoint)scanSocket.LocalEndPoint).Port, ((IPEndPoint)scanSocket.RemoteEndPoint).Address, ((IPEndPoint)scanSocket.RemoteEndPoint).Port, port);

                    //关闭套接字
                    scanIt.Close();
                }
                catch
                {
                    richTextBox1.AppendText("端口：" + p + "\t关闭"+"\n");
                    //关闭套接字
                    scanIt.Close();
                    continue;
                }
            }
        }
    }
}
