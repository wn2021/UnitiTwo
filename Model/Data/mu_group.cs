using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class mu_group: ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_ID 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ismu_idSetValue;

        private int _mu_id;

        /// <summary>
        /// 数据表名
        /// </summary>
        public int mu_id
        {
            get
            {
                return this._mu_id;
            }
            set
            {
                this._mu_id = value;
                this._ismu_idSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_NAME 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ismu_descriptionSetValue;

        private string _mu_description;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string mu_description
        {
            get
            {
                return this._mu_description;
            }
            set
            {
                this._mu_description = value;
                this._ismu_descriptionSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 mu_status 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ismu_statusSetValue;

        private string _mu_status;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string mu_status
        {
            get
            {
                return this._mu_status;
            }
            set
            {
                this._mu_status = value;
                this._ismu_statusSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_CREATE_TIME 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ismu_create_timeSetValue;

        private DateTime? _mu_create_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? mu_create_time
        {
            get
            {
                return this._mu_create_time;
            }
            set
            {
                this._mu_create_time = value;
                this._ismu_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_CREATE_USER 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ismu_create_userSetValue;

        private string _mu_create_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string mu_create_user
        {
            get
            {
                return this._mu_create_user;
            }
            set
            {
                this._mu_create_user = value;
                this._ismu_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_UPDATE_TIME 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ismu_update_timeSetValue;

        private DateTime? _mu_update_time;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? mu_update_time
        {
            get
            {
                return this._mu_update_time;
            }
            set
            {
                this._mu_update_time = value;
                this._ismu_update_timeSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_CREATE_USER 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _ismu_update_userSetValue;

        private string _mu_update_user;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string mu_update_user
        {
            get
            {
                return this._mu_update_user;
            }
            set
            {
                this._mu_update_user = value;
                this._ismu_update_userSetValue = true;
            }
        }
    }
}
