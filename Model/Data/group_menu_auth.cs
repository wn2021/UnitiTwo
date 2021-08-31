using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class group_menu_auth : ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isgm_group_idSetValue;

        private string _gm_group_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string gm_group_id
        {
            get
            {
                return this._gm_group_id;
            }
            set
            {
                this._gm_group_id = value;
                this._isgm_group_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isgm_menu_idSetValue;

        private string _gm_menu_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string gm_menu_id
        {
            get
            {
                return this._gm_menu_id;
            }
            set
            {
                this._gm_menu_id = value;
                this._isgm_menu_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_id 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isgm_statusSetValue;

        private string _gm_status;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string gm_status
        {
            get
            {
                return this._gm_status;
            }
            set
            {
                this._gm_status = value;
                this._isgm_statusSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_create_time 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isgm_create_timeSetValue;

        private DateTime? _gm_create_time;
        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? gm_create_time
        {
            get
            {
                return this._gm_create_time;
            }
            set
            {
                this._gm_create_time = value;
                this._isgm_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_create_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isgm_create_userSetValue;

        private string _gm_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string gm_create_user
        {
            get
            {
                return this._gm_create_user;
            }
            set
            {
                this._gm_create_user = value;
                this._isgm_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_UPDATE_TIME 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isgm_update_timeSetValue;

        private DateTime? _gm_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? gm_update_time
        {
            get
            {
                return this._gm_update_time;
            }
            set
            {
                this._gm_update_time = value;
                this._isgm_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 m_update_user 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isgm_update_userSetValue;

        private string _gm_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string gm_update_user
        {
            get
            {
                return this._gm_update_user;
            }
            set
            {
                this._gm_update_user = value;
                this._isgm_update_userSetValue = true;
            }
        }
    }
}
