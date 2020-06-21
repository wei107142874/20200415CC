using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;
using log4net;
using log4net.Config;
using log4net.Repository;
using LogWeb.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LogWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //log4net
            //Repository = LogManager.CreateRepository("");//需要获取日志的仓库名，也就是你的当然项目名

            ////指定配置文件，如果这里你遇到问题，应该是使用了InProcess模式，请查看Blog.Core.csproj,并删之 
            //XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));//配置文件
            Log4Helper.Init();
            LogSerilog.Init();
        }

       

      

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(o => {
                o.Filters.Add(o.Filters.Add(typeof(GlobalExceptionsFilter)));
            }).SetCompatibilityVersion(CompatibilityVersion.Latest); ;
            services.AddControllers();
            //log日志注入
            services.AddSingleton<ILog4rHelper, Log4Helper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
