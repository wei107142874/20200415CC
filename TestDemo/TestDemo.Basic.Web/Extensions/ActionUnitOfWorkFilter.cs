using Basic.IRepository;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Basic.Web.Web.Extensions
{
    public class ActionUnitOfWorkFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActionUnitOfWorkFilter(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await _unitOfWork.BeginTran();
            Console.WriteLine("开启事务成功");
            ActionExecutedContext executedContext= await next.Invoke();
            if (executedContext.Exception ==null)
            {
                await _unitOfWork.Commit();
                Console.WriteLine("提交事务成功");
            }
            else
            {
                await _unitOfWork.RollBack();
                Console.WriteLine("回滚事务成功");
            }
        }
    }
}
