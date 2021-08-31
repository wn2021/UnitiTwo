using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_Systeminfo: ModelBase
    {
        public int sysid {get;set;}
        public int sysubsid { get; set; }
        public string sysvalue { get; set; }
        public string sysvalue2 { get; set; }
        public string description { get; set; }
        public int sort { get; set; }
    }
    [Serializable()]
    public class Combobox_Option : ModelBase {
        public string id { get; set; }
        public string text { get; set; }
    }
}
