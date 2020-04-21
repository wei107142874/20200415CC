using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class LoginData 
    {
        public string UserName { get; set; }

        public string Pwd { get; set; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public string Login(string username, string pwd)
        {
            if (username == "admin" && pwd == "123")
                return "ok";
            else
                return "error";
        }

        [HttpPost]
        public ActionResult Login2(LoginData data)
        {
            if (data.UserName == "admin" && data.Pwd == "123")
                return Json(data);
            else
                return Json("请求错误");
        }


        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            string userName = Request.Headers["username"];
            string pwd = Request.Headers["pwd"];
            if (userName == "admin" && pwd == "123")
            {
                file.SaveAs(Server.MapPath("~/" + file.FileName));
                return Json("ok");
            }
            else
                return Json("请求错误");
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