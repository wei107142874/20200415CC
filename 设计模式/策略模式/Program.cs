using System;

namespace 策略模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("策略模式:它定义了算法家族,分别封装起来,让他们之前可以互相替换,此模式让算法的变化不会影响使用算法的客户");
            收费上下文 上下文 = new 收费上下文(new 原价(23));
            Console.WriteLine(上下文.获取结果());
            上下文 = new 收费上下文(new 打折(23,0.9));
            Console.WriteLine(上下文.获取结果());
            Console.ReadKey();
        }
    }
}
