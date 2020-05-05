using System;

namespace 单例模式
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //1、1、2、3、5、8、13、21、34......求第30位数是多少， 用递归算法实现。


            Console.ReadKey();
        }
    }

    public class Singleton
    {
        //volatile编译的时候不会微调代码
        private static volatile Singleton instance = new Singleton();
        

        //public static Singleton GetInstance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new Singleton();
        //        }
        //        return instance;
        //    }
        //}
        //你到底想搞什么飞机???
    }
}
