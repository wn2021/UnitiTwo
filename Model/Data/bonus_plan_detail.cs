using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class bonus_plan_detail : ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 bpd_plan_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbpd_plan_idSetValue;

        private string _bpd_plan_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bpd_plan_id
        {
            get
            {
                return this._bpd_plan_id;
            }
            set
            {
                this._bpd_plan_id = value;
                this._isbpd_plan_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bpd_company_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbpd_company_idSetValue;

        private string _bpd_company_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bpd_company_id
        {
            get
            {
                return this._bpd_company_id;
            }
            set
            {
                this._bpd_company_id = value;
                this._isbpd_company_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bpd_employee_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbpd_employee_idSetValue;

        private string _bpd_employee_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bpd_employee_id
        {
            get
            {
                return this._bpd_employee_id;
            }
            set
            {
                this._bpd_employee_id = value;
                this._isbpd_employee_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bpd_bonus_amount 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbpd_bonus_amountSetValue;

        private decimal _bpd_bonus_amount;

        /// <summary>
        /// 数据表名
        /// </summary>
        public decimal bpd_bonus_amount
        {
            get
            {
                return this._bpd_bonus_amount;
            }
            set
            {
                this._bpd_bonus_amount = value;
                this._isbpd_bonus_amountSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bpd_create_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbpd_create_timeSetValue;

        private DateTime? _bpd_create_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? bpd_create_time
        {
            get
            {
                return this._bpd_create_time;
            }
            set
            {
                this._bpd_create_time = value;
                this._isbpd_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bpd_create_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbpd_create_userSetValue;

        private string _bpd_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bpd_create_user
        {
            get
            {
                return this._bpd_create_user;
            }
            set
            {
                this._bpd_create_user = value;
                this._isbpd_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bpd_update_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbpd_update_timeSetValue;

        private DateTime? _bpd_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? bpd_update_time
        {
            get
            {
                return this._bpd_update_time;
            }
            set
            {
                this._bpd_update_time = value;
                this._isbpd_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bph_update_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbpd_update_userSetValue;

        private string _bpd_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bpd_update_user
        {
            get
            {
                return this._bpd_update_user;
            }
            set
            {
                this._bpd_update_user = value;
                this._isbpd_update_userSetValue = true;
            }
        }
    }
}
