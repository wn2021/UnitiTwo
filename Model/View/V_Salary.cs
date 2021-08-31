using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_Employee_Salary
    {
        public int? ues_id { get; set; }
        public string ues_emp_id { get; set; }
        public string use_emp_name { get; set; }
        public string ues_company_name { get; set; }
        public string ues_company_id { get; set; }
        public string ues_department_name { get; set; }
        public string ues_position_level { get; set; }
        public string use_bank_card { get; set; }
        public decimal? ues_salary { get; set; }
        public DateTime? ues_start_date { get; set; }
        public DateTime? ues_end_date { get; set; }
        public decimal? ues_salary_min { get; set; }
        public decimal? ues_salary_max { get; set; }
    }
    [Serializable()]
    public class V_Level_Salary {
        public string pl_position_level_id { get; set; }
        public string pl_position_level { get; set; }
        public string pl_company_id { get; set; }
        public string pl_company_name { get; set; }
        public string pl_department_id { get; set; }
        public string pl_department_name { get; set; }
        public decimal? pl_salary_min { get; set; }
        public decimal? pl_salary_max { get; set; }
    }
}
