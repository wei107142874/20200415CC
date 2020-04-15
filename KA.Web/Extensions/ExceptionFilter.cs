using KA.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KA.Web.Extensions
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
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
                //记录操作返回日志
            }
            else
            {
                //记录异常日志
            }
            return Task.CompletedTask;
        }
    }
}
