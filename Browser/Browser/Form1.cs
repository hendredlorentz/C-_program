using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Browser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 页面加载（返回主页）
        private void Form1_Load(object sender, EventArgs e)
        {
            // 
            webBrowser1.GoHome();
        }
        //进行页面跳转的按钮点击事件
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(toolStripTextBox1.Text);
        }

        // 对输入的网址进行跳转
        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                webBrowser1.Navigate(toolStripTextBox1.Text);
        }

        // 进行渲染（加载渲染）
        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            toolStripTextBox1.Text = webBrowser1.Url.ToString();
            toolStripStatusLabel1.Text = "正在打开网页 " + webBrowser1.Url.ToString();
        }

        // 进度条渲染
        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = (int) (e.CurrentProgress / e.MaximumProgress) * 100;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "完成";
            toolStripProgressBar1.Value = 0;
        }
        // 停止按钮
        private void StopButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
        }
        // 刷新按钮
        private void Fresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }
        // 返回主页按钮
        private void mainJsp_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }
        // 返回按钮
        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
