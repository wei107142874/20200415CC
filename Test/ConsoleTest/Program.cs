using Common;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //1,1,2,3,5,8,13,21,34
            Random random = new Random();
            
            for (int i = 0; i < 100000; i++)
            {
                OrderDto dto = new OrderDto()
                {
                    DesktopNo = i.ToString(),
                    UserName = "人物" + i.ToString(),
                    OrderItems = new List<OrderItem> 
                    {
                         new OrderItem{ Name="商品"+random.Next(1,10000), Num=i, Price=i*random.Next(1,20)},
                         new OrderItem{ Name="商品"+random.Next(1,10000), Num=i+1, Price=i*random.Next(1,20)},
                         new OrderItem{ Name="商品"+random.Next(1,10000), Num=i+5, Price=i*random.Next(1,20)},
                         new OrderItem{ Name="商品"+random.Next(1,10000), Num=i+2, Price=i*random.Next(1,20)},
                         new OrderItem{ Name="商品"+random.Next(1,10000), Num=i+23, Price=i*random.Next(1,20)},
                         new OrderItem{ Name="商品"+random.Next(1,10000), Num=i+6, Price=i*random.Next(1,20)},
                    }
                };
                var id = HttpClientHelp.Post<OrderDto, object>("http://jk.qskc.iotube.cn/api/order", dto).Result;
                Console.WriteLine(id);
            }

            Console.ReadKey();
        }

        

        

        
    }

    public class OrderDto
    {
        /// <summary>
        /// 桌号
        /// </summary>
        public string DesktopNo { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public List<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public string Name { get; set; }

        /// <summary>
        /// 单家
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
    }
}
