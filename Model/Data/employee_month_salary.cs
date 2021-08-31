using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class employee_month_salary
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 ems_month 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isems_monthSetValue;

        private string _ems_month;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ems_month
        {
            get
            {
                return this._ems_month;
            }
            set
            {
                this._ems_month = value;
                this._isems_monthSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ems_emp_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isems_emp_idSetValue;

        private string _ems_emp_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ems_emp_id
        {
            get
            {
                return this._ems_emp_id;
            }
            set
            {
                this._ems_emp_id = value;
                this._isems_emp_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ems_company_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isems_company_idSetValue;

        private string _ems_company_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ems_company_id
        {
            get
            {
                return this._ems_company_id;
            }
            set
            {
                this._ems_company_id = value;
                this._isems_company_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ems_begin_date 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isems_begin_dateSetValue;

        private DateTime _ems_begin_date;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime ems_begin_date
        {
            get
            {
                return this._ems_begin_date;
            }
            set
            {
                this._ems_begin_date = value;
                this._isems_begin_dateSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ems_end_date 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isems_end_dateSetValue;

        private DateTime _ems_end_date;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime ems_end_date
        {
            get
            {
                return this._ems_end_date;
            }
            set
            {
                this._ems_end_date = value;
                this._isems_end_dateSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ems_month_salary 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isems_month_salarySetValue;

        private decimal? _ems_month_salary;

        /// <summary>
        /// 数据表名
        /// </summary>
        public decimal? ems_month_salary
        {
            get
            {
                return this._ems_month_salary;
            }
            set
            {
                this._ems_month_salary = value;
                this._isems_month_salarySetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ems_month_bonus 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isems_month_bonusSetValue;

        private decimal? _ems_month_bonus;

        /// <summary>
        /// 数据表名
        /// </summary>
        public decimal? ems_month_bonus
        {
            get
            {
                return this._ems_month_bonus;
            }
            set
            {
                this._ems_month_bonus = value;
                this._isems_month_bonusSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ems_delivery_flag 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isems_delivery_flagSetValue;

        private string _ems_delivery_flag;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ems_delivery_flag
        {
            get
            {
                return this._ems_delivery_flag;
            }
            set
            {
                this._ems_delivery_flag = value;
                this._isems_delivery_flagSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ems_create_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isems_create_timeSetValue;

        private DateTime? _ems_create_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ems_create_time
        {
            get
            {
                return this._ems_create_time;
            }
            set
            {
                this._ems_create_time = value;
                this._isems_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ems_create_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isems_create_userSetValue;

        private string _ems_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ems_create_user
        {
            get
            {
                return this._ems_create_user;
            }
            set
            {
                this._ems_create_user = value;
                this._isems_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ems_update_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isems_update_timeSetValue;

        private DateTime? _ems_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ems_update_time
        {
            get
            {
                return this._ems_update_time;
            }
            set
            {
                this._ems_update_time = value;
                this._isems_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ems_update_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isems_update_userSetValue;

        private string _ems_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ems_update_user
        {
            get
            {
                return this._ems_update_user;
            }
            set
            {
                this._ems_update_user = value;
                this._isems_update_userSetValue = true;
            }
        }
    }
}
