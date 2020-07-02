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

            //ע��������repository��β����
            services.Scan(scan => scan
                .FromAssemblyOf<Startup>()  //��ʾ����Startup��������ڵĳ���
                    //��ʾҪע����Щ�࣬����Ĵ��뻹���˹��ˣ�ֻ�������� repository ��β����
                    .AddClasses(classes => classes.Where(t => t.Name.EndsWith("repository", StringComparison.OrdinalIgnoreCase)))
                    .AsImplementedInterfaces() // ��ʾ������ע��Ϊ�ṩ�����й����ӿ���Ϊ����
                    .WithTransientLifetime() //ע�����������Transient
                );

            //��ֹ�ظ�ע��
            services.Scan(scan => scan
                .FromAssemblyOf<Startup>()
                    .AddClasses(classes => classes.AssignableTo<IDuplicate>())
                        //�ֶ�����
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)  //Skip Append Replace
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
            );

            //services.Scan(scan => scan
            //.AddTypes(typeof(MyClass)) //��һ���㱼����
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
