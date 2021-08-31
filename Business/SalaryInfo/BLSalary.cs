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
    public class BLSalary:BusinessBase
    {
        /// <summary>
        /// 取得员工当前薪资
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_Employee_Salary> GetEmpSalary(SalarySearch condition) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLSalary dl = new DLSalary();
                List<V_Employee_Salary> result = dl.GetEmpSalary(condition);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 取得职级薪资水平
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_Level_Salary> GetLevelSalary(SalarySearch condition) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLSalary dl = new DLSalary();
                List<V_Level_Salary> result = dl.GetLevelSalary(condition);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 保存职级薪资范围
        /// </summary>
        /// <param name="views"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int SaveLevelSalary(List<V_Level_Salary> views, string user) {
            int intResult = -1;
            List<u_level_salary> list = new List<u_level_salary>();
            u_level_salary ls = null;
            DateTime saveTime = DateTime.Now;
            foreach (V_Level_Salary vw in views) {
                if (!vw.pl_salary_min.HasValue || !vw.pl_salary_max.HasValue) {
                    continue;
                }
                ls = new u_level_salary();
                ls.ull_company_id = vw.pl_company_id;
                ls.ull_department_id = vw.pl_department_id;
                ls.ull_position_level = vw.pl_position_level_id;
                ls.ull_salary_min = vw.pl_salary_min;
                ls.ull_salary_max = vw.pl_salary_max;
                ls.ull_update_time = saveTime;
                ls.ull_update_user = user;
                ls.ull_create_time = saveTime;
                ls.ull_create_user = user;
                list.Add(ls);
            }
            if (list.Count == 0) { return 0; }
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLSalary dl = new DLSalary();
                intResult = dl.SaveLevelSalary(list);
                ts.Complete();
            }
            return intResult;
        }
        /// <summary>
        /// 保存员工薪资
        /// </summary>
        /// <param name="views"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int SaveEmpSalary(List<V_Employee_Salary> views, string user)
        {
            List<u_emlpoyee_salary> list = new List<u_emlpoyee_salary>();
            int intResult = -1;
            u_emlpoyee_salary es = null;
            DateTime saveTime = DateTime.Now;
            foreach (V_Employee_Salary vw in views)
            {
                if (!vw.ues_salary.HasValue || string.IsNullOrEmpty(vw.use_bank_card))
                {
                    continue;
                }
                es = new u_emlpoyee_salary();
                es.ues_id = vw.ues_id;
                es.ues_company_id = vw.ues_company_id;
                es.ues_emp_id = vw.ues_emp_id;
                es.ues_salary = vw.ues_salary;
                es.use_bank_card = vw.use_bank_card;
                es.ues_start_date = vw.ues_start_date;
                es.ues_end_date = vw.ues_end_date;
                es.ues_update_time = saveTime;
                es.ues_update_user = user;
                es.ues_create_time = saveTime;
                es.ues_create_user = user;
                list.Add(es);
            }
            if (list.Count == 0) { return 0; }
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLSalary dl = new DLSalary();
                intResult = dl.SaveEmpSalary(list);
                ts.Complete();
            }
            return intResult;
        }
        /// <summary>
        /// 取得员工工资
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="isHistory">是否查询历史数据</param>
        /// <returns></returns>
        public List<V_Month_Salary> GetEmployeeSalaries(SalarySearchTwo condition, bool isHistory) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLSalary dl = new DLSalary();
                List<V_Month_Salary>  result = dl.GetEmployeeSalaries(condition, isHistory);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 计算月工资
        /// </summary>
        /// <param name="strMonth">月份</param>
        public void CalculateSalary(string strMonth)
        {
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLSalary dl = new DLSalary();
                dl.CalculateSalary(strMonth);
                ts.Complete();
            }
        }
        /// <summary>
        /// 取得日工资列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<V_Daily_Salary> GetDailySalaries(SalarySearchTwo condition) {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLSalary dl = new DLSalary();
                List<V_Daily_Salary> result = dl.GetDailySalaries(condition);
                ts.Complete();
                return result;
            }
        }
        /// <summary>
        /// 更新发放标志
        /// </summary>
        /// <param name="views"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public int SetDeliveried(List<V_Month_Salary> views,string username) {
            List<employee_month_salary> list = new List<employee_month_salary>();
            employee_month_salary ems = null;
            DateTime updateTime = DateTime.Now;
            foreach (V_Month_Salary vw in views) {
                if (string.IsNullOrEmpty(vw.ems_month)
                    || string.IsNullOrEmpty(vw.employee_id)
                    || string.IsNullOrEmpty(vw.company_id)) { continue; }
                ems = new employee_month_salary();
                ems.ems_month = vw.ems_month;
                ems.ems_emp_id = vw.employee_id;
                ems.ems_company_id = vw.company_id;
                ems.ems_delivery_flag = "1";
                ems.ems_update_time = updateTime;
                ems.ems_update_user = username;
                list.Add(ems);
            }
            if (list.Count == 0) { return 0; }
            using (DaoTransactionScope ts = new DaoTransactionScope(true))
            {
                DLSalary dl = new DLSalary();
                int result = dl.SetDeliveried(list);
                ts.Complete();
                return result;
            }
        }
       
    }
}
