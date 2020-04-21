using System;
using System.Reflection;

namespace 特性
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            person.Age = 123;
            person.Bz = "1232";
            person.Name = "张三";
            Type type = person.GetType(); 
            
            //获取对象的特性
            var mytests = (MyTestAttribute[])type.GetCustomAttributes(typeof(MyTestAttribute),false);
            foreach (var item in mytests)
            {
                Console.WriteLine(item.OrderSort);
            }

            //获取属性
            foreach (var prpo in type.GetProperties())
            {
                //获取属性对应的特性
                var myPropTests = (MyTestAttribute[])prpo.GetCustomAttributes(typeof(MyTestAttribute), false);
                foreach (var item in myPropTests)
                {
                    Console.WriteLine(item.OrderSort);
                }
            } 
            
            Console.ReadKey();
        }
    }

    public class MyTestAttribute : Attribute
    {
        public MyTestAttribute(int orderSort)
        {
            OrderSort = orderSort;
        }

        public int OrderSort { get; set; }
    }

    [MyTest(888)]
    public class Person
    {
        [MyTest(1)]
        public string Name { get; set; }

        [MyTest(3)]
        public int Age { get; set; }


        [MyTest(2)]
        public string Bz { get; set; }
    }
}
