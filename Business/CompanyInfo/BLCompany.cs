using BFC.FRM.Dao;
using Common;
using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BLCompany:BusinessBase
    {
        /// <summary>
        /// 取得公司列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<u_company> GetCompanys(CompanySearch condition) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLCompany dl = new DLCompany();
                List<u_company> result = dl.GetCompanys(condition);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 根据公司编号取得公司信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public u_company GetCompanyById(string companyId)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLCompany dl = new DLCompany();
                u_company result = dl.GetCompanyById(companyId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得新的公司编号
        /// </summary>
        /// <returns></returns>
        public string GetNextCompanyId() {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLCompany dl = new DLCompany();
                string result = dl.GetNextCompanyId();
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 保存公司数据
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public int SaveCompany(List<u_company> lst, ActionType actionType) {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLCompany dl = new DLCompany();
                int intResult = -1;
                if (actionType == ActionType.AddNew)
                {
                    //插入
                    intResult = dl.InsertCompany(lst);
                } else {
                    //更新
                    intResult = dl.UpdateCompany(lst);
                }

                ts.Complete();
                return intResult;
            }
        }
    }
}
