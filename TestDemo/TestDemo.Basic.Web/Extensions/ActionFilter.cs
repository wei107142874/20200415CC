using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Basic.Web.Web.Extensions
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("OnActionExecuted");
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
            result.Value = httpResult;// JsonConvert.SerializeObject(httpResult);
            context.Result = result;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("OnActionExecuting");
            //throw new NotImplementedException();
        }
    }
}
