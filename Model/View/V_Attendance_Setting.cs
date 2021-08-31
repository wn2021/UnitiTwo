using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_Attendance_Setting
    {
        public DateTime as_date { get; set; }
        public string as_day_of_week { get; set; }
        public string as_day_type { get; set; }
        public string as_day_type_name { get; set; }
        public decimal? as_salary_param { get; set; }
    }
}
