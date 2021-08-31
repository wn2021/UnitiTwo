using BFC.SDK.Data.DataAccess;
using Model;
using MySqlDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DLSalary: DaoBase
    {
        #region 薪资设置
        /// <summary>
        /// 取得员工当前薪资
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_Employee_Salary> GetEmpSalary(SalarySearch condition)
        {
            List<V_Employee_Salary> lst = new List<V_Employee_Salary>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ues.ues_id");
            sql.AppendLine(" , ue.ue_id ues_emp_id");
            sql.AppendLine(" , ue.ue_name use_emp_name");
            sql.AppendLine(" , ue.ue_company_id ues_company_id");
            sql.AppendLine(" , uc.uc_name ues_company_name");
            sql.AppendLine(" , ud.ud_name ues_department_name");
            sql.AppendLine(" , upl.upl_name ues_position_level");
            sql.AppendLine(" , use_bank_card");
            sql.AppendLine(" , ues_salary");
            sql.AppendLine(" , isnull(ues_start_date,isnull(max_ues_date,ue.ue_entry_time)) ues_start_date");
            sql.AppendLine(" , ues_end_date");
            sql.AppendLine(" , ull_salary_min ues_salary_min");
            sql.AppendLine(" , ull_salary_max ues_salary_max");
            sql.AppendLine(" from u_employee ue");
            sql.AppendLine(" inner join u_position_level upl on upl.upl_id = ue.ue_position_level");
            sql.AppendLine("  and upl.upl_department_id = ue.ue_department_id");
            sql.AppendLine("  and upl.upl_company_id = ue.ue_company_id");
            sql.AppendLine(" inner join u_department ud on ud.ud_id = ue.ue_department_id");
            sql.AppendLine("  and ud.ud_company_id = ue.ue_company_id");
            sql.AppendLine(" inner join u_company uc on uc.uc_id = ue.ue_company_id");
            //取得最后设置薪资的日期
            sql.AppendLine(" left join (");
            sql.AppendLine("   select max(ues_end_date) max_ues_date, ues_company_id, ues_emp_id");
            sql.AppendLine("   from u_emlpoyee_salary where ues_end_date < " + this.GetSqlValueString(condition.currentDate, this.DateFormatForDB));
            sql.AppendLine("   group by ues_company_id, ues_emp_id) mx on mx.ues_company_id = ue.ue_company_id");
            sql.AppendLine(" and mx.ues_emp_id = ue.ue_id");
            //取得当前薪资设置
            sql.AppendLine(" left join u_emlpoyee_salary ues on ues.ues_emp_id = ue.ue_id");
            sql.AppendLine("  and ues.ues_company_id=ue.ue_company_id ");
            sql.AppendLine("  and ues.ues_start_date <= "+this.GetSqlValueString(condition.currentDate,this.DateFormatForDB));
            sql.AppendLine("  and (ues.ues_end_date >= " + this.GetSqlValueString(condition.currentDate, this.DateFormatForDB));
            sql.AppendLine("   or ues.ues_end_date is null)");
            sql.AppendLine(" left join u_level_salary uls on uls.ull_company_id = upl.upl_company_id");
            sql.AppendLine(" and uls.ull_department_id = upl.upl_department_id");
            sql.AppendLine(" and uls.ull_position_level = upl.upl_id");
            sql.AppendLine(" where ue.ue_status = '10'");
            if (!string.IsNullOrEmpty(condition.companyId)) {
                sql.AppendLine("and ue.ue_company_id = "+this.GetSqlValueString(condition.companyId));
            }
            if (!string.IsNullOrEmpty(condition.departmentId))
            {
                sql.AppendLine("and ue.ue_department_id = " + this.GetSqlValueString(condition.departmentId));
            }
            if (!string.IsNullOrEmpty(condition.positionLevel))
            {
                sql.AppendLine("and ue.ue_position_level = " + this.GetSqlValueString(condition.positionLevel));
            }
            sql.AppendLine(" order by ue.ue_company_id,ue.ue_department_id,ue.ue_position_level,ue.ue_id");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        /// <summary>
        /// 取得职级薪资范围
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_Level_Salary> GetLevelSalary(SalarySearch condition)
        {
            List<V_Level_Salary> lst = new List<V_Level_Salary>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select upl.upl_id pl_position_level_id");
            sql.AppendLine(", upl.upl_name pl_position_level");
            sql.AppendLine(", upl.upl_company_id pl_company_id");
            sql.AppendLine(", uc.uc_name pl_company_name");
            sql.AppendLine(", upl.upl_department_id pl_department_id");
            sql.AppendLine(", ud.ud_name pl_department_name");
            sql.AppendLine(", ull_salary_min pl_salary_min");
            sql.AppendLine(", ull_salary_max pl_salary_max");
            sql.AppendLine(" from u_position_level upl");
            sql.AppendLine(" inner join u_department ud on ud.ud_id = upl.upl_department_id");
            sql.AppendLine("  and ud.ud_company_id = upl.upl_company_id");
            sql.AppendLine(" inner join u_company uc on uc.uc_id = upl.upl_company_id");
            sql.AppendLine(" left join u_level_salary ull on ull.ull_position_level = upl.upl_id");
            sql.AppendLine("  and ull.ull_department_id = upl.upl_department_id");
            sql.AppendLine("  and ull.ull_company_id = upl.upl_company_id");
            sql.AppendLine(" where upl.upl_status = '1'");
            if (!string.IsNullOrEmpty(condition.companyId))
            {
                sql.AppendLine(" and upl.upl_company_id = " + this.GetSqlValueString(condition.companyId));
            }
            if (!string.IsNullOrEmpty(condition.departmentId))
            {
                sql.AppendLine(" and upl.upl_department_id = " + this.GetSqlValueString(condition.departmentId));
            }
            if (!string.IsNullOrEmpty(condition.positionLevel))
            {
                sql.AppendLine(" and upl.upl_id = " + this.GetSqlValueString(condition.positionLevel));
            }
            sql.AppendLine(" order by upl.upl_company_id,upl.upl_department_id,upl.upl_id");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        /// <summary>
        /// 保存职级薪资范围
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveLevelSalary(List<u_level_salary> list) {
            this.CreateTmpTable("u_level_salary");
            this.DataAccessClient.Insert(list, "#u_level_salary");
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("merge into u_level_salary t1");
            sql.AppendLine("using #u_level_salary tmp");
            sql.AppendLine("on tmp.ull_company_id = t1.ull_company_id");
            sql.AppendLine("and tmp.ull_department_id = t1.ull_department_id");
            sql.AppendLine("and tmp.ull_position_level = t1.ull_position_level");
            sql.AppendLine("when matched then update");
            sql.AppendLine("set t1.ull_salary_min = tmp.ull_salary_min");
            sql.AppendLine(", t1.ull_salary_max = tmp.ull_salary_max");
            sql.AppendLine(", t1.ull_update_user = tmp.ull_update_user");
            sql.AppendLine(", t1.ull_update_time = tmp.ull_update_time");
            sql.AppendLine("when not matched then insert");
            sql.AppendLine("(ull_company_id, ull_department_id, ull_position_level, ull_salary_min");
            sql.AppendLine(", ull_salary_max, ull_create_user, ull_create_time, ull_update_user, ull_update_time)");
            sql.AppendLine("values(tmp.ull_company_id, tmp.ull_department_id, tmp.ull_position_level, tmp.ull_salary_min");
            sql.AppendLine(", tmp.ull_salary_max, tmp.ull_create_user, tmp.ull_create_time, tmp.ull_update_user, tmp.ull_update_time);");
            int intResult = this.DataAccessClient.ExecuteNonQuery(sql.ToString());
            return intResult;

        }
        /// <summary>
        /// 创建临时表
        /// </summary>
        /// <param name="tableName"></param>
        public void CreateTmpTable(string tableName)
        {
            string tmp = "#" + tableName;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * into {0}", tmp).AppendLine();
            sql.AppendFormat("from {0} where 1 = 0", tableName).AppendLine();
            this.DataAccessClient.ExecuteNonQuery(sql.ToString());
            
        }
        /// <summary>
        /// 保存员工薪资信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveEmpSalary(List<u_emlpoyee_salary> list)
        {
            //创建临时表
            this.CreateTmpTable("u_emlpoyee_salary");
            //删除自增长主键
            StringBuilder sql = new StringBuilder();
            sql.Append(" alter table #u_emlpoyee_salary drop column ues_id");
            this.DataAccessClient.ExecuteNonQuery(sql.ToString());
            //增加主键
            sql.Remove(0, sql.Length);
            sql.Append(" alter table #u_emlpoyee_salary add ues_id int ");
            this.DataAccessClient.ExecuteNonQuery(sql.ToString());
            //插入临时表
            this.DataAccessClient.Insert(list, "#u_emlpoyee_salary");
            //更新现有数据
            DateTime preDay = DateTime.Today.AddDays(-1);
            sql.Remove(0, sql.Length);
            sql.AppendLine(" update rt ");
            sql.AppendLine(" set rt.ues_salary = tmp.ues_salary ");
            sql.AppendLine(" ,rt.use_bank_card = tmp.use_bank_card ");
            sql.AppendLine(" ,rt.ues_start_date = tmp.ues_start_date ");
            sql.AppendLine(" ,rt.ues_end_date = tmp.ues_end_date");
            sql.AppendLine(" ,rt.ues_update_time = tmp.ues_update_time ");
            sql.AppendLine(" ,rt.ues_update_user = tmp.ues_update_user ");
            sql.AppendLine(" from u_emlpoyee_salary rt  ");
            sql.AppendLine(" inner join  #u_emlpoyee_salary tmp  ");
            sql.AppendLine(" on tmp.ues_id=rt.ues_id  ");
            int intResult = this.DataAccessClient.ExecuteNonQuery(sql.ToString());
            //插入新数据
            sql.Remove(0, sql.Length);
            sql.AppendLine(" insert into u_emlpoyee_salary(");
            sql.AppendLine(" ues_emp_id,ues_company_id,ues_salary,use_bank_card,ues_start_date,ues_end_date,ues_create_time,ues_create_user,ues_update_time,ues_update_user)");
            sql.AppendLine(" select ues_emp_id,ues_company_id,ues_salary,use_bank_card,ues_start_date,ues_end_date,ues_create_time,ues_create_user,ues_update_time,ues_update_user");
            sql.AppendLine(" from #u_emlpoyee_salary tmp  ");
            sql.AppendLine(" where ues_id is null ");
            intResult += this.DataAccessClient.ExecuteNonQuery(sql.ToString());
            return intResult;
        }
        #endregion 薪资设置
        /// <summary>
        /// 取得待发放工资
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_Month_Salary> GetEmployeeSalaries(SalarySearchTwo condition,bool isHistory)
        {
            List<V_Month_Salary> lst = new List<V_Month_Salary>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ems.ems_company_id company_id");
            sql.AppendLine(", uc.uc_name company_name");
            sql.AppendLine(", ue.ue_department_id department_id");
            sql.AppendLine(", ud.ud_name department_name");
            sql.AppendLine(", ems.ems_emp_id employee_id");
            sql.AppendLine(", ue.ue_name employee_name");
            sql.AppendLine(", ems.ems_month");
            sql.AppendLine(", convert(varchar(10),ems.ems_begin_date,120) ems_begin_date");
            sql.AppendLine(", convert(varchar(10),ems.ems_end_date,120) ems_end_date");
            sql.AppendLine(", ems_month_salary");
            sql.AppendLine(", ems_month_bonus");
            sql.AppendLine("from employee_month_salary ems");
            sql.AppendLine("inner join u_employee ue on ue.ue_id = ems.ems_emp_id");
            sql.AppendLine("and ue.ue_company_id = ems.ems_company_id");
            sql.AppendLine("  left join u_company uc on uc.uc_id = ems.ems_company_id");
            sql.AppendLine("  left join u_department ud on ud.ud_id = ue.ue_department_id");
            sql.AppendLine("  and ud.ud_company_id = ue.ue_company_id");
            if (isHistory)
            {
                //历史查询
                sql.AppendLine(" where ems.ems_delivery_flag = '1'");
            }
            else
            {
                //待发放查询
                sql.AppendLine("  where ems.ems_delivery_flag != '1'");
            }
            if (!string.IsNullOrEmpty(condition.companyId)) {
                sql.AppendLine("  and ems.ems_company_id="+this.GetSqlValueString(condition.companyId));
            }
            if (!string.IsNullOrEmpty(condition.departmentId))
            {
                sql.AppendLine("  and ue.ue_department_id=" + this.GetSqlValueString(condition.companyId));
            }
            if (!string.IsNullOrEmpty(condition.employeeId))
            {
                sql.AppendLine("  and ems.ems_emp_id=" + this.GetSqlValueString(condition.employeeId));
            }
            if (!string.IsNullOrEmpty(condition.salaryMonth))
            {
                sql.AppendLine("  and ems.ems_month=" + this.GetSqlValueString(condition.salaryMonth));
            }
            sql.AppendLine("  order by ems.ems_emp_id, ems_month");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) { return null; }
            return lst;
        }
        /// <summary>
        /// 收集考勤数据
        /// </summary>
        /// <param name="strMonth">考勤月份yyyy-MM</param>
        public void CalculateSalary(string strMonth)
        {
            IDataParameter[] param = new IDataParameter[1];
            param[0] = this.DataAccessClient.GetDataParameterInstance("calcMonth", strMonth, ParameterDirection.Input);
            this.DataAccessClient.ExecuteNonQueryBySP("P_Calc_Employee_Salary", param);
        }
        /// <summary>
        /// 取得员工日工资
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_Daily_Salary> GetDailySalaries(SalarySearchTwo condition)
        {
            List<V_Daily_Salary> lst = new List<V_Daily_Salary>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select eds.eds_company_id company_id");
            sql.AppendLine(" , uc.uc_name company_name");
            sql.AppendLine(" , ue.ue_department_id department_id");
            sql.AppendLine(" , ud.ud_name department_name");
            sql.AppendLine(" , eds.eds_emp_id employee_id");
            sql.AppendLine(" , ue.ue_name employee_name");
            sql.AppendLine(" , eds_att_date");
            sql.AppendLine(" , us.sysvalue2 eds_att_flag");
            sql.AppendLine(" , eds_daily_salary");
            sql.AppendLine(" , uw.sysvalue2 eds_work_flag");
            sql.AppendLine(" , eds_salary_param");
            sql.AppendLine(" , eds_real_salary");
            sql.AppendLine("from employee_daily_salary eds");
            sql.AppendLine("inner join u_employee ue on ue.ue_id = eds.eds_emp_id");
            sql.AppendLine("and ue.ue_company_id = eds.eds_company_id");
            sql.AppendLine(" left join u_company uc on uc.uc_id = eds.eds_company_id");
            sql.AppendLine(" left join u_department ud on ud.ud_id = ue.ue_department_id");
            sql.AppendLine(" and ud.ud_company_id = ue.ue_company_id");
            sql.AppendLine(" left join u_systeminfo us on us.sysid = 1005 and us.sysubid != 0");
            sql.AppendLine(" and us.sysvalue = eds_att_flag");
            sql.AppendLine(" left join u_systeminfo uw on uw.sysid = 1005 and uw.sysubid != 0");
            sql.AppendLine(" and uw.sysvalue = eds_work_flag");
            sql.AppendLine(" where 1 = 1");
            if (!string.IsNullOrEmpty(condition.companyId))
            {
                sql.AppendLine("  and eds.eds_company_id=" + this.GetSqlValueString(condition.companyId));
            }            
            if (!string.IsNullOrEmpty(condition.employeeId))
            {
                sql.AppendLine("  and eds.eds_emp_id=" + this.GetSqlValueString(condition.employeeId));
            }
            if (!string.IsNullOrEmpty(condition.salaryMonth))
            {
                sql.AppendLine("  and convert(varchar(7),eds_att_date,120)=" + this.GetSqlValueString(condition.salaryMonth));
                //sql.AppendLine("  and date_format(eds_att_date, '%Y%m')=" + this.GetSqlValueString(condition.salaryMonth));
            }
            sql.AppendLine("  order by eds_att_date");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            //new DBO().FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) { return null; }
            return lst;
        }
        /// <summary>
        /// 更新发放标志（置为1）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SetDeliveried(List<employee_month_salary> list)
        {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            //只更新部分字段，这里特别指定，防止更新其他字段           
            UpdateFields updfields = new UpdateFields(UpdateFieldsOptions.IncludeFields, "ems_delivery_flag"
                , "ems_update_user", "ems_update_time");
            return this.DataAccessClient.Update(list, "employee_month_salary", updfields, new string[] { "ems_month" , "ems_company_id", "ems_emp_id" });
        }
    }
}
