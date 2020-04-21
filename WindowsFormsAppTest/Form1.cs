using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Headers.Add("username", "admin");
            content.Headers.Add("pwd", "123");
            using (FileStream fileStream =File.OpenRead("C:/Users/wy/Desktop/IMG_2612.JPG"))
            {
                content.Add(new StreamContent(fileStream),"file","1.jpg");
                var httpContent = await Post("http://localhost:51214/home/upload", content);
                string result = await httpContent.ReadAsStringAsync();
                MessageBox.Show(result);
            }
        }

        //private async Task<HttpContent> Upload(string url, HttpContent httpContent)
        //{
            
        //}

        private async Task<HttpContent> Post(string url, HttpContent httpContent)
        {
            HttpClient httpClient = new HttpClient();
            var msg= await httpClient.PostAsync(url, httpContent);
            return msg.Content;
        }
    }
}
