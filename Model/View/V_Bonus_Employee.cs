using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_Bonus_Employee
    {
        public string employee_Id { get; set; }
        public string employee_Name { get; set; }
        public decimal? bonus_amount { get; set; }
    }
    [Serializable()]
    public class V_Month_Bonus_Detail
    {
        public string company_id { get; set; }
        public string company_name { get; set; }
        public string department_id { get; set; }
        public string department_name { get; set; }
        public string employee_id { get; set; }
        public string employee_name { get; set; }
        public string bonus_month { get; set; }
        public string bonus_type { get; set; }
        public string bonus_type_name { get; set; }
        public string bonus_from { get; set; }
        public decimal bonus_amount { get; set; }
    }
}
