using BFC.SDK.Data.DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DLAttendance:DaoBase
    {
        #region 考勤设置
        /// <summary>
        /// 插入考勤数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int InsertAttendanceSet(List<attendance_setting> list)
        {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            this.DataAccessClient.Delete(list, "attendance_setting", "as_date");
            return this.DataAccessClient.Insert(list, "attendance_setting");
        }
        /// <summary>
        /// 更新考勤设置
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int UpdateAttendanceSet(List<attendance_setting> list)
        {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            //只更新部分字段，这里特别指定，防止更新其他字段           
            UpdateFields updfields = new UpdateFields(UpdateFieldsOptions.IncludeFields, "as_salary_param", "as_day_type"
                , "as_update_user", "as_update_time");
            return this.DataAccessClient.Update(list, "attendance_setting", updfields, new string[] { "as_date" });
        }
        /// <summary>
        /// 取得考勤月设置
        /// </summary>
        /// <param name="month">月份yyyy-MM</param>
        /// <returns></returns>
        public List<V_Attendance_Setting> GetAttSetting(string month)
        {
            List<V_Attendance_Setting> lst = new List<V_Attendance_Setting>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ats.as_date,");
            sql.AppendLine("ats.as_day_type as_day_type,");
            sql.AppendLine("us.description as_day_type_name,");
            sql.AppendLine("fs.description as_day_of_week,");
            sql.AppendLine("ats.as_salary_param");
            sql.AppendLine("from attendance_setting ats");
            sql.AppendLine("left join u_systeminfo us on us.sysid = 2002 and us.sysubid != 0");
            sql.AppendLine("and us.sysvalue = ats.as_day_type");
            sql.AppendLine("left join u_systeminfo fs on fs.sysid = 2003 and fs.sysubid != 0");
            sql.AppendLine("and fs.sysvalue = ats.as_day_of_week");
            sql.AppendLine(" where 1=1");
            sql.AppendLine("and convert(varchar(7),as_date,120) = " + this.GetSqlValueString(month));
            sql.AppendLine(" order by as_date");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        #endregion 考勤设置

        #region 考勤查询
        /// <summary>
        /// 取得员工考勤数据
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public List<V_Employee_Attendance> GetEmpAttendances(EmpAttSearch condtion)
        {
            List<V_Employee_Attendance> lst = new List<V_Employee_Attendance>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ass.as_date at_date");
            sql.AppendLine("  , ue.ue_id at_emp_id");
            sql.AppendLine("  , ue.ue_name at_emp_name");
            sql.AppendLine("  , ue.ue_company_id at_company_id");
            sql.AppendLine("  , uc.uc_name at_company_name");
            sql.AppendLine("  , convert(varchar(10), eda_login_time, 108) at_login_time");
            sql.AppendLine("  , convert(varchar(10), eda_level_time, 108) at_level_time");
            sql.AppendLine("  , eda_work_hour at_work_hours");
            sql.AppendLine("  , case when eda_work_flag = '1' then 'Y' else '' end as at_work_flag");
            sql.AppendLine("  , case when eda_work_flag = '1' then '' else us.description end at_no_att_reason");
            sql.AppendLine("  , eda_remarks at_remarks");
            sql.AppendLine("from u_employee ue");
            sql.AppendLine("inner join attendance_setting ass on ass.as_date>="+this.GetSqlValueString(condtion.beginDate,this.DateFormatForDB));
            sql.AppendLine(" and ass.as_date <= " + this.GetSqlValueString(condtion.endDate,this.DateFormatForDB));
            sql.AppendLine("inner join u_company uc on uc.uc_id = ue.ue_company_id");
            sql.AppendLine("inner join emp_daily_attendance eda on eda.eda_date = ass.as_date");
            sql.AppendLine("and eda.eda_emp_id = ue.ue_id");
            sql.AppendLine("  and eda.eda_company_id = ue.ue_company_id");
            sql.AppendLine("  left join u_systeminfo us on us.sysid = 3001");
            sql.AppendLine("  and us.sysubid != 0");
            sql.AppendLine("  and us.sysvalue = eda_no_att_reason");
            sql.AppendLine("  where 1 =1");
            if (!string.IsNullOrEmpty(condtion.companyId)) {
                sql.AppendLine("  and ue.ue_company_id="+this.GetSqlValueString(condtion.companyId));
            }
            if (!string.IsNullOrEmpty(condtion.empId))
            {
                sql.AppendLine("  and ue.ue_id=" + this.GetSqlValueString(condtion.empId));
            }
            if (!string.IsNullOrEmpty(condtion.empName))
            {
                sql.AppendLine("  and ue.ue_name like " + this.GetLikeSqlValueString(condtion.empName));
            }
            sql.AppendLine("  order by ass.as_date, ue.ue_company_id, ue.ue_id");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        #endregion 考勤查询

        #region 考勤收集
        /// <summary>
        /// 收集考勤数据
        /// </summary>
        /// <param name="strMonth">考勤月份yyyy-MM</param>
        public void CollectAttendance(string strMonth) {
            IDataParameter[] param = new IDataParameter[1];
            param[0] = this.DataAccessClient.GetDataParameterInstance("calcMonth", strMonth, ParameterDirection.Input);
            this.DataAccessClient.ExecuteNonQueryBySP("P_Calc_Employee_Attendance", param);
        }
        #endregion 考勤收集

        #region 调休计划
        /// <summary>
        /// 取得未结束的调休计划
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        public List<V_WS_change> GetWSChanges(string companyId, string depId,DateTime beginDate,DateTime endDate)
        {
            List<V_WS_change> lst = new List<V_WS_change>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from work_schedule_change ws");
            sql.AppendLine("  where 1 =1");
            sql.AppendLine("  and wsc_status !='9'");
            sql.AppendLine("  and wsc_date_from >=" + this.GetSqlValueString(beginDate, this.DateFormatForDB));
            sql.AppendLine("  and wsc_date_to <=" + this.GetSqlValueString(endDate, this.DateFormatForDB));
            if (!string.IsNullOrEmpty(companyId))
            {
                sql.AppendLine(" and (exists(select 1 from work_schedule_change_target wst ");
                sql.AppendLine("   inner join u_department ud on ud.ud_status = '1'");
                sql.AppendLine("   and ud.ud_company_id =" + this.GetSqlValueString(companyId));
                sql.AppendLine("   where wst.wct_id = ws.wsc_id");
                sql.AppendLine("   and wsc_range_type = '1'  and ud.ud_company_id = wst.wct_target_id");
                //sql.AppendLine("    or(wsc_range_type = '2'  and ud.ud_id = wst.wct_target_id))");
                sql.AppendLine("  )");
                sql.AppendLine(" or wsc_company_id=" + this.GetSqlValueString(companyId));
                sql.AppendLine("  )");
            }
            if (!string.IsNullOrEmpty(depId))
            {
                sql.AppendLine(" and exists(select 1 from work_schedule_change_target wst ");
                sql.AppendLine("   inner join u_department ud on ud.ud_status = '1'");
                sql.AppendLine("   and ud.ud_id =" + this.GetSqlValueString(depId));
                sql.AppendLine("   where wst.wct_id = ws.wsc_id");
                sql.AppendLine("   and((wsc_range_type = '1'  and ud.ud_company_id = wst.wct_target_id)");
                sql.AppendLine("    or(wsc_range_type = '2'  and ud.ud_id = wst.wct_target_id))");
                sql.AppendLine("  )");
            }
            sql.AppendLine("  order by wsc_date_from, wsc_date_to, wsc_id");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        /// <summary>
        /// 取得调休计划范围
        /// </summary>
        /// <param name="wsChangeId">计划编号</param>
        /// <param name="targetype">范围类型</param>
        /// <returns></returns>
        public List<V_WS_Target> GetWSChangeTarfet(string wsChangeId, string targetype,string wsCompanyId)
        {
            List<V_WS_Target> lst = new List<V_WS_Target>();
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(wsChangeId))
            {
                if (targetype.Equals("1"))
                {
                    //公司
                    sql.AppendLine("select ws.wsc_id wct_id");
                    sql.AppendLine(", uc.uc_id wct_target_id");
                    sql.AppendLine(", uc.uc_name wct_target_name");
                    sql.AppendLine(",case when wst.wct_target_id is null then '0' else '1' end is_target");
                    sql.AppendLine("from work_schedule_change ws");
                    sql.AppendLine("left join u_company uc on uc.uc_status = '10'");
                    sql.AppendLine("left join work_schedule_change_target wst on wst.wct_id = ws.wsc_id");
                    sql.AppendLine("and uc.uc_id = wst.wct_target_id");
                    sql.AppendLine("where ws.wsc_id =" + this.GetSqlValueString(wsChangeId));
                    sql.AppendLine("and ws.wsc_range_type = " + this.GetSqlValueString(targetype));
                    sql.AppendLine("order by uc.uc_id");
                }
                else if (targetype.Equals("2"))
                {
                    //部门
                    sql.AppendLine("select ws.wsc_id wct_id");
                    sql.AppendLine(", ud.ud_id wct_target_id");
                    sql.AppendLine(", ud.ud_name wct_target_name");
                    sql.AppendLine(",case when wst.wct_target_id is null then '0' else '1' end is_target");
                    sql.AppendLine("from work_schedule_change ws");
                    sql.AppendLine("left join u_department ud on ud.ud_status = '1'");
                    sql.AppendLine("and ud.ud_company_id=" + this.GetSqlValueString(wsCompanyId));
                    sql.AppendLine("left join work_schedule_change_target wst on wst.wct_id = ws.wsc_id");
                    sql.AppendLine("and ud.ud_id = wst.wct_target_id");
                    sql.AppendLine("where ws.wsc_id =" + this.GetSqlValueString(wsChangeId));
                    sql.AppendLine("and ws.wsc_range_type = " + this.GetSqlValueString(targetype));
                    sql.AppendLine("order by ud.ud_id");
                }
            }
            else
            {
                if (targetype.Equals("1"))
                {
                    //公司
                    sql.AppendLine("select '' wct_id");
                    sql.AppendLine(", uc.uc_id wct_target_id");
                    sql.AppendLine(", uc.uc_name wct_target_name");
                    sql.AppendLine(", '0' is_target");
                    sql.AppendLine("from  u_company uc where uc.uc_status = '10'");
                }
                else if (targetype.Equals("2"))
                {
                    //公司
                    sql.AppendLine("select '' wct_id");
                    sql.AppendLine(", ud.ud_id wct_target_id");
                    sql.AppendLine(", ud.ud_name wct_target_name");
                    sql.AppendLine(", '0' is_target");
                    sql.AppendLine("from  u_department ud where ud.ud_status = '10'");
                    sql.AppendLine("and ud.ud_company_id=" + this.GetSqlValueString(wsCompanyId));
                }
            }
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        /// <summary>
        /// 查询调休计划
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int InsertWSChange(List<work_schedule_change> list)
        {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            this.DataAccessClient.Delete(list, "work_schedule_change", "wsc_id");
            return this.DataAccessClient.Insert(list, "work_schedule_change");
        }
        /// <summary>
        /// 删除调休计划
        /// </summary>
        /// <param name="wsid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public int DeleteWSChange(string wsid,string username) {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" delete from work_schedule_change_target where wct_id=" + this.GetSqlValueString(wsid));
            this.DataAccessClient.ExecuteNonQuery(sql.ToString());
            sql.Remove(0,sql.Length);
            sql.AppendLine(" update work_schedule_change set wsc_status='9'");
            sql.AppendLine(" ,wsc_update_user="+this.GetSqlValueString(username));
            sql.AppendLine(" ,wsc_update_time=getdate()");
            sql.AppendLine(" where wsc_id="+this.GetSqlValueString(wsid));
            return this.DataAccessClient.ExecuteNonQuery(sql.ToString());
        }
        /// <summary>
        /// 更新调休计划
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int UpdateWSChange(List<work_schedule_change> list)
        {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            //插入临时表
            new DLCommon().CreateTmpTable("work_schedule_change");
            this.DataAccessClient.Insert(list, "#work_schedule_change");
            //调休范围改变时，删除原来的调休目标
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("delete from work_schedule_change_target");
            sql.AppendLine("where  wct_id in (select tmp.wsc_id from #work_schedule_change tmp");
            sql.AppendLine("inner join work_schedule_change wsc on wsc.wsc_id = tmp.wsc_id");
            sql.AppendLine("and wsc.wsc_range_type != tmp.wsc_range_type)");
            this.DataAccessClient.ExecuteNonQuery(sql.ToString());
            //只更新部分字段，这里特别指定，防止更新其他字段           
            UpdateFields updfields = new UpdateFields(UpdateFieldsOptions.ExcludeFields, "wsc_create_time"
                , "wsc_create_user");
            return this.DataAccessClient.Update(list, "work_schedule_change", updfields, new string[] { "wsc_id" });
        }
        /// <summary>
        /// 保存调休计划范围
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveWSCTarget(List<work_schedule_change_target> list)
        {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            this.DataAccessClient.Delete(list, "work_schedule_change_target", "wct_id", "wct_target_id");
            return this.DataAccessClient.Insert(list, "work_schedule_change_target");
        }
        /// <summary>
        /// 删除计划目标
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int DeleteWSCTarget(List<work_schedule_change_target> list)
        {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            return this.DataAccessClient.Delete(list, "work_schedule_change_target", "wct_id", "wct_target_id");
        }
        /// <summary>
        /// 根据计划编号删除目标
        /// </summary>
        /// <param name="wsId"></param>
        /// <returns></returns>
        public int DeleteWSCTargetByPlanId(string wsId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" delete from work_schedule_change_target where wct_id=" + this.GetSqlValueString(wsId));
            return this.DataAccessClient.ExecuteNonQuery(sql.ToString());
        }
        /// <summary>
        /// 插入目标
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int InsertWSCTarget(List<work_schedule_change_target> list)
        {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            this.DataAccessClient.Delete(list, "work_schedule_change_target", "wct_id");
            return this.DataAccessClient.Insert(list, "work_schedule_change_target");
        }
        #endregion 调休计划

        #region 考勤补录
        /// <summary>
        /// 取得员工打卡记录
        /// </summary>
        /// <param name="strMonth">月份</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="employeeId">员工编号</param>
        /// <returns></returns>
        public List<V_Crad_Click_Log> GetEmpCardClicks(string strMonth,string companyId,string employeeId)
        {
            List<V_Crad_Click_Log> lst = new List<V_Crad_Click_Log>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select employee_id,card_id");
            sql.AppendLine(", work_date");
            sql.AppendLine(", convert(varchar(5), min_click_time, 108) begin_click_time");
            sql.AppendLine(", convert(varchar(5), max_click_time, 108) end_click_time");
            sql.AppendLine(", '' new_flag");
            sql.AppendLine("from(");
            sql.AppendLine(" select ue.ue_id employee_id");
            sql.AppendLine(" , card_id");
            sql.AppendLine(" , convert(varchar(10), click_time, 120) work_date");
            sql.AppendLine(" , min(click_time) min_click_time");
            sql.AppendLine(" , max(click_time) max_click_time");
            sql.AppendLine(" from emp_card_click_log cll");
            sql.AppendLine(" inner join u_employee ue on ue.ue_card_number = cll.card_id");
            sql.AppendLine(" where convert(varchar(7),click_time,120) =" + this.GetSqlValueString(strMonth));
            sql.AppendLine("  and ue.ue_company_id = "+this.GetSqlValueString(companyId));
            sql.AppendLine("  and ue.ue_id = "+this.GetSqlValueString(employeeId));
            sql.AppendLine("  group by ue.ue_id");
            sql.AppendLine("  , card_id");
            sql.AppendLine("  , convert(varchar(10), click_time, 120))t");           
            sql.AppendLine("order by work_date,begin_click_time");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        /// <summary>
        /// 保存打卡记录
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveCardClick(List<emp_card_click_log> list,bool isDelete) {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            int intResult = -1;
            intResult= this.DataAccessClient.Delete(list, "emp_card_click_log", "card_id", "click_time");
            if (!isDelete)
            {
                intResult= this.DataAccessClient.Insert(list, "emp_card_click_log");
            }
            return intResult;
        }
        #endregion 考勤补录

        #region Excel操作
        /// <summary>
        /// 取得导出数据
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public DataTable GetDataForExport(EmpAttSearch condtion) {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select");
            sql.AppendLine("ue.ue_company_id at_company_id");
            sql.AppendLine(", uc.uc_name at_company_name");
            sql.AppendLine(", ue.ue_id at_emp_id");
            sql.AppendLine(", ue.ue_name at_emp_name");
            sql.AppendLine(", isnull(ue.ue_card_number,ue.ue_id) cardId");
            sql.AppendLine(", ass.as_date at_date");
            sql.AppendLine(", convert(varchar(5), min(click_time), 108)  at_login_time");
            sql.AppendLine(", convert(varchar(5), max(click_time), 108)  at_level_time");
            sql.AppendLine("from u_employee ue");
            sql.AppendLine("inner join attendance_setting ass on ass.as_date>=" + this.GetSqlValueString(condtion.beginDate, this.DateFormatForDB));
            sql.AppendLine(" and ass.as_date <= " + this.GetSqlValueString(condtion.endDate, this.DateFormatForDB));
            sql.AppendLine("inner join u_company uc on uc.uc_id = ue.ue_company_id");
            sql.AppendLine("left join emp_card_click_log cll on cll.card_id = ue.ue_card_number");
            sql.AppendLine("and convert(varchar(10), click_time, 120)= convert(varchar(10), ass.as_date, 120)");
            sql.AppendLine("where 1 = 1");
            if (!string.IsNullOrEmpty(condtion.companyId))
            {
                sql.AppendLine("  and ue.ue_company_id=" + this.GetSqlValueString(condtion.companyId));
            }
            if (!string.IsNullOrEmpty(condtion.empId))
            {
                sql.AppendLine("  and ue.ue_id=" + this.GetSqlValueString(condtion.empId));
            }
            if (!string.IsNullOrEmpty(condtion.empName))
            {
                sql.AppendLine("  and ue.ue_name like " + this.GetLikeSqlValueString(condtion.empName));
            }
            sql.AppendLine("group by  ue.ue_company_id");
            sql.AppendLine(" , uc.uc_name");
            sql.AppendLine("  , ue.ue_id");
            sql.AppendLine("  , ue.ue_name");
            sql.AppendLine(" , ass.as_date,ue.ue_card_number");
            sql.AppendLine("order by ue.ue_company_id , ue.ue_id , ass.as_date");
            return this.DataAccessClient.ExecuteDataTable(sql.ToString());
        }
        /// <summary>
        /// 取得本次Excel保存的数据展示在画面
        /// </summary>
        /// <param name="condtion">查询条件</param>
        /// <param name="batchNo">导入批次号</param>
        /// <returns></returns>
        public List<V_Excel_Card_Click> GetListForExport(EmpAttSearch condtion,string batchNo)
        {
            List<V_Excel_Card_Click> lst = new List<V_Excel_Card_Click>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select");
            sql.AppendLine("ue.ue_company_id companyId");
            sql.AppendLine(", uc.uc_name companyName");
            sql.AppendLine(", ue.ue_id empId");
            sql.AppendLine(", ue.ue_name empName");
            sql.AppendLine(", isnull(ue.ue_card_number,ue.ue_id) cardId");
            sql.AppendLine(", ass.as_date attDate");
            sql.AppendLine(", convert(varchar(5), min(click_time), 108)  at_login_time");
            sql.AppendLine(", convert(varchar(5), max(click_time), 108)  at_level_time");
            sql.AppendLine("from u_employee ue");
            sql.AppendLine("inner join attendance_setting ass on ass.as_date>=" + this.GetSqlValueString(condtion.beginDate, this.DateFormatForDB));
            sql.AppendLine(" and ass.as_date <= " + this.GetSqlValueString(condtion.endDate, this.DateFormatForDB));
            sql.AppendLine("inner join u_company uc on uc.uc_id = ue.ue_company_id");
            sql.AppendLine("inner join emp_card_click_log cll on cll.card_id = ue.ue_card_number");
            sql.AppendLine("and convert(varchar(10), click_time, 120)= convert(varchar(10), ass.as_date, 120)");
            sql.AppendLine("and excel_batch_no = "+this.GetSqlValueString(batchNo));
            sql.AppendLine("where 1 = 1");
            if (!string.IsNullOrEmpty(condtion.companyId))
            {
                sql.AppendLine("  and ue.ue_company_id=" + this.GetSqlValueString(condtion.companyId));
            }
            if (!string.IsNullOrEmpty(condtion.empId))
            {
                sql.AppendLine("  and ue.ue_id=" + this.GetSqlValueString(condtion.empId));
            }
            if (!string.IsNullOrEmpty(condtion.empName))
            {
                sql.AppendLine("  and ue.ue_name like " + this.GetLikeSqlValueString(condtion.empName));
            }
            sql.AppendLine("group by  ue.ue_company_id");
            sql.AppendLine(" , uc.uc_name");
            sql.AppendLine("  , ue.ue_id");
            sql.AppendLine("  , ue.ue_name");
            sql.AppendLine(" , ass.as_date,ue.ue_card_number");
            sql.AppendLine("order by ue.ue_company_id , ue.ue_id , ass.as_date");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        #endregion Excel操作
    }
}
