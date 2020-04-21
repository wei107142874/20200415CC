using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using static TestConsole.XmlHelper;

namespace TestConsole
{
    class Program
    {

        private static int TaskMethod(string name, int seconds, CancellationToken token)
        {
            Console.WriteLine("Task {0} 运行在线程 {1} 上。是否是线程池线程: {2}",
            name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            for (int i = 0; i < seconds; i++)
            {
                Thread.Sleep(1000);
                if (token.IsCancellationRequested) return -1;
            }
            return 42 * seconds;
        }

        static void Main(string[] args)
        {

            var cts = new CancellationTokenSource();
            var longTask = new Task<int>(() => TaskMethod("Task 2", 10, cts.Token), cts.Token);
            longTask.Start(); //启动任务
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine(longTask.Status);
            }
            cts.Cancel();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine(longTask.Status);
            }

            Console.WriteLine("A task has been completed with result {0}.", longTask.Result);
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
