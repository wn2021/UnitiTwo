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
    public class BLBonus:BusinessBase
    {
        /// <summary>
        /// 取得奖金计划头档
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_Bonus_Plan_Header> GetBonusPlanList(BonusSearch condition)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLBonus dl = new DLBonus();
                List<V_Bonus_Plan_Header> result = dl.GetBonusPlanList(condition);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 根据主键取得计划头档
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public V_Bonus_Plan_Header GetBonusPlanById(string planId, string companyId) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLBonus dl = new DLBonus();
                V_Bonus_Plan_Header result = dl.GetBonusPlanById(planId, companyId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 保存奖金计划
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="atpye"></param>
        /// <returns></returns>
        public int SaveBonusPlan(List<bonus_plan_header> lst, List<bonus_plan_detail> dtls, ActionType atpye)
        {
            int intResult = -1;
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLBonus dl = new DLBonus();
                //1.保存头表
                if (atpye == ActionType.AddNew)
                {
                    intResult = dl.InsertPlan(lst);
                }
                else {
                    intResult = dl.UpdatePlan(lst);
                }
                //2.保存明细
                if (dtls != null && dtls.Count > 0)
                {
                    dl.SaveBonusDtls(dtls);
                }
                ts.Complete();
                return intResult;
            }
        }
        /// <summary>
        /// 取得奖金计划明细
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<V_Bonus_Employee> GetBonusPlanDtl(string planId, string companyId)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLBonus dl = new DLBonus();
                List<V_Bonus_Employee> result = dl.GetBonusPlanDtl(planId, companyId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得奖金计划未分配的员工明细
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<V_Bonus_Employee> GetNoBonusEmp(string planId, string companyId)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLBonus dl = new DLBonus();
                List<V_Bonus_Employee> result = dl.GetNoBonusEmp(planId, companyId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得员工奖金明细
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_Month_Bonus_Detail> GetBonusDetail(SalarySearchTwo condition)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLBonus dl = new DLBonus();
                List<V_Month_Bonus_Detail> result = dl.GetBonusDetail(condition);
                ts.Complete();
                return result;
            }
        }
    }
}
