using Basic.Common.Log;
using Basic.Common.Seesion;
using Basic.Common.Web.Extensions;
using Basic.IRepository;
using Basic.Repository;
using Basic.Service;
using Basic.Web.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Basic.Web
{
    public class Startup
    {
        public Startup()
        {
            LogHelper.Init();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.DefaultService();
            //services.AddAuthentication("Bearer").AddJwtBearer()
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.DefaultConfigure(env);
        }
    }
}
