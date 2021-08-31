using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class u_emlpoyee_salary : ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 ues_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isues_idSetValue;

        private int? _ues_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public int? ues_id
        {
            get
            {
                return this._ues_id;
            }
            set
            {
                this._ues_id = value;
                this._isues_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ues_emp_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isues_emp_idSetValue;

        private string _ues_emp_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ues_emp_id
        {
            get
            {
                return this._ues_emp_id;
            }
            set
            {
                this._ues_emp_id = value;
                this._isues_emp_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ues_company_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isues_company_idSetValue;

        private string _ues_company_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ues_company_id
        {
            get
            {
                return this._ues_company_id;
            }
            set
            {
                this._ues_company_id = value;
                this._isues_company_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ues_salary 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isues_salarySetValue;

        private decimal? _ues_salary;

        /// <summary>
        /// 数据表名
        /// </summary>
        public decimal? ues_salary
        {
            get
            {
                return this._ues_salary;
            }
            set
            {
                this._ues_salary = value;
                this._isues_salarySetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 use_bank_card 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuse_bank_cardSetValue;

        private string _use_bank_card;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string use_bank_card
        {
            get
            {
                return this._use_bank_card;
            }
            set
            {
                this._use_bank_card = value;
                this._isuse_bank_cardSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ues_start_date 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isues_start_dateSetValue;

        private DateTime? _ues_start_date;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ues_start_date
        {
            get
            {
                return this._ues_start_date;
            }
            set
            {
                this._ues_start_date = value;
                this._isues_start_dateSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ues_end_date 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isues_end_dateSetValue;

        private DateTime? _ues_end_date;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ues_end_date
        {
            get
            {
                return this._ues_end_date;
            }
            set
            {
                this._ues_end_date = value;
                this._isues_end_dateSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ues_create_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isues_create_timeSetValue;

        private DateTime? _ues_create_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ues_create_time
        {
            get
            {
                return this._ues_create_time;
            }
            set
            {
                this._ues_create_time = value;
                this._isues_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ues_create_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isues_create_userSetValue;

        private string _ues_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ues_create_user
        {
            get
            {
                return this._ues_create_user;
            }
            set
            {
                this._ues_create_user = value;
                this._isues_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ues_update_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isues_update_timeSetValue;

        private DateTime? _ues_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ues_update_time
        {
            get
            {
                return this._ues_update_time;
            }
            set
            {
                this._ues_update_time = value;
                this._isues_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ues_update_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isues_update_userSetValue;

        private string _ues_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ues_update_user
        {
            get
            {
                return this._ues_update_user;
            }
            set
            {
                this._ues_update_user = value;
                this._isues_update_userSetValue = true;
            }
        }
    }
}
