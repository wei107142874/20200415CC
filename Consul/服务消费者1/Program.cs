using Consul;
using System;

namespace 服务消费者1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri("http://127.0.0.1:8500");
                c.Datacenter = "consul";
            }))
            {
                var serivces = client.Agent.Services().Result.Response;
                foreach (var ser in serivces.Values)
                {
                    Console.WriteLine($"Id={ ser.ID},service ={ser.Service},Address={ser.Address},port={ser.Port}");
                }
            }
            Console.ReadKey();
        }
    }
}
