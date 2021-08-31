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
    public class BLDepartment: BusinessBase
    {
        /// <summary>
        /// 取得部门信息
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public List<V_department> GetDepartments(DepartmentSearch condtion) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLDepartment dl = new DLDepartment();
                List<V_department> result = dl.GetDepartments(condtion);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 根据主键取得部门信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        public u_department GetDeptByKeyValue(string companyId, string depId) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLDepartment dl = new DLDepartment();
                u_department result = dl.GetDeptByKeyValue(companyId, depId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得职级信息
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public List<V_position_level> GetDeptPositionLevels(PositionLeveltSearch condtion)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLDepartment dl = new DLDepartment();
                List<V_position_level> result = dl.GetDeptPositionLevels(condtion);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 根据主键取得职级信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        public u_position_level GetLevelByKeyValue(string companyId, string depId, string levelId)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLDepartment dl = new DLDepartment();
                u_position_level result = dl.GetLevelByKeyValue(companyId, depId, levelId);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得新一个部门编号
        /// </summary>
        /// <returns></returns>
        public string GetNextDepartId(string companyId) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLDepartment dl = new DLDepartment();
                string result = dl.GetNextDepartId(companyId);
                ts.Complete();
                return result;
            }
        }
        public int InsertDept(List<u_department> list) {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLDepartment dl = new DLDepartment();
               int result = dl.InsertDept(list);
                ts.Complete();
                return result;
            }
        }
        public int UpdateDept(List<u_department> list)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLDepartment dl = new DLDepartment();
                int result = dl.UpdateDept(list);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得下一个职级编号
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depid"></param>
        /// <returns></returns>
        public string GetNextLevelId(string companyid, string depid) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLDepartment dl = new DLDepartment();
                string result = dl.GetNextLevelId(companyid, depid);
                ts.Complete();
                return result;
            }
        }
        public int InsertLevel(List<u_position_level> list)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLDepartment dl = new DLDepartment();
                int result = dl.InsertLevel(list);
                ts.Complete();
                return result;
            }
        }
        public int UpdateLevel(List<u_position_level> list)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLDepartment dl = new DLDepartment();
                int result = dl.UpdateLevel(list);
                ts.Complete();
                return result;
            }
        }
    }
}
