using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class u_employee : ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_idSetValue;

        private string _ue_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_id
        {
            get
            {
                return this._ue_id;
            }
            set
            {
                this._ue_id = value;
                this._isue_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_name 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_nameSetValue;

        private string _ue_name;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_name
        {
            get
            {
                return this._ue_name;
            }
            set
            {
                this._ue_name = value;
                this._isue_nameSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_name 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_company_idSetValue;

        private string _ue_company_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_company_id
        {
            get
            {
                return this._ue_company_id;
            }
            set
            {
                this._ue_company_id = value;
                this._isue_company_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_department_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_department_idSetValue;

        private string _ue_department_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_department_id
        {
            get
            {
                return this._ue_department_id;
            }
            set
            {
                this._ue_department_id = value;
                this._isue_department_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_position_level 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_position_levelSetValue;

        private string _ue_position_level;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_position_level
        {
            get
            {
                return this._ue_position_level;
            }
            set
            {
                this._ue_position_level = value;
                this._isue_position_levelSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_idcrad_number 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_idcrad_numberSetValue;

        private string _ue_idcrad_number;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_idcrad_number
        {
            get
            {
                return this._ue_idcrad_number;
            }
            set
            {
                this._ue_idcrad_number = value;
                this._isue_idcrad_numberSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_gender 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_genderSetValue;

        private string _ue_gender;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_gender
        {
            get
            {
                return this._ue_gender;
            }
            set
            {
                this._ue_gender = value;
                this._isue_genderSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_age 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_ageSetValue;

        private int _ue_age;

        /// <summary>
        /// 数据表名
        /// </summary>
        public int ue_age
        {
            get
            {
                return this._ue_age;
            }
            set
            {
                this._ue_age = value;
                this._isue_ageSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_birthday 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_birthdaySetValue;

        private DateTime? _ue_birthday;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ue_birthday
        {
            get
            {
                return this._ue_birthday;
            }
            set
            {
                this._ue_birthday = value;
                this._isue_birthdaySetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_email 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_emailSetValue;

        private string _ue_email;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_email
        {
            get
            {
                return this._ue_email;
            }
            set
            {
                this._ue_email = value;
                this._isue_emailSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_phone 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_phoneSetValue;

        private string _ue_phone;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_phone
        {
            get
            {
                return this._ue_phone;
            }
            set
            {
                this._ue_phone = value;
                this._isue_phoneSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_address 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_addressSetValue;

        private string _ue_address;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_address
        {
            get
            {
                return this._ue_address;
            }
            set
            {
                this._ue_address = value;
                this._isue_addressSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_entry_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_entry_timeSetValue;

        private DateTime? _ue_entry_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ue_entry_time
        {
            get
            {
                return this._ue_entry_time;
            }
            set
            {
                this._ue_entry_time = value;
                this._isue_entry_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_exit_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isuue_exit_timeSetValue;

        private DateTime? _ue_exit_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ue_exit_time
        {
            get
            {
                return this._ue_exit_time;
            }
            set
            {
                this._ue_exit_time = value;
                this._isuue_exit_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_url 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_statusSetValue;

        private string _ue_status;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_status
        {
            get
            {
                return this._ue_status;
            }
            set
            {
                this._ue_status = value;
                this._isue_statusSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_create_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_create_timeSetValue;

        private DateTime? _ue_create_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ue_create_time
        {
            get
            {
                return this._ue_create_time;
            }
            set
            {
                this._ue_create_time = value;
                this._isue_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_create_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_create_userSetValue;

        private string _ue_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_create_user
        {
            get
            {
                return this._ue_create_user;
            }
            set
            {
                this._ue_create_user = value;
                this._isue_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_update_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_update_timeSetValue;

        private DateTime? _ue_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? ue_update_time
        {
            get
            {
                return this._ue_update_time;
            }
            set
            {
                this._ue_update_time = value;
                this._isue_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_update_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_update_userSetValue;

        private string _ue_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_update_user
        {
            get
            {
                return this._ue_update_user;
            }
            set
            {
                this._ue_update_user = value;
                this._isue_update_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 ue_card_number 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isue_card_numberSetValue;

        private string _ue_card_number;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string ue_card_number
        {
            get
            {
                return this._ue_card_number;
            }
            set
            {
                this._ue_card_number = value;
                this._isue_card_numberSetValue = true;
            }
        }
    }
}
