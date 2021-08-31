using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class DepartmentSearch
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string companyId { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string departmentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string departmentName { get; set; }
        /// <summary>
        /// 部门状态
        /// </summary>
        public string deptstatus { get; set; }
    }
    [Serializable()]
    public class PositionLeveltSearch
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string companyId { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string departmentId { get; set; }
        /// <summary>
        /// 职级编号
        /// </summary>
        public string levelId { get; set; }
        /// <summary>
        /// 职级名称
        /// </summary>
        public string levelname { get; set; }
        /// <summary>
        /// 职级状态
        /// </summary>
        public string levelstatus { get; set; }
    }
}
