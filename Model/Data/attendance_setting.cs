using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class attendance_setting : ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 as_date 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isas_dateSetValue;

        private DateTime _as_date;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime as_date
        {
            get
            {
                return this._as_date;
            }
            set
            {
                this._as_date = value;
                this._isas_dateSetValue = true;
            }
        }        
        /// <summary>
        /// 指示当前对象自创建以来，属性 as_day_of_week 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isas_day_of_weekSetValue;

        private int? _as_day_of_week;

        /// <summary>
        /// 数据表名
        /// </summary>
        public int? as_day_of_week
        {
            get
            {
                return this._as_day_of_week;
            }
            set
            {
                this._as_day_of_week = value;
                this._isas_day_of_weekSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 as_day_type 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isas_day_typeSetValue;

        private string _as_day_type;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string as_day_type
        {
            get
            {
                return this._as_day_type;
            }
            set
            {
                this._as_day_type = value;
                this._isas_day_typeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 as_create_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isas_create_timeSetValue;

        private DateTime? _as_create_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? as_create_time
        {
            get
            {
                return this._as_create_time;
            }
            set
            {
                this._as_create_time = value;
                this._isas_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 as_create_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isas_create_userSetValue;

        private string _as_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string as_create_user
        {
            get
            {
                return this._as_create_user;
            }
            set
            {
                this._as_create_user = value;
                this._isas_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 as_update_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isas_update_timeSetValue;

        private DateTime? _as_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? as_update_time
        {
            get
            {
                return this._as_update_time;
            }
            set
            {
                this._as_update_time = value;
                this._isas_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 as_update_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isas_update_userSetValue;

        private string _as_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string as_update_user
        {
            get
            {
                return this._as_update_user;
            }
            set
            {
                this._as_update_user = value;
                this._isas_update_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 as_salary_param 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isas_salary_paramSetValue;

        private decimal? _as_salary_param;

        /// <summary>
        /// 数据表名
        /// </summary>
        public decimal? as_salary_param
        {
            get
            {
                return this._as_salary_param;
            }
            set
            {
                this._as_salary_param = value;
                this._isas_salary_paramSetValue = true;
            }
        }
    }
}
