using KA.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace KA.Manager.Entity
{
    public class Article:BaseEntity
    {
        public string Text { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public long? CategoryId { get; set; }
    }
}
