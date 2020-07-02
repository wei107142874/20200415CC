using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Common
{
    public class ThreadPoolHelp
    {
        /// <summary>
        /// 线程池的NotSupportedException异常
        /// </summary>
        public static void NotSupportedException()
        {
            try
            {
                for (int i = 0; i < 100000; i++)
                {
                   bool isSuccess=  ThreadPool.QueueUserWorkItem((obj) =>
                    {                       
                        Console.WriteLine(obj);
                    },i);

                    if(!isSuccess)
                    {
                        Console.WriteLine($"线程{i}执行失败");
                    }
                    else
                    {
                        Console.WriteLine($"线程{i}执行成功");
                    }
                }
            }
            catch (NotSupportedException e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
