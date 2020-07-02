using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Scrutor;

namespace ScrutorUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //注册所有以repository结尾的类
            services.Scan(scan => scan
                .FromAssemblyOf<Startup>()  //表示加载Startup这个类所在的程序集
                    //表示要注册那些类，上面的代码还做了过滤，只留下了以 repository 结尾的类
                    .AddClasses(classes => classes.Where(t => t.Name.EndsWith("repository", StringComparison.OrdinalIgnoreCase)))
                    .AsImplementedInterfaces() // 表示将类型注册为提供其所有公共接口作为服务
                    .WithTransientLifetime() //注册的生命周期Transient
                );

            //防止重复注册
            services.Scan(scan => scan
                .FromAssemblyOf<Startup>()
                    .AddClasses(classes => classes.AssignableTo<IDuplicate>())
                        //手动高亮
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)  //Skip Append Replace
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
            );

            //services.Scan(scan => scan
            //.AddTypes(typeof(MyClass)) //单一的裸奔的类
            //    .AsSelf()
            //    .WithSingletonLifetime()
            //);
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
    public interface IUserService { }
    public class UserService : IUserService { }

    public interface IUserRepository { }
    public class UserRepository : IUserRepository { }

    public interface IProductRepository { }
    public class ProductRepository : IProductRepository { }

    public interface IDuplicate { }
    public class FirstDuplicate : IDuplicate { }
    public class SecondDuplicate : IDuplicate { }
}
