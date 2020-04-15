using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KA.Web.Extensions
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is FileStreamResult)
            {
                return;
            }
            var result = context.Result as ObjectResult;

            var httpResult = new HttpResult
            {
                Data = result?.Value,
                Success = true
            };
            if (result == null)
            {
                result = new ObjectResult("");
                result.StatusCode = 200;
            }
            result.Value = JsonConvert.SerializeObject(httpResult);
            context.Result = result;
        }   

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }

        //public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
           
        //}
    }
}
