using Business;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnitiTwo.Controllers
{
    [Authentication]
    public class SolutionController : Controller
    {
        // GET: Solution
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SolutionList()
        {
            return View();
        }

        [HttpPost]
        [Display(Description = "取得解决方案列表")]
        public ActionResult GetSolutionList(int? pageIndex, int? pageSize, string key, string status)
        {
            List<V_Solution> lst = new List<V_Solution>();
            BLSolution bl = new BLSolution();
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 20;
            lst = bl.GetSolutionList(key, status);
            var total = lst.Count;
            var list = lst.OrderBy(d => d.solution_id).Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}