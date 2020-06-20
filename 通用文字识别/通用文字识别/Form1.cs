using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace 通用文字识别
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            JObject jtoken = JsonConvert.DeserializeObject<JObject>(getAccessToken());
            access_token = jtoken["access_token"].ToString();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            fileInfos = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "图片集").GetFiles();
        }
        // 设置APPID/AK/SK
        static string APP_ID = "18910988";
        static string API_KEY = "YtGvRU82RkdxHOBFvC1qUwIx";
        static string SECRET_KEY = "E2rr6aRaKhxsNGP1Lm9Ij3cavbIZs7y7";
        static string access_token = "";
        static FileInfo[] fileInfos =null;
        //static int Convert.ToInt32( textBox2.Text) = 0;

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        // 通用文字识别（高精度版）
        public static string accurateBasic(string imagePath)
        {
            string token = access_token;
            string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic?access_token=" + token;
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;
            // 图片的base64编码
            string base64 = getFileBase64(imagePath);
            String str = "image=" + HttpUtility.UrlEncode(base64);
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            Console.WriteLine("通用文字识别（高精度版）:");
            Console.WriteLine(result);
            return result;
        }

        public static String getFileBase64(String fileName)
        {
            FileStream filestream = new FileStream(fileName, FileMode.Open);
            byte[] arr = new byte[filestream.Length];
            filestream.Read(arr, 0, (int)filestream.Length);
            string baser64 = Convert.ToBase64String(arr);
            filestream.Close();
            return baser64;
        }

        public static String getAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", API_KEY));
            paraList.Add(new KeyValuePair<string, string>("client_secret", SECRET_KEY));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            页码.Text = (Convert.ToInt32(页码.Text) + 1).ToString(); 
            识别();
        }

        //上一页
        private void button1_Click_1(object sender, EventArgs e)
        {
            页码.Text=(Convert.ToInt32( 页码.Text)-1).ToString();
            识别();
        }

        private void 识别()
        {
            
            if (Convert.ToInt32(页码.Text) < 0 || Convert.ToInt32(页码.Text) >= fileInfos.Length)
            {
                MessageBox.Show("选择已超出范围");
                页码.Text = "0";
                return;
            }
            label1.Text = "正在识别,请等待.....";
            //File.Exists(fileInfos[Convert.ToInt32( textBox2.Text)].te)
            Image image;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "文字集/" + fileInfos[Convert.ToInt32(页码.Text)].Name + ".txt"))
            {
                内容.Text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "文字集/" + fileInfos[Convert.ToInt32(页码.Text)].Name + ".txt");
                pictureBox1.LoadAsync(fileInfos[Convert.ToInt32(页码.Text)].FullName);
                label1.Text = "完成...";
                return;
            }
            using (FileStream fs = new FileStream(fileInfos[Convert.ToInt32(页码.Text)].FullName, FileMode.Open, FileAccess.Read))
            {
                image = System.Drawing.Image.FromStream(fs);
              

            }
            if (image.Width > 2000)
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "图片集/双页"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "图片集/双页");
                }
                Image image1 = CutImage(image, new Point(0, 0), image.Width / 2, image.Height);
                Image image2 = CutImage(image, new Point(image.Width / 2, 0), image.Width / 2, image.Height);
                string tpName= Path.GetFileNameWithoutExtension(fileInfos[Convert.ToInt32(页码.Text)].Name);
                string kzName = Path.GetExtension(fileInfos[Convert.ToInt32(页码.Text)].Name);
                string iamge1Path = AppDomain.CurrentDomain.BaseDirectory + "图片集/双页/" + tpName + "01" + kzName;
                image1.Save(iamge1Path);
                string iamge2Path = AppDomain.CurrentDomain.BaseDirectory + "图片集/双页/" + tpName + "02" + kzName;
                image2.Save(iamge2Path);
                Task.Run(() =>
                {
                    string result = accurateBasic(iamge1Path);
                    JObject bd = JsonConvert.DeserializeObject<JObject>(result);
                    var words_result = bd["words_result"];
                    var array = JsonConvert.DeserializeObject<JArray>(words_result.ToString());
                    string text = string.Empty;
                    foreach (JObject word in array)
                    {
                        text += word["words"].ToString() + Environment.NewLine;

                    }
                    BeginInvoke(new Action(() =>
                    {
                        内容.AppendText(text);
                        pictureBox1.LoadAsync(fileInfos[Convert.ToInt32(页码.Text)].FullName);
                        //label1.Text = "完成...";
                    }));
                    File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "文字集/" + fileInfos[Convert.ToInt32(页码.Text)].Name + ".txt", text);
                }).Wait();

                Task.Run(() =>
                {
                    string result = accurateBasic(iamge2Path);
                    JObject bd = JsonConvert.DeserializeObject<JObject>(result);
                    var words_result = bd["words_result"];
                    var array = JsonConvert.DeserializeObject<JArray>(words_result.ToString());
                    string text = string.Empty;
                    text = "================换页咯....==================" + Environment.NewLine;
                    foreach (JObject word in array)
                    {
                        text += word["words"].ToString() + Environment.NewLine;

                    }
                    BeginInvoke(new Action(() =>
                    {
                        内容.AppendText(text);
                        //pictureBox1.LoadAsync(fileInfos[Convert.ToInt32(页码.Text)].FullName);
                        label1.Text = "完成...";
                    }));
                    File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "文字集/" + fileInfos[Convert.ToInt32(页码.Text)].Name + ".txt", text);
                }).Wait();
            }
            else
            {
                Task.Run(() =>
                {
                    string result = accurateBasic(fileInfos[Convert.ToInt32(页码.Text)].FullName);
                    JObject bd = JsonConvert.DeserializeObject<JObject>(result);
                    var words_result = bd["words_result"];
                    var array = JsonConvert.DeserializeObject<JArray>(words_result.ToString());
                    string text = string.Empty;
                    foreach (JObject word in array)
                    {
                        text += word["words"].ToString() + Environment.NewLine;

                    }
                    BeginInvoke(new Action(() =>
                    {
                        内容.Text = text;
                        pictureBox1.LoadAsync(fileInfos[Convert.ToInt32(页码.Text)].FullName);
                        label1.Text = "完成...";
                    }));
                    File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "文字集/" + fileInfos[Convert.ToInt32(页码.Text)].Name + ".txt", text);
                }).Wait();
            }
        }
        /// <summary>
        /// 截取图片区域，返回所截取的图片
        /// </summary>
        /// <param name="SrcImage"></param>
        /// <param name="pos"></param>
        /// <param name="cutWidth"></param>
        /// <param name="cutHeight"></param>
        /// <returns></returns>
        private Image CutImage(Image SrcImage, Point pos, int cutWidth, int cutHeight)
        {

            Image cutedImage = null;

            //先初始化一个位图对象，来存储截取后的图像
            Bitmap bmpDest = new Bitmap(cutWidth, cutHeight, PixelFormat.Format32bppRgb);
            Graphics g = Graphics.FromImage(bmpDest);

            //矩形定义,将要在被截取的图像上要截取的图像区域的左顶点位置和截取的大小
            Rectangle rectSource = new Rectangle(pos.X, pos.Y, cutWidth, cutHeight);


            //矩形定义,将要把 截取的图像区域 绘制到初始化的位图的位置和大小
            //rectDest说明，将把截取的区域，从位图左顶点开始绘制，绘制截取的区域原来大小
            Rectangle rectDest = new Rectangle(0, 0, cutWidth, cutHeight);

            //第一个参数就是加载你要截取的图像对象，第二个和第三个参数及如上所说定义截取和绘制图像过程中的相关属性，第四个属性定义了属性值所使用的度量单位
            g.DrawImage(SrcImage, rectDest, rectSource, GraphicsUnit.Pixel);

            //在GUI上显示被截取的图像
            cutedImage = (Image)bmpDest;

            g.Dispose();

            return cutedImage;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//使最大化窗口失效
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "文字集"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "文字集");
            }
            页码.Text = "0";
            识别();
            //textBox2.Text = (Convert.ToInt32(textBox2.Text) + 1).ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "文字集"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "文字集");
            }
            识别();
        }
    }
}
