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
    public class BLUserInfo: BusinessBase
    {        
        /// <summary>
        /// 根据用户账户信息取得用户信息
        /// </summary>
        /// <param name="_userAccount"></param>
        /// <returns></returns>
        public u_user GetUserByKeyValue(string _userAccount) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLUserInfo dl = new DLUserInfo();
                u_user result = dl.GetUserByKeyValue(_userAccount);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 根据用户ID取得用户信息
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns></returns>
        public u_user GetUserById(string _userId) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLUserInfo dl = new DLUserInfo();
                u_user result = dl.GetUserById(_userId);
                ts.Complete();
                return result;
            }
        }
        public List<u_user> GetUsers()
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLUserInfo dl = new DLUserInfo();
                List<u_user> result = dl.GetUsers();
                ts.Complete();
                return result;
            }
        }
        public List<V_UserInfo> GetUserList() {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLUserInfo dl = new DLUserInfo();
                List<V_UserInfo> result = dl.GetUserList();
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public int UserSave(u_user u)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLUserInfo dl = new DLUserInfo();
                int result = -1;
                if (u.UU_ID == 0)
                {
                    u.UU_ID = this.GetNextUserId();
                    result= dl.Insert(u);
                }
                else { result= dl.Update(u); }
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(List<u_user> model) {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLUserInfo dl = new DLUserInfo();
                int result = dl.Delete(model);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得新一个用户编号
        /// </summary>
        /// <returns></returns>
        public int GetNextUserId()
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLUserInfo dl = new DLUserInfo();
                int result = dl.GetNextUserId();
                ts.Complete();
                return result;
            }
        }
    }
}
