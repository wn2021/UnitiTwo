using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_UserInfo: ModelBase
    {
        public int uu_id { get; set; }
        public string uu_name { get; set; }
        public string uu_password { get; set; }
        public string uu_email { get; set; }
        public string uu_phone { get; set; }
        public string uu_address { get; set; }
        public string uu_type { get; set; }
        public string uu_type_name { get; set; }
        public string uu_status { get; set; }
        public string uu_status_name { get; set; }
        public DateTime? uu_create_time { get; set; }
        public string uu_create_user { get; set; }
        public DateTime? uu_update_time { get; set; }
        public string uu_update_user { get; set; }
    }
    [Serializable()]
    public class V_Password {
        public string old_password { get; set; }
        public string new_password { get; set; }
        public string cfrm_password { get; set; }
    }
}
