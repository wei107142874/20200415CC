using System;
using System.Threading;

namespace 观察者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            //观察者1 观1 = new 观察者1();
            //观察者2 观2 = new 观察者2();
            //具体通知者 通知1 = new 具体通知者();
            //通知1.添加进监听(观1);
            //通知1.添加进监听(观2);
            //Thread.Sleep(2000);
            //通知1.State = "大事不妙,赶紧撤退";
            //通知1.发送通知();
            观察者1 观1 = new 观察者1();
            观察者2 观2 = new 观察者2();
            具体通知者 通知1 = new 具体通知者();
            //通知1.添加进监听(观1);
            //通知1.添加进监听(观2);
            //Thread.Sleep(2000);
            通知1.State = "大事不妙,赶紧撤退";
            通知1.事件发送通知 += 观1.Update观察者1;
            通知1.事件发送通知 += 观2.Update观察者2;
            通知1.发送通知(通知1.State);

            Console.WriteLine(dg(11));
            Console.ReadKey();
        }
        static int dg(int v)
        {
            if (v < 0)
                return -1;
            if (v == 0)
                return 0;
            if (v == 1)
                return 1;
            return dg(v - 1) + dg(v - 2);
        }

    }


}
