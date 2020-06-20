using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 简单工厂模式
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("请输入数字A...");
                string 数字1 = Console.ReadLine();
                Console.WriteLine("请输入数字B...");
                string 数字2 = Console.ReadLine();
                Console.WriteLine("请输入运算符...");
                string 运算符 = Console.ReadLine();
                计算抽象 计算 = 计算工厂.创建工厂(运算符, Convert.ToInt32(数字1), Convert.ToInt32(数字2));
                Console.WriteLine("结果为:" + 计算.获取结果());
                Console.ReadKey();
            }

        }
    }
}
