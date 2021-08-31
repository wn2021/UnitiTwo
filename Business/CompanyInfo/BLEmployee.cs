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
    public class BLEmployee:BusinessBase
    {
        /// <summary>
        /// 取得员工信息列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_employee> GetEmployees(EmployeeSearch condition) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLEmployee dl = new DLEmployee();
                List<V_employee> result = dl.GetEmployees(condition);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 根据员工编号取得员工信息
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public u_employee GetEmployeeById(string empId) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLEmployee dl = new DLEmployee();
                u_employee result = dl.GetEmployeeById(empId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得新员工编号
        /// </summary>
        /// <returns></returns>
        public string GetNextEmployeeId() {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLEmployee dl = new DLEmployee();
                string result = dl.GetNextEmployeeId();
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 保存员工信息
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public int SaveEmployee(List<u_employee> lst,ActionType action) {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLEmployee dl = new DLEmployee();
                int intResult = -1;
                if (action == ActionType.AddNew)
                {
                    intResult=dl.InsertEmployee(lst);
                }
                else { intResult=dl.UpdateEmployee(lst); }
                ts.Complete();
                return intResult;
            }
        }
    }
}
