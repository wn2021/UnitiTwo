using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class menu : ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ism_idSetValue;

        private string _m_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string m_id
        {
            get
            {
                return this._m_id;
            }
            set
            {
                this._m_id = value;
                this._ism_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_level 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ism_levelSetValue;

        private int _m_level;

        /// <summary>
        /// 数据表名
        /// </summary>
        public int m_level
        {
            get
            {
                return this._m_level;
            }
            set
            {
                this._m_level = value;
                this._ism_levelSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ism_parent_idSetValue;

        private string _m_parent_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string m_parent_id
        {
            get
            {
                return this._m_parent_id;
            }
            set
            {
                this._m_parent_id = value;
                this._ism_parent_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_iconCls 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ism_iconClsSetValue;

        private string _m_iconCls;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string m_iconCls
        {
            get
            {
                return this._m_iconCls;
            }
            set
            {
                this._m_iconCls = value;
                this._ism_iconClsSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_text 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ism_textSetValue;

        private string _m_text;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string m_text
        {
            get
            {
                return this._m_text;
            }
            set
            {
                this._m_text = value;
                this._ism_textSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_url 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ism_urlSetValue;

        private string _m_url;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string m_url
        {
            get
            {
                return this._m_url;
            }
            set
            {
                this._m_url = value;
                this._ism_urlSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_url 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ism_statusSetValue;

        private string _m_status;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string m_status
        {
            get
            {
                return this._m_status;
            }
            set
            {
                this._m_status = value;
                this._ism_statusSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_create_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ism_create_timeSetValue;

        private DateTime? _m_create_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? m_create_time
        {
            get
            {
                return this._m_create_time;
            }
            set
            {
                this._m_create_time = value;
                this._ism_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_create_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ism_create_userSetValue;

        private string _m_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string m_create_user
        {
            get
            {
                return this._m_create_user;
            }
            set
            {
                this._m_create_user = value;
                this._ism_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_UPDATE_TIME 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ism_update_timeSetValue;

        private DateTime? _m_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? m_update_time
        {
            get
            {
                return this._m_update_time;
            }
            set
            {
                this._m_update_time = value;
                this._ism_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_update_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ism_update_userSetValue;

        private string _m_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string m_update_user
        {
            get
            {
                return this._m_update_user;
            }
            set
            {
                this._m_update_user = value;
                this._ism_update_userSetValue = true;
            }
        }
    }
}
