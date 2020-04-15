using PlainElastic.Net;
using PlainElastic.Net.Queries;
using PlainElastic.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchDemo
{


    class Program
    {
        static void Main(string[] args)
        {
            //var person = new Person
            //{
            //    Id = "5",
            //    Age = 10,
            //    Name = "阿拉蕾dj ",
            //    Desc = "氨基酸的计算机"
            //};
            //ElasticConnection client = new ElasticConnection("localhost");
            //JsonNetSerializer serializer = new JsonNetSerializer();
            //IndexCommand cmd = new IndexCommand("EDemo", "persons", person.Id);
            //OperationResult result = client.Post(cmd, serializer.Serialize(person));
            //IndexResult indexResult = serializer.ToIndexResult(result);
            //if (indexResult.created)
            //{
            //    Console.WriteLine("创建了");
            //}
            //else
            //{
            //    Console.WriteLine("没有=" + indexResult.error);
            //}
          
            Console.ReadKey();
        }

        //public static void Query()
        //{
        //    ElasticConnection client = new ElasticConnection("localhost");
        //    SearchCommand cmd = new SearchCommand("EDemo", "persons");
        //    var query = new QueryBuilder<Person>()
        //        .Query(b => b.Bool(m => m.Must(t => t.QueryString(t1 => t1.DefaultField("Id").Query("5"))))).From(0).Size(10).Build();

        //    var result = client.Post(cmd, query);
        //    var serializer = new JsonNetSerializer();
        //    var list = serializer.ToSearchResult<Person>(result);

        //    foreach (var item in list.Documents)
        //    {
        //        Console.WriteLine(item.Id);
        //    }
        //}
    }
}
