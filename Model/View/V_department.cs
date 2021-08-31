using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_department
    {
        public string ud_id { get; set; }
        public string ud_name { get; set; }
        public string ud_company_id { get; set; }
        public string ud_company_name { get; set; }
        public string ud_status { get; set; }
        public string ud_status_name { get; set; }
        public DateTime? ud_create_time { get; set; }
        public string ud_create_user { get; set; }
        public DateTime? ud_update_time { get; set; }
        public string ud_update_user { get; set; }
    }
    [Serializable()]
    public class V_position_level
    {
        public string upl_id { get; set; }
        public string upl_name { get; set; }
        public string upl_company_id { get; set; }
        public string upl_company_name { get; set; }
        public string upl_department_id { get; set; }
        public string upl_department_name { get; set; }
        public string upl_status { get; set; }
        public string upl_status_name { get; set; }
        public DateTime? upl_create_time { get; set; }
        public string upl_create_user { get; set; }
        public DateTime? upl_update_time { get; set; }
        public string upl_update_user { get; set; }
    }
}
