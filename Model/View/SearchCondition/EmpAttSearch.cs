using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class EmpAttSearch
    {
        public DateTime beginDate{ get; set; }
        public DateTime endDate { get; set; }
        public string empId { get; set; }
        public string empName { get; set; }
        public string companyId { get; set; }
    }
}
