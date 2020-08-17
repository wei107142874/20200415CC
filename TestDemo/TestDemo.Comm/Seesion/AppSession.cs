using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace Basic.Common.Seesion
{
    /// <summary>
    /// Defines some session information that can be useful for applications.
    /// 定义一些对应用程序有用的会话信息
    /// </summary>
    public class AppSession
    {
       public  long? UserId { get; set; }
    }
}
