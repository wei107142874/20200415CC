using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            tb_data.Text = string.Join(",", array.Array);
        }

        ArrayQueue array = new ArrayQueue(3);
        Random random = new Random();

        private void btn_addqueue_Click(object sender, EventArgs e)
        {
            array.AddQueue(random.Next(1,100));
            tb_data.Text = string.Join(",", array.Array);
        }

        private void btn_quitqueue_Click(object sender, EventArgs e)
        {
            try
            {
                lab_quitData.Text ="出队列:"+ array.QuitQueue().ToString();
                tb_data.Text = string.Join(",", array.Array);
            }
            catch( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class ArrayQueue
    {
        public ArrayQueue(int maxSize)
        {
            MaxSize = maxSize;
            Array = new int[MaxSize];
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="data"></param>
        public void AddQueue(int data)
        {
            if (IsFull())
            {
                MessageBox.Show ("队列满");
                return;
            }
            Rear = Rear + 1;
            Array[Rear] = data;
        }

        /// <summary>
        /// 出队列
        /// </summary>
        /// <returns></returns>
        public int QuitQueue()
        {
            if (IsEmpty())
            {
                throw new Exception("队列空");
            }
            Front = Front + 1;
            return Array[Front];
        }

        bool IsFull()
        {
            return MaxSize - 1 == Rear;
        }

        bool IsEmpty()
        {
            return Rear == Front;
        }

        /// <summary>
        /// 队列数组
        /// </summary>
        public int[] Array { get; set; }

        /// <summary>
        /// 前端(队首)
        /// </summary>
        public int Front { get; set; } = -1;

        /// <summary>
        /// 后端(队尾)
        /// </summary>
        public int Rear { get; set; } = -1;

        /// <summary>
        /// 队列最大长度
        /// </summary>
        public int MaxSize { get; set; }

    }
}
