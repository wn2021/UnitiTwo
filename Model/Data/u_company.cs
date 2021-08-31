using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class u_company : ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_idSetValue;

        private string _uc_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string uc_id
        {
            get
            {
                return this._uc_id;
            }
            set
            {
                this._uc_id = value;
                this._isuc_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_nameSetValue;

        private string _uc_name;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string uc_name
        {
            get
            {
                return this._uc_name;
            }
            set
            {
                this._uc_name = value;
                this._isuc_nameSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_corporationSetValue;

        private string _uc_corporation;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string uc_corporation
        {
            get
            {
                return this._uc_corporation;
            }
            set
            {
                this._uc_corporation = value;
                this._isuc_corporationSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_tax_noSetValue;

        private string _uc_tax_no;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string uc_tax_no
        {
            get
            {
                return this._uc_tax_no;
            }
            set
            {
                this._uc_tax_no = value;
                this._isuc_tax_noSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 uc_register_capital 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_register_capitalSetValue;

        private System.Decimal _uc_register_capital;

        /// <summary>
        /// 数据表名
        /// </summary>
        public System.Decimal uc_register_capital
        {
            get
            {
                return this._uc_register_capital;
            }
            set
            {
                this._uc_register_capital = value;
                this._isuc_register_capitalSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 uc_register_address 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_register_addressSetValue;

        private string _uc_register_address;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string uc_register_address
        {
            get
            {
                return this._uc_register_address;
            }
            set
            {
                this._uc_register_address = value;
                this._isuc_register_addressSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 uc_current_address 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_current_addressSetValue;

        private string _uc_current_address;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string uc_current_address
        {
            get
            {
                return this._uc_current_address;
            }
            set
            {
                this._uc_current_address = value;
                this._isuc_current_addressSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 uc_phone 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_phoneSetValue;

        private string _uc_phone;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string uc_phone
        {
            get
            {
                return this._uc_phone;
            }
            set
            {
                this._uc_phone = value;
                this._isuc_phoneSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 uc_description 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_descriptionSetValue;

        private string _uc_description;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string uc_description
        {
            get
            {
                return this._uc_description;
            }
            set
            {
                this._uc_description = value;
                this._isuc_descriptionSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 uc_status 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_statusSetValue;

        private string _uc_status;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string uc_status
        {
            get
            {
                return this._uc_status;
            }
            set
            {
                this._uc_status = value;
                this._isuc_statusSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 uc_create_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_create_timeSetValue;

        private DateTime? _uc_create_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? uc_create_time
        {
            get
            {
                return this._uc_create_time;
            }
            set
            {
                this._uc_create_time = value;
                this._isuc_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 uc_create_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_create_userSetValue;

        private string _uc_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string uc_create_user
        {
            get
            {
                return this._uc_create_user;
            }
            set
            {
                this._uc_create_user = value;
                this._isuc_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 uc_update_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_update_timeSetValue;

        private DateTime? _uc_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? uc_update_time
        {
            get
            {
                return this._uc_update_time;
            }
            set
            {
                this._uc_update_time = value;
                this._isuc_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 uc_update_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_update_userSetValue;

        private string _uc_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string uc_update_user
        {
            get
            {
                return this._uc_update_user;
            }
            set
            {
                this._uc_update_user = value;
                this._isuc_update_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 uc_register_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_register_timeSetValue;

        private DateTime _uc_register_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime uc_register_time
        {
            get
            {
                return this._uc_register_time;
            }
            set
            {
                this._uc_register_time = value;
                this._isuc_register_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 uc_destroy_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuc_destroy_timeSetValue;

        private DateTime? _uc_destroy_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? uc_destroy_time
        {
            get
            {
                return this._uc_destroy_time;
            }
            set
            {
                this._uc_destroy_time = value;
                this._isuc_destroy_timeSetValue = true;
            }
        }
    }
}
