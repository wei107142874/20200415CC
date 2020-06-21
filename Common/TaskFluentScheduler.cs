using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class TaskFluentScheduler<Job>: Registry where Job:IJob
    {
        
        public TaskFluentScheduler()
        {
            //让Job进行单线程跑，避免没跑完时的重复执行。(全局)
            NonReentrantAsDefault();
            //让Job进行单线程跑，避免没跑完时的重复执行。(单个任务)
            //Schedule<DataSyncJob>().NonReentrant().ToRunNow().AndEvery(5).Seconds();

            //立即执行每5秒一次的计划任务。（指定一个时间间隔运行，根据自己需求，可以是秒、分、时、天、月、年等。）
            Schedule<Job>().ToRunNow().AndEvery(5).Seconds();

            ////立即执行一个每个月第一个星期一18：00的计划任务
            //Schedule<DataSyncJob>().ToRunNow().AndEvery(1).Months().OnTheFirst(DayOfWeek.Monday).At(18, 0);

            ////延迟5秒执行的一次计划任务。（指定一个时间间隔运行，根据自己需求，可以是秒、分、时、天、月、年等。）
            //Schedule<DataSyncJob>().ToRunOnceIn(5).Seconds();

            ////指定时间执行计划任务（最常用，这里是在每天18：00执行。）
            //Schedule(() => Console.WriteLine("It's 18:00 now.")).ToRunEvery(1).Days().At(18, 0);

            ////在同一个计划中执行多个任务
            //Schedule<DataSyncJob>().AndThen<TestJob>().ToRunNow().AndEvery(5).Seconds();
        }
    }
}
