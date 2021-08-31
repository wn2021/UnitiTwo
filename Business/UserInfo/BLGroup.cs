using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFC.FRM.Dao;
using DataAccess;
using Model;
using Common;

namespace Business
{
    public class BLGroup:BusinessBase
    {
        #region 数据更新
        /// <summary>
        /// 保存权限组
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public int GroupSave(mu_group u, ActionType actionType)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLGroup dl = new DLGroup();
                int result = -1;
                if (actionType == ActionType.AddNew)
                {
                    result = dl.Insert(u);
                }
                else { result = dl.Update(u); }
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 删除权限组
        /// </summary>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public int Delete(string groupIds,string updateUser) {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLGroup dl = new DLGroup();
                int result = dl.Delete(groupIds, updateUser); 
                ts.Complete();
                return result;
            }
        }
        #endregion 数据更新

        #region 数据获取
        /// <summary>
        /// 取得用户权限组
        /// </summary>
        /// <param name="_groupId"></param>
        /// <returns></returns>
        public List<mu_group> GetGroupById(string _groupId)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLGroup dl = new DLGroup();
                List<mu_group> result = dl.GetGroupById(_groupId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 根据状态取得权限组
        /// </summary>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public List<mu_group> GetGroupByStatus(string strStatus)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLGroup dl = new DLGroup();
                List<mu_group> result = dl.GetGroupByStatus(strStatus);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 根据模糊描述查询权限组
        /// </summary>
        /// <param name="strDesc"></param>
        /// <returns></returns>
        public List<mu_group> GetGroupByName(string strDesc)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLGroup dl = new DLGroup();
                List<mu_group> result = dl.GetGroupByName(strDesc);
                ts.Complete();
                return result;
            }
        }
        public int GetNextGroudId()
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLGroup dl = new DLGroup();
                int result = dl.GetNextGroudId();
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得跟当前权限组相关的用户
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        public List<V_UserInfo> GetReleatedUser(string groups)
        {
            return GetReleatedUser(groups.Split(','));
        }
        /// <summary>
        /// 取得跟当前权限组相关的用户
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public List<V_UserInfo> GetReleatedUser(string[] arr) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLGroup dl = new DLGroup();
                List<V_UserInfo> result = dl.GetReleatedUser(arr);
                ts.Complete();
                return result;
            }
        }
        #endregion 数据获取
    }
}
