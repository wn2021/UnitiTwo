using Business;
using Common;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnitiTwo.Controllers
{
    [Authentication]
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult Index()
        {
            return View();
        }
        #region 考勤设置
        [HttpGet]
        [Display(Description = "考勤设置")]
        public ActionResult AttendanceSetting()
        {
            return View();
        }
        [HttpPost]
        [Display(Description = "初始化考勤月")]
        public ActionResult initSetting(string strYmonth)
        {
            int intResult = new BLAttendance().InitMonthAttendance(strYmonth, "admin");
            ResponseResult result = new ResponseResult();
            if (intResult > 0)
            {
                result.success = true;
            }
            else {
                result.success = false;
                result.message = "初始化失败！";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "取得考勤月设置")]
        public ActionResult GetAttSetting(int? pageIndex, int? pageSize, string strYmonth) {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 10;
            List<V_Attendance_Setting> lst = new BLAttendance().GetAttSetting(strYmonth);
            if (lst == null) { lst = new List<V_Attendance_Setting>(); }
            var total = lst.Count;
            var list = lst.OrderBy(d => d.as_date).Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Display(Description = "保存考勤设置")]
        public ActionResult SaveSettings(string data)
        {
            ResponseResult result = new ResponseResult();
            if (data == null)
            {
                result.success = false;
                result.message = "没有需要保存的数据。";
            }
            else
            {
                List<V_Attendance_Setting> lst = JsonConvert.DeserializeObject<List<V_Attendance_Setting>>(data);
                int intResult = new BLAttendance().SaveAttSetting(lst, "admin");
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
        #endregion 考勤设置

        #region 考勤查询
        [HttpGet]
        [Display(Description = "打开考勤查询画面")]
        public ActionResult EmpAttendanceQuery()
        {
            return View();
        }

        [HttpPost]
        [Display(Description = "取得员工考勤")]
        public ActionResult GetEmpAttendances(int? pageIndex, int? pageSize, EmpAttSearch condtion)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 10;
            if (condtion == null) { condtion = new EmpAttSearch();
                condtion.beginDate = DateTime.Today.AddDays(-1);
                condtion.endDate = DateTime.Today;
            }
            List<V_Employee_Attendance> lst = new BLAttendance().GetEmpAttendances(condtion);
            if (lst == null) { lst = new List<V_Employee_Attendance>(); }
            var total = lst.Count;
            var list = lst.OrderBy(d => d.at_date).Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
        #endregion 考勤查询

        #region 考勤收集
        [HttpGet]
        public ActionResult CollectAttendances()
        {
            return View();
        }
        [HttpPost]
        [Display(Description = "收集考勤数据")]
        public ActionResult CollectAtt(string strYmonth)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                new BLAttendance().CollectAttendance(strYmonth);
                result.success = true;
            }
            catch
            {
                result.success = false;
                result.message = "考勤收集失败！";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion 考勤收集

        #region 调休计划
        [HttpGet]
        [Display(Description = "调休计划")]
        public ActionResult WSChange()
        {
            return View();
        }
        [HttpPost]
        [Display(Description = "取得调休计划")]
        public ActionResult GetWSChanges(int? pageIndex, int? pageSize, string companyId, string depId, string beginDate, string endDate)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 10;
            DateTime dateBegin = DateTime.Parse(beginDate);
            DateTime dateEnd = DateTime.Parse(endDate);
            List<V_WS_change> lst = new BLAttendance().GetWSChanges(companyId, depId, dateBegin, dateEnd);
            if (lst == null) { lst = new List<V_WS_change>(); }
            var total = lst.Count;
            var list = lst.Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "保存调休计划")]
        public ActionResult SaveWSChanges(string data)
        {
            ResponseResult result = new ResponseResult();
            if (data == null)
            {
                result.success = false;
                result.message = "没有需要保存的数据。";
            }
            else
            {
                List<V_WS_change> lst = JsonConvert.DeserializeObject<List<V_WS_change>>(data);
                int intResult = new BLAttendance().SaveWSChanges(lst);
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
        [Display(Description = "取得调休计划范围")]
        public ActionResult GetWSChangeTarfet(int? pageIndex, int? pageSize, string wsChangeId, string targetype,string wsCompanyId)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 10;
            var total = 0;
            if (string.IsNullOrEmpty(wsChangeId) ||string.IsNullOrEmpty(targetype))
            {
                return Json(new { total = total, data = new List<V_WS_Target>() }, JsonRequestBehavior.AllowGet);
            }
            List<V_WS_Target> lst = new BLAttendance().GetWSChangeTarfet(wsChangeId, targetype, wsCompanyId);
            if (lst == null) { lst = new List<V_WS_Target>(); }
            total = lst.Count;
            var list = lst.Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "删除调休计划")]
        public ActionResult DelWSChange(string id)
        {
            ResponseResult result = new ResponseResult();
            if (string.IsNullOrEmpty(id))
            {
                result.success = false;
                result.message = "没有需要删除的数据。";
            }
            else
            {
                int intResult = new BLAttendance().DeleteWSChange(id, "admin");
                if (intResult > 0)
                {
                    result.success = true;
                    result.message = "删除成功。";
                }
                else
                {
                    result.success = false;
                    result.message = "删除失败。";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //[Display(Description = "保存调休计划范围")]
        //public ActionResult SaveWSCTarget(string data)
        //{
        //    ResponseResult result = new ResponseResult();
        //    if (data == null)
        //    {
        //        result.success = false;
        //        result.message = "没有需要保存的数据。";
        //    }
        //    else
        //    {
        //        List<V_WS_Target> lst = JsonConvert.DeserializeObject<List<V_WS_Target>>(data);
        //        int intResult = new BLAttendance().SaveWSCTarget(lst,"admin");
        //        if (intResult > 0)
        //        {
        //            result.success = true;
        //            result.message = "保存成功。";
        //        }
        //        else
        //        {
        //            result.success = false;
        //            result.message = "保存失败。";
        //        }
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [Display(Description = "保存调休计划范围")]
        public ActionResult SaveWSCTarget(string wsId, string targetIds)
        {
            ResponseResult result = new ResponseResult();
            if (string.IsNullOrEmpty(wsId))
            {
                result.success = false;
                result.message = "未取得计划编号。";
            }
            else
            {
                int intResult = 0;
                List<work_schedule_change_target> lst = new List<work_schedule_change_target>();
                work_schedule_change_target wst = null;
                DateTime currentTime = DateTime.Now;
                string[] arr = targetIds.Split(',');
                foreach (string tragetId in arr) {
                    wst = new work_schedule_change_target();
                    wst.wct_id = wsId;
                    wst.wct_target_id = tragetId;
                    wst.wct_create_user = "admin";
                    wst.wct_create_time = currentTime;
                    wst.wct_update_user = "admin";
                    wst.wct_update_time = currentTime;
                    lst.Add(wst);
                }
                if (lst.Count > 0)
                {
                    //删除原有，插入选中
                    intResult = new BLAttendance().InsertWSCTarget(lst);
                }
                else {
                    //未有选中，删除所有
                    intResult = new BLAttendance().DeleteWSCTargetByPlanId(wsId);
                }
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
        #endregion 调休计划

        #region 考勤补录
        [HttpGet]
        [Display(Description = "考勤补录初始化")]
        public ActionResult RelogAttendance() {
            return View();
        }
        [HttpPost]
        [Display(Description = "取得员工考勤打卡数据")]
        public ActionResult GetCardClickLog(int? pageIndex, int? pageSize, string strMonth, string companyId, string employeeId)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 10;
            List<V_Crad_Click_Log> lst = new BLAttendance().GetEmpCardClicks(strMonth, companyId, employeeId);
            if (lst == null) { lst = new List<V_Crad_Click_Log>(); }
            var total = lst.Count;
            var list = lst.Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Display(Description = "保存员工打卡数据")]
        public ActionResult SaveCardClick(string data)
        {
            ResponseResult result = new ResponseResult();
            if (data == null)
            {
                result.success = false;
                result.message = "没有需要保存的数据。";
            }
            else
            {
                List<V_Crad_Click_Log> lst = JsonConvert.DeserializeObject<List<V_Crad_Click_Log>>(data);
                int intResult = new BLAttendance().SaveCardClick(lst);
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
        [Display(Description = "删除员工打卡记录")]
        public ActionResult DelCardClick(string data)
        {
            ResponseResult result = new ResponseResult();
            if (data == null)
            {
                result.success = false;
                result.message = "没有需要保存的数据。";
            }
            else
            {
                List<V_Crad_Click_Log> lst = JsonConvert.DeserializeObject<List<V_Crad_Click_Log>>(data);
                int intResult = new BLAttendance().DelCardClick(lst);
                if (intResult >= 0)
                {
                    result.success = true;
                    result.message = "删除成功。";
                }
                else
                {
                    result.success = false;
                    result.message = "删除失败。";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion 考勤补录

        #region Excel操作
        [HttpGet]
        [Display(Description = "Excel模式导出和复制考勤打卡信息")]
        public ActionResult ExcelLogAttendance() {
            return View();
        }
        [HttpGet]
        [Display(Description = "取得Excel导出数据")]
        public FileResult ExportFile(string strMonth, string companyId, string employeeId)
        {
            EmpAttSearch condtion = new EmpAttSearch();

            if (!string.IsNullOrEmpty(strMonth))
            {
                condtion.beginDate = DateTime.Parse(strMonth + "-01");
                condtion.endDate = DateTime.Today;
            }
            else
            {

                condtion.endDate = DateTime.Today;
                condtion.beginDate = DateTime.Today.AddDays((DateTime.Today.Day * -1)+1);
            }
            condtion.companyId = companyId;
            condtion.empId = employeeId;
            DataTable dt = new BLAttendance().GetDataForExport(condtion);
            string folderSrc = "/Admin.Uploads/" + DateTime.Today.Date.ToString("yyyy-MM") + "/";
            if (!Directory.Exists(HttpContext.Server.MapPath(folderSrc)))
            {
                Directory.CreateDirectory(HttpContext.Server.MapPath(folderSrc));
            }
            Random rand = new Random();//随生成器
            string fileName = folderSrc + DateTime.Now.Ticks + rand.Next(1000, 9999) + ".xls";
            string repath = HttpContext.Server.MapPath(fileName);
            string[] captions = { "公司编号", "公司名称", "员工编号", "员工名称","员工卡号", "考勤日期", "上班打卡", "下班打卡"};//列名
            MemoryStream ms = ExcelHelper.RenderToExcel(captions, dt, repath);
            return File(ms, "application/ms-excel", "Attendance.xls");
        }
        [HttpPost]
        [Display(Description = "保存数据")]
        public ActionResult SaveExcelCardClick(string data)
        {
            ResponseResult result = new ResponseResult();
            if (data == null)
            {
                result.success = false;
                result.message = "没有需要保存的数据。";
            }
            else
            {
                string batchNo = DateTime.Now.ToString("yyyyMMddHHmmsss");
                List<V_Excel_Card_Click> lst = JsonConvert.DeserializeObject<List<V_Excel_Card_Click>>(data);
                int intResult = new BLAttendance().SaveCardClick(lst, batchNo);
                if (intResult > 0)
                {
                    result.success = true;
                    result.message = batchNo;
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
        [Display(Description = "取得保存的数据展示在画面")]
        public ActionResult GetCardClicks(int? pageIndex, int? pageSize,string strMonth, string companyId, string employeeId,string batchNo)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 10;
            EmpAttSearch condtion = new EmpAttSearch();
            if (!string.IsNullOrEmpty(strMonth))
            {
                condtion.beginDate = DateTime.Parse(strMonth + "-01");
                condtion.endDate = DateTime.Today;
            }
            else
            {

                condtion.endDate = DateTime.Today;
                condtion.beginDate = DateTime.Today.AddDays(DateTime.Today.Day * -1);
            }
            condtion.companyId = companyId;
            condtion.empId = employeeId;
            
            List<V_Excel_Card_Click> lst = new BLAttendance().GetListForExport(condtion, batchNo);
            if (lst == null) { lst = new List<V_Excel_Card_Click>(); }
            var total = lst.Count;
            var list = lst.Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
        #endregion Excel操作
    }
}