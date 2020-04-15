using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KA.Manager.IDQ
{
    public class ArticleDtoAdd
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public long? CategoryId { get; set; }
    }
}
