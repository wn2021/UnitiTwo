using BFC.SDK.Data.DataAccess;
using Model;
using MySqlDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DLMenu: DaoBase
    {
        /// <summary>
        /// 取得用户菜单
        /// </summary>
        /// <param name="menuLevel">菜单级别</param>
        /// <param name="parentId">父菜单编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public List<V_Menu> GetUserMenus(int menuLevel,string parentId,string userId) {
            List<V_Menu> lst = new List<V_Menu>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select m_id as id");
            sql.AppendLine(", m_iconCls as iconCls");
            sql.AppendLine(", m_text as text");
            sql.AppendLine(", m_url as url");
            sql.AppendLine(" from menu me");
            sql.AppendLine(" inner join (");
            sql.AppendLine("     select ua_menu_id u_menu_id");
            sql.AppendLine("     from u_user_auth t1");
            sql.AppendLine("     where ua_user_id = "+this.GetSqlValueString(userId));
            sql.AppendLine("     and uu_status = '1'");
            sql.AppendLine("     union");
            sql.AppendLine("     select gm_menu_id u_menu_id");
            sql.AppendLine("     from group_menu_auth t2");
            sql.AppendLine("     inner join u_user t3 on t3.uu_type = t2.gm_group_id");
            sql.AppendLine("     where t3.uu_id = " + this.GetSqlValueString(userId));
            sql.AppendLine("     and gm_status = '1'");
            //设置了用户菜单的，以用户菜单设置为准
            sql.AppendLine("     and not exists(select 1 from u_user_auth where ua_user_id = t3.uu_id and uu_status = '1')");
            sql.AppendLine("      )um on um.u_menu_id = me.m_id");
            sql.AppendLine(" where m_level="+ menuLevel);
            sql.AppendLine(" and m_status='1'");
            if (!string.IsNullOrEmpty(parentId)) {
                sql.AppendLine(" and m_parent_id="+this.GetSqlValueString(parentId));
            }
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) { return null; }
            return lst;
        }
        /// <summary>
        /// 取得菜单全部信息
        /// </summary>
        /// <param name="intLevel">菜单级别</param>
        /// <param name="parentId">父菜单编号</param>
        /// <returns></returns>
        public List<V_Menu_List> GetMenus(int intLevel, string parentId) {
            List<V_Menu_List> lst = new List<V_Menu_List>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select me.* ");
            sql.AppendLine(" ,mp.m_text as m_parent_text");
            sql.AppendLine(" ,fs1.description as m_level_name");
            sql.AppendLine(" ,fs2.description as m_status_name");
            sql.AppendLine(" from menu me");
            sql.AppendLine(" left join menu mp on mp.m_id=me.m_parent_id ");
            sql.AppendLine(" left join u_systeminfo fs1 on fs1.sysid=1001 and fs1.sysubid !=0 and fs1.sysvalue=me.m_level ");
            sql.AppendLine(" left join u_systeminfo fs2 on fs2.sysid=1002 and fs2.sysubid !=0 and fs2.sysvalue=me.m_status ");
            sql.AppendLine(" where 1=1 ");
            if (intLevel >= 0) {
                sql.AppendLine(" and me.m_level = " + intLevel);
            }
            //sql.AppendLine(" and me.m_status='1'");
            if (!string.IsNullOrEmpty(parentId))
            {
                sql.AppendLine(" and me.m_parent_id=" + this.GetSqlValueString(parentId));
            }
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) { return null; }
            return lst;
        }
        /// <summary>
        /// 取得权限组菜单
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="level"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<V_Menu_List> GetGroupMenus(string groupId,int level,string pid)
        {
            List<V_Menu_List> lst = new List<V_Menu_List>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select me.* ");
            sql.AppendLine(" ,mp.m_text as m_parent_text");
            sql.AppendLine(" ,fs1.description as m_level_name");
            sql.AppendLine(" ,fs2.description as m_status_name");
            sql.AppendLine(" ,case when gm.gm_menu_id  is not null then '1' else '0'end auth_flag");
            sql.AppendLine(" from menu me");
            if (string.IsNullOrEmpty(pid))
            {
                sql.AppendLine(" left join menu mp on mp.m_id=me.m_parent_id ");
            }
            else {
                //父菜单要先赋权
                sql.AppendLine(" inner join menu mp on mp.m_id=me.m_parent_id ");
                sql.AppendLine(" inner join group_menu_auth gmp on gmp.gm_menu_id=mp.m_id");
                sql.AppendLine(" and gmp.gm_group_id=" + this.GetSqlValueString(groupId));
                sql.AppendLine(" and gmp.gm_status ='1'");
            }
            sql.AppendLine(" left join u_systeminfo fs1 on fs1.sysid=1001 and fs1.sysubid !=0 and fs1.sysvalue=me.m_level ");
            sql.AppendLine(" left join u_systeminfo fs2 on fs2.sysid=1002 and fs2.sysubid !=0 and fs2.sysvalue=me.m_status ");
            sql.AppendLine(" left join group_menu_auth gm on gm.gm_menu_id = me.m_id and gm.gm_status !='9'");
            sql.AppendLine(" and gm.gm_group_id = " + this.GetSqlValueString(groupId));
            sql.AppendLine(" where 1=1 ");
            if (level >= 0)
            {
                sql.AppendLine(" and me.m_level = " + level);
            }
            sql.AppendLine(" and me.m_status='1'");
            if (!string.IsNullOrEmpty(pid))
            {
                sql.AppendLine(" and me.m_parent_id=" + this.GetSqlValueString(pid));
            }
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) { return null; }
            return lst;
        }
        /// <summary>
        /// 为用户取得菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="level"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<V_Menu_List> GetUserMenus(string userId, int level, string pid)
        {
            List<V_Menu_List> lst = new List<V_Menu_List>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select me.* ");
            sql.AppendLine(" ,mp.m_text as m_parent_text");
            sql.AppendLine(" ,fs1.description as m_level_name");
            sql.AppendLine(" ,fs2.description as m_status_name");
            sql.AppendLine(" ,case when ua.ua_menu_id is not null then '1' else '0'end auth_flag");
            sql.AppendLine(" from menu me");
            if (!string.IsNullOrEmpty(pid)) {
                sql.AppendLine(" inner join menu mp on mp.m_id=me.m_parent_id ");
            }
            else
            {
                sql.AppendLine(" left join menu mp on mp.m_id=me.m_parent_id ");
                //sql.AppendLine(" inner join u_user_auth gmp on gmp.ua_menu_id=me.m_id");
                //sql.AppendLine(" and gmp.ua_user_id=" + this.GetSqlValueString(userId));
                //sql.AppendLine(" and gmp.uu_status ='1'");
            }
            sql.AppendLine(" left join u_systeminfo fs1 on fs1.sysid=1001 and fs1.sysubid !=0 and fs1.sysvalue=me.m_level ");
            sql.AppendLine(" left join u_systeminfo fs2 on fs2.sysid=1002 and fs2.sysubid !=0 and fs2.sysvalue=me.m_status ");
            sql.AppendLine(" left join u_user_auth ua on ua.ua_menu_id=me.m_id and ua.uu_status !='9'");
            sql.AppendLine(" and ua.ua_user_id = " + this.GetSqlValueString(userId));
            sql.AppendLine(" where 1=1 ");
            if (level >= 0)
            {
                sql.AppendLine(" and me.m_level = " + level);
            }
            sql.AppendLine(" and me.m_status='1'");
            if (!string.IsNullOrEmpty(pid))
            {
                sql.AppendLine(" and me.m_parent_id=" + this.GetSqlValueString(pid));
            }
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) { return null; }
            return lst;
        }
        /// <summary>
        /// 保存权限组菜单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveGroupMenus(List<group_menu_auth> list) {
           
            this.DataAccessClient.Delete(list, "group_menu_auth", "gm_group_id", "gm_menu_id");
            return this.DataAccessClient.Insert(list, "group_menu_auth");

        }
        /// <summary>
        /// 根据菜单名称模糊查询
        /// </summary>
        /// <param name="menuLevel">菜单级别</param>
        /// <param name="strText"></param>
        /// <returns></returns>
        public menu GetMenuById(string strId) 
        {
            List<menu> lst = new List<menu>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select * from menu");
            sql.AppendLine(" where 1=1 ");
            if (!string.IsNullOrEmpty(strId)) {
                sql.AppendLine(" and m_id=" + this.GetSqlValueString(strId));
            }
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0) { return null; }
            return lst[0];
        }
        /// <summary>
        /// 取得新菜单编号
        /// </summary>
        /// <param name="level"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string GetNewMenuId(int level,string pid) {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select max(m_id)from menu");
            sql.AppendLine(" where 1 = 1");
            sql.AppendLine(" and m_level = "+ level);
            if (!string.IsNullOrEmpty(pid))
            {
                sql.AppendLine(" and m_parent_id =" + this.GetSqlValueString(pid));
            }
            object obj= DataAccessClient.ExecuteScalar(sql.ToString());
            if (obj == null) {
                if (string.IsNullOrEmpty(pid))
                {
                    return (level + 1).ToString();
                }
                else {
                    return pid+"-"+(level+1).ToString();
                }
            }
            string tmp = obj.ToString();
            if (string.IsNullOrEmpty(pid))
            {
                tmp=(int.Parse(tmp) + 1).ToString();
            }
            else
            {
                if (string.IsNullOrEmpty(tmp))
                {
                    tmp = "1";
                }
                else
                {
                    tmp = (int.Parse(tmp.Substring(pid.Length + 1)) + 1).ToString();
                }
                tmp = pid+"-"+ tmp;
            }
            return tmp;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public int InsertMenu(List<menu> lst) {
            BFC.SDK.Argument.CheckParameterNull(lst, "menu");
            //DBO dbo = new DBO();
            //new DBO().Delete(lst, "menu", "m_id");
            //return new DBO().Insert(lst, "menu");
            this.DataAccessClient.Delete(lst, "menu", "m_id");
            return this.DataAccessClient.Insert(lst, "menu");
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateMenu(List<menu> lst)
        {
            BFC.SDK.Argument.CheckParameterNull(lst, "menu");
            menu model = new menu();
            //只更新部分字段，这里特别指定，防止更新其他字段           
            UpdateFields updfields = new UpdateFields(UpdateFieldsOptions.ExcludeFields
                , nameof(model.m_id), nameof(model.m_create_time), nameof(model.m_create_user));
            return this.DataAccessClient.Update(lst, "menu", updfields,new string[] { "m_id" });
        }
    }
}
