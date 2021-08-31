using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_Bonus_Plan_Header
    {
        public string bph_plan_id { get; set; }
        public string bph_company_id { get; set; }
        public string bph_company_name { get; set; }
        public string bph_bonus_type { get; set; }
        public string bph_bonus_type_name { get; set; }
        public string bph_bonus_from { get; set; }
        public string bph_bonus_month { get; set; }
        public string bph_status { get; set; }
        public string bph_status_name { get; set; }
        public string bph_remarks { get; set; }
    }
}
