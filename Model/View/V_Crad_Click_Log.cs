using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_Crad_Click_Log
    {
        public string employee_id { get; set; }
        public string card_id { get; set; }
        public DateTime? work_date { get; set; }
        public string begin_click_time { get; set; }
        public string end_click_time { get; set; }
        public string new_flag { get; set; }
    }
    [Serializable()]
    public class V_Excel_Card_Click
    {
        public string companyId { get; set; }
        public string companyName { get; set; }
        public string empId { get; set; }
        public string empName { get; set; }
        public string cardId { get; set; }
        public DateTime? attDate { get; set; }
        public string at_login_time { get; set; }
        public string at_level_time { get; set; }
    }
}
