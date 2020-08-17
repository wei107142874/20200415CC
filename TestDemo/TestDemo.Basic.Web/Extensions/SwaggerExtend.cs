using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Basic.Web.Web.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerExtend
    {
        /// <summary>
        /// Swagger服务注册
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerGenService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "魏勇的Swagger",
                    Description = "基于.NET Core 3.1 的服务",
                    Contact = new OpenApiContact
                    {
                        Name = "魏勇",
                        Email = "1071427874@qq.com",
                        Url = new Uri("http://cnblogs.com/microfisher"),
                    },
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                // 加载程序集的xml描述文档
                var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                var xmlFile = AppDomain.CurrentDomain.FriendlyName + ".xml";
                var xmlPath = Path.Combine(baseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// swagger使用
        /// </summary>
        /// <param name="app"></param>
        public static void SwaggerGenConfigure(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "魏勇的Swagger");
                // 访问Swagger的路由后缀
                c.RoutePrefix = "swagger";
            });
        }
    }
}
