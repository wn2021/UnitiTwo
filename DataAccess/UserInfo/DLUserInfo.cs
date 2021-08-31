using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFC.SDK.Data.DataAccess;

namespace DataAccess
{
    public class DLUserInfo: DaoBase
    {
        
        public u_user GetUserByKeyValue(string _userAccount)
        {
            List<u_user> lst = new List<u_user>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select * from u_user  ");
            sql.AppendLine(" where 1=1");
            sql.AppendLine(" and (uu_name=" + this.GetSqlValueString(_userAccount));
            sql.AppendLine(" or uu_email=" + this.GetSqlValueString(_userAccount));
            sql.AppendLine(" or uu_phone=" + this.GetSqlValueString(_userAccount));
            sql.AppendLine(")");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) { return null; }
            return lst[0];
        }
        /// <summary>
        /// 根据用户ID取得用户信息
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns></returns>
        public u_user GetUserById(string _userId)
        {
            List<u_user> lst = new List<u_user>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select * from u_user  ");
            sql.AppendLine(" where 1=1");
            sql.AppendLine(" and uu_id=" + this.GetSqlValueString(_userId));
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) { return null; }
            return lst[0];
        }
        /// <summary>
        /// 取得用户列表
        /// </summary>
        /// <returns></returns>
        public List<u_user> GetUsers() {
            List<u_user> lst = new List<u_user>();
            this.DataAccessClient.FillQuery(lst, "select * from u_user");
            return lst;
        }
        /// <summary>
        /// 取得用户列表信息
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public List<V_UserInfo> GetUserList()
        {
            List<V_UserInfo> lst = new List<V_UserInfo>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select uu.*, mu_description as uu_type_name");
            sql.AppendLine(" ,fs.description as uu_status_name");
            sql.AppendLine("from u_user uu");
            sql.AppendLine(" left join mu_group ug on ug.mu_id = uu.uu_type");
            sql.AppendLine(" left join u_systeminfo fs on fs.sysid=1000 and fs.sysubid !=0");
            sql.AppendLine(" and fs.sysvalue=uu.uu_status");
            sql.AppendLine("where 1=1");
            //sql.AppendLine("and uu.muu_id in(''");
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    sql.AppendLine("," + this.GetSqlValueString(arr[i]));
            //}
            //sql.AppendLine(")");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(u_user model)
        {
            BFC.SDK.Argument.CheckParameterNull(model, "model");
            int cnt = this.DataAccessClient.Insert(model, "u_user");
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
        /// 插入用户信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public int Insert(List<u_user> modelList)
        {
            BFC.SDK.Argument.CheckParameterNull(modelList, "model");
            int cnt = this.DataAccessClient.Insert(modelList, "u_user");
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
        public int Update(u_user model)
        {
            BFC.SDK.Argument.CheckParameterNull(model, "model");

            //只更新部分字段，这里特别指定，防止更新其他字段           
            UpdateFields updfields = new UpdateFields(UpdateFieldsOptions.IncludeFields
                , nameof(model.UU_NAME), nameof(model.UU_EMAIL), nameof(model.UU_PHONE), nameof(model.UU_ADDRESS)
                , nameof(model.UU_STATUS), nameof(model.UU_UPDATE_USER), nameof(model.UU_UPDATE_TIME)
                , nameof(model.UU_PASSWORD), nameof(model.UU_TYPE));

            return this.DataAccessClient.Update(model, "u_user", updfields,new string[] { nameof(model.UU_ID) });           
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(List<u_user> model) {
            BFC.SDK.Argument.CheckParameterNull(model, "model");
            //只更新部分字段，这里特别指定，防止更新其他字段           
            UpdateFields updfields = new UpdateFields(UpdateFieldsOptions.IncludeFields                
                , "UU_STATUS","UU_UPDATE_USER","UU_UPDATE_TIME");
            return this.DataAccessClient.Update(model, "u_user", updfields, new string[] { "UU_ID" });
            
        }       
        /// <summary>
        /// 取得下一个权限组编号
        /// </summary>
        /// <returns></returns>
        public int GetNextUserId()
        {
            object obj = this.DataAccessClient.ExecuteScalar("select max(uu_id) from u_user");
            if (obj == null) { return 1; }
            return int.Parse(obj.ToString()) + 1;
        }
    }
}
