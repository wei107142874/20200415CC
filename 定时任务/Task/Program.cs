using Common;
using Quartz;
using System;
using System.Threading.Tasks;

namespace TaskScheduling
{
    class Program
    {
        static async Task Main(string[] args)
        {
            LogSerilog.Init();  //第一次加载时调用
            await TaskQuartz.AddJob<Job1>("每隔5秒", "*/5 * * * * ?");//每隔5秒执行一次这个方法
            Console.ReadKey();
        }
    }

    public class Job1 : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
           return Task.Factory.StartNew(() => {
                Console.WriteLine("执行一次");
            });
        }
    }
}
