using System;
using System.IO;
using System.Net;

namespace FTPCmd
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0].Equals("/?"))
            {
                DisplayUsage();
            }
            else if (args.Length == 1)
            {
                Download(args[0]);
            }
            else if (args.Length == 2)
            {
                if (args[0].Equals("/list"))
                    List(args[1]);
                else
                    Upload(args[0], args[1]);
            }
            else
            {
                Console.WriteLine("参数无效.");
            }
        }


        static void DisplayUsage()
        {
            Console.WriteLine("用法:");
            Console.WriteLine("    FtpCmd [/? | <FTP download URL> | <local file>");
            Console.WriteLine("               <FTP upload URL> | /list <FTP list URL>]");
            Console.WriteLine();
            Console.WriteLine("说明");
            Console.WriteLine("    FTP download URL   从 FTP 服务器上下载 URL 指定的文件.");
            Console.WriteLine("    FTP upload URL     将文件上传到 URL 指定的 FTP 服务器.");
            Console.WriteLine("    FTP list URL       列出 FTP 服务器上的目录清单.");
            Console.WriteLine("    local file         上传到 FTP 服务器的文件.");
            Console.WriteLine();
            Console.WriteLine("    选项:");
            Console.WriteLine("        /?             显示本帮助信息.");
            Console.WriteLine("        /list          列出目录清单.");
            Console.WriteLine();
            Console.WriteLine("举例:");
            Console.WriteLine("    下载文件           FtpCmd ftp://myserver/download.txt");
            Console.WriteLine("    上传文件           FtpCmd upload.txt ftp://myserver/upload.txt");
        }

        static void Download(string downloadUrl)
        {
            Stream responseStream = null;
            FileStream fileStream = null;
            StreamReader reader = null;
            try
            {
                FtpWebRequest downloadRequest =
                    (FtpWebRequest)WebRequest.Create(downloadUrl);
                FtpWebResponse downloadResponse =
                    (FtpWebResponse)downloadRequest.GetResponse();
                responseStream = downloadResponse.GetResponseStream();

                string fileName =
                    Path.GetFileName(downloadRequest.RequestUri.AbsolutePath);

                if (fileName.Length == 0)
                {
                    reader = new StreamReader(responseStream);
                    Console.WriteLine(reader.ReadToEnd());
                }
                else
                {
                    fileStream = File.Create(fileName);
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    while (true)
                    {
                        bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                            break;
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }
                Console.WriteLine("下载完成.");
            }
            catch (UriFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                else if (responseStream != null)
                    responseStream.Close();
                if (fileStream != null)
                    fileStream.Close();
            }
        }

        static void Upload(string fileName, string uploadUrl)
        {
            Stream requestStream = null;
            FileStream fileStream = null;
            FtpWebResponse uploadResponse = null;
            try
            {
                FtpWebRequest uploadRequest =
                    (FtpWebRequest)WebRequest.Create(uploadUrl);
                uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;

                // 上传文件不支持 Http 代理
                // 所以我们要禁止代理.
                uploadRequest.Proxy = null;

                requestStream = uploadRequest.GetRequestStream();
                fileStream = File.Open(fileName, FileMode.Open);

                byte[] buffer = new byte[1024];
                int bytesRead;
                while (true)
                {
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;
                    requestStream.Write(buffer, 0, bytesRead);
                }

                // 必须先关闭 request 流才能获得 response 流.
                requestStream.Close();

                uploadResponse =
                    (FtpWebResponse)uploadRequest.GetResponse();
                Console.WriteLine("上传完成.");
            }
            catch (UriFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (uploadResponse != null)
                    uploadResponse.Close();
                if (fileStream != null)
                    fileStream.Close();
                if (requestStream != null)
                    requestStream.Close();
            }
        }

        private static void List(string listUrl)
        {
            StreamReader reader = null;
            try
            {
                FtpWebRequest listRequest =
                    (FtpWebRequest)WebRequest.Create(listUrl);
                listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse listResponse =
                    (FtpWebResponse)listRequest.GetResponse();
                reader = new StreamReader(listResponse.GetResponseStream());
                Console.WriteLine(reader.ReadToEnd());
                Console.WriteLine("列表完成.");
            }
            catch (UriFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}