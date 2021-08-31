using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_WS_change
    {
        public string wsc_id { get; set; }
        public DateTime? wsc_date_from { get; set; }
        public DateTime? wsc_date_to { get; set; }
        public string wsc_change_type { get; set; }
        public string wsc_change_reason { get; set; }
        public string wsc_remarks { get; set; }
        public string wsc_range_type { get; set; }
        public decimal? wsc_salary_param { get; set; }
        public string wsc_create_user { get; set; }
        public DateTime? wsc_create_time { get; set; }
        public string wsc_update_user { get; set; }
        public DateTime? wsc_update_time { get; set; }
        public string wsc_company_id { get; set; }
    }
}
