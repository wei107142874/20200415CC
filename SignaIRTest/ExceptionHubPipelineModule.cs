using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignaIRTest
{
    public class ExceptionHubPipelineModule: HubPipelineModule
    {
        protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
        {
            //异常信息
            //exceptionContext.Error;

            dynamic caller = invokerContext.Hub.Clients.Caller;
            caller.onException("发送错误");
            base.OnIncomingError(exceptionContext, invokerContext);
        }
    }
}