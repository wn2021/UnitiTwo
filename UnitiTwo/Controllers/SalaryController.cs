using Business;
using Common;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnitiTwo.Controllers
{
    [Authentication]
    public class SalaryController : Controller
    {
        // GET: Salary
        public ActionResult Index()
        {
            return View();
        }
        #region 薪资管理画面
        [HttpGet]
        [Display(Description = "薪资管理画面初始化")]
        public ActionResult SalaryPage()
        {
            return View();
        }

        [HttpPost]
        [Display(Description = "取得职级薪资水平")]
        public ActionResult PositionSalary(int? pageIndex, int? pageSize,SalarySearch condition) {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 20;
            if (condition == null) { condition = new SalarySearch(); }
            List<V_Level_Salary> lst = new BLSalary().GetLevelSalary(condition);
            if (lst == null) { lst = new List<V_Level_Salary>(); }
            var total = lst.Count;
            var list = lst.Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Display(Description = "取得员工当前薪资水平")]
        public ActionResult EmployeeSalary(int? pageIndex, int? pageSize, SalarySearch condition)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 20;
            if (condition == null) { condition = new SalarySearch(); }
            condition.currentDate = DateTime.Today;
            List<V_Employee_Salary> lst = new BLSalary().GetEmpSalary(condition);
            if (lst == null) { lst = new List<V_Employee_Salary>(); }
            var total = lst.Count;
            var list = lst.Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "保存级别薪资水平")]
        public ActionResult SaveLevelSalary(string data)
        {
            ResponseResult result = new ResponseResult();
            if (data == null)
            {
                result.success = false;
                result.message = "没有需要保存的数据。";
            }
            else
            {
                List<V_Level_Salary> lst = JsonConvert.DeserializeObject<List<V_Level_Salary>>(data);
                int intResult = new BLSalary().SaveLevelSalary(lst, "admin");
                if (intResult > 0)
                {
                    result.success = true;
                    result.message = "保存成功。";
                }
                else
                {
                    result.success = false;
                    result.message = "保存失败。";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Display(Description = "保存员工薪资水平")]
        public ActionResult SaveEmpSalary(string data)
        {
            ResponseResult result = new ResponseResult();
            if (data == null)
            {
                result.success = false;
                result.message = "没有需要保存的数据。";
            }
            else
            {
                List<V_Employee_Salary> lst = JsonConvert.DeserializeObject<List<V_Employee_Salary>>(data);
                int intResult = new BLSalary().SaveEmpSalary(lst, "admin");
                if (intResult > 0)
                {
                    result.success = true;
                    result.message = "保存成功。";
                }
                else
                {
                    result.success = false;
                    result.message = "保存失败。";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion 薪资管理画面


        #region 薪资查询画面
        [HttpGet]
        [Display(Description = "薪资查询画面初始化")]
        public ActionResult SalarySearch(string historyflag)
        {
            Search_View_Type vw = new Search_View_Type();
            //区分历史查询还是带发放薪资查询
            vw.historyflag = historyflag;
            return View(vw);
        }

        [HttpPost]
        [Display(Description = "员工工资查询")]
        public ActionResult QuerySalaries(int? pageIndex, int? pageSize, SalarySearchTwo condition,string historyflag)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 20;
            bool isHistory = (historyflag == "1") ? true : false;
            if (condition == null) { condition = new SalarySearchTwo(); }
            List<V_Month_Salary> lst = new BLSalary().GetEmployeeSalaries(condition, isHistory);
            if (lst == null) { lst = new List<V_Month_Salary>(); }
            var total = lst.Count;
            var list = lst.Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "计算月度工资")]
        public ActionResult CalculateSalary(string strYmonth)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                new BLSalary().CalculateSalary(strYmonth);
                result.success = true;
            }
            catch
            {
                result.success = false;
                result.message = "考勤收集失败！";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "设置员工工资已发放")]
        public ActionResult SetDeliveried(string data)
        {
            ResponseResult result = new ResponseResult();
            if (data == null)
            {
                result.success = false;
                result.message = "没有需要保存的数据。";
            }
            else
            {
                List<V_Month_Salary> lst = JsonConvert.DeserializeObject<List<V_Month_Salary>>(data);
                int intResult = new BLSalary().SetDeliveried(lst, "admin");
                if (intResult >= 0)
                {
                    result.success = true;
                    result.message = "操作成功。";
                }
                else
                {
                    result.success = false;
                    result.message = "操作失败。";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #region 工资明细
        [HttpGet]
        [Display(Description = "员工薪资明细画面（日薪资列表）")]
        public ActionResult SalaryDetail(string strKey)
        {
            string[] arr = strKey.Split(',');
            SalarySearchTwo vw = new SalarySearchTwo();
            vw.companyId = arr[0];
            if (arr.Length > 1)
            {
                vw.employeeId = arr[1];
            }
            else { vw.employeeId = ""; }
            if (arr.Length > 1)
            {
                vw.salaryMonth = arr[2];
            }
            else { vw.salaryMonth = ""; }
            vw.employeeName = "";
            if (!string.IsNullOrEmpty(vw.employeeId))
            {
                u_employee ue =new BLEmployee().GetEmployeeById(vw.employeeId);
                if (ue != null && ue.ue_name != null) {
                    vw.employeeName = ue.ue_name;
                }
            }
            return View(vw);
        }       
        [HttpPost]
        [Display(Description = "员工工资查询")]
        public ActionResult QueryDailySalaries(int? pageIndex, int? pageSize, SalarySearchTwo condition)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 20;
            if (condition == null) { condition = new SalarySearchTwo(); }
            List<V_Daily_Salary> lst = new BLSalary().GetDailySalaries(condition);
            if (lst == null) { lst = new List<V_Daily_Salary>(); }
            var total = lst.Count;
            var list = lst.Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Display(Description = "导出工资明细")]
        public FileResult ExportDailySalary(string salaryMonth,string companyId,string employeeId) {
            try
            {
               SalarySearchTwo condition = new SalarySearchTwo();
                condition.companyId = companyId;
                condition.salaryMonth = salaryMonth;
                condition.employeeId = employeeId;
                List<V_Daily_Salary> lst = new BLSalary().GetDailySalaries(condition);
                V_Daily_Salary sum = new V_Daily_Salary();
                sum.employee_id = "合计";
                sum.eds_real_salary = 0;
                
                foreach (V_Daily_Salary ds in lst) {
                    //sum.eds_daily_salary += ds.eds_daily_salary;
                    if (!ds.eds_real_salary.HasValue) { ds.eds_real_salary = 0; }
                    sum.eds_real_salary += ds.eds_real_salary.Value;
                }
                lst.Add(sum);
                //导出的文件名
                string outFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "EmployeeDailySalary.xls";
                //临时文件
                string folderSrc = "/Admin.Uploads/" + DateTime.Today.Date.ToString("yyyy-MM") + "/";
                if (!Directory.Exists(HttpContext.Server.MapPath(folderSrc)))
                {
                    Directory.CreateDirectory(HttpContext.Server.MapPath(folderSrc));
                }
                Random rand = new Random();//随生成器
                string fileName = folderSrc + DateTime.Now.Ticks + rand.Next(1000, 9999) + ".xls";              
                string repath = HttpContext.Server.MapPath(fileName);

                string[] captions = { "公司编号", "公司名称", "部门编号", "部门名称", "员工编号", "员工名称", "日期", "工作日", "日薪资", "是否出勤", "薪资参数" , "实付薪资" };//列名
                           
                MemoryStream ms = ExcelHelper.RenderToExcel(captions,lst, repath,true);
                System.IO.File.Delete(repath);
                return File(ms, "application/ms-excel", outFileName);
            }
            catch(Exception ex) {
                throw ex;
                //return File(new MemoryStream(), "application/ms-excel", "Attendance.xls");
            }
        }
        #endregion 工资明细

        #region 奖金明细
        [HttpGet]
        [Display(Description = "员工奖金明细画面（按奖金类型）")]
        public ActionResult BonusDetail(string strKey)
        {
            string[] arr = strKey.Split(',');
            SalarySearchTwo vw = new SalarySearchTwo();
            vw.companyId = arr[0];
            if (arr.Length > 1)
            {
                vw.employeeId = arr[1];
            }
            else { vw.employeeId = ""; }
            if (arr.Length > 1)
            {
                vw.salaryMonth = arr[2];
            }
            else { vw.salaryMonth = ""; }
            vw.employeeName = "";
            if (!string.IsNullOrEmpty(vw.employeeId))
            {
                u_employee ue = new BLEmployee().GetEmployeeById(vw.employeeId);
                if (ue != null && ue.ue_name != null)
                {
                    vw.employeeName = ue.ue_name;
                }
            }
            return View(vw);
        }
        [HttpPost]
        [Display(Description = "员工月度奖金明细查询")]
        public ActionResult GetBonusDetail(int? pageIndex, int? pageSize, SalarySearchTwo condition)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 20;
            if (condition == null) { condition = new SalarySearchTwo(); }
            List<V_Month_Bonus_Detail> lst = new BLBonus().GetBonusDetail(condition);
            if (lst == null) { lst = new List<V_Month_Bonus_Detail>(); }
            var total = lst.Count;
            var list = lst.Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Display(Description = "导出奖金明细")]
        public FileResult ExportBonusDetail(string salaryMonth, string companyId, string employeeId)
        {
            try
            {
                SalarySearchTwo condition = new SalarySearchTwo();
                condition.companyId = companyId;
                condition.salaryMonth = salaryMonth;
                condition.employeeId = employeeId;
                List<V_Month_Bonus_Detail> lst = new BLBonus().GetBonusDetail(condition);
                string folderSrc = "/Admin.Uploads/" + DateTime.Today.Date.ToString("yyyy-MM") + "/";
                if (!Directory.Exists(HttpContext.Server.MapPath(folderSrc)))
                {
                    Directory.CreateDirectory(HttpContext.Server.MapPath(folderSrc));
                }
                Random rand = new Random();//随生成器
                string fileName = folderSrc + DateTime.Now.Ticks + rand.Next(1000, 9999) + ".xls";
                string repath = HttpContext.Server.MapPath(fileName);
                string[] captions = { "公司编号", "公司名称","部门编号","部门名称", "员工编号", "员工名称", "月份", "类型编号", "奖金类型", "项目编号","金额" };//列名
                MemoryStream ms = ExcelHelper.RenderToExcel(captions,lst, repath);
                //删除临时文件
                System.IO.File.Delete(repath);
                return File(ms, "application/ms-excel", "EmployeeBonusDetail.xls");
            }
            catch (Exception ex)
            {
                throw ex;
                //return File(new MemoryStream(), "application/ms-excel", "Attendance.xls");
            }
        }
        #endregion 奖金明细
        #endregion 薪资查询画面

        #region 奖金管理
        [HttpGet]
        [Display(Description = "奖金计划画面初始化")]
        public ActionResult BonusSearch()
        {
            return View();
        }
        [HttpPost]
        [Display(Description = "取得奖金计划头档")]
        public ActionResult GeBonusPlanList(int? pageIndex, int? pageSize, BonusSearch condition)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 20;

            if (condition == null) { condition = new BonusSearch(); }
           
            List<V_Bonus_Plan_Header> lst = new BLBonus().GetBonusPlanList(condition);
            if (lst == null) { lst = new List<V_Bonus_Plan_Header>(); }
            var total = lst.Count;
            var list = lst.Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Display(Description = "奖金计划编辑画面")]
        public ActionResult BonusPage(string planId,string companyId)
        {
            V_Bonus_Plan_Header vw = new BLBonus().GetBonusPlanById(planId, companyId);
            if (vw == null) { vw = new V_Bonus_Plan_Header(); }
            return View(vw);
        }
        [HttpPost]
        [Display(Description = "取得奖金计划明细")]
        public ActionResult GetBonusPlanDtl(string planId,string companyId)
        {
            List<V_Bonus_Employee> lst = new BLBonus().GetBonusPlanDtl(planId, companyId);
            if (lst == null) { lst = new List<V_Bonus_Employee>(); }
            var total = lst.Count;
            return Json(new { total = total, data = lst }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Display(Description = "取得未设置奖金的员工列表")]
        public ActionResult GetNoBonusEmp(string planId, string companyId)
        {
            List<V_Bonus_Employee> lst = new BLBonus().GetNoBonusEmp(planId, companyId);
            if (lst == null) { lst = new List<V_Bonus_Employee>(); }
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "保存奖金计划")]
        public ActionResult SaveBonusPlan(V_Bonus_Plan_Header data, string data2)
        {
            ResponseResult result = new ResponseResult();
            if (data == null)
            {
                result.success = false;
                result.message = "没有需要保存的数据。";
            }
            else
            {
                string planId = string.Empty;
                ActionType atpye = ActionType.Edit;
                List<bonus_plan_header> lst = new List<bonus_plan_header>();
                bonus_plan_header hdr = new bonus_plan_header();
                hdr.bph_company_id = data.bph_company_id;
                hdr.bph_bonus_type = data.bph_bonus_type;
                hdr.bph_bonus_month = data.bph_bonus_month;
                hdr.bph_bonus_from = data.bph_bonus_from;
                hdr.bph_remarks = data.bph_remarks;
                hdr.bph_update_time = DateTime.Now;
                hdr.bph_update_user = "admin";
                hdr.bph_status = "00";//默认待审批
                if (string.IsNullOrEmpty(data.bph_plan_id))
                {
                    atpye = ActionType.AddNew;
                    hdr.bph_plan_id = DateTime.Now.ToString("yyyyMMddHHmmsss");                    
                    hdr.bph_create_time = DateTime.Now;
                    hdr.bph_create_user = "admin";
                }               
                else { hdr.bph_plan_id = data.bph_plan_id; }
                planId = hdr.bph_plan_id;
                lst.Add(hdr);
                List<V_Bonus_Employee> lst2 = JsonConvert.DeserializeObject<List<V_Bonus_Employee>>(data2);
                List<bonus_plan_detail> dtls = new List<bonus_plan_detail>();
                if (lst2 != null) {
                    bonus_plan_detail dtl = null;
                    foreach (V_Bonus_Employee vbe in lst2) {
                        dtl= new bonus_plan_detail();
                        dtl.bpd_plan_id = hdr.bph_plan_id;
                        dtl.bpd_company_id = hdr.bph_company_id;
                        dtl.bpd_employee_id = vbe.employee_Id;
                        dtl.bpd_bonus_amount = (vbe.bonus_amount.HasValue)? vbe.bonus_amount.Value:0;
                        dtl.bpd_create_time= DateTime.Now;
                        dtl.bpd_create_user = "admin";
                        dtl.bpd_update_time= DateTime.Now;
                        dtl.bpd_update_user = "admin";
                        dtls.Add(dtl);
                    }
                }
                int intResult = new BLBonus().SaveBonusPlan(lst, dtls,atpye);
                if (intResult > 0)
                {
                    result.success = true;
                    result.message = "保存成功。";
                }
                else
                {
                    result.success = false;
                    result.message = "保存失败。";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "更新奖金计划状态")]
        public ActionResult UpdateBonusPlan(string updateType,string data) {
            ResponseResult result = new ResponseResult();
            if (string.IsNullOrEmpty(updateType)) {
                result.success = false;
                result.message = "无法区分操作类型，保存失败。";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(data)) {
                result.success = false;
                result.message = "缺少比要参数，保存失败。";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            List<V_Bonus_Plan_Header> vlst = JsonConvert.DeserializeObject<List<V_Bonus_Plan_Header>>(data);
            List<bonus_plan_header> lst = new List<bonus_plan_header>();
            bonus_plan_header hdr = null;
            foreach (V_Bonus_Plan_Header vw in vlst) {
                if (string.IsNullOrEmpty(vw.bph_plan_id) || string.IsNullOrEmpty(vw.bph_company_id)) {
                    continue;
                }
                hdr = new bonus_plan_header();
                hdr.bph_plan_id = vw.bph_plan_id;
                hdr.bph_company_id = vw.bph_company_id;
                hdr.bph_bonus_from = vw.bph_bonus_from;
                hdr.bph_bonus_month = vw.bph_bonus_month;
                hdr.bph_bonus_type = vw.bph_bonus_type;
                hdr.bph_remarks = vw.bph_remarks;
                if (updateType == "delete") {
                    hdr.bph_status = "99"; //已删除                 
                }
                else if (updateType == "confirm")
                {
                    hdr.bph_status = "10";//已确认
                }
                else if (updateType == "cancel")
                {
                    hdr.bph_status = "90";//无效
                }
                hdr.bph_update_time = DateTime.Now;
                hdr.bph_update_user = "admin";
                lst.Add(hdr);
            }
            //保存数据
            int intResult = new BLBonus().SaveBonusPlan(lst, null, ActionType.Edit);
            if (intResult > 0)
            {
                result.success = true;
                result.message = "保存成功。";
            }
            else
            {
                result.success = false;
                result.message = "保存失败。";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        
        [HttpGet]
        [Display(Description = "员工薪资设置")]
        public ActionResult EmployeeSalaryPage()
        {
            return View();
        }
    }
}