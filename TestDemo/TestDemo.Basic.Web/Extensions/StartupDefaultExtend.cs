using Basic.Common.Log;
using Basic.Common.Seesion;
using Basic.IRepository;
using Basic.Repository;
using Basic.Service;
using Basic.Web.Extensions;
using Basic.Web.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basic.Common.Web.Extensions
{
    public static class StartupDefaultExtend
    {
        public static void DefaultService(this IServiceCollection services)
        {
            services.AddControllers(cfg => {
                cfg.Filters.Add<AuthFilter>();
                cfg.Filters.Add<ActionFilter>();
                cfg.Filters.Add<ActionUnitOfWorkFilter>();
                cfg.Filters.Add<ExceptionFilter>();
            });
            services.AddId4Service();
            services.AddSwaggerGenService();
            services.AddDbContext<Basic.Entites.EFDbContext>();
            services.AddScoped(typeof(AppSession));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(UserService));
            //log日志注入
            services.AddSingleton<ILoggerHelper, LogHelper>();
        }

        public static void DefaultConfigure(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.SwaggerGenConfigure();
            }
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
