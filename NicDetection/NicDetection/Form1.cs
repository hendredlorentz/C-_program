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
using System.Net.NetworkInformation;
namespace NicDetection
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //网卡检测点击按钮事件
        private void button0_Click(object sender, EventArgs e)
        {
            //定义NetworkInterface类型的数组，以便获取内容
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            richTextBox1.AppendText("适配器个数：" + adapters.Length+"\n");
            int index = 0;

            foreach (NetworkInterface adapter in adapters)
            {
                index++;
                //显示网络适配器描述信息、名称、类型、速度、MAC 地址
                richTextBox1.AppendText("---------------------第" + index + "个适配器信息---------------------" + "\n");
                richTextBox1.AppendText("描述信息：" + adapter.Name + "\n");
                richTextBox1.AppendText("类型：" + adapter.NetworkInterfaceType + "\n");
                richTextBox1.AppendText("速度：" + adapter.Speed / 1000 / 1000 + "MB" + "\n");
                richTextBox1.AppendText("MAC 地址：" + adapter.GetPhysicalAddress() + "\n");
                richTextBox1.AppendText("是否支持IPv4：" + adapter.Supports(NetworkInterfaceComponent.IPv4) + "\n");
                richTextBox1.AppendText("是否支持IPv6：" + adapter.Supports(NetworkInterfaceComponent.IPv6) + "\n");

                //获取IPInterfaceProperties实例
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                //获取网卡地址需要的定义
                GatewayIPAddressInformationCollection ipCollection = adapterProperties.GatewayAddresses;
                //获取广播地址需要的定义
                MulticastIPAddressInformationCollection Mcollection = adapterProperties.MulticastAddresses;
                //获取并显示DNS服务器IP地址信息
                IPAddressCollection dnsServers = adapterProperties.DnsAddresses;
                //获取DHCP地址需要的定义
                IPAddressCollection ipcol = adapterProperties.DhcpServerAddresses;

                //Count大于0代表存在数据，直接进行打印即可
                if (Mcollection.Count > 0)
                {
                    foreach (MulticastIPAddressInformation ok2 in Mcollection)
                    {
                        richTextBox1.AppendText("广播地址：" + ok2.Address + "\n");
                    }
                }
                else
                {
                    richTextBox1.AppendText("广播地址：无" + "\n");
                }

                //Count大于0代表存在数据，直接进行打印即可
                if (ipcol.Count > 0)
                {
                    foreach (IPAddress ok1 in ipcol)
                    {
                        richTextBox1.AppendText("DHCP地址：" + ok1 + "\n");
                    }
                }
                else
                {
                    richTextBox1.AppendText("DHCP地址：无" + "\n");
                }

                //Count大于0代表存在数据，直接进行打印即可
                if (ipCollection.Count > 0)
                {
                    foreach(GatewayIPAddressInformation ok3 in ipCollection)
                    {
                        richTextBox1.AppendText("网关地址:" + ok3.Address + "\n");

                    }

                }
                else
                {
                    richTextBox1.AppendText("网关地址:" +"无" + "\n");

                }

                //Count大于0代表存在数据，直接进行打印即可

                if (dnsServers.Count > 0)
                {
                    foreach (IPAddress dns in dnsServers)
                    {
                        richTextBox1.AppendText("DNS 服务器IP地址：" + dns + "\n");
                    }
                }
                else
                {
                    richTextBox1.AppendText("DNS 服务器IP地址：无" + "\n");
                }
            }
        }

        //数据包检测按钮点击事件
        private void button1_Click(object sender, EventArgs e)
        {
            //基本定义
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPGlobalStatistics ipstat = properties.GetIPv4GlobalStatistics();
            TcpConnectionInformation[] tcpConnections = properties.GetActiveTcpConnections();
            richTextBox2.AppendText("******\n");
            //打印相关数据
            richTextBox2.AppendText("本机所在域："+ properties.DomainName +"\n");
            richTextBox2.AppendText("接收数据包数：" + ipstat.ReceivedPackets + "\n");
            richTextBox2.AppendText("转发数据包数：" + ipstat.ReceivedPacketsForwarded + "\n");
            //循环遍历TcpConnectionInformation数组，得到TcpConnectionInformation类型的属性，同时进行打印
            foreach (TcpConnectionInformation t in tcpConnections)
            {
                string str = "";
                str += "Local endpoint:" + t.LocalEndPoint.Address;
                str += ",Remote endpoint:" + t.RemoteEndPoint.Address;
                str += ",  " + t.State;
                richTextBox2.AppendText(str + "\n");

            }
        }
    }
}
