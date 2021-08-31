using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BFC.SDK.Data.DataAccess;

namespace DataAccess
{
    public class DLGroup:DaoBase
    {
        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(mu_group model)
        {
            BFC.SDK.Argument.CheckParameterNull(model, "model");
            int cnt = this.DataAccessClient.Insert(model, "mu_group");
            if (cnt == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(mu_group model)
        {
            BFC.SDK.Argument.CheckParameterNull(model, "model");

            //只更新部分字段，这里特别指定，防止更新其他字段           
            UpdateFields updfields = new UpdateFields(UpdateFieldsOptions.ExcludeFields,nameof(model.mu_id)
                ,nameof(model.mu_update_time),nameof(model.mu_update_time));
            int cnt = this.DataAccessClient.Update(model, "mu_group", updfields, new string[] { nameof(model.mu_id) });
            if (cnt == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 删除权限组
        /// </summary>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public int Delete(string groupIds,string updateUser) {
            int intResult = -1;
            if (!string.IsNullOrEmpty(groupIds))
            {
                string[] arr = groupIds.Split(',');
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("update mu_group set mu_status='99'");
                sql.AppendLine(" ,mu_update_time = getdate()");
                sql.AppendLine(" ,mu_update_user =" + this.GetSqlValueString(updateUser));
                sql.AppendLine(" where 1=1");
                sql.AppendLine(" and mu_id in (" + this.GetSqlValueString(arr[0]));
                for (int i = 1; i < arr.Length; i++)
                {
                    sql.AppendLine("," + this.GetSqlValueString(arr[i]));
                }
                sql.AppendLine(" )");
                intResult = this.DataAccessClient.ExecuteNonQuery(sql.ToString());
            }
            return intResult;
        }
        /// <summary>
        /// 取得权限组
        /// </summary>
        /// <param name="_groupId"></param>
        /// <returns></returns>
        public List<mu_group> GetGroupById(string _groupId)
        {
            List<mu_group> lst = new List<mu_group>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from mu_group where 1=1");
            if (!string.IsNullOrEmpty(_groupId))
            {
                sql.AppendLine(" and mu_id=" + this.GetSqlValueString(_groupId));
            }
            else
            {
                sql.AppendLine(" and mu_status = '10'");
            }

            this.DataAccessClient.FillQuery(lst, sql.ToString());

            return lst;
        }
        /// <summary>
        /// 根据状态取得权限组
        /// </summary>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public List<mu_group> GetGroupByStatus(string strStatus)
        {
            List<mu_group> lst = new List<mu_group>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from mu_group where 1=1");
            if (!string.IsNullOrEmpty(strStatus))
            {
                sql.AppendLine(" and mu_status = " + this.GetSqlValueString(strStatus));
            }
            this.DataAccessClient.FillQuery(lst, sql.ToString());

            return lst;
        }
        /// <summary>
        /// 根据描述取得权限组
        /// </summary>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public List<mu_group> GetGroupByName(string strDesc)
        {
            List<mu_group> lst = new List<mu_group>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from mu_group where 1=1");
            sql.AppendLine(" and mu_description like " + this.GetLikeSqlValueString(strDesc));
            this.DataAccessClient.FillQuery(lst, sql.ToString());

            return lst;
        }
        /// <summary>
        /// 取得下一个权限组编号
        /// </summary>
        /// <returns></returns>
        public int GetNextGroudId() {
            object obj = this.DataAccessClient.ExecuteScalar("select max(mu_id) from mu_group");
            if (obj == null) { return 1; }
            return int.Parse(obj.ToString()) + 1;
        }
        /// <summary>
        /// 取得关联用户
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public List<V_UserInfo> GetReleatedUser(string[] arr) {
            List<V_UserInfo> lst = new List<V_UserInfo>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select uu.*, mu_description as uu_type_name");
            sql.AppendLine(" ,fs.description as uu_status_name");
            sql.AppendLine("from u_user uu");
            sql.AppendLine("inner join mu_group ug on ug.mu_id = uu.uu_type");
            sql.AppendLine(" left join u_systeminfo fs on fs.sysid=1000 and fs.sysubid !=0");
            sql.AppendLine(" and fs.sysvalue=uu.uu_status");
            sql.AppendLine("where uu_status != '99'");
            sql.AppendLine("and ug.mu_id in(''");
            for (int i = 0; i < arr.Length; i++) {
                sql.AppendLine(","+this.GetSqlValueString(arr[i]));
            }
            sql.AppendLine(")");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
    }
}
