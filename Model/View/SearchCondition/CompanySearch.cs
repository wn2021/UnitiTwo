using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class CompanySearch
    {
        public string companyId { get; set; }
        public string companyName { get; set; }
        public string companyStutus { get; set; }
        public DateTime? registerTime { get; set; }
        public string companyAddress { get; set; }
        public string corporation { get; set; }
    }
}
