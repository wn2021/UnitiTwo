using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDB
{
    [Description("指定更新字段选项。")]
    public enum MyUpdateFieldsOptions
    {
        /// <summary>
        /// 包含指定的字段。
        /// </summary>
        // Token: 0x04000261 RID: 609
        [Description("包含指定的字段。")]
        IncludeFields,
        /// <summary>
        /// 排除指定的字段。
        /// </summary>
        // Token: 0x04000262 RID: 610
        [Description("排除指定的字段。")]
        ExcludeFields
    }
    public class MyUpdateFields
    {
        public MyUpdateFields(MyUpdateFieldsOptions option, params string[] fields)
        {
            this._option = option;
            this._fields = new List<string>();
            this._hashFields = new Dictionary<string, string>();
            if (fields != null)
            {
                foreach (string text in fields)
                {
                    if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text.Trim()))
                    {
                        string text2 = text.Trim();
                        if (!this._hashFields.ContainsKey(text2.ToUpper()))
                        {
                            this._hashFields.Add(text2.ToUpper(), text2);
                            this._fields.Add(text2);
                        }
                    }
                }
            }
        }
        // Token: 0x04000263 RID: 611
        private MyUpdateFieldsOptions _option;

        // Token: 0x04000264 RID: 612
        private List<string> _fields;

        // Token: 0x04000265 RID: 613
        private Dictionary<string, string> _hashFields;
        public MyUpdateFieldsOptions Option
        {
            get
            {
                return this._option;
            }
            set
            {
                this._option = value;
            }
        }
        public string[] Fields
        {
            get
            {
                return this._fields.ToArray();
            }
        }
        /// <summary>
        /// 检查指定字段是否存在于字段集合中。
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool ContainsField(string field)
        {
            return !string.IsNullOrEmpty(field) && !string.IsNullOrEmpty(field.Trim()) && this._hashFields.ContainsKey(field.ToUpper());
        }
    }
}
