using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class SalarySearch
    {
        public string companyId { get; set; }
        public string departmentId { get; set; }
        public string positionLevel { get; set; }
        public string employeeName { get; set; }
        public DateTime currentDate { get; set; }
    }
    [Serializable()]
    public class SalarySearchTwo
    {
        public string companyId { get; set; }
        public string departmentId { get; set; }
        public string employeeId { get; set; }
        public string employeeName { get; set; }
        public string salaryMonth { get; set; }
    }
}
