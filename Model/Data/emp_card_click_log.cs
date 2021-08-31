using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class emp_card_click_log : ModelBase
    {
        /// <summary>
        /// 员工卡号
        /// </summary>
        protected bool _iscard_idSetValue;
        private string _card_id;
        public string card_id
        {
            get
            {
                return this._card_id;
            }
            set
            {
                this._card_id = value;
                this._iscard_idSetValue = true;
            }
        }
        /// <summary>
        /// 打卡时间
        /// </summary>
        protected bool _isclick_timeSetValue;
        private DateTime _click_time;
        public DateTime click_time
        {
            get
            {
                return this._click_time;
            }
            set
            {
                this._click_time = value;
                this._isclick_timeSetValue = true;
            }
        }
        /// <summary>
        /// excel导入批次号
        /// </summary>
        protected bool _isexcel_batch_noSetValue;
        private string _excel_batch_no;
        public string excel_batch_no
        {
            get
            {
                return this._excel_batch_no;
            }
            set
            {
                this._excel_batch_no = value;
                this._isexcel_batch_noSetValue = true;
            }
        }
    }
}
