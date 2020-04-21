using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _50音图
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitData();
        }

        List<Yt> yts = new List<Yt>();
        private void InitData()
        {
            //ta ku shi i
            string scoureData =File.ReadAllText(acbUrl+ "/source.txt").Replace(" ","").Replace(System.Environment.NewLine, "");
            string[] list = scoureData.Split(',');
            foreach (var item in list)
            {
                string[] a = item.Split('→');
                yts.Add(
                    new Yt
                    {
                        Hiragana = a[1],
                        Katakana = a[3],
                        HSource = a[0],
                        KSource = a[2],
                        Pronunciation = a[4],
                        HimageUrl = $"{acbUrl}/image/{a[4]}.jpg",
                        Remark = a[5].Replace("`", "  ")
                    });
            }
            tb_random.Text = $"0,{(yts.Count - 1).ToString()}";
        }
        static string acbUrl = System.AppDomain.CurrentDomain.BaseDirectory;

        Yt yt = null;
        DateTime? nowTime=null;

        bool lb = false;
        private void button4_Click(object sender, EventArgs e)
        {
            lb = lb ? false : true;

            button4.Text = !lb ? "轮播" : "停止";
            StartLb(sender, e);
            t1.Start();
            if (!lb)
            {
                zdyts.Clear();
            }
        }


        Thread t1;
        /// <summary>
        /// 开始轮播
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StartLb(object sender, EventArgs e)
        {
            tb_check.Focus();
            t1 = new Thread(() =>
            {
                while (lb)
                {
                    Console.WriteLine("切换....");
                    btn_kz_Click(sender, e);
                    Thread.Sleep(Convert.ToInt32(tb_lbjg.Text));
                }
            });
        }

        private void btn_kz_Click(object sender, EventArgs e)
        {
            Thread.Sleep(100);
            Random rd = new Random();
            if (nowTime != null)
            {
                yts.Where(x => x == yt).First().DelayTime = (DateTime.Now - nowTime.Value).TotalMilliseconds;
            }
            string[] radomRange = tb_random.Text.Split(',');
            int i = rd.Next(Convert.ToInt32(radomRange[0]), Convert.ToInt32(radomRange[1]));
            //同期练习大于20次则不在显示
            if (yts[i].Count > 5)
            {
                btn_kz_Click(sender, e);
                return;
            }
            yt = yts[i];
            if (yt?.Know == true)
            {
                if (yts.Where(x => x.Know == true).Count() == yts.Count)
                {
                    foreach (var item in yts)
                    {
                        item.Know = false;
                    }
                    MessageBox.Show("完毕");
                    return;
                }
                btn_kz_Click(sender, e);
                return;
            }
            yts[i].Count++;

            BeginInvoke(new Action(() =>
            {

                nowTime = DateTime.Now;
                lab_show.Text = nowTime.Value.Millisecond % 2 == 0 ? yt.Hiragana : yt.Katakana;
                lab_cut.Text = yt.Count.ToString();
            }));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetData();
        }

        private void SetData()
        {
            pictureBox1.Image = Image.FromFile(yt.HimageUrl, false);
            lab_show.Text = $"平假名:【{yt.Hiragana}】->来源{yt.HSource},片假名:【{yt.Katakana}】->来源{yt.KSource}";
            lab_sort.Text=(yts.FindIndex(x => x == yt)+1).ToString();
            lab_ci.Text = yt.Remark;
        }

        private void tb_sort_TextChanged(object sender, EventArgs e)
        {
            try
            {
                yt = yts[Convert.ToInt32(tb_sort.Text)-1];
                //lab_sort.Text = tb_sort.Text;
                SetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"请输入{yts.Count}以内的数字");
                tb_sort.Text = "1";
            }
        }

        private void tb_yin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                yt = yts.First(x => x.Pronunciation == tb_yin.Text.Trim());
                
                SetData();
            }
            catch (Exception)
            {

               
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            yt = yts[Convert.ToInt32(lab_sort.Text)];
            SetData();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            yt = yts[Convert.ToInt32(lab_sort.Text)-2];
            SetData();
        }


        List<Yt> zdyts = new List<Yt>();

        /// <summary>
        /// 显示没记住的
        /// </summary>
        private void ShowNoZd()
        {
            string data = $"{yt.Pronunciation},{yt.Hiragana},{yt.Katakana},{yt.Know},{yt.KSource},{yt.HSource}{Environment.NewLine}";
            tb_zd.AppendText(data);
            //File.AppendAllText($"{acbUrl}/没记住的50音.txt", data);
            //foreach (var yt in zdyts.Distinct())
            //{

            //}
            //zdyts.Clear();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            yt.Know = false;
            zdyts.Add(yt);
            t1.Abort();
            Console.WriteLine("线程销毁");
            StartLb(sender, e);
            t1.Start();
            ShowNoZd();

            tb_check.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            yt.Know = true;
            t1.Abort();
            Console.WriteLine("线程销毁");
            StartLb(sender, e);
            t1.Start();
            ShowNoZd();
        }

        private void tb_check_TextChanged(object sender, EventArgs e)
        {
            if (this.tb_check.Text == yt.Pronunciation)
            {
                yt.Know = true;
                Console.WriteLine($"{yt.Pronunciation}:正确");
                t1.Abort();
                Console.WriteLine("线程销毁");
                StartLb(sender, e);
                t1.Start();
                ShowNoZd();
                this.tb_check.Text = string.Empty;
                //tb_check.ST
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.PageDown || keyData == Keys.PageUp)
            {
                yt.Know = keyData == Keys.PageDown ? false : true;
                zdyts.Add(yt);
                t1.Abort();
                Console.WriteLine("线程销毁");
                StartLb(new object(), new EventArgs());
                t1.Start();
                ShowNoZd();

                tb_check.Focus();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        随机生成 sj =null;
        private void button7_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            List<Yt> hiraganas = new List<Yt>();

            List<Yt> katakanas = new List<Yt>();
            for (int i = 0; i < 5; i++)
            {
                int j = rd.Next(Convert.ToInt32(0), Convert.ToInt32(45));
                hiraganas.Add(yts[j]);
            }
            for (int i = 0; i < 5; i++)
            {
                int j = rd.Next(Convert.ToInt32(0), Convert.ToInt32(45));
                katakanas.Add(yts[j]);
            }



            if (sj == null)
            {
                sj = new 随机生成();
                sj.CenterScreen();
                //sj.Location = this.Location;

                sj.yts2 = hiraganas;
                sj.yts1 = katakanas;
                sj.SetData();
                sj.Show();
                sj = null;
            }
           
        }
    }
    public class Yt
    {
        /// <summary>
        /// 平假名
        /// </summary>
        public string Hiragana { get; set; }

        /// <summary>
        /// 片假名
        /// </summary>
        public string Katakana { get; set; }

        /// <summary>
        /// 平假名来源
        /// </summary>
        public string HSource { get; set; }

        /// <summary>
        /// 片假名来源
        /// </summary>
        public string KSource { get; set; }

        /// <summary>
        /// 发音
        /// </summary>
        public string Pronunciation { get; set; }

        /// <summary>
        /// 平假名协助记忆图片地址
        /// </summary>
        public string HimageUrl { get; set; }

        /// <summary>
        /// 说明备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 练习等待时长
        /// </summary>
        public double DelayTime { get; set; }

        /// <summary>
        /// 同期练习次数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 认识？
        /// </summary>
        public bool? Know { get; set; }

        ///// <summary>
        ///// 录音地址
        ///// </summary>
        //public string SoundUrl { get; set; }

        //public string 
    }
}
