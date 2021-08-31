using BFC.SDK.Data.DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DLDepartment:DaoBase
    {
        /// <summary>
        /// 取得部门信息
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public List<V_department> GetDepartments(DepartmentSearch condtion) {
            List<V_department> list = new List<V_department>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ud_id");
            sql.AppendLine("  , ud_name");
            sql.AppendLine("  , ud_company_id");
            sql.AppendLine("  , uc_name ud_company_name");
            sql.AppendLine("  , ud_status");
            sql.AppendLine("  , us.description ud_status_name");
            sql.AppendLine("  , ud_create_time");
            sql.AppendLine("  , ud_create_user");
            sql.AppendLine("  , ud_update_time");
            sql.AppendLine("  , ud_update_user");
            sql.AppendLine("  FROM u_department ud");
            sql.AppendLine("  inner join u_company uc on uc.uc_id = ud.ud_company_id");
            sql.AppendLine("  left join u_systeminfo us on us.sysid = 1002");
            sql.AppendLine("  and us.sysubid <> 0");
            sql.AppendLine("  and us.sysvalue = ud.ud_status");
            sql.AppendLine("  where 1=1 ");
            if (!string.IsNullOrEmpty(condtion.companyId)) {
                sql.AppendLine(" and  ud_company_id="+this.GetSqlValueString(condtion.companyId));
            }
            if (!string.IsNullOrEmpty(condtion.departmentId))
            {
                sql.AppendLine(" and  ud_id=" + this.GetSqlValueString(condtion.departmentId));
            }
            if (!string.IsNullOrEmpty(condtion.departmentName))
            {
                sql.AppendLine(" and  ud_name like " + this.GetLikeSqlValueString(condtion.departmentName));
            }
            if (!string.IsNullOrEmpty(condtion.deptstatus))
            {
                sql.AppendLine(" and  ud_status=" + this.GetSqlValueString(condtion.deptstatus));
            }
            else {
                sql.AppendLine(" and  ud_status !='9'");//默认不显示已删除的部门
            }

         this.DataAccessClient.FillQuery(list, sql.ToString());
            return list;
        }
        /// <summary>
        /// 根据主键取得部门信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        public u_department GetDeptByKeyValue(string companyId,string depId) {
            List<u_department> list = new List<u_department>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select * from u_department where 1=1");
            sql.AppendLine(" and ud_company_id ="+this.GetSqlValueString(companyId));
            sql.AppendLine(" and ud_id =" + this.GetSqlValueString(depId));
            this.DataAccessClient.FillQuery(list, sql.ToString());
            if (list != null && list.Count > 0) {
                return list[0];
            }
            return null;
        }
        /// <summary>
        /// 取得职级信息
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public List<V_position_level> GetDeptPositionLevels(PositionLeveltSearch condtion)
        {
            List<V_position_level> list = new List<V_position_level>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT upl.* ");
            sql.AppendLine(", uc.uc_name upl_company_name");
            sql.AppendLine(", ud.ud_name upl_department_name");
            sql.AppendLine(", us.description upl_status_name");
            sql.AppendLine("FROM u_position_level upl");
            sql.AppendLine("inner join u_company uc on upl.upl_company_id = uc.uc_id");
            sql.AppendLine("inner join u_department ud on ud.ud_id = upl.upl_department_id");
            sql.AppendLine("left join u_systeminfo us on us.sysid = 1002 and us.sysubid <> 0");
            sql.AppendLine("and us.sysvalue = upl.upl_status");
            sql.AppendLine("  where 1=1 ");
            if (!string.IsNullOrEmpty(condtion.companyId))
            {
                sql.AppendLine(" and  upl_company_id=" + this.GetSqlValueString(condtion.companyId));
            }
            if (!string.IsNullOrEmpty(condtion.departmentId))
            {
                sql.AppendLine(" and  upl_department_id=" + this.GetSqlValueString(condtion.departmentId));
            }
            if (!string.IsNullOrEmpty(condtion.levelId))
            {
                sql.AppendLine(" and  upl_id=" + this.GetSqlValueString(condtion.levelId));
            }
            if (!string.IsNullOrEmpty(condtion.levelname))
            {
                sql.AppendLine(" and  upl_name like " + this.GetLikeSqlValueString(condtion.levelname));
            }
            if (!string.IsNullOrEmpty(condtion.levelstatus))
            {
                sql.AppendLine(" and  upl_status=" + this.GetSqlValueString(condtion.levelstatus));
            }
            else
            {
                sql.AppendLine(" and  upl_status !='9'");//默认不显示已删除的部门
            }

            this.DataAccessClient.FillQuery(list, sql.ToString());
            return list;
        }
        /// <summary>
        /// 根据主键取得职级信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        public u_position_level GetLevelByKeyValue(string companyId, string depId,string levelId)
        {
            List<u_position_level> list = new List<u_position_level>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select * from u_position_level where 1=1");
            sql.AppendLine(" and upl_company_id =" + this.GetSqlValueString(companyId));
            sql.AppendLine(" and upl_department_id =" + this.GetSqlValueString(depId));
            sql.AppendLine(" and upl_id =" + this.GetSqlValueString(levelId));
            this.DataAccessClient.FillQuery(list, sql.ToString());
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        /// <summary>
        /// 取得新一个部门编号
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public string GetNextDepartId(string companyId)
        {
            int maxNo = 1;
            string tmp = "000";
            object obj = this.DataAccessClient.ExecuteScalar("select max(ud_id) from u_department where ud_company_id="+this.GetSqlValueString(companyId));
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {

                int.TryParse(obj.ToString(), out maxNo);
                maxNo++;
            }
            tmp = tmp + maxNo.ToString();
            return tmp.Substring(tmp.Length - 3, 3);
        }
        /// <summary>
        /// 插入部门信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int InsertDept(List<u_department> list) {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            return this.DataAccessClient.Insert(list, "u_department");
        }
        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int UpdateDept(List<u_department> list) {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            //只更新部分字段，这里特别指定，防止更新其他字段           
            UpdateFields updfields = new UpdateFields(UpdateFieldsOptions.ExcludeFields, "ud_id"
                , "ud_create_time", "ud_create_user");
            return  this.DataAccessClient.Update(list, "u_department", updfields, new string[] { "ud_id", "ud_company_id" });
        }
        /// <summary>
        /// 取得新一个职级编号
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depid"></param>
        /// <returns></returns>
        public string GetNextLevelId(string companyid,string depid)
        {
            int maxNo = 1;
            string tmp = "0000";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select max(upl_id) from u_position_level where 1=1");
            sql.AppendLine(" and upl_company_id="+this.GetSqlValueString(companyid));
            sql.AppendLine(" and upl_department_id=" + this.GetSqlValueString(depid));
            object obj = this.DataAccessClient.ExecuteScalar(sql.ToString());
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {

                int.TryParse(obj.ToString(), out maxNo);
                maxNo++;
            }
            tmp = tmp + maxNo.ToString();
            return tmp.Substring(tmp.Length - 4, 4);
        }
        /// <summary>
        /// 插入部门信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int InsertLevel(List<u_position_level> list)
        {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            return this.DataAccessClient.Insert(list, "u_position_level");
        }
        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int UpdateLevel(List<u_position_level> list)
        {
            BFC.SDK.Argument.CheckParameterNull(list, "model");
            //只更新部分字段，这里特别指定，防止更新其他字段           
            UpdateFields updfields = new UpdateFields(UpdateFieldsOptions.ExcludeFields, "upl_id"
                , "upl_create_time", "upl_create_user");
            return this.DataAccessClient.Update(list, "u_position_level", updfields, new string[] { "upl_id","upl_company_id", "upl_department_id" });
        }
    }
}
