using BFC.SDK.Data.DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DLEmployee: DaoBase
    {
        /// <summary>
        /// 取得员工信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_employee> GetEmployees(EmployeeSearch condition)
        {
            List<V_employee> lst = new List<V_employee>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ue.ue_id employeeId");
            sql.AppendLine(" , ue.ue_name employeeName");
            sql.AppendLine(" , uc.uc_name companyName");
            sql.AppendLine(" , ud.ud_name departName");
            sql.AppendLine(" , upl.upl_name positionLevel");
            sql.AppendLine(" , ue.* ");
            sql.AppendLine("from u_employee ue");
            sql.AppendLine(" inner join u_department ud on ud.ud_id = ue.ue_department_id");
            sql.AppendLine(" and ud.ud_company_id = ue.ue_company_id");
            //sql.AppendLine(" and ud.ud_status != '9'");
            sql.AppendLine("inner join u_company uc on uc.uc_id = ue.ue_company_id");
            //sql.AppendLine(" and uc.uc_status != '9'");
            sql.AppendLine("inner join u_position_level upl on upl.upl_id = ue.ue_position_level");
            sql.AppendLine(" and upl.upl_company_id = ue.ue_company_id");
            sql.AppendLine(" and upl.upl_department_id = ue.ue_department_id");
            sql.AppendLine("where 1=1");
            if (!string.IsNullOrEmpty(condition.companyId)) {
                sql.AppendLine(" and ue.ue_company_id="+this.GetSqlValueString(condition.companyId));
            }
            if (!string.IsNullOrEmpty(condition.departmentId))
            {
                sql.AppendLine(" and ue.ue_department_id=" + this.GetSqlValueString(condition.departmentId));
            }
            if (!string.IsNullOrEmpty(condition.positionLevel))
            {
                sql.AppendLine(" and ue.ue_position_level=" + this.GetSqlValueString(condition.positionLevel));
            }
            if (!string.IsNullOrEmpty(condition.employeeName))
            {
                sql.AppendLine(" and ue.ue_name like " + this.GetLikeSqlValueString(condition.employeeName));
            }
            sql.AppendLine("order by ue.ue_company_id,ue.ue_department_id,ue.ue_status");
            this.DataAccessClient.FillQuery(lst, sql.ToString());           
            if (lst == null || lst.Count == 0) { return null; }
            return lst;
        }
        /// <summary>
        /// 根据员工号取得员工信息
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public u_employee GetEmployeeById(string empId)
        {
            List<u_employee> lst = new List<u_employee>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from u_employee where ue_id=" + this.GetSqlValueString(empId));
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) { return null; }
            return lst[0];
        }

        /// <summary>
        /// 取得下一个员工编号
        /// </summary>
        /// <returns></returns>
        public string GetNextEmployeeId()
        {
            int maxNo = 1;
            string tmp = "0000000000";
            object obj = this.DataAccessClient.ExecuteScalar("select max(ue_id) from u_employee");
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {

                int.TryParse(obj.ToString(), out maxNo);
                maxNo++;
            }
            tmp = tmp + maxNo.ToString();
            return tmp.Substring(tmp.Length - 10, 10);
        }
        public int InsertEmployee(List<u_employee> lst) {
            BFC.SDK.Argument.CheckParameterNull(lst, "model");
            return this.DataAccessClient.Insert(lst, "u_employee");
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public int UpdateEmployee(List<u_employee> lst)
        {
            BFC.SDK.Argument.CheckParameterNull(lst, "model");

            //只更新部分字段，这里特别指定，防止更新其他字段           
            UpdateFields updfields = new UpdateFields(UpdateFieldsOptions.ExcludeFields, "ue_id"
                , "ue_create_time", "ue_create_user");
            return this.DataAccessClient.Update(lst, "u_employee", updfields, new string[] { "ue_id" });
        }

        
    }
}
