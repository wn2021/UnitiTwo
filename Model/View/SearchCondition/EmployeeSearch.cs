using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class EmployeeSearch
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string companyId { get; set; }
        /// <summary>
        /// 所属部门编号
        /// </summary>
        public string departmentId { get; set; }
        /// <summary>
        /// 职位级别编号
        /// </summary>
        public string positionLevel { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string employeeName { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string employeeId { get; set; }
    }
}
