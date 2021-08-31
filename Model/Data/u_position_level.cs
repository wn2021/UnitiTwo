using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class u_position_level : ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isupl_idSetValue;

        private string _upl_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string upl_id
        {
            get
            {
                return this._upl_id;
            }
            set
            {
                this._upl_id = value;
                this._isupl_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 upl_name 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isupl_nameSetValue;

        private string _upl_name;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string upl_name
        {
            get
            {
                return this._upl_name;
            }
            set
            {
                this._upl_name = value;
                this._isupl_nameSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 upl_company_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isupl_company_idSetValue;

        private string _upl_company_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string upl_company_id
        {
            get
            {
                return this._upl_company_id;
            }
            set
            {
                this._upl_company_id = value;
                this._isupl_company_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 upl_department_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isupl_department_idSetValue;

        private string _upl_department_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string upl_department_id
        {
            get
            {
                return this._upl_department_id;
            }
            set
            {
                this._upl_department_id = value;
                this._isupl_department_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 upl_status 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isupl_statusSetValue;

        private string _upl_status;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string upl_status
        {
            get
            {
                return this._upl_status;
            }
            set
            {
                this._upl_status = value;
                this._isupl_statusSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 upl_create_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isupl_create_timeSetValue;

        private DateTime? _upl_create_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? upl_create_time
        {
            get
            {
                return this._upl_create_time;
            }
            set
            {
                this._upl_create_time = value;
                this._isupl_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 upl_create_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isupl_create_userSetValue;

        private string _upl_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string upl_create_user
        {
            get
            {
                return this._upl_create_user;
            }
            set
            {
                this._upl_create_user = value;
                this._isupl_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 upl_update_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isupl_update_timeSetValue;

        private DateTime? _upl_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? upl_update_time
        {
            get
            {
                return this._upl_update_time;
            }
            set
            {
                this._upl_update_time = value;
                this._isupl_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 upl_update_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isupl_update_userSetValue;

        private string _upl_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string upl_update_user
        {
            get
            {
                return this._upl_update_user;
            }
            set
            {
                this._upl_update_user = value;
                this._isupl_update_userSetValue = true;
            }
        }
    }
}
