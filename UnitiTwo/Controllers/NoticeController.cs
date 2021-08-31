using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitiTwo.Models;

namespace UnitiTwo.Controllers
{
    [Authentication]
    public class NoticeController : Controller
    {
        // GET: Notice
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NoticePanel()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NoticeView(int? id)
        {
            Notice nc = new Notice();
            nc.Id = (int)id;
            nc.Title = "关于武汉市商品房网上签约和合同备案系统、武汉市房地产经纪服务平台维护的通知";
            nc.Content = "关于武汉市商品房网上签约和合同备案系统、武汉市房地产经纪服务平台维护的通知,关于武汉市商品房网上签约和合同备案系统、武汉市房地产经纪服务平台维护的通知,关于武汉市商品房网上签约和合同备案系统、武汉市房地产经纪服务平台维护的通知,关于武汉市商品房网上签约和合同备案系统、武汉市房地产经纪服务平台维护的通知";
            nc.Public_Date = DateTime.Parse("2020-02-01");
            return View(nc);
        }
    }
}