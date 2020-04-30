using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MsgService
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     foreach (var item in args)
                     {
                         Console.WriteLine("Êä³ö:"+item);
                         
                     }
                     //Console.WriteLine("a:"+ args[1]);
                     //Console.WriteLine("b:" + args[3]);
                     string ip = args[1];
                     string port = args[3];
                     webBuilder.UseStartup<Startup>();
                     webBuilder.UseUrls($"http://{ip}:{port}");
                 });
        }
    }
}
