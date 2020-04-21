using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 防坑手册
{
    class Test
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string FisrtContent { get; set; }

        public List<Comment> CommentList { get; set; }
    }

    class Comment
    {
        /// <summary>
        /// 是否正面
        /// </summary>
        public bool IsJust { get; set; }

        /// <summary>
        /// 评论
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Date { get; set; }

        public List<Hf> HfList { get; set; }
    }

    /// <summary>
    /// 评论下的回复
    /// </summary>
    class Hf
    {
        public string Text { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Date { get; set; }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Test> entitys = new List<Test>();
        HttpClient client = new HttpClient();
        private async void button1_Click(object sender, EventArgs e)
        {
            string listHtm = await client.GetStringAsync("http://www.mettew.com/companies");
            MatchCollection matchs = Regex.Matches(listHtm, "/companies/(?<id>[0-9]+)\">(?<name>\\S+)</a>");
            foreach (Match match in matchs)
            {
                Test entity = new Test();
                entity.Id = match.Groups["id"].Value;
                entity.Name = match.Groups["name"].Value;


                await GetEnterpriseInfo(entity);
                string json = JsonConvert.SerializeObject(entity);
                File.AppendAllLines(AppDomain.CurrentDomain.BaseDirectory + "数据.txt", new List<string> { json });
                Console.WriteLine(json);
                //await Task.Delay(5000);
                //entitys.Add(entity);
            }
            //GetEnterpriseInfo(new Test { Id = "459" });
        }

        private async Task GetEnterpriseInfo(Test entity)
        {
            //根据Id获取内容
            string contentHtm = await client.GetStringAsync("http://www.mettew.com/companies/" + entity.Id);
            //string contentHtm = await client.GetStringAsync("http://www.mettew.com/companies/" + entity.Id);

            Match match = Regex.Match(contentHtm, "h4>(?<firstContent>\\S+)</h4>");

            entity.FisrtContent = match.Groups["firstContent"].Value;

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(contentHtm);

            HtmlNodeCollection nodels = doc.DocumentNode.SelectNodes("//div[@class='commentType']");
            entity.CommentList = new List<Comment>();
            if (nodels == null)
                return;
            foreach (HtmlNode node in nodels)
            {
                //评论
                HtmlNode textNode = node.SelectSingleNode("./div[@class='comment_txt pull-left']");
                if (textNode == null)
                    continue;
                Comment comment = new Comment();
                comment.HfList = new List<Hf>();
                

                comment.Text = textNode.SelectSingleNode("./p").InnerText;
                comment.Date = textNode.SelectSingleNode("./div[1]/div[1]").InnerText;
                //回复
                HtmlNodeCollection huifus = node.SelectNodes("./div[@class='comment_child']");
                if (huifus != null)
                {
                    foreach (HtmlNode huifu in huifus)
                    {
                        Hf hf = new Hf();
                        HtmlNode huifuText = huifu.SelectSingleNode("./p");
                        hf.Text = huifuText.InnerText;

                        HtmlNode hfsj = huifu.SelectSingleNode("./div[1]/div[1]");
                        hf.Date = hfsj.InnerText;
                        comment.HfList.Add(hf);
                    }
                }
                entity.CommentList.Add(comment);
            }
        }
    }
}
