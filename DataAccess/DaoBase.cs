using BFC.FRM.Dao;
using BFC.SDK;
using BFC.SDK.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class  DaoBase: InfraDaoBase
    {
        private static byte[] iv = Convert.FromBase64String("4PFkjMN1fD0=");
        private static byte[] key = Convert.FromBase64String("Tyix3yBrcauS6KXchFQVd142VRyeyUZ7");

        static DaoBase()
        {
            SetEncryptKeyIV();
        }

        public DaoBase() : base()
        {
            //默认将执行SQL语句的超时时间设置为2分钟
            SetDataAccessClientCommandTimeOut(300);//lcm 2012-9-6 默认超时时间设置为5分钟
        }
        public DaoBase(DaoBase dao)
            : base(dao)
        {
        }
        private static void SetEncryptKeyIV()
        {
            DataAccessFactory.SetDecryptKeyIV(key, iv);
        }
        protected void SetDataAccessClientCommandTimeOut(int timeout)
        {
            if (timeout == 0
                || timeout > this.DataAccessClient.CommandTimeOut)
            {
                this.DataAccessClient.CommandTimeOut = timeout;
            }
        }
        protected override string DateFormatForDB
        {
            get
            {
                return "yyyyMMdd";
            }
        }

        protected override string DateTimeFormatForDB
        {
            get
            {
                return @"yyyyMMdd HH\:mm\:ss";
            }
        }

        protected string PosVersionDateTimeFormatForDB
        {
            get
            {
                return @"yyyy-MM-dd HH\:mm\:ss.000";
            }
        }
        /// <summary>
        /// 获取组装的日期类型比较的 SQL 条件字符串，
        /// 本方法仅仅适用于不含时间的日期作为SQL语句比较条件。
        /// 注意：本方法适用于某个字段日期=提供的参数日期。
        /// </summary>
        /// <param name="date">需要查询的日期值。</param>
        /// <returns>返回组装的日期类型比较的 SQL 条件字符串。</returns>
        protected string GetSqlDateCondition(string fieldName, DateTime? date)
        {
            Argument.CheckStringParameter(fieldName, "fieldName");
            if (!date.HasValue)
            {
                return "1=1";
            }
            else
            {
                return "(" + fieldName + ">=" + GetSqlValueString(date.Value, DateFormatForDB) + " AND " + fieldName + "<" + GetSqlValueString(date.Value.AddDays(1), DateFormatForDB) + ")";
            }
        }

        /// <summary>
        /// 获取组装的日期范围比较的 SQL 条件字符串，
        /// 本方法仅仅适用于不含时间的日期作为SQL语句比较条件。
        /// 如果不提供开始日期，则组装小于或等于结束日期的SQL语句。
        /// 如果不提供结束日期，则组装大于或等于开始日期的SQL语句。
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="dateFrom">需要查询的日期起始值。</param>
        /// <param name="dateTo">需要查询的日期结束值。</param>
        /// <returns>返回组装的日期类型比较的 SQL 条件字符串。</returns>
        protected string GetSqlDateCondition(string fieldName, DateTime? dateFrom, DateTime? dateTo)
        {
            Argument.CheckStringParameter(fieldName, "fieldName");
            if (!dateFrom.HasValue && !dateTo.HasValue)
            {
                return "1=1";
            }
            if (dateFrom.HasValue && dateTo.HasValue)
            {
                return "(" + fieldName + ">=" + GetSqlValueString(dateFrom.Value, DateFormatForDB) + " AND " + fieldName + "<" + GetSqlValueString(dateTo.Value.AddDays(1), DateFormatForDB) + ")";
            }
            else if (!dateFrom.HasValue)
            {
                return "(" + fieldName + "<" + GetSqlValueString(dateTo.Value.AddDays(1), DateFormatForDB) + ")";
            }
            else
            {
                return "(" + fieldName + ">=" + GetSqlValueString(dateFrom.Value, DateFormatForDB) + ")";
            }
        }

        /// <summary>
        /// 获取组装的日期范围比较的 SQL 条件字符串，
        /// 必须指定年、月的值
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        protected string GetSqlDateConditionByYearMonth(string fieldName, int year, int month)
        {
            Argument.CheckStringParameter(fieldName, "fieldName");

            //年月开始日、结束日
            DateTime dateFrom = new DateTime(year, month, 1);
            DateTime dateTo = dateFrom.AddMonths(1).AddDays(-1);

            return this.GetSqlDateCondition(fieldName, dateFrom, dateTo);
        }
        /// <summary>
        /// 判断值是否为空
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>返回是否为空</returns>
        /// <remarks>added by 000131 2009-12-29</remarks>
        public bool IsNullOrEmpty(object value)
        {
            if (value == null) return true;
            if (value == DBNull.Value) return true;
            if (value == Type.Missing) return true;
            if (value.ToString().Trim() == string.Empty) return true;
            return false;

        }
        #region Add by lcm 2018-4-21 增加支持多个数据库连接配置的支持======================
        /// <summary>
        /// 重写 DataAccessFactoryConectionName 属性提供数据连接配置名称.
        /// </summary>
        protected override string DataAccessFactoryConectionName
        {
            get
            {
                return null;
            }
        }
        /// <summary>
        /// 重写 DecryptConnectionStringIV 属性提供解密数据库连接字符串的 KEY.
        /// </summary>
        protected override byte[] DecryptConnectionStringKey
        {
            get
            {
                return key;
            }
        }
        /// <summary>
        /// 重写 DecryptConnectionStringIV 属性提供解密数据库连接字符串的 IV.
        /// </summary>
        protected override byte[] DecryptConnectionStringIV
        {
            get
            {
                return iv;
            }
        }


        #endregion
        public enum SysValueType
        {
            SysValueInt,
            SysValue2Int,
            SysValueDecimal,
            SysValue2Decimal,
            SysValueIntSysValue2Decimal,
            SysValueDecimalSysValue2Int,
            BothInt,
            BothDecimal,
            None
        }
    }
}
