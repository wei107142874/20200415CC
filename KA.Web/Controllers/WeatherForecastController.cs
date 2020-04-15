using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KA.Common;
using KA.Manager;
using KA.Web.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KA.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ArticleManager _articleManager;
        private readonly IWebHostEnvironment _environment;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ArticleManager articleManager,
            IWebHostEnvironment environment
            )
        {
            _logger = logger;
            this._articleManager = articleManager;
            this._environment = environment;
        }

        [HttpGet]
        public void Get()
        {
            //业务级错误
            //throw new BusinessException("密码输入错误");
            //return "";
            //系统级错误
            //string a = "大哥你好";
            //a.ToString();
            //return a;

            //return new { age = 19,Name="大哥" };
            //var filePath = "/新建文本文档.txt";
            ////var fileName = "项目原型.docx";
            //FileStream fs = new FileStream(_environment.WebRootPath + filePath, FileMode.OpenOrCreate);
            ////fs.Close();
            //var fe= new FileDownload(fs, "application/octet-stream", "我也不知道.txt");
            //return fe;
            ////return File(new FileStream(_environment.WebRootPath + filePath, FileMode.Open), "application/octet-stream", fileName);
            //return "年末的";
            //var rng = new Random();
            // Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}
