using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_Solution
    {
        public int solution_id { get; set; }
        public string solution_name { get; set; }
        public string solution_summary { get; set; }
        public string solution_detailed_description { get; set; }
        public string solution_big_image { get; set; }
        public string solution_small_image { get; set; }
        public string  solution_status { get; set; }
        public int solution_point { get; set; }
        public string solution_create_user { get; set; }
        public DateTime? solution_create_time { get; set; }
        public string solution_update_user { get; set; }
        public DateTime? solution_update_time { get; set; }
    }
}
