using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class u_department: ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 ud_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isud_idSetValue;

        private string _ud_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ud_id
        {
            get
            {
                return this._ud_id;
            }
            set
            {
                this._ud_id = value;
                this._isud_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ud_name 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isud_nameSetValue;

        private string _ud_name;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ud_name
        {
            get
            {
                return this._ud_name;
            }
            set
            {
                this._ud_name = value;
                this._isud_nameSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ud_company_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isud_company_idSetValue;

        private string _ud_company_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ud_company_id
        {
            get
            {
                return this._ud_company_id;
            }
            set
            {
                this._ud_company_id = value;
                this._isud_company_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ud_status 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isud_statusSetValue;

        private string _ud_status;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ud_status
        {
            get
            {
                return this._ud_status;
            }
            set
            {
                this._ud_status = value;
                this._isud_statusSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ud_create_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isud_create_timeSetValue;

        private DateTime? _ud_create_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ud_create_time
        {
            get
            {
                return this._ud_create_time;
            }
            set
            {
                this._ud_create_time = value;
                this._isud_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ud_create_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isud_create_userSetValue;

        private string _ud_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ud_create_user
        {
            get
            {
                return this._ud_create_user;
            }
            set
            {
                this._ud_create_user = value;
                this._isud_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ud_update_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isud_update_timeSetValue;

        private DateTime? _ud_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ud_update_time
        {
            get
            {
                return this._ud_update_time;
            }
            set
            {
                this._ud_update_time = value;
                this._isud_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ud_update_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isud_update_userSetValue;

        private string _ud_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ud_update_user
        {
            get
            {
                return this._ud_update_user;
            }
            set
            {
                this._ud_update_user = value;
                this._isud_update_userSetValue = true;
            }
        }
    }
}
