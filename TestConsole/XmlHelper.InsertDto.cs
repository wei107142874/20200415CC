using System.Collections.Generic;
namespace TestConsole
{
    public partial class XmlHelper
    {
        public class InsertDto
        {
            /// <summary>
            /// root/persons/student
            /// </summary>
            public string Xpath { get; set; }

            /// <summary>
            /// 创建具有指定名称的元素
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 设置元素的文本值
            /// </summary>
            public string Text { get; set; }

            /// <summary>
            /// 元素的属性
            /// </summary>
            public Dictionary<string, string> AttrDic { get; set; }
        }

        public class UpdateDto
        {

            /// <summary>
            /// InnerText
            /// </summary>
            public string Text { get; internal set; }

            /// <summary>
            /// root/persons/student
            /// </summary>
            public string Xpath { get; internal set; }

            /// <summary>
            /// 实际需要更新的属性名(确定唯一性)
            /// attr=Name=zs
            /// text=zs
            /// </summary>
            public string HandleAttrOrText { get; set; }

            /// <summary>
            /// 元素的属性
            /// </summary>
            public Dictionary<string, string> AttrDic { get; set; }
        }

        public class DeleteDto
        {
            /// <summary>
            /// root/persons/student
            /// </summary>
            public string Xpath { get; internal set; }

            /// <summary>
            /// 实际需要更新的属性名(确定唯一性)
            /// attr=Name=zs
            /// text=zs
            /// </summary>
            public string HandleAttrOrText { get; set; }
        }
    }
}
