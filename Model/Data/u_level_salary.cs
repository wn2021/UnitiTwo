using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class u_level_salary : ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 ull_company_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isull_company_idSetValue;

        private string _ull_company_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ull_company_id
        {
            get
            {
                return this._ull_company_id;
            }
            set
            {
                this._ull_company_id = value;
                this._isull_company_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ull_department_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isull_department_idSetValue;

        private string _ull_department_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ull_department_id
        {
            get
            {
                return this._ull_department_id;
            }
            set
            {
                this._ull_department_id = value;
                this._isull_department_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ull_position_level 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isull_position_levelSetValue;

        private string _ull_position_level;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ull_position_level
        {
            get
            {
                return this._ull_position_level;
            }
            set
            {
                this._ull_position_level = value;
                this._isull_position_levelSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ull_salary_min 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isull_salary_minSetValue;

        private decimal? _ull_salary_min;

        /// <summary>
        /// 数据表名
        /// </summary>
        public decimal? ull_salary_min
        {
            get
            {
                return this._ull_salary_min;
            }
            set
            {
                this._ull_salary_min = value;
                this._isull_salary_minSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ull_salary_max 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isull_salary_maxSetValue;

        private decimal? _ull_salary_max;

        /// <summary>
        /// 数据表名
        /// </summary>
        public decimal? ull_salary_max
        {
            get
            {
                return this._ull_salary_max;
            }
            set
            {
                this._ull_salary_max = value;
                this._isull_salary_maxSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ull_create_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isull_create_timeSetValue;

        private DateTime? _ull_create_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ull_create_time
        {
            get
            {
                return this._ull_create_time;
            }
            set
            {
                this._ull_create_time = value;
                this._isull_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ull_create_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isull_create_userSetValue;

        private string _ull_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ull_create_user
        {
            get
            {
                return this._ull_create_user;
            }
            set
            {
                this._ull_create_user = value;
                this._isull_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ull_update_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isull_update_timeSetValue;

        private DateTime? _ull_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ull_update_time
        {
            get
            {
                return this._ull_update_time;
            }
            set
            {
                this._ull_update_time = value;
                this._isull_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ull_update_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isull_update_userSetValue;

        private string _ull_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ull_update_user
        {
            get
            {
                return this._ull_update_user;
            }
            set
            {
                this._ull_update_user = value;
                this._isull_update_userSetValue = true;
            }
        }
    }
}
