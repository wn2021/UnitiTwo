using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class BonusSearch
    {
        public string strYear { get; set; }
        public string companyId { get; set; }
        public string bonusType { get; set; }
        public string planStatus { get; set; }
        public string bonusFrom { get; set; }
        public string employeeId { get; set; }
    }
}
