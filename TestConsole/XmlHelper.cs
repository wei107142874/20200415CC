using System;
using System.Xml;
namespace TestConsole
{
    public partial class XmlHelper
    {
        XmlDocument xml = new System.Xml.XmlDocument();
        public XmlHelper(string xmlFilePath)
        {
            //xml = new XmlDocument();
            this._xmlFilePath = xmlFilePath;
            xml.Load(xmlFilePath);
        }
        private string _xmlFilePath;

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="xpath">"root/persons/student"</param>
        /// <param name="name">创建具有指定名称的元素</param>
        /// <param name="text">设置元素的文本值</param>
        /// <param name="attrDic">设置元素的属性</param>
        public void Insert(InsertDto dto)
        {
            XmlNode nodeparams = xml.SelectSingleNode(dto.Xpath);
            XmlElement Name = xml.CreateElement(dto.Name);
            if (dto.Text != null)//赋文本
                Name.InnerText = dto.Text;
            if (dto.AttrDic != null) //赋属性
            {
                foreach (var key in dto.AttrDic.Keys)
                {
                    Name.SetAttribute(key, dto.AttrDic[key]);
                }
            }

           
            nodeparams.AppendChild(Name); //New Node
            xml.Save(_xmlFilePath);//修改完成后保存
        }

       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="dto"></param>
        public void Update(UpdateDto dto)
        {
            //修改元素
            //var xml = new System.Xml.XmlDocument();
            //xml.Load(xmlFilePath);
            XmlNodeList nodeparams = xml.SelectNodes(dto.Xpath);//找根节点
            if (nodeparams != null)
            {
                foreach (XmlElement node in nodeparams)
                {
                    
                    string[] sjattr = dto.HandleAttrOrText.Split('=');
                    if (sjattr.Length > 0)
                    {
                        //属性
                        if (dto.AttrDic != null && sjattr[0] == "attr")
                        {
                            if (node.GetAttribute(sjattr[1]) == sjattr[2])
                            {
                                foreach (string key in dto.AttrDic.Keys)
                                {
                                    node.SetAttribute(key, dto.AttrDic[key]);
                                }
                            }
                            //else
                            //{
                            //    foreach (string key in dto.AttrDic.Keys)
                            //    {
                            //        node.SetAttribute(key, dto.AttrDic[key]);
                            //    }
                            //}
                        }
                        //值
                        if (sjattr[0] == "text" && !string.IsNullOrEmpty(dto.Text))
                        {
                            if (node.InnerText == sjattr[1])
                            {
                                node.InnerText = dto.Text;
                            }
                        }
                    }
                    else
                    {
                        if (dto.AttrDic != null)
                        {
                            foreach (string key in dto.AttrDic.Keys)
                            {
                                node.SetAttribute(key, dto.AttrDic[key]);
                            }
                        }
                        node.InnerText = dto.Text;
                    }
                }
                xml.Save(_xmlFilePath);//修改完成后保存
                //XmlNode nodeparams2 = nodeparams.SelectSingleNode("Student");//子节点
                //if (nodeparams2 != null)
                //{
                //    XmlNodeList nodelist = nodeparams2.ChildNodes;
                //    if (nodelist != null)
                //    {
                //        XmlNode ID = nodelist[0];
                //        ID.InnerText = "2221";
                //        xml.Save(xmlFilePath);//修改完成后保存
                //    }
                //}
            }
            else
            {
                throw  new Exception("no element");
            }
        }

        public void Delete( DeleteDto dto)
        {
            XmlNodeList nodeparams = xml.SelectNodes(dto.Xpath);
            string[] array = dto.HandleAttrOrText.Split('=');
            foreach (XmlElement node in nodeparams)
            {
                if (array.Length == 0)
                {
                    node.RemoveChild(node);
                    continue;
                }
                if (array[0] == "text" && node.InnerText == array[1])
                {
                    node.RemoveChild(node);
                }
                else if (array[0] == "attr" )
                {
                    if (node.GetAttribute(array[1]) == array[2])
                    {
                        node.RemoveChild(node);
                    }
                }
            }
            xml.Save(_xmlFilePath);//修改完成后保存
        }
    }
}
