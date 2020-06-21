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
            //Repository = LogManager.CreateRepository("");//��Ҫ��ȡ��־�Ĳֿ�����Ҳ������ĵ�Ȼ��Ŀ��

            ////ָ�������ļ�������������������⣬Ӧ����ʹ����InProcessģʽ����鿴Blog.Core.csproj,��ɾ֮ 
            //XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));//�����ļ�
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
            //log��־ע��
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
