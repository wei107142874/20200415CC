using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basic.Web.Extensions
{
    public static class Identity4Extend
    {
        public static void AddId4Service(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5000";//id4服务器
                    options.RequireHttpsMetadata = false;//是否启用https
                    options.Audience = "scope1"; //资源名称
                });
        }
    }
}
