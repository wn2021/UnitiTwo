using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class u_user: ModelBase
    {
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_ID 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isUU_IDSetValue;

        private int _UU_ID;

        /// <summary>
        /// 数据表名
        /// </summary>
        public int UU_ID
        {
            get
            {
                return this._UU_ID;
            }
            set
            {
                this._UU_ID = value;
                this._isUU_IDSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_NAME 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isUU_NAMESetValue;

        private string _UU_NAME;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string UU_NAME
        {
            get
            {
                return this._UU_NAME;
            }
            set
            {
                this._UU_NAME = value;
                this._isUU_NAMESetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_PASSWORD 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isUU_PASSWORDSetValue;

        private string _UU_PASSWORD;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string UU_PASSWORD
        {
            get
            {
                return this._UU_PASSWORD;
            }
            set
            {
                this._UU_PASSWORD = value;
                this._isUU_PASSWORDSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_EMAIL 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isUU_EMAILSetValue;

        private string _UU_EMAIL;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string UU_EMAIL
        {
            get
            {
                return this._UU_EMAIL;
            }
            set
            {
                this._UU_EMAIL = value;
                this._isUU_EMAILSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_PHONE 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isUU_PHONESetValue;

        private string _UU_PHONE;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string UU_PHONE
        {
            get
            {
                return this._UU_PHONE;
            }
            set
            {
                this._UU_PHONE = value;
                this._isUU_PHONESetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_ADDRESS 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isUU_ADDRESSSetValue;

        private string _UU_ADDRESS;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string UU_ADDRESS
        {
            get
            {
                return this._UU_ADDRESS;
            }
            set
            {
                this._UU_ADDRESS = value;
                this._isUU_ADDRESSSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_TYPE 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isUU_TYPESetValue;

        private string _UU_TYPE;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string UU_TYPE
        {
            get
            {
                return this._UU_TYPE;
            }
            set
            {
                this._UU_TYPE = value;
                this._isUU_TYPESetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_STATUS 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isUU_STATUSSetValue;

        private string _UU_STATUS;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string UU_STATUS
        {
            get
            {
                return this._UU_STATUS;
            }
            set
            {
                this._UU_STATUS = value;
                this._isUU_STATUSSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_CREATE_TIME 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isUU_CREATE_TIMESetValue;

        private DateTime? _UU_CREATE_TIME;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? UU_CREATE_TIME
        {
            get
            {
                return this._UU_CREATE_TIME;
            }
            set
            {
                this._UU_CREATE_TIME = value;
                this._isUU_CREATE_TIMESetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_CREATE_USER 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isUU_CREATE_USERSetValue;

        private string _UU_CREATE_USER;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string UU_CREATE_USER
        {
            get
            {
                return this._UU_CREATE_USER;
            }
            set
            {
                this._UU_CREATE_USER = value;
                this._isUU_CREATE_USERSetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_UPDATE_TIME 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isUU_UPDATE_TIMESetValue;

        private DateTime? _UU_UPDATE_TIME;

        /// <summary>
        /// 数据表名
        /// </summary>
        public DateTime? UU_UPDATE_TIME
        {
            get
            {
                return this._UU_UPDATE_TIME;
            }
            set
            {
                this._UU_UPDATE_TIME = value;
                this._isUU_UPDATE_TIMESetValue = true;
            }
        }
        /// <summary>
        /// 指示当前对象自创建以来，属性 UU_CREATE_USER 是否已经设置了值（含设置为 null）。
        /// </summary>
        protected bool _isUU_UPDATE_USERSetValue;

        private string _UU_UPDATE_USER;

        /// <summary>
        /// 数据表名
        /// </summary>
        public string UU_UPDATE_USER
        {
            get
            {
                return this._UU_UPDATE_USER;
            }
            set
            {
                this._UU_UPDATE_USER = value;
                this._isUU_UPDATE_USERSetValue = true;
            }
        }
    }
}
