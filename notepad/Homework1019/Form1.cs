using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework1019
{
    public partial class Form1 : Form
    {
        private OpenFileDialog od = new OpenFileDialog();
        private SaveFileDialog saveD = new SaveFileDialog();
        private bool changed = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //保存文件的格式
            od.FileName = "";
            od.Filter = "TXT FILE(*.txt)|*.txt";
            saveD.Filter = "TXT FILE(*.txt)|*.txt";

        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //更改就保存
            if (od.FileName != "" && changed == true && MessageBox.Show("文本已经更改了，要保存么？", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK) ;
            {
                richTextBox1.LoadFile(od.FileName, RichTextBoxStreamType.PlainText);
            }
            od.FileName = ""; // 文件为空
            this.Text = "记事本"; // 改变文本
            this.richTextBox1.Clear(); //清空文本框
            this.changed = false; //改变是否更改的私有变量
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            od.ShowDialog();//选择文件
            if (od.FileName!="")
            {
                richTextBox1.LoadFile(od.FileName, RichTextBoxStreamType.PlainText);//加载文件，读取文件内容
                this.Text = od.FileName + "-记事本";

            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (od.FileName != "")
            {
                richTextBox1.SaveFile(od.FileName, RichTextBoxStreamType.PlainText);//保存文见
            }
            else
                MessageBox.Show("先打开文本文件", "信息提示", MessageBoxButtons.OK);//直接弹出框进行提示
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // 另存为类库
                richTextBox1.SaveFile(saveD.FileName, RichTextBoxStreamType.PlainText);
            }

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //基本退出代码
            this.Dispose();//隐藏
            this.Close(); // 关闭
        }
        //这底下的都是基本的文本类库，就是剪切，复制...
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void 查看帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://answers.microsoft.com/en-us/windows/forum/apps_windows_10");
        }

        private void 剪切ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void 复制ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void 全选ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void 粘贴ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }
    }
}
