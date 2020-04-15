using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            using (ConnectionMultiplexer conn = await ConnectionMultiplexer.ConnectAsync("127.0.0.1:6379"))
            {
                IDatabase db = conn.GetDatabase();
                //await db.StringSetAsync("key", "value");
                //List< KeyValuePair<RedisKey, RedisValue>> pair = new List <KeyValuePair<RedisKey, RedisValue>>();
                // pair.Add(new KeyValuePair<RedisKey, RedisValue>("k1","v1"));
                // pair.Add(new KeyValuePair<RedisKey, RedisValue>("k2", "v2"));
                // await db.StringSetAsync(pair.ToArray());

                // await db.KeyExpireAsync("key", TimeSpan.FromSeconds(100));

                //string key= db.StringGet("key");

                //db.StringIncrement("key", 2);
                //db.StringIncrement("key", 2.2);

                //db.SetAdd("key", 2);
                //db.SetAdd("key", "1");
                //long len = db.SetLength("key");
                //db.SetRemove("key", "value");
                //RedisValue[] redisValues = db.SetMembers("key");
                //bool r= db.SetContains("key", "val;ue");//判断某个元素是否存在

                //MessageBox.Show(len.ToString());

                db.SortedSetIncrement("key", "zhang", 1);
                db.SortedSetIncrement("key", "zhang", 1);
                db.SortedSetIncrement("key", "wang", 1);
                db.SortedSetIncrement("key", "wang", 1);
                db.SortedSetIncrement("key", "li", 1);
                db.SortedSetDecrement("key", "wang", 1);
                SortedSetEntry[] items= db.SortedSetRangeByRankWithScores("key",0,2,Order.Descending);
                foreach (var item in items)
                {
                    MessageBox.Show(item.Element + ":" + item.Score);
                }



            }
            MessageBox.Show("OK");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            using (ConnectionMultiplexer conn = await ConnectionMultiplexer.ConnectAsync("127.0.0.1:6379"))
            {
                IDatabase db = conn.GetDatabase();

                await db.ListLeftPushAsync("key", this.textBox1.Text);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            using (ConnectionMultiplexer conn = await ConnectionMultiplexer.ConnectAsync("127.0.0.1:6379"))
            {
                IDatabase db = conn.GetDatabase();

                string value = await db.ListLeftPopAsync("key");
                MessageBox.Show(value);
            }
        }
    }
}
