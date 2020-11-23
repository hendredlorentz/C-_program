using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

namespace WebClientDownload
{
    public partial class Form1 : Form
    {
        private bool isBusy;
        private WebClient client;

        public Form1()
        {
            InitializeComponent();
            client = new WebClient();

            // 订阅下载完成事件.
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            // 订阅下载进度事件.
            client.DownloadProgressChanged += client_DownloadProgressChanged;
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            // 如果正在下载中, 则取消下载.
            if (isBusy)
            {
                client.CancelAsync();
                isBusy = false;
                downloadButton.Text = "下载";
            }
            // 否则，下载
            else
            {
                try
                {
                    Uri uri = new Uri(urlTextBox.Text);
                    downloadProgressBar.Value = 0;
                    if (urlTextBox.Text.EndsWith("/"))
                        //直接下载到本地
                        client.DownloadFileAsync(uri, "localfile.html");
                    else
                        client.DownloadFileAsync(uri, urlTextBox.Text.Substring(urlTextBox.Text.LastIndexOf("/") + 1));
                    downloadButton.Text = "取消";
                    isBusy = true;
                }
                catch (UriFormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // 显示下载是否完成的消息.
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            isBusy = false;
            downloadButton.Text = "下载";
            if (e.Error == null)
                MessageBox.Show("下载完成");
            else
                MessageBox.Show("下载未能完成: " + e.Error.Message);
        }

        // 刷新进度条
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadProgressBar.Value = e.ProgressPercentage;
        }
    }
}