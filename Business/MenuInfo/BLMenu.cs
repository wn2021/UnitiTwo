using BFC.FRM.Dao;
using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BLMenu : BusinessBase
    {
        /// <summary>
        /// 取得用户菜单
        /// </summary>
        /// <param name="menuLevel">菜单级别</param>
        /// <param name="parentId">父菜单编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public List<V_Menu> GetUserMenus(int menuLevel, string parentId, string userId)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLMenu dl = new DLMenu();
                List<V_Menu> result = dl.GetUserMenus(menuLevel, parentId, userId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得用户菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<V_Menu1Zero> GetUserMenus(string userId) {
            List<V_Menu1Zero> list = new List<V_Menu1Zero>();
            List<V_Menu> orgLst = null;
            //第1层
            orgLst = this.GetUserMenus(0,"", userId);
            if (orgLst == null || orgLst.Count == 0) {
                return list;
            }
            //第2层
            V_Menu1Zero mp = null;
            foreach (V_Menu me in orgLst) {
                mp = new V_Menu1Zero();
                mp.id = me.id;
                mp.iconCls = me.iconCls;
                mp.text = me.text;
                mp.url = me.url;
                //第3层
                List < V_Menu > clist= this.GetUserMenus(1, mp.id, userId);
                if (clist != null && clist.Count > 0)
                {
                    V_MenuOne mp1 = null;
                    List<V_MenuOne> lstOne = new List<V_MenuOne>();
                    foreach (V_Menu mee in clist)
                    {
                        mp1 = new V_MenuOne();
                        mp1.id = mee.id;
                        mp1.iconCls = mee.iconCls;
                        mp1.text = mee.text;
                        mp1.url = mee.url;
                        mp1.children = this.GetUserMenus(2, mp1.id, userId);
                        lstOne.Add(mp1);
                        
                    }
                    mp.children = lstOne;
                }
                list.Add(mp);
            }
            return list;
        }
        /// <summary>
        /// 取得菜单信息
        /// </summary>
        /// <param name="intLevel">菜单级别</param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<V_Menu_List> GetMenus(int intLevel, string parentId) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLMenu dl = new DLMenu();
                List< V_Menu_List> result = dl.GetMenus(intLevel, parentId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得当前所有有效菜单
        /// </summary>
        /// <returns></returns>
        public List<V_Menu_List> GetMenus() {
            List<V_Menu_List> menuList = new List<V_Menu_List>();
            for (int i = 0; i < 3; i++)
            {
                List<V_Menu_List> lst = this.GetMenus(i, "");
                if (lst != null && lst.Count > 0) { menuList.AddRange(lst); }

            }
            return menuList.OrderBy(m => m.m_id).ToList();
        }
        /// <summary>
        /// 根据编号取得菜单
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        public menu GetMenuById(string strId) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLMenu dl = new DLMenu();
                menu result = dl.GetMenuById(strId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 为权限组取得菜单
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="level"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<V_Menu_List> GetGroupMenus(string groupId, int level, string pid) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLMenu dl = new DLMenu();
                List<V_Menu_List> result = dl.GetGroupMenus(groupId, level,pid);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 保存权限组菜单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveGroupMenus(List<group_menu_auth> list) {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLMenu dl = new DLMenu();
                int result = dl.SaveGroupMenus(list);
                ts.Complete();
                return result;
            }
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
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLMenu dl = new DLMenu();
                List<V_Menu_List> result = dl.GetUserMenus(userId, level, pid);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得新菜单编号
        /// </summary>
        /// <param name="level">菜单层级</param>
        /// <param name="pid">父菜单编号</param>
        /// <returns></returns>
        public string GetNewMenuId(int level, string pid) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLMenu dl = new DLMenu();
                string result = dl.GetNewMenuId(level, pid);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public int InsertMenu(List<menu> lst) {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLMenu dl = new DLMenu();
                int intResult = dl.InsertMenu(lst);
                ts.Complete();
                return intResult;
            }
        }
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public int UpdateMenu(List<menu> lst)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLMenu dl = new DLMenu();
                int intResult = dl.UpdateMenu(lst);
                ts.Complete();
                return intResult;
            }
        }
    }
}
