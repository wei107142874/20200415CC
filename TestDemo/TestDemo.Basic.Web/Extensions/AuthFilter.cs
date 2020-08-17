using Basic.Common;
using Basic.Common.Seesion;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basic.Common.Web.Extensions
{
    public class AuthFilter : IAsyncAuthorizationFilter
    {
        private readonly AppSession _appSession;

        public AuthFilter(AppSession appSession)
        {
            this._appSession = appSession;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            Console.WriteLine("OnAuthorizationAsync");
            if (_appSession.UserId != null)
            {
                _appSession.UserId = _appSession.UserId + 1;
            }
            else
            {
                _appSession.UserId = 1;
            }
            return Task.CompletedTask;
        }
    }
}
