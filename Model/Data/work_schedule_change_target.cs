using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class work_schedule_change_target : ModelBase
    {
        /// <summary>
        /// 调休计划编号
        /// </summary>
        protected bool _iswct_idSetValue;
        private string _wct_id;
        public string wct_id
        {
            get
            {
                return this._wct_id;
            }
            set
            {
                this._wct_id = value;
                this._iswct_idSetValue = true;
            }
        }
        /// <summary>
        /// 调休计划编号
        /// </summary>
        protected bool _iswct_target_idSetValue;
        private string _wct_target_id;
        public string wct_target_id
        {
            get
            {
                return this._wct_target_id;
            }
            set
            {
                this._wct_target_id = value;
                this._iswct_target_idSetValue = true;
            }
        }
        /// <summary>
        /// 创建者
        /// </summary>
        protected bool _iswct_create_userSetValue;
        private string _wct_create_user;
        public string wct_create_user
        {
            get
            {
                return this._wct_create_user;
            }
            set
            {
                this._wct_create_user = value;
                this._iswct_create_userSetValue = true;
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        protected bool _iswct_create_timeSetValue;
        private DateTime _wct_create_time;
        public DateTime wct_create_time
        {
            get
            {
                return this._wct_create_time;
            }
            set
            {
                this._wct_create_time = value;
                this._iswct_create_timeSetValue = true;
            }
        }
        /// <summary>
        /// 更新者
        /// </summary>
        protected bool _iswct_update_userSetValue;
        private string _wct_update_user;
        public string wct_update_user
        {
            get
            {
                return this._wct_update_user;
            }
            set
            {
                this._wct_update_user = value;
                this._iswct_update_userSetValue = true;
            }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        protected bool _iswct_update_timeSetValue;
        private DateTime _wct_update_time;
        public DateTime wct_update_time
        {
            get
            {
                return this._wct_update_time;
            }
            set
            {
                this._wct_update_time = value;
                this._iswct_update_timeSetValue = true;
            }
        }
    }
}
