using BFC.FRM.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFC.FRM.Dao;
using Model;
using DataAccess;
using Common;

namespace Business
{
    public class BusinessBase : InfraBusinessBase, IDisposable
    {
        #region IDisposable 成员

        public virtual void Dispose()
        {
            return;
        }

        #endregion
        /// <summary>
        /// 取得系统配置参数
        /// </summary>
        /// <param name="sysid"></param>
        /// <returns></returns>
        public List<V_Systeminfo> GetSystemInfo(string sysid) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLCommon dl = new DLCommon();
                List<V_Systeminfo> result = dl.GetSystemInfo(sysid);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得系统配置参数
        /// </summary>
        /// <param name="sysid"></param>
        /// <param name="syssubid"></param>
        /// <returns></returns>
        public List<V_Systeminfo> GetSystemInfo(string sysid, string syssubid)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLCommon dl = new DLCommon();
                List<V_Systeminfo> result = dl.GetSystemInfo(sysid, syssubid);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得组织下拉框（公司，部门，职级）
        /// </summary>
        /// <param name="orgType"></param>
        /// <param name="companyid"></param>
        /// <param name="depid"></param>
        /// <returns></returns>
        public List<OrganizationSelect> GetOrganizations(OrganizationType orgType, string companyid, string depid) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLCommon dl = new DLCommon();
                List<OrganizationSelect> result = dl.GetOrganizations(orgType, companyid, depid);
                ts.Complete();
                return result;
            }
        }
    }
}
