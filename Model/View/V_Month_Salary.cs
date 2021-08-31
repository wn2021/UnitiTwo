using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class Search_View_Type {
        public string historyflag { get; set; }
    }
    [Serializable()]
    public class V_Month_Salary
    {
        public string company_id { get; set; }
        public string company_name { get; set; }
        public string department_id { get; set; }
        public string department_name { get; set; }
        public string employee_id { get; set; }
        public string employee_name { get; set; }
        public string ems_month { get; set; }
        public string ems_begin_date { get; set; }
        public string ems_end_date { get; set; }
        public decimal ems_month_salary { get; set; }
        public decimal ems_month_bonus { get; set; }
    }

    [Serializable()]
    public class V_Daily_Salary
    {
        public string company_id { get; set; }
        public string company_name { get; set; }
        public string department_id { get; set; }
        public string department_name { get; set; }
        public string employee_id { get; set; }
        public string employee_name { get; set; }
        public DateTime? eds_att_date { get; set; }
        public string eds_att_flag { get; set; }
        public decimal? eds_daily_salary { get; set; }
        public string eds_work_flag { get; set; }
        public decimal? eds_salary_param { get; set; }
        public decimal? eds_real_salary { get; set; }
    }
}
