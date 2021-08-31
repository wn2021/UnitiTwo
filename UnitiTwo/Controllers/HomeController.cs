using Business;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UnitiTwo.Models;

namespace UnitiTwo.Controllers
{
    
    public class HomeController : Controller
    {
        [Authentication]
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Index2()
        {
            return View();
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
        public ActionResult Login()
        {
            FormsAuthentication.SetAuthCookie("", false);//将用户名放入Cookie中
            System.Web.HttpContext.Current.Session.Clear();
            
            return View();
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetUserMenus() {
            
           string userId = (System.Web.HttpContext.Current.Session["usernumber"] == null) ? "system" : System.Web.HttpContext.Current.Session["usernumber"].ToString();
            List < V_Menu1Zero >  lst = new BLMenu().GetUserMenus(userId);
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }
    }
    
}