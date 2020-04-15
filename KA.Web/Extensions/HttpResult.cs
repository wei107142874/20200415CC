namespace KA.Web.Extensions
{
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
