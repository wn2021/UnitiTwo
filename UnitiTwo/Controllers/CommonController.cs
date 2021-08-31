using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Model;
using Business;
using Common;

namespace UnitiTwo.Controllers
{
    [Authentication]
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Display(Description = "加载系统配置信息")]
        public ActionResult GetSystemInfoList(string sysid)
        {
            List<V_Systeminfo> lst = new BusinessBase().GetSystemInfo(sysid);
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Display(Description = "加载系统配置信息")]
        public ActionResult GetSystemInfo(string sysid,string syssubid)
        {
            List<V_Systeminfo> lst = new BusinessBase().GetSystemInfo(sysid, syssubid);
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Display(Description = "加载系统配置信息")]
        public ActionResult GetCmbOptions(string sysid)
        {
            List<V_Systeminfo> lst = new BusinessBase().GetSystemInfo(sysid);
            List<Combobox_Option> lst2 = new List<Combobox_Option>();
            Combobox_Option op = null;
            foreach (V_Systeminfo info in lst) {
                op = new Combobox_Option();
                op.id = info.sysvalue;
                op.text = info.description;
                lst2.Add(op);
            }
            return Json(new { data = lst2 }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Display(Description = "加载系统配置信息")]
        public ActionResult GetCompanys() {
            List<OrganizationSelect> lst = new BusinessBase().GetOrganizations(OrganizationType.Company, string.Empty,string.Empty);
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Display(Description = "加载系统配置信息")]
        public ActionResult GetDepartments(string companyid)
        {
            List<OrganizationSelect> lst = new BusinessBase().GetOrganizations(OrganizationType.Department, companyid, string.Empty);
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Display(Description = "加载系统配置信息")]
        public ActionResult GetLevels(string companyid,string depid)
        {
            List<OrganizationSelect> lst = new BusinessBase().GetOrganizations(OrganizationType.PositionLevel, companyid, depid);
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Display(Description = "加载系统配置信息")]
        public ActionResult GetMonths(int? intYear)
        {
            if (intYear == null) { intYear = DateTime.Today.Year; }
            List<V_Systeminfo> lst = new List<V_Systeminfo>();
            V_Systeminfo fs = null;
            if(intYear== DateTime.Today.Year)
            {
                DateTime threeMonthPre = DateTime.Today.AddMonths(-3);
                while (threeMonthPre.Year != intYear) {
                    fs = new V_Systeminfo();
                    fs.sysvalue = threeMonthPre.Year.ToString() + "-" + threeMonthPre.Month.ToString().PadLeft(2, '0');
                    fs.description = fs.sysvalue;
                    lst.Add(fs);
                    threeMonthPre = threeMonthPre.AddMonths(1);
                }                
            }
            for (int i = 1; i < 13; i++) {
                fs = new V_Systeminfo();
                fs.sysvalue = intYear.ToString() + "-" + i.ToString().PadLeft(2,'0');
                fs.description = fs.sysvalue;
                lst.Add(fs);
            }
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Display(Description = "打开员工选择画面")]
        public ActionResult SelectEmployee(string companyId,string departmentId) {
            EmployeeSearch view = new EmployeeSearch();
            view.companyId = companyId;
            view.departmentId = departmentId;
            return View(view);
        }
    }
}