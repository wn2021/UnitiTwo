using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_Employee_Attendance
    {
        public DateTime at_date { get; set; }
        public string at_emp_id { get; set; }
        public string at_emp_name { get; set; }
        public string at_company_id { get; set; }
        public string at_company_name { get; set; }
        public string at_login_time { get; set; }
        public string at_level_time { get; set; }
        public int? at_work_hours { get; set; }
        public string at_work_flag { get; set; }
        public string at_no_att_reason { get; set; }
        public string at_remarks { get; set; }
    }
}
