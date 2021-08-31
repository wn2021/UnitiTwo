using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class bonus_plan_header : ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 bph_plan_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbph_plan_idSetValue;

        private string _bph_plan_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bph_plan_id
        {
            get
            {
                return this._bph_plan_id;
            }
            set
            {
                this._bph_plan_id = value;
                this._isbph_plan_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bph_company_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbph_company_idSetValue;

        private string _bph_company_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bph_company_id
        {
            get
            {
                return this._bph_company_id;
            }
            set
            {
                this._bph_company_id = value;
                this._isbph_company_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bph_bonus_type 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbph_bonus_typeSetValue;

        private string _bph_bonus_type;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bph_bonus_type
        {
            get
            {
                return this._bph_bonus_type;
            }
            set
            {
                this._bph_bonus_type = value;
                this._isbph_bonus_typeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bph_bonus_from 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbph_bonus_fromSetValue;

        private string _bph_bonus_from;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bph_bonus_from
        {
            get
            {
                return this._bph_bonus_from;
            }
            set
            {
                this._bph_bonus_from = value;
                this._isbph_bonus_fromSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bph_bonus_month 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbph_bonus_monthSetValue;

        private string _bph_bonus_month;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bph_bonus_month
        {
            get
            {
                return this._bph_bonus_month;
            }
            set
            {
                this._bph_bonus_month = value;
                this._isbph_bonus_monthSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bph_status 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbph_statusSetValue;

        private string _bph_status;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bph_status
        {
            get
            {
                return this._bph_status;
            }
            set
            {
                this._bph_status = value;
                this._isbph_statusSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bph_remarks 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbph_remarksSetValue;

        private string _bph_remarks;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bph_remarks
        {
            get
            {
                return this._bph_remarks;
            }
            set
            {
                this._bph_remarks = value;
                this._isbph_remarksSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bph_create_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbph_create_timeSetValue;

        private DateTime? _bph_create_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? bph_create_time
        {
            get
            {
                return this._bph_create_time;
            }
            set
            {
                this._bph_create_time = value;
                this._isbph_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bph_create_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbph_create_userSetValue;

        private string _bph_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bph_create_user
        {
            get
            {
                return this._bph_create_user;
            }
            set
            {
                this._bph_create_user = value;
                this._isbph_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bph_update_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbph_update_timeSetValue;

        private DateTime? _bph_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? bph_update_time
        {
            get
            {
                return this._bph_update_time;
            }
            set
            {
                this._bph_update_time = value;
                this._isbph_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 bph_update_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isbph_update_userSetValue;

        private string _bph_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string bph_update_user
        {
            get
            {
                return this._bph_update_user;
            }
            set
            {
                this._bph_update_user = value;
                this._isbph_update_userSetValue = true;
            }
        }
    }
}
