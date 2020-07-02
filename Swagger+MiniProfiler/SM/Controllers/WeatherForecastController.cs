using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling;

namespace SM.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            IEnumerable<WeatherForecast> res;
            using (MiniProfiler.Current.Step("Get方法"))
            {
                Console.WriteLine("123");
                using (MiniProfiler.Current.Step("准备数据"))
                {
                    using (MiniProfiler.Current.CustomTiming("SQL", "SELECT * FROM Config"))
                    {
                        var rng = new Random();
                         res= Enumerable.Range(1, 5).Select(index => new WeatherForecast
                        {
                            Date = DateTime.Now.AddDays(index),
                            TemperatureC = rng.Next(-20, 55),
                            Summary = Summaries[rng.Next(Summaries.Length)]
                        })
                        .ToArray();
                    }
                }
            }

            using (MiniProfiler.Current.Step("使用从数据库中查询的数据，进行Http请求"))
            {
                using (MiniProfiler.Current.CustomTiming("HTTP", "GET " + "https://www.cnblogs.com/sylone/p/11024386.html"))
                {
                    var client = new WebClient();
                    var reply = client.DownloadString("https://www.cnblogs.com/sylone/p/11024386.html");
                }

                using (MiniProfiler.Current.CustomTiming("HTTP", "GET " + "https://www.cnblogs.com/sylone/p/11024386.html"))
                {
                    var client = new WebClient();
                    var reply = client.DownloadString("https://www.cnblogs.com/sylone/p/11024386.html");
                }
            }
            return res;
        }
    }
}
