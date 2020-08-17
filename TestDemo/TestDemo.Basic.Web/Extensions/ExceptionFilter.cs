using Basic.Common;
using Basic.Common.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Basic.Web.Web.Extensions
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILoggerHelper _loggerHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="loggerHelper"></param>
        public ExceptionFilter(IWebHostEnvironment env, ILoggerHelper loggerHelper)
        {
            this._env = env;
            this._loggerHelper = loggerHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is BusinessException)
            {
                var result = new ObjectResult("");
                result.StatusCode = 200;
                result.Value = new HttpResult
                {
                    Success = false,
                    Msg = context.Exception.Message
                };
                context.Result = result;
            }
            //记录操作返回日志
            var json = new JsonErrorResponse();
            json.Message = context.Exception.Message;//错误信息
                                                     //if (_env.IsDevelopment())
            if (_env.IsDevelopment())
            {
                json.DevelopmentMessage = context.Exception.StackTrace;//堆栈信息
            }
            //采用log4net 进行错误日志记录
            _loggerHelper.Error(json.Message, WriteLog(json.Message, context.Exception));
            return Task.CompletedTask;
        }

        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
        }
    }
    //返回错误信息
    public class JsonErrorResponse
    {
        /// <summary>
        /// 生产环境的消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 开发环境的消息
        /// </summary>
        public string DevelopmentMessage { get; set; }
    }
}
