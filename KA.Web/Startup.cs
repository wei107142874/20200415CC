using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KA.Common;
using KA.Common.LogHelper;
using KA.Extensions;
using KA.IRepository;
using KA.IRepository.UnitOfWork;
using KA.Manager;
using KA.Repository;
using KA.Repository.UnitWork;
using KA.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KA.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment Env;
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.Env = env;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(cfg=> {
                cfg.Filters.Add<ActionFilter>();
                cfg.Filters.Add<ExceptionFilter>();
            });
            //services.AddMemoryCacheSetup();
            services.AddSqlsugarSetup();
            //services.AddDbSetup();
            //services.AddAutoMapperSetup();
            //services.AddCorsSetup();
            //services.AddMiniProfilerSetup();
            //services.AddSwaggerSetup();
            //services.AddJobSetup();
            //services.AddHttpContextSetup();
            //services.AddAuthorizationSetup();
            services.AddSingleton(new Appsettings(Env.ContentRootPath));
            services.AddSingleton(new LogLock(Env.ContentRootPath));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ArticleManager));


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

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
