using System;

namespace KA.Common
{
    /// <summary>
    /// 业务异常
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string message):base(message)
        {
        }
    }
}
