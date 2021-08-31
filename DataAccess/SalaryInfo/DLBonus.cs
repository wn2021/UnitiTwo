using Model;
using MySqlDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DLBonus : DaoBase
    {
        /// <summary>
        /// 取得奖金计划头档
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_Bonus_Plan_Header> GetBonusPlanList(BonusSearch condition) {
            List<V_Bonus_Plan_Header> lst = new List<V_Bonus_Plan_Header>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select  bph_plan_id");
            sql.AppendLine(" , bph_company_id");
            sql.AppendLine(" , uc.uc_name bph_company_name");
            sql.AppendLine(" , bph_bonus_type");
            sql.AppendLine(" , ut.description bph_bonus_type_name");
            sql.AppendLine(" , bph_bonus_from");
            sql.AppendLine(" , bph_bonus_month");
            sql.AppendLine(" , bph_status");
            sql.AppendLine(" , us.description bph_status_name");
            sql.AppendLine(" , bph_remarks");
            sql.AppendLine("from bonus_plan_header ph");
            sql.AppendLine("left join u_company uc on ph.bph_company_id = uc.uc_id");
            sql.AppendLine("left join u_systeminfo ut on ut.sysid = 3007");
            sql.AppendLine("and ut.sysubid != 0");
            sql.AppendLine("and ut.sysvalue = ph.bph_bonus_type");
            sql.AppendLine("left join u_systeminfo us on us.sysid = 3006");
            sql.AppendLine("and us.sysubid != 0");
            sql.AppendLine("and us.sysvalue = ph.bph_status");
            sql.AppendLine("where 1 = 1");
            if (!string.IsNullOrEmpty(condition.strYear)) {
                sql.AppendLine(" and left(bph_bonus_month,4)=" + this.GetSqlValueString(condition.strYear));
            }
            if (!string.IsNullOrEmpty(condition.companyId))
            {
                sql.AppendLine(" and bph_company_id=" + this.GetSqlValueString(condition.companyId));
            }
            if (!string.IsNullOrEmpty(condition.bonusType))
            {
                sql.AppendLine(" and bph_bonus_type=" + this.GetSqlValueString(condition.bonusType));
            }
            if (!string.IsNullOrEmpty(condition.planStatus))
            {
                sql.AppendLine(" and bph_status=" + this.GetSqlValueString(condition.planStatus));
            }
            if (!string.IsNullOrEmpty(condition.bonusFrom))
            {
                sql.AppendLine(" and bph_bonus_from=" + this.GetSqlValueString(condition.bonusFrom));
            }
            if (!string.IsNullOrEmpty(condition.employeeId))
            {
                sql.AppendLine("and exists(select 1from bonus_plan_detail where bpd_plan_id = bph_plan_id");
                sql.AppendLine("and bpd_company_id = bph_company_id");
                sql.AppendLine("and bpd_employee_id =" + this.GetSqlValueString(condition.employeeId));
                sql.AppendLine(")");
            }
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        /// <summary>
        /// 根据主键取得计划头档
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public V_Bonus_Plan_Header GetBonusPlanById(string planId, string companyId)
        {
            List<V_Bonus_Plan_Header> lst = new List<V_Bonus_Plan_Header>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select  bph_plan_id");
            sql.AppendLine(" , bph_company_id");
            sql.AppendLine(" , uc.uc_name bph_company_name");
            sql.AppendLine(" , bph_bonus_type");
            sql.AppendLine(" , ut.description bph_bonus_type_name");
            sql.AppendLine(" , bph_bonus_from");
            sql.AppendLine(" , bph_bonus_month");
            sql.AppendLine(" , bph_status");
            sql.AppendLine(" , us.description bph_status_name");
            sql.AppendLine(" , bph_remarks");
            sql.AppendLine("from bonus_plan_header ph");
            sql.AppendLine("left join u_company uc on ph.bph_company_id = uc.uc_id");
            sql.AppendLine("left join u_systeminfo ut on ut.sysid = 3007");
            sql.AppendLine("and ut.sysubid != 0");
            sql.AppendLine("and ut.sysvalue = ph.bph_bonus_type");
            sql.AppendLine("left join u_systeminfo us on us.sysid = 3006");
            sql.AppendLine("and us.sysubid != 0");
            sql.AppendLine("and us.sysvalue = ph.bph_status");
            sql.AppendLine("where 1 = 1");
            sql.AppendLine(" and bph_plan_id=" + this.GetSqlValueString(planId));
            sql.AppendLine(" and bph_company_id=" + this.GetSqlValueString(companyId));

            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) {
                return null;
            }
            return lst[0];
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public int InsertPlan(List<bonus_plan_header> lst)
        {
            BFC.SDK.Argument.CheckParameterNull(lst, "model");
            return this.DataAccessClient.Insert(lst, "bonus_plan_header");
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public int UpdatePlan(List<bonus_plan_header> lst)
        {
            //    BFC.SDK.Argument.CheckParameterNull(lst, "model");
            //    string sqlFormat = "update bonus_plan_header set bph_bonus_type={0},bph_bonus_from={1},bph_bonus_month={2},bph_status={3}"
            //            + ",bph_remarks={4},bph_update_user={5},bph_update_time={6} ";
            //    string sqlWhere = " where bph_plan_id={0} and bph_company_id={1}";
            //    string sql = "";
            //    int intResult = 0;
            //    foreach (bonus_plan_header hdr in lst) {
            //        sql = string.Format(sqlFormat,
            //            this.GetSqlValueString(hdr.bph_bonus_type),
            //            this.GetSqlValueString(hdr.bph_bonus_from),
            //            this.GetSqlValueString(hdr.bph_bonus_month),
            //            this.GetSqlValueString(hdr.bph_status),
            //            this.GetSqlValueString(hdr.bph_remarks),
            //            this.GetSqlValueString(hdr.bph_update_user),
            //            this.GetDateTimeSqlString(hdr.bph_update_time.Value, ""));
            //        sql = sql + string.Format(sqlWhere, this.GetSqlValueString(hdr.bph_plan_id), this.GetSqlValueString(hdr.bph_company_id));
            //        intResult += this.ExecuteSql(sql, null);
            //    }
            int intResult =this.DataAccessClient.Update(lst, "bonus_plan_header", "bph_plan_id", "bph_company_id");
            return intResult;
        }
        /// <summary>
        /// 保存明细
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveBonusDtls(List<bonus_plan_detail> list) {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            if (list.Count == 0) { return 0; }
            string strDelete = " delete from bonus_plan_detail where bpd_plan_id={0} and bpd_company_id={1}";
            ////删除明细
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(strDelete, this.GetSqlValueString(list[0].bpd_plan_id), this.GetSqlValueString(list[0].bpd_company_id));
            this.DataAccessClient.ExecuteNonQuery(sql.ToString());

            //删除
            //this.DataAccessClient.Delete(list, "bonus_plan_detail", "bpd_plan_id", "bpd_company_id", "bpd_employee_id");
            //插入
            return this.DataAccessClient.Insert(list, "bonus_plan_detail");          
        }
        /// <summary>
        /// 取得奖金计划明细
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<V_Bonus_Employee> GetBonusPlanDtl(string planId,string companyId)
        {
            List<V_Bonus_Employee> lst = new List<V_Bonus_Employee>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select pd. bpd_employee_id employee_Id");
            sql.AppendLine(",ue.ue_name employee_Name");
            sql.AppendLine(",bpd_bonus_amount bonus_amount");
            sql.AppendLine("from bonus_plan_detail pd");
            sql.AppendLine("inner join u_employee ue on ue.ue_id = pd.bpd_employee_id");
            sql.AppendLine("and ue.ue_company_id = pd.bpd_company_id");
            sql.AppendLine(" and bpd_plan_id=" + this.GetSqlValueString(planId));
            sql.AppendLine(" and bpd_company_id=" + this.GetSqlValueString(companyId));
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        /// <summary>
        /// 取得奖金计划未分配的员工明细
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<V_Bonus_Employee> GetNoBonusEmp(string planId, string companyId)
        {
            List<V_Bonus_Employee> lst = new List<V_Bonus_Employee>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ue_id employee_Id");
            sql.AppendLine(",ue_name employee_Name");
            sql.AppendLine(",0 bonus_amount");
            sql.AppendLine("from u_employee ");
            sql.AppendLine("where not exists(select 1 from bonus_plan_detail where ue_id=bpd_employee_id");
            sql.AppendLine(" and bpd_company_id=ue_company_id");
            sql.AppendLine(" and bpd_plan_id=" + this.GetSqlValueString(planId));
            sql.AppendLine(" and bpd_company_id=" + this.GetSqlValueString(companyId));
            sql.AppendLine(")");
            sql.AppendLine("and ue_status ='10' ");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        /// <summary>
        /// 取得员工月奖金明细
        /// </summary>
        /// <param name="strMonth"></param>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<V_Month_Bonus_Detail> GetBonusDetail(SalarySearchTwo condition)
        {
            List<V_Month_Bonus_Detail> lst = new List<V_Month_Bonus_Detail>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select h.bph_company_id company_id ");
            sql.AppendLine(" , uc.uc_name company_name");
            sql.AppendLine(" , ue.ue_department_id department_id");
            sql.AppendLine(" , ud.ud_name department_name");
            sql.AppendLine(" , d.bpd_employee_id employee_id");
            sql.AppendLine(" , ue.ue_name employee_name");
            sql.AppendLine(" , h.bph_bonus_month bonus_month");
            sql.AppendLine(" , h.bph_bonus_type bonus_type");
            sql.AppendLine(" , us.description bonus_type_name");
            sql.AppendLine(" , h.bph_bonus_from bonus_from");
            sql.AppendLine(" , d.bpd_bonus_amount bonus_amount");
            sql.AppendLine("  from bonus_plan_detail d");
            sql.AppendLine(" inner join bonus_plan_header h on h.bph_plan_id = d.bpd_plan_id");
            sql.AppendLine("and d.bpd_company_id = h.bph_company_id");
            sql.AppendLine("inner join u_company uc on uc.uc_id = h.bph_company_id");
            sql.AppendLine("inner join u_employee ue on ue.ue_id = d.bpd_employee_id");
            sql.AppendLine(" and ue.ue_company_id = d.bpd_company_id");
            sql.AppendLine("inner join u_department ud on ud.ud_company_id = ue.ue_company_id");
            sql.AppendLine("and ud.ud_id = ue.ue_department_id");
            sql.AppendLine("left join u_systeminfo us on us.sysid = 3007 and us.sysubid != 0");
            sql.AppendLine("and us.sysvalue = h.bph_bonus_type");
            sql.AppendLine("where 1=1");
            sql.AppendLine("and h.bph_status = '50'");
            if (!string.IsNullOrEmpty(condition.salaryMonth))
            {
                sql.AppendLine("and h.bph_bonus_month =" + this.GetSqlValueString(condition.salaryMonth));
            }
            if (!string.IsNullOrEmpty(condition.companyId))
            {
                sql.AppendLine("and h.bph_company_id =" + this.GetSqlValueString(condition.companyId));
            }
            if (!string.IsNullOrEmpty(condition.employeeId))
            {
                sql.AppendLine("and d.bpd_employee_id =" + this.GetSqlValueString(condition.employeeId));
            }
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
    }
}
