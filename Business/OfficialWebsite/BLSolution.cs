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
    public class BLSolution: BusinessBase
    {
        /// <summary>
        /// 取得解决方案列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_Solution> GetSolutionList(string strKey, string status)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLSolution dl = new DLSolution();
                List<V_Solution> result = dl.GetSolutionList(strKey, status);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 根据主键取得解决方案
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public V_Solution GetSolutionById(string solutionId)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLSolution dl = new DLSolution();
                V_Solution result = dl.GetSolutionById(solutionId);
                ts.Complete();
                return result;
            }
        }
    }
}
