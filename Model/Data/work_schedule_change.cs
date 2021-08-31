using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class work_schedule_change : ModelBase
    {
        /// <summary>
        /// 调休计划编号
        /// </summary>
        protected bool _iswsc_idSetValue;
        private string _wsc_id;
        public string wsc_id
        {
            get
            {
                return this._wsc_id;
            }
            set
            {
                this._wsc_id = value;
                this._iswsc_idSetValue = true;
            }
        }
        /// <summary>
        /// 开始日期
        /// </summary>
        protected bool _iswsc_date_fromSetValue;
        private DateTime _wsc_date_from;
        public DateTime wsc_date_from
        {
            get
            {
                return this._wsc_date_from;
            }
            set
            {
                this._wsc_date_from = value;
                this._iswsc_date_fromSetValue = true;
            }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        protected bool _iswsc_date_toSetValue;
        private DateTime _wsc_date_to;
        public DateTime wsc_date_to
        {
            get
            {
                return this._wsc_date_to;
            }
            set
            {
                this._wsc_date_to = value;
                this._iswsc_date_toSetValue = true;
            }
        }
        /// <summary>
        /// 调休类型
        /// </summary>
        protected bool _iswsc_change_typeSetValue;
        private string _wsc_change_type;
        public string wsc_change_type
        {
            get
            {
                return this._wsc_change_type;
            }
            set
            {
                this._wsc_change_type = value;
                this._iswsc_change_typeSetValue = true;
            }
        }
        /// <summary>
        /// 调休原因
        /// </summary>
        protected bool _iswsc_change_reasonSetValue;
        private string _wsc_change_reason;
        public string wsc_change_reason
        {
            get
            {
                return this._wsc_change_reason;
            }
            set
            {
                this._wsc_change_reason = value;
                this._iswsc_change_reasonSetValue = true;
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        protected bool _iswsc_remarksSetValue;
        private string _wsc_remarks;
        public string wsc_remarks
        {
            get
            {
                return this._wsc_remarks;
            }
            set
            {
                this._wsc_remarks = value;
                this._iswsc_remarksSetValue = true;
            }
        }
        /// <summary>
        /// 调休范围
        /// </summary>
        protected bool _iswsc_range_typeSetValue;
        private string _wsc_range_type;
        public string wsc_range_type
        {
            get
            {
                return this._wsc_range_type;
            }
            set
            {
                this._wsc_range_type = value;
                this._iswsc_range_typeSetValue = true;
            }
        }
        /// <summary>
        /// 工作参数
        /// </summary>
        protected bool _iswsc_salary_paramSetValue;
        private decimal _wsc_salary_param;
        public decimal wsc_salary_param
        {
            get
            {
                return this._wsc_salary_param;
            }
            set
            {
                this._wsc_salary_param = value;
                this._iswsc_salary_paramSetValue = true;
            }
        }
        /// <summary>
        /// 创建者
        /// </summary>
        protected bool _iswsc_create_userSetValue;
        private string _wsc_create_user;
        public string wsc_create_user
        {
            get
            {
                return this._wsc_create_user;
            }
            set
            {
                this._wsc_create_user = value;
                this._iswsc_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        protected bool _iswsc_create_timeSetValue;
        private DateTime _wsc_create_time;
        public DateTime wsc_create_time
        {
            get
            {
                return this._wsc_create_time;
            }
            set
            {
                this._wsc_create_time = value;
                this._iswsc_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 更新者
        /// </summary>
        protected bool _iswsc_update_userSetValue;
        private string _wsc_update_user;
        public string wsc_update_user
        {
            get
            {
                return this._wsc_update_user;
            }
            set
            {
                this._wsc_update_user = value;
                this._iswsc_update_userSetValue = true;
            }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        protected bool _iswsc_update_timeSetValue;
        private DateTime _wsc_update_time;
        public DateTime wsc_update_time
        {
            get
            {
                return this._wsc_update_time;
            }
            set
            {
                this._wsc_update_time = value;
                this._iswsc_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        protected bool _iswsc_statusSetValue;
        private string _wsc_status;
        public string wsc_status
        {
            get
            {
                return this._wsc_status;
            }
            set
            {
                this._wsc_status = value;
                this._iswsc_statusSetValue = true;
            }
        }
        /// <summary>
        /// 部门调休时需要选择公司
        /// </summary>
        protected bool _iswsc_company_idSetValue;
        private string _wsc_company_id;
        public string wsc_company_id
        {
            get
            {
                return this._wsc_company_id;
            }
            set
            {
                this._wsc_company_id = value;
                this._iswsc_company_idSetValue = true;
            }
        }
    }
}
