using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_employee
    {
        /// <summary>
        /// 员工编号
        /// </summary>
        public string employeeId { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string employeeName { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public string departName { get; set; }
        /// <summary>
        /// 职位级别
        /// </summary>
        public string positionLevel { get; set; }
        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime? ue_entry_time { get; set; }
        /// <summary>
        /// 员工卡号（考勤打卡用）
        /// </summary>
        public string ue_card_number { get; set; }

        public string ue_id { get; set; }
        public string ue_name { get; set; }
        public string ue_company_id { get; set; }
        public string ue_department_id { get; set; }
        public string ue_position_level { get; set; }
        public string ue_idcrad_number { get; set; }
        public string ue_gender { get; set; }
        public int ue_age { get; set; }
        public DateTime? ue_birthday { get; set; }
        public string ue_email { get; set; }
        public string ue_phone { get; set; }
        public string ue_address { get; set; }
        public DateTime? ue_exit_time { get; set; }
        public string ue_status { get; set; }
        public DateTime? ue_create_time { get; set; }
        public string ue_create_user { get; set; }
        public DateTime? ue_update_time { get; set; }
        public string ue_update_user { get; set; }
    }
    [Serializable()]
    public class OrganizationSelect {
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
    }
}
