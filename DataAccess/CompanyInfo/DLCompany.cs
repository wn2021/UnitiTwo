using BFC.SDK.Data.DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DLCompany : DaoBase
    {
        /// <summary>
        /// 取得公司列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<u_company> GetCompanys(CompanySearch condition) {
            List<u_company> lst = new List<u_company>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from u_company");
            sql.AppendLine(" where 1=1");
            if (!string.IsNullOrEmpty(condition.companyId))
            {
                sql.AppendLine(" and uc_id=" + this.GetSqlValueString(condition.companyId));
            }
            if (!string.IsNullOrEmpty(condition.companyStutus))
            {
                sql.AppendLine(" and uc_status=" + this.GetSqlValueString(condition.companyStutus));
            }
            if (!string.IsNullOrEmpty(condition.companyName))
            {
                sql.AppendLine(" and uc_name like " + this.GetLikeSqlValueString(condition.companyName));
            }
            if (!string.IsNullOrEmpty(condition.corporation))
            {
                sql.AppendLine(" and uc_corporation like " + this.GetLikeSqlValueString(condition.corporation));
            }
            if (!string.IsNullOrEmpty(condition.companyAddress))
            {
                sql.AppendLine(" and uc_current_address like " + this.GetLikeSqlValueString(condition.companyAddress));
            }
            if (condition.registerTime.HasValue)
            {
                sql.AppendLine(" and uc_register_time >= " + this.GetSqlValueString(condition.registerTime, this.DateFormatForDB));
            }
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) { return null; }
            return lst;
        }
        /// <summary>
        /// 根据公司编号取得公司信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public u_company GetCompanyById(string companyId)
        {
            List<u_company> lst = new List<u_company>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from u_company");
            sql.AppendLine(" where 1=1");

            sql.AppendLine(" and uc_id=" + this.GetSqlValueString(companyId));
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) { return null; }
            return lst[0];
        }
        /// <summary>
        /// 取得下一个公司编号
        /// </summary>
        /// <returns></returns>
        public string GetNextCompanyId() {
            int maxNo = 1;
            string tmp = "0000";
            object obj =this.DataAccessClient.ExecuteScalar("select max(uc_id) from u_company");
            if (obj != null && !string.IsNullOrEmpty(obj.ToString())) {
               
                int.TryParse(obj.ToString(),out maxNo);
                maxNo++;
            }
            tmp = tmp + maxNo.ToString();
            return tmp.Substring(tmp.Length - 4, 4);
        }
        /// <summary>
        /// 插入新公司
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public int InsertCompany(List<u_company> lst) {
            BFC.SDK.Argument.CheckParameterNull(lst, "model");
            return this.DataAccessClient.Insert(lst, "u_company");
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public int UpdateCompany(List<u_company> lst)
        {
            BFC.SDK.Argument.CheckParameterNull(lst, "model");

            //只更新部分字段，这里特别指定，防止更新其他字段           
            UpdateFields updfields = new UpdateFields(UpdateFieldsOptions.ExcludeFields, "uc_id"
                , "uc_create_time", "uc_create_user");
           return this.DataAccessClient.Update(lst, "u_company", updfields, new string[] { "uc_id" });           
        }
    }
}
