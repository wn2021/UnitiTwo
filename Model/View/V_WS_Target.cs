using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_WS_Target
    {
        public string wct_id { get; set; }
        public string wct_target_id { get; set; }
        public string wct_target_name { get; set; }
        public string is_target { get; set; }
    }
}
