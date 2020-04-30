using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MsgService
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IHostApplicationLifetime hostApplicationLifetime)
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


            string ip = Configuration["ip"];
            string port = Configuration["port"]; 
            string serviceName = "MsgService"; //��������
            string serviceId = serviceName + Guid.NewGuid();
            using (var consulCilent = new ConsulClient(ConsulConfig))
            {
                AgentServiceRegistration asr= new AgentServiceRegistration();
                asr.Address = ip;
                asr.Port = Convert.ToInt32(port);
                asr.ID = serviceId;
                asr.Name = serviceName;
                asr.Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//����ֹͣ��ú�ע��
                    HTTP = $"http://{ip}:{port}/api/health",
                    Interval = TimeSpan.FromSeconds(5),//�������ʱ���������߳�Ϊ�������
                    Timeout = TimeSpan.FromSeconds(5)
                };
                asr.Tags = new string[] { "" };

                consulCilent.Agent.ServiceRegister(asr).Wait(); //����ע��

                hostApplicationLifetime.ApplicationStopped.Register(() => {
                    using (var consulCilent = new ConsulClient(ConsulConfig))
                    {
                        Console.WriteLine("Ӧ���˳���ʼ��consulע��");
                        consulCilent.Agent.ServiceDeregister(serviceId).Wait();
                    }
                });
            }

           
        }

        private void ConsulConfig(ConsulClientConfiguration  configuration)
        {
            
            configuration.Address = new Uri("http://127.0.0.1:8500");//consul url��ַ��Ĭ��8500�˿�
            configuration.Datacenter = "consul";
        }
    }
}
