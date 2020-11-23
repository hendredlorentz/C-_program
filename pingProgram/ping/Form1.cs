using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ping
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
        string ipName = "";
 //       输入的目的地址进行ping
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //获取输入框内容
            this.ipName = textBox1.Text;
            Console.WriteLine(ipName);
        }

        //定义一个全局的Reply变量
        PingReply globalPing;
        
 //     按钮点击事件
        private void button1_Click(object sender, EventArgs e)
        {
            //这里是首先将Ping类进行引入
            var PingPro = new System.Net.NetworkInformation.Ping();
            //将包发去目标地址进行判断
            var result = PingPro.Send(ipName);
            //判断是否成功
            if(result.Status != System.Net.NetworkInformation.IPStatus.Success)
            {
                return;
            }
            // 输出调试（result返回类型，只需要将其渲染到richText就可以了
            
            Console.WriteLine("这里是返回包的内容："+ result.Address + " " +  result.RoundtripTime);
            globalPing = result;
            //数据渲染
            richTextBox1.AppendText("**************************************\n");
            richTextBox1.AppendText("答复的主机地址:"+ result.Address + "\n");
            richTextBox1.AppendText("往返时间:" + result.RoundtripTime + "\n");
            richTextBox1.AppendText("缓冲区大小:" + result.Buffer.Length + "\n");
            richTextBox1.AppendText("生存时间(TTL):" + result.Options.Ttl + "\n");
            richTextBox1.AppendText("是否分段:" + result.Options.DontFragment + "\n");
            richTextBox1.AppendText("**************************************\n");


        }
        //文本框点击事件
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
