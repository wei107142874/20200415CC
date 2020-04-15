using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _50音图
{
    public partial class 随机生成 : Form
    {

        public List<Yt> yts1;
        public List<Yt> yts2;

        public 随机生成()
        {
            InitializeComponent();
            
        }

        public void SetData ()
        {
            label1.Text = string.Join(",", yts1.Select(x=>x.Katakana));
            label2.Text = string.Join(",", yts2.Select(x => x.Hiragana));

            lb1 = label1.Text;
            lb2 = label2.Text;
        }


        private string lb1;
        private string lb2;

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            if (label1.Text != lb1)
            {
                return;
            }
            label1.Text = lb1 + "   " + string.Join(",", yts1.Select(x => x.Pronunciation));
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            if (label2.Text != lb2)
            {
                return;
            }
            label2.Text = lb2 + "   " + string.Join(",", yts2.Select(x => x.Pronunciation));
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = lb2;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Text = lb1;
        }
    }
}
