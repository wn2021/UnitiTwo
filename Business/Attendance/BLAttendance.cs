using BFC.FRM.Dao;
using Common;
using DataAccess;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BLAttendance:BusinessBase
    {
        /// <summary>
        /// 判断当前日期是工作日/周末还是节假日
        /// 0工作日;1周末;2节假日
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        public string GetDayType(DateTime searchDate)
        {
            int dayofType =0;
            using (var client = new HttpClient())
            {
                string url = "http://tool.bitefu.net/jiari/?d="+ searchDate.ToString("yyyyMMdd");
                //string url = "http://lanfly.vicp.io/api/holiday/info/$" + searchDate.ToString("yyyyMMdd");

                HttpContent httpContent = new StringContent("");
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = client.PostAsync(url,httpContent).Result.Content.ReadAsStringAsync().Result;
                if (!int.TryParse(result, out dayofType)) {
                    var day = searchDate.DayOfWeek;

                    //判断是否为周末
                    if (day == DayOfWeek.Sunday || day == DayOfWeek.Saturday)
                        return "1";
                }
                return dayofType.ToString();
            }
        }
        #region 考勤设置
        /// <summary>
        /// 初始化考勤设置
        /// </summary>
        /// <param name="strYmonth">月份</param>
        /// <param name="strUser">用户</param>
        /// <returns></returns>
        public int InitMonthAttendance(string strYmonth,string strUser) {
            DateTime thisMonth = DateTime.Parse(strYmonth + "-01");
            int days = DateTime.DaysInMonth(thisMonth.Year, thisMonth.Month);
            DateTime calculateTime = DateTime.Now;
            List<attendance_setting> list = new List<attendance_setting>();
            attendance_setting ats = null;
            for (int i = 0; i < days; i++) {
                ats = new attendance_setting();
                ats.as_date = thisMonth.AddDays(i);
                ats.as_day_of_week = (int)ats.as_date.DayOfWeek;
                ats.as_day_type = GetDayType(ats.as_date);
                if (ats.as_day_type == "0") {
                    //工作日
                    ats.as_salary_param = 1;
                }else if (ats.as_day_type == "1")
                {
                    //周末
                    ats.as_salary_param = 2;
                }
                else if (ats.as_day_type == "2")
                {
                    //节假日
                    ats.as_salary_param = 3;
                }
                ats.as_create_time = calculateTime;
                ats.as_update_time = calculateTime;
                ats.as_create_user = strUser;
                ats.as_update_user = strUser;
                list.Add(ats);
            }
            int intResult = -1;
            if (list.Count == 0) { return intResult; }
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLAttendance dl = new DLAttendance();
                intResult = dl.InsertAttendanceSet(list);
                ts.Complete();
            }
            return intResult;
        }
        /// <summary>
        /// 取得考勤月设置
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<V_Attendance_Setting> GetAttSetting(string month) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLAttendance dl = new DLAttendance();
                List<V_Attendance_Setting>  result = dl.GetAttSetting(month);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="views"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int SaveAttSetting(List<V_Attendance_Setting> views,string userName) {
            List<attendance_setting> list = new List<attendance_setting>();
            attendance_setting ass = null;
            DateTime currentTime = DateTime.Now;
            foreach (V_Attendance_Setting vw in views) {
                ass = new attendance_setting();
                ass.as_date = vw.as_date;
                ass.as_salary_param = vw.as_salary_param;
                ass.as_day_type = vw.as_day_type;
                ass.as_update_time = currentTime;
                ass.as_update_user = userName;
                list.Add(ass);
            }
            if (list.Count == 0) {
                return 0;
            }
            return this.SaveAttSetting(list);
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveAttSetting(List<attendance_setting> list)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLAttendance dl = new DLAttendance();
                int result = dl.UpdateAttendanceSet(list);
                ts.Complete();
                return result;
            }
        }
        #endregion 考勤设置

        #region 考勤查询画面
        /// <summary>
        /// 员工考勤查询
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public List<V_Employee_Attendance> GetEmpAttendances(EmpAttSearch condtion) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLAttendance dl = new DLAttendance();
                List<V_Employee_Attendance> result = dl.GetEmpAttendances(condtion);
                ts.Complete();
                return result;
            }
        }
        #endregion 考勤查询画面

        #region 考勤收集
        /// <summary>
        /// 收集考勤数据
        /// </summary>
        /// <param name="strMonth"></param>
        public void CollectAttendance(string strMonth) {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLAttendance dl = new DLAttendance();
                 dl.CollectAttendance(strMonth);
                ts.Complete();
            }
        }
        #endregion 考勤收集

        #region 调休计划
        /// <summary>
        /// 取得调休计划
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="depId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<V_WS_change> GetWSChanges(string companyId, string depId, DateTime beginDate, DateTime endDate) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLAttendance dl = new DLAttendance();
                List<V_WS_change> result = dl.GetWSChanges(companyId,depId, beginDate, endDate);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 保存调休计划
        /// </summary>
        /// <param name="views"></param>
        /// <returns></returns>
        public int SaveWSChanges(List<V_WS_change> views)
        {
            DateTime currentTime = DateTime.Now;
            List<work_schedule_change> insertList = new List<work_schedule_change>();
            List<work_schedule_change> updateList = new List<work_schedule_change>();
            work_schedule_change wsc = null;
            foreach (V_WS_change vw in views) {
                wsc = new work_schedule_change();                
                wsc.wsc_date_from = vw.wsc_date_from.Value;
                wsc.wsc_date_to = vw.wsc_date_to.Value;
                wsc.wsc_change_type = vw.wsc_change_type;
                wsc.wsc_change_reason = vw.wsc_change_reason;
                wsc.wsc_range_type = vw.wsc_range_type;
                wsc.wsc_salary_param = vw.wsc_salary_param.Value;
                wsc.wsc_remarks = vw.wsc_remarks;
                wsc.wsc_create_time = currentTime;
                wsc.wsc_create_user = "admin";
                wsc.wsc_update_time = currentTime;
                wsc.wsc_update_user= "admin";
                wsc.wsc_status = "1";//有效
                wsc.wsc_company_id = vw.wsc_company_id;
                if (!string.IsNullOrEmpty(vw.wsc_id))
                {
                    wsc.wsc_id = vw.wsc_id;
                    updateList.Add(wsc);   
                }
                else {
                    wsc.wsc_id = DateTime.Now.ToString("yyyyMMddhhmmsss");
                    insertList.Add(wsc);
                }
            }
            int intResult = 0;
            if (insertList.Count == 0 && updateList.Count == 0) { return intResult; }
                using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLAttendance dl = new DLAttendance();
                if (insertList.Count > 0)
                {
                    intResult += dl.InsertWSChange(insertList);
                }
                if (updateList.Count > 0)
                {
                    intResult += dl.UpdateWSChange(updateList);
                }
                ts.Complete();
                return intResult;
            }
        }
        /// <summary>
        /// 删除调休计划
        /// </summary>
        /// <param name="wsid"></param>
        /// <returns></returns>
        public int DeleteWSChange(string wsid,string username) {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLAttendance dl = new DLAttendance();
                int result = dl.DeleteWSChange(wsid, username);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得调休计划范围
        /// </summary>
        /// <param name="wsChangeId"></param>
        /// <param name="targetype"></param>
        /// <returns></returns>
        public List<V_WS_Target> GetWSChangeTarfet(string wsChangeId, string targetype,string wsCompanyId) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLAttendance dl = new DLAttendance();
                List<V_WS_Target> result = dl.GetWSChangeTarfet(wsChangeId, targetype, wsCompanyId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 保存调休计划范围
        /// </summary>
        /// <param name="views"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public int SaveWSCTarget(List<V_WS_Target> views,string username) {
            DateTime currentTime = DateTime.Now;
            List<work_schedule_change_target> list = new List<work_schedule_change_target>();
            List<work_schedule_change_target> delList = new List<work_schedule_change_target>();
            work_schedule_change_target wst = null;
            foreach (V_WS_Target vw in views) {
                wst = new work_schedule_change_target();                                          
                wst.wct_id = vw.wct_id;
                wst.wct_target_id = vw.wct_target_id;
                wst.wct_update_time = currentTime;
                wst.wct_create_time = currentTime;
                wst.wct_create_user = username;
                wst.wct_update_user = username;
                if (!vw.is_target.Equals("1"))
                {
                    delList.Add(wst);
                }
                else
                {
                    list.Add(wst);
                }
            }
            int intResult = 0;
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLAttendance dl = new DLAttendance();
                if(list.Count > 0) { intResult += dl.SaveWSCTarget(list); }
                if (delList.Count > 0) { intResult += dl.DeleteWSCTarget(delList); }
                ts.Complete();
                return intResult;
            }
        }
        /// <summary>
        /// 插入调休计划目标
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int InsertWSCTarget(List<work_schedule_change_target> list) {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLAttendance dl = new DLAttendance();
                int intResult = dl.InsertWSCTarget(list);
                ts.Complete();
                return intResult;
            }
        }
        /// <summary>
        /// 根据计划编号删除计划目标
        /// </summary>
        /// <param name="wsId"></param>
        /// <returns></returns>
        public int DeleteWSCTargetByPlanId(string wsId)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLAttendance dl = new DLAttendance();
                int intResult = dl.DeleteWSCTargetByPlanId(wsId);
                ts.Complete();
                return intResult;
            }
        }
        #endregion 调休计划

        #region 考勤补录
        /// <summary>
        /// 取得员工打开记录
        /// </summary>
        /// <param name="strMonth"></param>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<V_Crad_Click_Log> GetEmpCardClicks(string strMonth, string companyId, string employeeId) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLAttendance dl = new DLAttendance();
                List<V_Crad_Click_Log>  result = dl.GetEmpCardClicks(strMonth, companyId, employeeId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 保存打卡记录
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public int SaveCardClick(List<V_Crad_Click_Log> view) {
            List<emp_card_click_log> list = new List<emp_card_click_log>();
            emp_card_click_log clog = null;
            string cardId = string.Empty;
            foreach (V_Crad_Click_Log vw in view) {
                if (string.IsNullOrEmpty(vw.card_id) ||!vw.work_date.HasValue) { continue; }
                if (!string.IsNullOrEmpty(vw.begin_click_time))
                {
                    clog = new emp_card_click_log();
                    clog.card_id = vw.card_id;
                    clog.click_time = DateTime.Parse(vw.work_date.Value.ToString("yyyy-MM-dd") + " " + DateTime.Parse(vw.begin_click_time).ToString("HH:mm"));
                    list.Add(clog);
                }
                if (!string.IsNullOrEmpty(vw.end_click_time))
                {
                    clog = new emp_card_click_log();
                    clog.card_id = vw.card_id;
                    clog.click_time = DateTime.Parse(vw.work_date.Value.ToString("yyyy-MM-dd") + " " + DateTime.Parse(vw.end_click_time).ToString("HH:mm"));
                    list.Add(clog);
                }
            }
            if (list.Count == 0) { return 0; }
            cardId = list[0].card_id;
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLAttendance dl = new DLAttendance();
                int result = dl.SaveCardClick(list,false);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 保存打卡记录(删除)
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public int DelCardClick(List<V_Crad_Click_Log> view)
        {
            List<emp_card_click_log> list = new List<emp_card_click_log>();
            emp_card_click_log clog = null;
            string cardId = string.Empty;
            foreach (V_Crad_Click_Log vw in view)
            {
                if (string.IsNullOrEmpty(vw.card_id) || !vw.work_date.HasValue) { continue; }
                if (!string.IsNullOrEmpty(vw.begin_click_time))
                {
                    clog = new emp_card_click_log();
                    clog.card_id = vw.card_id;
                    clog.click_time = DateTime.Parse(vw.work_date.Value.ToString("yyyy-MM-dd") + " " + vw.begin_click_time);
                    list.Add(clog);
                }
                if (!string.IsNullOrEmpty(vw.end_click_time))
                {
                    clog = new emp_card_click_log();
                    clog.card_id = vw.card_id;
                    clog.click_time = DateTime.Parse(vw.work_date.Value.ToString("yyyy-MM-dd") + " " + vw.end_click_time);
                    list.Add(clog);
                }
            }
            if (list.Count == 0) { return 0; }
            cardId = list[0].card_id;
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLAttendance dl = new DLAttendance();
                int result = dl.SaveCardClick(list,true);
                ts.Complete();
                return result;
            }
        }
        #endregion 考勤补录

        #region Excel操作
        /// <summary>
        /// 取得导出数据
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public DataTable GetDataForExport(EmpAttSearch condtion) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLAttendance dl = new DLAttendance();
                DataTable result = dl.GetDataForExport(condtion);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 保存excel导入数据
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public int SaveCardClick(List<V_Excel_Card_Click> view,string batchNo)
        {
            List<emp_card_click_log> list = new List<emp_card_click_log>();
            emp_card_click_log clog = null;
            string cardId = string.Empty;
            foreach (V_Excel_Card_Click vw in view)
            {
                if (string.IsNullOrEmpty(vw.cardId) || !vw.attDate.HasValue) { continue; }
                if (!string.IsNullOrEmpty(vw.at_login_time))
                {
                    clog = new emp_card_click_log();
                    clog.card_id = vw.cardId;
                    clog.excel_batch_no = batchNo;
                    clog.click_time = DateTime.Parse(vw.attDate.Value.ToString("yyyy-MM-dd") + " " + DateTime.Parse(vw.at_login_time).ToString("HH:mm"));
                    list.Add(clog);
                }
                if (!string.IsNullOrEmpty(vw.at_level_time))
                {
                    clog = new emp_card_click_log();
                    clog.card_id = vw.cardId;
                    clog.excel_batch_no = batchNo;
                    clog.click_time = DateTime.Parse(vw.attDate.Value.ToString("yyyy-MM-dd") + " " + DateTime.Parse(vw.at_level_time).ToString("HH:mm"));
                    list.Add(clog);
                }
            }
            if (list.Count == 0) { return 0; }
            cardId = list[0].card_id;
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLAttendance dl = new DLAttendance();
                int result = dl.SaveCardClick(list, false);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 导入保存后刷新画面
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public List<V_Excel_Card_Click> GetListForExport(EmpAttSearch condtion,string batchNo) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLAttendance dl = new DLAttendance();
                List<V_Excel_Card_Click> result = dl.GetListForExport(condtion, batchNo);
                ts.Complete();
                return result;
            }
        }
        #endregion Excel操作
    }
}
