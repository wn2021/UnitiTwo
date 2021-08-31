using Business;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnitiTwo.Controllers
{
    [Authentication]
    public class ContactController : Controller
    {
        private static ContactRepository contactRepository = new ContactRepository();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(contactRepository.Get(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Contact contact)
        {
            contactRepository.Put(contact);
            return Json(contactRepository.Get());
        }

        [HttpPost]
        public JsonResult Update(Contact contact)
        {
            contactRepository.Post(contact);
            return Json(contactRepository.Get());
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            var contact = contactRepository.Get(id);
            contactRepository.Delete(id);
            return Json(contactRepository.Get());
        }


        public ActionResult ArticleList()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetProducts()
        {
            List<Product> lst = new List<Product>();
            Product pro = new Product();
            pro.pro_name = "【斯文】甘油 | 丙三醇";
            pro.pro_brand = "skc";
            pro.pro_place = "韩国";
            pro.pro_purity = "99.7%";
            pro.pro_min = "215千克";
            pro.pro_depot = "上海仓海仓储";
            pro.pro_num = 1;
            //pro.pro_img = AppDomain.CurrentDomain.BaseDirectory+"Content\\vue\\images\\testimg.jpg";
            string url = Request.Url.ToString();
            url = url.Substring(0, url.IndexOf("Contact"));
            pro.pro_img = url+@"Content/vue/images/testimg.jpg";
            pro.pro_price = 800;
            lst.Add(pro);
            lst.Add(pro);
            lst.Add(pro);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Settlement(string proList)
        {
            List<Product> lst = JsonConvert.DeserializeObject<List<Product>>(proList);
            return Json(new { @return = 1 }, JsonRequestBehavior.AllowGet);
        }
    }
}