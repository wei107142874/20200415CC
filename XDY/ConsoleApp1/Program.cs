using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace _9frf
{
    class Program
    {
        static int pageIndex = 1;
        static int index = 31171;
        // 定义一个标识确保线程同步
        private static readonly object locker = new object();
        static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            Console.WriteLine("输入开启的线程数...");

            int num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("请输入开始页码");
            pageIndex = Convert.ToInt32(Console.ReadLine());
            ccc = num;
            for (int i = 0; i < num; i++)
            {
                ThreadPool.QueueUserWorkItem(state => {
                    Pp(state);
                }, i);
            }
            manualResetEvent.Set();
            while (true)
            {
                string read = Console.ReadLine();
                if (read == "end")
                {
                    Console.WriteLine("等待任务结束....");
                    manualResetEvent.Reset();
                    rr = true;
                }
            }
        }
        static bool rr = false;
        static int ccc = 0;
        private static void Pp(object obj)
        {
            if (rr)
            {
                ccc--;
                Console.WriteLine($"{obj}:关门");
                if (ccc == 0)
                {
                    Console.WriteLine("所有任务结束....");
                    //关机
                }
            }
            Console.WriteLine($"{obj}等待开门...");
            manualResetEvent.WaitOne();
            Console.WriteLine($"{obj}开门了....");
            int Cut = 0;
            int cPageIndex = 0;
            lock (locker)
            {
                //Cut = index++;
                cPageIndex = pageIndex++;
            }
            Console.WriteLine("线程..." + obj);
            //获取当前页面的几个id
            List<string> pageListHref = GetPageListId(cPageIndex);
            bool zc = true;
            while (pageListHref.Count > 0)
            {
                string path = "";
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        int pageListIndex = 0;
                        if (!zc)
                        {
                            pageListIndex = pageListHref.Count - 1;
                        }
                        string baseUrl = "https://9frf.com";
                        string html = client.GetAsync($"{baseUrl}/{pageListHref[pageListIndex]}").Result.Content.ReadAsStringAsync().Result;
                        string tempHref = pageListHref[pageListIndex];
                        pageListHref.Remove(pageListHref[pageListIndex]);
                        var doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(html);
                        //标题
                        var titleHmtl = doc.DocumentNode.SelectSingleNode("//div[@class='bn_bt']");
                        string title = titleHmtl.InnerText;

                        var adrHtml = doc.DocumentNode.SelectSingleNode("//span[@id='vpath']");
                        string adr = adrHtml.InnerText;
                        adr = adr.Substring(0, adr.IndexOf('/'));
                        //Console.WriteLine(title);
                        //文件

                        string basePath = AppDomain.CurrentDomain.BaseDirectory + "/Videos/";
                        path = $"{basePath}{title}";
                        if (Directory.Exists(path))
                        {
                            Console.WriteLine(Cut + ":已存在;当前页码:" + cPageIndex);
                            continue;
                        }
                        else
                        {
                            //Console.WriteLine($"[{Cut}]正在写入..." + title);
                            Console.WriteLine($"[{cPageIndex}]正在写入..." + title);
                            //创建目录
                            Directory.CreateDirectory(path);
                            //保存文件

                            string m3u8 = Getm3u8(adr, out int kb);
                            if (m3u8 == null)
                            {
                                Console.WriteLine($"{pageIndex}{title}:得不到哦。。。");
                            }
                            string m3u8File = $"https://v.s91b.com/v/{adr}/{m3u8}";
                            Stream m3u8FileStream = client.GetAsync(m3u8File).Result.Content.ReadAsStreamAsync().Result;

                            using (StreamReader streamReader = new StreamReader(m3u8FileStream))
                            {
                                int m3u8_Cut = 0;
                                while (!streamReader.EndOfStream)
                                {
                                    string line = streamReader.ReadLine();
                                    if (!line.Contains("#"))
                                    {
                                        if (m3u8_Cut > 2)
                                        {
                                            string tsFile = $"https://v.s91b.com/v/{adr}/{kb}kb/hls/{line}";
                                            var tsStream = client.GetAsync(tsFile).Result.Content.ReadAsStreamAsync().Result;
                                            if (tsStream.Length == 0)
                                            {
                                                Directory.Delete(path);
                                                zc = false;
                                                pageListHref.Add(tempHref);
                                                break;
                                            }
                                            //Console.WriteLine($"【{title}】写入{m3u8_Cut++}");
                                            string c = JsCut(m3u8_Cut++);
                                            FileStream tsNewStream = File.Create(path + "/" + c + ".ts");
                                            {
                                                //从当前流中读取字节，读入字节数组中
                                                byte[] fileBytes = new byte[tsStream.Length];
                                                tsStream.Read(fileBytes, 0, Convert.ToInt32(tsStream.Length));
                                                //将字节数组写入流
                                                tsNewStream.Write(fileBytes, 0, Convert.ToInt32(tsStream.Length));
                                                tsNewStream.Close();
                                            };
                                        }
                                        else
                                            m3u8_Cut++;
                                    }
                                }
                                if (m3u8_Cut > 2)
                                {
                                    //从当前流中读取字节，读入字节数组中
                                    FileStream fileStream = File.Create(path + "/index.m3u8");
                                    {
                                        //从当前流中读取字节，读入字节数组中
                                        byte[] fileBytes = new byte[m3u8FileStream.Length];
                                        m3u8FileStream.Read(fileBytes, 0, Convert.ToInt32(m3u8FileStream.Length));
                                        //将字节数组写入流
                                        fileStream.Write(fileBytes, 0, Convert.ToInt32(m3u8FileStream.Length));
                                        fileStream.Close();
                                    };
                                }
                                else
                                {
                                    Directory.Delete(path);
                                }
                            }
                        }
                        Console.WriteLine(title + "完成");
                    }
                    catch (Exception e)
                    {
                        if (!string.IsNullOrEmpty(path))
                            Directory.Delete(path, true);
                        Console.WriteLine($"异常——{Cut}--{e.Message}");
                        continue;
                    }
                }
            }

            Console.WriteLine(cPageIndex + "：结束");
            ThreadPool.QueueUserWorkItem(state => {
                Pp(state);
            }, obj);
        }

        private static string Getm3u8(string adr, out int kb)
        {
            using (HttpClient client = new HttpClient())
            {
                Stream stream = client.GetAsync($"https://v.s91o.com/v/{adr}/index.m3u8").Result.Content.ReadAsStreamAsync().Result;
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();
                        if (!line.Contains("#"))
                        {
                            kb = Convert.ToInt32(line.Substring(0, 3));
                            return line;
                        }
                    }
                }
            }
            kb = 0;
            return null;
        }

        private static List<string> GetPageListId(int cPageIndex)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            using (HttpClient httpClient1 = new HttpClient())
            {
                string strHmt = httpClient1.GetAsync($"https://9frf.com/html/category/video/video1/page_{cPageIndex}.html").Result.Content.ReadAsStringAsync().Result;
                //读取
                doc.LoadHtml(strHmt);

                var masonry = doc.DocumentNode.SelectSingleNode("//ul[@class='masonry']");
                var masonry_lis = masonry.SelectNodes("./li");
                List<string> aList = new List<string>();
                for (int j = 0; j < masonry_lis.Count; j++)
                {
                    var masonry_li = masonry_lis[j];
                    //链接
                    var a = masonry_li.SelectSingleNode("./div[@class='t_p']/a");
                    string href = a.Attributes["href"].Value;
                    aList.Add(href);
                }
                return aList;
            }
        }

        private static string JsCut(int cut)
        {
            string val = "";
            if (cut < 10)
            {

                val = "000" + cut;
            }
            else if (cut < 100 && cut >= 10)
            {
                val = "00" + cut;
            }
            else if (cut < 1000 && cut >= 100)
            {
                val = "0" + cut;
            }
            else
            {
                val = cut.ToString();
            }
            return val;
        }


        /// <summary>
        /// 将某一文件夹下的ts文件 合并为一个完整版  BY GJW
        /// </summary>
        /// <param name="nameList">需要合并的视频地址集合</param>
        /// <param name="savePath">保存地址</param>
        /// <param name="fileNmae">合成的文件名</param>
        public static void MergeVideo(List<string> nameList, string savePath, string fileNmae)
        {
            if (nameList.Count == 0 || string.IsNullOrEmpty(savePath)) throw new Exception("文件路径不能为空");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序
            //向cmd窗口发送输入信息
            //拼接命令
            //string cmdstr = string.Format(@"copy /b {0}\*.ts  {1}\{2}_完整版.ts",videoPath,savePath,fileNmae);
            string cmdstr = string.Format(@"copy /b  {0}  {1}\{2}", string.Join("+", nameList), savePath, fileNmae);

            p.StandardInput.WriteLine(cmdstr + "&exit");
            p.StandardInput.AutoFlush = true;

            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();

            p.WaitForExit();//等待程序执行完退出进程
            p.Close();

        }

    }

    public class Video
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        public string Cover { get; set; }
    }

    public class HtmlAgilityPackExtions
    {
        public HtmlAgilityPackExtions()
        {

        }

        public HtmlDocument Doc { get; set; }

        public HtmlAgilityPackExtions(string url)
        {
            Doc = new HtmlAgilityPack.HtmlDocument();
            Doc.Load(url);
        }

        //public async Task GG()
        //{

        //}
    }
}
