using System;
using System.Collections.Generic;
using System.Text;

namespace 观察者模式
{
    public abstract class 通知者
    {
        //public List<观察者> 观察者集合 = new List<观察者>();

        public string State { get; set; }

        //public abstract void 添加进监听(观察者 观察者);

        //public abstract void 移除监听(观察者 观察者);

        public abstract void 发送通知(string state);
    }
    public class 具体通知者: 通知者
    {
        public event Action<string> 事件发送通知;

        //public override void 添加进监听(观察者 观察者)
        //{
        //    观察者集合.Add(观察者);
        //}

        //public override void 移除监听(观察者 观察者)
        //{
        //    观察者集合.Remove(观察者);
        //}

        public override void 发送通知(string state)
        {
            事件发送通知(state);
            //foreach (var 观察者 in 观察者集合)
            //{
            //    观察者.Update();
            //}
        }
    }

    //public abstract class 观察者
    //{
    //    public abstract void Update(string state);
    //}

    public class 观察者1//: 观察者
    {
        public  void Update观察者1(string state)
        {
            Console.WriteLine($"{state},观察者1更新了自己");
        }
    }

    public class 观察者2//: 观察者
    {
        public  void Update观察者2(string state)
        {
            Console.WriteLine($"{state},观察者2更新了自己");
        }
    }
}
