using Serilog;
using Serilog.Events;
using System.IO;

namespace Common
{
    public class LogSerilog
    {
        private static int i = 0;

        /// <summary>
        /// 应用程序第一次加载时调用
        /// </summary>
        public static void Init()
        {
            if (i==1)
                return;
            string p = Path.Combine("logs", @"log.txt");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(p, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            i++;
        }
    }
}
