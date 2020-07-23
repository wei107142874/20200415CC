using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCollect.Web.Dto
{
    public class ArticleDto
    {
    }

    public class InsertArticle
    {
        public string Title { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// 正文类型，1：markdown 2:html 3:纯文本
        /// </summary>
        public int Content_Type { get; set; }
    }

    public class UpdateArticle:InsertArticle
    {
        public long Id { get; set; }
    }
}
