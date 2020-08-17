using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basic.Web.Web.Extensions
{
    /// <summary>
    /// 网络数据返回格式
    /// </summary>
    public class HttpResult
    {
        /// <summary>
        /// 程序只要无异常就为true
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 成功时返回的数据
        /// </summary>
        public object Data { get; set; }
    }
}
