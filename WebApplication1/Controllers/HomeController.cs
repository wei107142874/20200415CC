using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FF()
        {
            string age = Request.Form["Age"];
            string fileName = "1.txt";//客户端保存的文件名
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "/Content/1.txt";
            return File(new FileStream(filePath, FileMode.Open), "text/plain",
            fileName);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}