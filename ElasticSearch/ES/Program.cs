using Common;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES
{
    class Program
    {
       static  List<Student> students = new List<Student>();
        static async Task Main(string[] args)
        {
            ElasticSearchHelp.GetClient();
            //InitData(ElasticSearchHelp._client);
            //foreach (var student in students)
            //{
            //    await ElasticSearchHelp.CreateIndex(student);
            //}

            var searchResults = ElasticSearchHelp._client.Search<Student>(s => s
                .Size(0)
                .Aggregations(a=>a
                    .Average("Average", a=>a.Field(f=>f.Age))
                    .Max("Max", a => a.Field(f => f.Age))
                    .Stats("Stats", a => a.Field(f => f.Age))
                )
            );

            IReadOnlyCollection<Student> students = searchResults.Documents;
            foreach (var student in students)
            {
                Console.WriteLine($"Name={student.Name}");
                Console.WriteLine($"Age={student.Age}");
                Console.WriteLine($"Sex={student.Sex}");
                Console.WriteLine($"Desc={string.Join(",", student.Desc)}");
                Console.WriteLine("=======================");
            }
            //Console.WriteLine(students.First().Name);
            Console.WriteLine("hello ");
            Console.ReadKey();
        }

        public static void InitData(ElasticClient client)
        {
            students.Add(new Student
            {
                Name = "赵云",
                Age = 17,
                Sex = 1,
                Desc = new string[]{ "万将从冲取敌将首级","长枪依在","保家卫国" }
            }) ;
            students.Add(new Student
            {
                Name = "钱大富",
                Age = 16,
                Sex = 1,
                Desc = new string[] { "是钱如命", "肚子大", "胆子小" }
            });
            students.Add(new Student
            {
                Name = "孙悟空",
                Age = 3000,
                Sex = 1,
                Desc = new string[] { "嫉恶如仇", "斗战胜佛"}
            });
            students.Add(new Student
            {
                Name = "李云龙",
                Age = 40,
                Sex = 1,
                Desc = new string[] { "团长", "没文化","凶巴巴" }
            });
            students.Add(new Student
            {
                Name = "周卫国",
                Age = 40,
                Sex = 1,
                Desc = new string[] { "团长", "有文化" }
            });
            students.Add(new Student
            {
                Name = "吴辉",
                Age = 24,
                Sex = 1,
                Desc = new string[] { "说话不算话" }
            });
            students.Add(new Student
            {
                Name = "郑和",
                Age = 60,
                Sex = 1,
                Desc = new string[] { "开拓了丝绸之路","太监" }
            });
            students.Add(new Student
            {
                Name = "王昭君",
                Age = 24,
                Sex = 0,
                Desc = new string[] { "认不到" }
            });
        }
    }

    

    public class Student
    {
        public string Name { get; set; }
        public int Sex { get; set; }

        public long Age { get; set; }

        public string[] Desc { get; set; }
    }
}
