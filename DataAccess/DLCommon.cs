using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Common;

namespace DataAccess
{
    public class DLCommon:DaoBase
    {
        /// <summary>
        /// 取得系统配置参数
        /// </summary>
        /// <param name="sysid"></param>
        /// <returns></returns>
        public List<V_Systeminfo> GetSystemInfo(string sysid)
        {
            List<V_Systeminfo> lst = new List<V_Systeminfo>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from u_systeminfo where sysubid !=0");
            sql.AppendLine(" and sysid = " + this.GetSqlValueString(sysid));
            sql.AppendLine(" order by sort");
            this.DataAccessClient.FillQuery(lst, sql.ToString());

            return lst;
        }
        public List<V_Systeminfo> GetSystemInfo(string sysid,string syssubid)
        {
            List<V_Systeminfo> lst = new List<V_Systeminfo>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from u_systeminfo where sysubid ="+this.GetSqlValueString(syssubid));
            sql.AppendLine(" and sysid = " + this.GetSqlValueString(sysid));
            sql.AppendLine(" order by sort");
            this.DataAccessClient.FillQuery(lst, sql.ToString());

            return lst;
        }
        /// <summary>
        /// 取得组织选择下拉框（公司，部门，职级）
        /// </summary>
        /// <param name="orgType">组织类型</param>
        /// <param name="companyid">公司编号</param>
        /// <param name="depid">部门编号</param>
        /// <returns></returns>
        public List<OrganizationSelect> GetOrganizations(OrganizationType orgType,string companyid,string depid) {
            List<OrganizationSelect> lst = new List<OrganizationSelect>();
            StringBuilder sql = new StringBuilder();
            if (orgType == OrganizationType.Company) {
                sql.AppendLine(" select uc_id as OrganizationId");
                sql.AppendLine(" ,uc_name as OrganizationName");
                sql.AppendLine(" ,'0' as OrganizationType");
                sql.AppendLine(" from u_company");
                sql.AppendLine(" where uc_status !='9'");
                this.DataAccessClient.FillQuery(lst, sql.ToString());
            }
            else if (orgType == OrganizationType.Department)
            {
                sql.AppendLine(" select ud_id as OrganizationId");
                sql.AppendLine(" ,ud_name as OrganizationName");
                sql.AppendLine(" ,'1' as OrganizationType");
                sql.AppendLine(" from u_department");
                sql.AppendLine(" where ud_status !='9'");
                if (!string.IsNullOrEmpty(companyid))
                {
                    sql.AppendLine(" and ud_company_id=" + this.GetSqlValueString(companyid));
                }
                this.DataAccessClient.FillQuery(lst, sql.ToString());
            }
            else if (orgType == OrganizationType.PositionLevel)
            {
                sql.AppendLine(" select upl_id as OrganizationId");
                sql.AppendLine(" ,upl_name as OrganizationName");
                sql.AppendLine(" ,'2' as OrganizationType");
                sql.AppendLine(" from u_position_level");
                sql.AppendLine(" where upl_status !='9'");
                if (!string.IsNullOrEmpty(companyid))
                {
                    sql.AppendLine(" and upl_company_id=" + this.GetSqlValueString(companyid));
                }
                if (!string.IsNullOrEmpty(depid))
                {
                    sql.AppendLine(" and upl_department_id=" + this.GetSqlValueString(depid));
                }
                this.DataAccessClient.FillQuery(lst, sql.ToString());
            }           
            return lst;
        }
        public void CreateTmpTable(string tableName)
        {
            string tmp = "#" + tableName;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * into {0}", tmp).AppendLine();
            sql.AppendFormat("from {0} where 1 = 0", tableName).AppendLine();
            this.DataAccessClient.ExecuteNonQuery(sql.ToString());
        }
    }
}
