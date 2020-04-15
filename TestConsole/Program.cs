using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static TestConsole.XmlHelper;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlFilePath = AppDomain.CurrentDomain.BaseDirectory + "1.xml";

            XmlHelper xml = new XmlHelper(xmlFilePath);
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            pairs["count1"] = "5";
            pairs["count3"] = "8";
            //xml.Insert(new InsertDto
            //{
            //    Name = "Name",
            //    Text = "231",
            //    Xpath = "User/Student",
            //    AttrDic = pairs
            //});

            xml.Update(new UpdateDto
            {
                Text = "zz1",
                HandleAttrOrText = "attr=count=1",
                Xpath = "User/Student/Name",
                AttrDic = pairs
            });

            //var xml = new System.Xml.XmlDocument();
            //XmlDeclaration declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", "");//xml文档的声明部分           
            //xml.AppendChild(declaration);//添加至XmlDocument对象中
            //XmlElement User = xml.CreateElement("User");//创建根节点User
            //XmlNode Student = xml.CreateElement("Student");//创建子节点ID
            //XmlElement ID = xml.CreateElement("ID");//创建子节点元素
            //ID.InnerText = "123";
            //User.AppendChild(Student);//子节点
            //Student.AppendChild(ID);//子节点元素
            //xml.AppendChild(User);//根目录User,有且只有一个
            //xml.Save(xmlFilePath);

            //插入元素
            //var xml = new System.Xml.XmlDocument();
            //xml.Load(xmlFilePath);
            //XmlNode nodeparams = xml.SelectSingleNode("User/Student");//找根节点
            //XmlElement Name = xml.CreateElement("Name");
            //Name.InnerText = "猪猪侠";
            //Name.SetAttribute("count", "1");
            //nodeparams.AppendChild(Name); //New Node
            //xml.Save(xmlFilePath);//修改完成后保存

            //修改元素
            //var xml = new System.Xml.XmlDocument();
            //xml.Load(xmlFilePath);
            //XmlNode nodeparams = xml.SelectSingleNode("User/Student/Name");//找根节点
            //if (nodeparams != null)
            //{
            //    nodeparams.InnerText = "朱大哥";
            //    xml.Save(xmlFilePath);//修改完成后保存
            //    //XmlNode nodeparams2 = nodeparams.SelectSingleNode("Student");//子节点
            //    //if (nodeparams2 != null)
            //    //{
            //    //    XmlNodeList nodelist = nodeparams2.ChildNodes;
            //    //    if (nodelist != null)
            //    //    {
            //    //        XmlNode ID = nodelist[0];
            //    //        ID.InnerText = "2221";
            //    //        xml.Save(xmlFilePath);//修改完成后保存
            //    //    }
            //    //}
            //}
            //else
            //{
            //    Console.WriteLine("no element");
            //}

            //删除元素
            //var xml = new System.Xml.XmlDocument();
            //xml.Load(xmlFilePath);
            //XmlNode nodeparams = xml.SelectSingleNode("User/Student");
            //XmlNode Name = nodeparams.SelectSingleNode("Name");
            //nodeparams.RemoveChild(Name); //New Node
            //xml.Save(xmlFilePath);//修改完成后保存

            Console.WriteLine("ok");
            Console.ReadKey();
        }

        public class MenuTreeViewModel
        {
            public Hashtable item { set; get; }

            public List<MenuTreeViewModel> children { set; get; }
        }

        public static List<MenuTreeViewModel> GetMenuTree2(List<Hashtable> list, int pid)
        {
            Func<int, List<MenuTreeViewModel>> func = null;
            func = new Func<int, List<MenuTreeViewModel>>(m => {
                List<MenuTreeViewModel> t = new List<MenuTreeViewModel>();
                foreach (var item in list.Where(h => h["pid"].ToString() == m.ToString()))
                {
                    var childs = func(Convert.ToInt32(item["id"]));
                    t.Add(new MenuTreeViewModel()
                    {
                        item = item,
                        children = childs
                    });
                }
                return t;
            });
            return func(-1);
        }

        public static List<MenuTreeViewModel> GetMenuTree1(List<Hashtable> list, int pid)
        {
            List<MenuTreeViewModel> tree = new List<MenuTreeViewModel>();
            var children = list.Where(m => m["pid"].ToString() == pid.ToString()).ToList();
            if (children.Count > 0)
            {
                for (var i = 0; i < children.Count; i++)
                {
                    MenuTreeViewModel itemMenu = new MenuTreeViewModel();
                    itemMenu.item = children[i];
                    itemMenu.children = GetMenuTree1(list, Convert.ToInt32(children[i]["id"]));
                    tree.Add(itemMenu);
                }
            }
            return tree;
        }

        public static List<Hashtable> InitData()
        {
            List<Hashtable> listMenu = new List<Hashtable>();
            Hashtable ht1 = new Hashtable();
            ht1.Add("id", 1);
            ht1.Add("pid", -1);
            ht1.Add("url", "/");
            ht1.Add("name", "首页");
            listMenu.Add(ht1);

            Hashtable ht2 = new Hashtable();
            ht2.Add("id", 2);
            ht2.Add("pid", -1);
            ht2.Add("url", "/news");
            ht2.Add("name", "资讯");
            listMenu.Add(ht2);

            Hashtable ht3 = new Hashtable();
            ht3.Add("id", 3);
            ht3.Add("pid", 2);
            ht3.Add("url", "/news/hot");
            ht3.Add("name", "热点");
            listMenu.Add(ht3);

            Hashtable ht4 = new Hashtable();
            ht4.Add("id", 4);
            ht4.Add("pid", 2);
            ht4.Add("url", "/news/latest");
            ht4.Add("name", "滚动新闻");
            listMenu.Add(ht4);

            Hashtable ht5 = new Hashtable();
            ht5.Add("id", 5);
            ht5.Add("pid", 4);
            ht5.Add("url", "/news/latest/international");
            ht5.Add("name", "国际快讯");
            listMenu.Add(ht5);

            Hashtable ht6 = new Hashtable();
            ht6.Add("id", 6);
            ht6.Add("pid", -1);
            ht6.Add("url", "/domain");
            ht6.Add("name", "行业");
            listMenu.Add(ht6);

            Hashtable ht7 = new Hashtable();
            ht7.Add("id", 7);
            ht7.Add("pid", 5);
            ht7.Add("url", "/news/latest/international/politics");
            ht7.Add("name", "政治");
            listMenu.Add(ht7);

            Hashtable ht8 = new Hashtable();
            ht8.Add("id", 8);
            ht8.Add("pid", 5);
            ht8.Add("url", "/news/latest/international/military");
            ht8.Add("name", "军事");
            listMenu.Add(ht8);

            return listMenu;
        }

        
    }
}
