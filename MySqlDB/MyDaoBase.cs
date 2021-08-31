
using BFC.SDK;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDB
{
    public class MyDaoBase
    {
        public MySqlConnection myConnection;
        public MySqlTransaction myTransaction;
        private int TrueValue = 1;
        private int FalseValue = 0;
        public MyDaoBase()
        {
            myConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString());
        }
        public MyDaoBase(bool isRequiredTransaction) {
            myConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString());
            myTransaction = myConnection.BeginTransaction();
            myConnection.Open();
        }
        /// <summary>
        /// 提交事务，释放连接
        /// </summary>
        public void Complete() {
            if (myTransaction != null) {
                myTransaction.Commit();
            }
            if (myConnection != null) {
                if (myConnection.State != ConnectionState.Closed) { myConnection.Close(); }
            }
        }
        /// <summary>
        /// 释放连接
        /// </summary>
        public void Dispose() {
            if (myTransaction != null)
            {
                myTransaction.Rollback();
            }
            if (myConnection != null)
            {
                if (myConnection.State != ConnectionState.Closed) { myConnection.Close(); }
            }
        }
        #region 执行SQL语句
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public int ExecuteNonQueryBySP(string strSql) {
            return ExecuteNonQueryBySP(strSql, null);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="strSql">脚本</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public int ExecuteNonQueryBySP(string strSql, IDataParameter[] param) {
            try {
                if (myConnection.State != ConnectionState.Open)
                {
                    myConnection.Open();
                }
                MySqlCommand cmd = new MySqlCommand(strSql, myConnection);
                //if (param != null) {
                //    MySqlParameter[] myParam = new MySqlParameter[param.Length];
                //    for (int i = 0; i < param.Length; i++) {
                //        myParam[i].Value = param[i].Value;
                //        myParam[i].ParameterName = param[i].ParameterName;
                //        myParam[i].Direction = param[i].Direction;
                //    }
                //    cmd.Parameters.AddRange(myParam);
                //}
                cmd.Parameters.AddRange(param);
                cmd.CommandType = CommandType.StoredProcedure;
                int intResult = cmd.ExecuteNonQuery();
                return intResult;
            }
            catch (Exception ex) {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
                this.Dispose();
                return -1;
            }
        }
        public int ExecuteSql(string strSql) {
            return ExecuteSql(strSql, null);
        }
        /// <summary>
        /// 无事务执行
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="myPar"></param>
        /// <returns></returns>
        public int ExecuteSql(string strSql, MySqlParameter[] myPar)
        {
            try
            {
                if (myConnection.State != ConnectionState.Open)
                {
                    myConnection.Open();
                }
                MySqlCommand cmd = new MySqlCommand(strSql, myConnection);
                if (myPar != null)
                {
                    foreach (MySqlParameter spar in myPar)
                    {
                        cmd.Parameters.Add(spar);
                    }
                }
                if (myTransaction != null) {
                    cmd.Transaction = myTransaction;
                }
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
                //if (myConnection.State != ConnectionState.Closed) { myConnection.Close(); }
                this.Dispose();
                return -1;
            }
        }
        /// <summary>
        /// 执行时带上事务
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="myPar"></param>
        /// <returns></returns>
        public int ExecuteSqlWithTransaction(string strSql, MySqlParameter[] myPar)
        {
            MySqlTransaction transaction = null;
            try
            {
                transaction = myConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand(strSql, myConnection);
                if (myPar != null)
                {
                    foreach (MySqlParameter spar in myPar)
                    {
                        cmd.Parameters.Add(spar);
                    }
                }
                cmd.Transaction = transaction;
                int result = cmd.ExecuteNonQuery();
                transaction.Commit();
                myConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
                if (myConnection.State != ConnectionState.Closed) {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    myConnection.Close();
                }
                return -1;
            }
        }

        #endregion

        #region 查询
        /// <summary>
        /// 查询语句GetDataSet
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string strSql)
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(strSql, myConnection);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 查询返回DataTable
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string strSql) {
            DataTable dt = null;
            DataSet ds= GetDataSet(strSql);
            if (ds != null) {
                dt = ds.Tables[0];
            }
            return dt;
        }
        /// <summary>
        /// 查询特定对象
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public object ExecuteScalar(string strSql) {
            DataTable dt = GetDataTable(strSql);
            if (dt == null|| dt.Rows.Count==0)
            { return null; }
            return dt.Rows[0][0];
        }
        /// <summary>
        /// 查询结果封装到List
        /// </summary>
        /// <param name="resultList"></param>
        /// <param name="t"></param>
        /// <param name="strSql"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="returnRowCountMaxValue"></param>
        /// <returns></returns>
        protected int DataReaderToList(IList resultList, Type t, string strSql, int startIndex, int? endIndex,int returnRowCountMaxValue) {
            if (resultList == null)
            {
                throw new ArgumentNullException("resultList");
            }
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }
            if (string.IsNullOrEmpty(strSql))
            {
                throw new ArgumentNullException("query sql");
            }
            if (endIndex != null && endIndex < startIndex) {
                throw new ArgumentOutOfRangeException("endIndex", "参数值不能小于 startIndex 参数的值。");
            }
            if (endIndex != null && returnRowCountMaxValue > 0 && endIndex >= returnRowCountMaxValue)
            {
                throw new ArgumentOutOfRangeException("returnRowCountMaxValue", "参数值要么为0，否则必须不小于 endIndex 参数的值。");
            }
            DataTable dt = GetDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0) {
                return -1;
            }

            //取得所有属性
            string[] array = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++) {
                array[i]=dt.Columns[i].ColumnName;
            }
            PropertyInfo[] array2 = new PropertyInfo[array.Length];
            PropertyInfo[] properties = t.GetProperties();
            bool[] array3 = new bool[array.Length];
            Type[] array4 = new Type[array.Length];
            foreach (PropertyInfo propertyInfo in properties)
            {
                int l = 0;
                while (l < array.Length)
                {
                    if (string.Compare(propertyInfo.Name, array[l], true, CultureInfo.InvariantCulture) == 0)
                    {
                        array2[l] = propertyInfo;
                        array4[l] = this.GetRealDataType(propertyInfo.PropertyType);
                        if (!array4[l].Equals(dt.Columns[array[l]].DataType))
                        {
                            array3[l] = true;
                            break;
                        }
                        break;
                    }
                    else
                    {
                        l++;
                    }
                }
            }
            int num = -1;
            //循环数据行赋值
            foreach (DataRow dr in dt.Rows)
            {
                num++;
                if (startIndex > num)
                {
                    continue;
                }
                if (startIndex == num)
                {
                    //DevDebug.WriteTraceInfo(this, "DataReaderToList() 要获取的页面数据读取开始...");
                }
                if (endIndex != null && endIndex.Value < num)
                {
                    //超出结束位
                    break;
                }               
                object obj = Activator.CreateInstance(t);
                for (int m = 0; m < array2.Length; m++)
                {
                    if (array2[m] != null && array2[m].CanWrite)
                    {
                        try
                        {
                            if (dr[array2[m].Name]==null)
                            {
                                array2[m].SetValue(obj, null, null);
                            }
                            else if (array3[m])
                            {
                                array2[m].SetValue(obj, Convert.ChangeType(dr[array2[m].Name], array4[m]), null);
                            }
                            else
                            {
                                array2[m].SetValue(obj, dr[array2[m].Name], null);
                            }
                        }
                        catch (Exception innerException)
                        {
                            throw new InvalidCastException(string.Format("给属性赋值时发生错误", new object[]
                            {
                                array2[m].Name
                            }), innerException);
                        }
                    }
                }
                resultList.Add(obj);
                if (endIndex == num)
                {
                    //到达结束位
                    //DevDebug.WriteTraceInfo(this, "DataReaderToList() 要获取的页面数据读取结束。");
                    break;
                }
                if (num >= returnRowCountMaxValue)
                {
                    //到达最大位
                    //DevDebug.WriteTraceInfo(this, "DataReaderToList() 已读取到最大返回行数。");
                    break;
                }
            }
            return num;
        }
        private Type GetRealDataType(Type t)
        {
            return DevReflection.GetNullableOriginalType(t);
        }
        /// <summary>
        /// 查询结果封装到List
        /// </summary>
        /// <param name="resultList"></param>
        /// <param name="t"></param>
        /// <param name="strSql"></param>
        /// <returns></returns>
        protected int DataReaderToList(IList resultList, Type t, string strSql) {
            return DataReaderToList(resultList,t, strSql,0,null,0);
        }
        /// <summary>
        /// 将查询结果封装为List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="selectSql"></param>
        public void FillQuery<T>(IList<T> list, string selectSql){
            DataReaderToList(list as IList, typeof(T), selectSql);
        }
        #endregion

        #region 插入
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="dataModelObj">对象</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public int Insert(object dataModelObj, string tableName)
        {
            return this.Insert(dataModelObj, tableName, null);
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="dataModelObj">对象</param>
        /// <param name="tableName">表名</param>
        /// <param name="fields">特定字段</param>
        /// <returns></returns>
        public int Insert(object dataModelObj, string tableName, MyUpdateFields fields) {
            if (dataModelObj is DataRow)
            {
                return InsertDataRow((DataRow)dataModelObj, tableName, fields);
            }
            if (dataModelObj is DataTable)
            {
                return InsertDataTable((DataTable)dataModelObj, tableName, fields);
            }
            if (dataModelObj is IList) {
                IList list = dataModelObj as IList;
                int num = 0;
                if (list.Count == 0)
                {
                    return 0;
                }
                Type type = null;
                PropertyInfo[] properties = null;
                string b = null;
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] != null)
                        {
                            if (list[i] is IList || list[i] is DataTable || list[i] is DataRow || list[i] is DataSet)
                            {
                                num += this.Insert(list[i], tableName, fields);
                            }
                            else
                            {
                                if (type == null)
                                {
                                    type = list[i].GetType();
                                    properties = type.GetProperties();
                                    b = type.FullName;
                                }
                                else if (list[i].GetType().FullName != b)
                                {
                                    type = list[i].GetType();
                                    properties = type.GetProperties();
                                    b = type.FullName;
                                }
                                num += this.InsertNonDataTable(list[i], type, properties, tableName, fields);
                            }
                        }
                    }
                    return num;
                }
                catch
                {
                    throw;
                }
            }
                return -1;
        }
        /// <summary>
        /// 根据非表结构数据取得插入数据
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="objType">对象类型</param>
        /// <param name="properties">对象属性</param>
        /// <param name="tableName">表名</param>
        /// <param name="fields">特定字段</param>
        /// <returns></returns>
        protected int InsertNonDataTable(object obj, Type objType, PropertyInfo[] properties, string tableName, MyUpdateFields fields) {
           string strSql= GenerateInsertNonDataTableSql(obj, objType, properties, tableName, fields);
            if (string.IsNullOrEmpty(strSql)) {
                return -1;
            }
            return this.ExecuteSql(strSql,null);
        }
        /// <summary>
        /// 根据非表结构数据取得插入脚本
        /// </summary>
        /// <param name="obj">对象（非表相关）</param>
        /// <param name="objType">对象类型</param>
        /// <param name="properties">对象属性</param>
        /// <param name="tableName">表名</param>
        /// <param name="fields">特定字段</param>
        /// <returns></returns>
        protected string GenerateInsertNonDataTableSql(object obj, Type objType, PropertyInfo[] properties, string tableName, MyUpdateFields fields) {
            return GenerateInsertNonDataTableSql(obj, objType, properties, tableName, fields, false);
        }
        /// <summary>
        /// 根据非表结构数据取得插入脚本
        /// </summary>
        /// <param name="obj">对象（非表相关）</param>
        /// <param name="objType">对象类型</param>
        /// <param name="properties">对象属性</param>
        /// <param name="tableName">表名</param>
        /// <param name="fields">特定字段</param>
        /// <param name="appendSeparator">是否需要分隔符</param>
        /// <returns></returns>
        protected string GenerateInsertNonDataTableSql(object obj, Type objType, PropertyInfo[] properties, string tableName, MyUpdateFields fields,bool appendSeparator) {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (objType == null)
            {
                throw new ArgumentNullException("type");
            }
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }
            if (string.IsNullOrEmpty(tableName))
            {
                throw new StringArgumentException("", "tableName");
            }
            if (properties.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder sqlBuffer = new StringBuilder();
            StringBuilder strName = new StringBuilder();
            StringBuilder strValue = new StringBuilder();
            try {
                strName.Append("INSERT INTO " + tableName);
                strValue.Append("VALUES");
                int idx = 0;
                foreach (PropertyInfo propertyInfo in properties) {
                    if (fields != null)
                    {
                        if (fields.ContainsField(propertyInfo.Name))
                        {
                            if (fields.Option == MyUpdateFieldsOptions.ExcludeFields)
                            {
                                continue;
                            }
                        }
                        else if (fields.Option == MyUpdateFieldsOptions.IncludeFields)
                        {
                            continue;
                        }                       
                    }
                    if (idx == 0)
                    {
                        strName.Append("(");
                        strValue.Append("(");
                    }
                    strName.Append(propertyInfo.Name);
                    string sqlValueString = this.GetSqlValueString(propertyInfo.GetValue(obj, null));
                    if (sqlValueString == null)
                    {
                        throw new Exception("Invalid value of " + propertyInfo.Name);
                    }
                    strValue.Append(sqlValueString);
                    idx++;
                }
                if (idx == 0) {
                    return null;
                }
                strName.Append(")");
                strValue.Append(")");
                sqlBuffer.Append(strName.ToString()).Append(strValue.ToString());
                if (appendSeparator)
                {
                    sqlBuffer.Append(";");
                }
                return sqlBuffer.ToString();
            } catch {
                return null;
            }
        }
        /// <summary>
        /// 根据DataTable插入数据
        /// </summary>
        /// <param name="dataTable">表数据</param>
        /// <param name="tableName">表名</param>
        /// <param name="fields">特定字段</param>
        /// <returns></returns>
        public int InsertDataTable(DataTable dataTable, string tableName, MyUpdateFields fields)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("dataTable");
            }
            if (dataTable.Rows.Count == 0 || dataTable.Columns.Count == 0)
            {
                return 0;
            }
            int num = 0;
            int result;
            try
            {
                foreach (object obj in dataTable.Rows)
                {
                    DataRow insertRow = (DataRow)obj;
                    num += this.InsertDataRow(insertRow, tableName, fields);
                }
                result = num;
            }
            catch
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 根据表行插入
        /// </summary>
        /// <param name="insertRow">行数据</param>
        /// <param name="tablename">表名</param>
        /// <param name="fields">特定字段</param>
        /// <returns></returns>
        public int InsertDataRow(DataRow insertRow, string tablename, MyUpdateFields fields) {
            string str = GenerateInsertDataRowSql(insertRow, tablename, fields);
            if (string.IsNullOrEmpty(str)) {
                return -1;
            }
            return this.ExecuteSql(str,null);
        }
        /// <summary>
        /// 根据DataRow生成插入语句
        /// </summary>
        /// <param name="insertRow">行数据</param>
        /// <param name="tablename">表名</param>
        /// <param name="fields">特定字段</param>
        /// <returns></returns>
        protected string GenerateInsertDataRowSql(DataRow insertRow, string tablename, MyUpdateFields fields) {
            return GenerateInsertDataRowSql(insertRow, tablename, fields, false);
        }
        /// <summary>
        /// 根据DataRow生成插入语句
        /// </summary>
        /// <param name="insertRow">行数据</param>
        /// <param name="tablename">表名</param>
        /// <param name="fields">特定字段</param>
        /// <param name="appendSeparator">是否要分隔符号</param>
        /// <returns></returns>
        protected string GenerateInsertDataRowSql(DataRow insertRow, string tablename, MyUpdateFields fields,bool appendSeparator) {
            if (insertRow == null)
            {
                throw new ArgumentNullException("insertRow");
            }
            if (string.IsNullOrEmpty(tablename))
            {
                throw new ArgumentNullException("tablename");
            }
            if (insertRow.Table.Columns.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder sqlBuffer = new StringBuilder();
            StringBuilder strName = new StringBuilder();
            StringBuilder strValue = new StringBuilder();
            strName.Append("INSERT INTO ").Append(tablename);
            strValue.Append("VALUES");
            DataTable table = insertRow.Table;
            int idx = 0;
            foreach (object obj in table.Columns) {
                DataColumn dataColumn = (DataColumn)obj;
                if (fields != null)
                {
                    if (fields.ContainsField(dataColumn.ColumnName))
                    {
                        if (fields.Option == MyUpdateFieldsOptions.ExcludeFields)
                        {
                            continue;
                        }
                    }
                    else if (fields.Option == MyUpdateFieldsOptions.IncludeFields)
                    {
                        continue;
                    }
                }
                if (idx == 0)
                {
                    strName.Append("(");
                    strValue.Append("(");
                }
                else
                {
                    strName.Append(",");
                    strValue.Append(",");
                }
                strName.Append(dataColumn.ColumnName);               
                string sqlValueString = this.GetSqlValueString(insertRow[dataColumn.ColumnName]);
                if (sqlValueString == null) {
                    throw new Exception("invalid value of"+dataColumn.ColumnName);
                }
                strValue.Append(sqlValueString);
                idx++;
            }
            if (idx == 0) {
                return null;
            }
            strName.Append(")");
            strValue.Append(")");
            sqlBuffer.Append(strName.ToString()).Append(strValue.ToString());
            if (appendSeparator)
            {
                sqlBuffer.Append(";");
            }
            return sqlBuffer.ToString();
        }
        #endregion

        #region 更新数据
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="dataModelObj"></param>
        /// <param name="tableName"></param>
        /// <param name="primaryKeyFields"></param>
        /// <returns></returns>
        public int Update(object dataModelObj, string tableName, string[] primaryKeyFields)
        {
            return this.Update(dataModelObj, tableName, null, primaryKeyFields);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="dataModelObj"></param>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="primaryKeyFields"></param>
        /// <returns></returns>
        public int Update(object dataModelObj, string tableName, MyUpdateFields fields,string[] primaryKeyFields)
        {
            if (dataModelObj is DataRow)
            {
                return UpdateDataRow((DataRow)dataModelObj, tableName, fields, primaryKeyFields);
            }
            if (dataModelObj is DataTable)
            {
                return UpdateDataTable((DataTable)dataModelObj, tableName, fields, primaryKeyFields);
            }
            if (dataModelObj is IList)
            {
                IList list = dataModelObj as IList;
                int num = 0;
                if (list.Count == 0)
                {
                    return 0;
                }
                Type type = null;
                PropertyInfo[] properties = null;
                string b = null;
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] != null)
                        {
                            if (list[i] is IList || list[i] is DataTable || list[i] is DataRow || list[i] is DataSet)
                            {
                                num += this.Update(list[i], tableName, fields, primaryKeyFields);
                            }
                            else
                            {
                                if (type == null)
                                {
                                    type = list[i].GetType();
                                    properties = type.GetProperties();
                                    b = type.FullName;
                                }
                                else if (list[i].GetType().FullName != b)
                                {
                                    type = list[i].GetType();
                                    properties = type.GetProperties();
                                    b = type.FullName;
                                }
                                num += this.UpdateNonDataTable(list[i], type, properties, tableName, fields, primaryKeyFields);
                            }
                        }
                    }
                    return num;
                }
                catch
                {
                    throw;
                }
            }
            return -1;
        }
        /// <summary>
        /// 根据DataTable更新表数据
        /// </summary>
        /// <param name="dataTable">表数据</param>
        /// <param name="tableName">表名</param>
        /// <param name="fields">特定字段</param>
        /// <param name="primaryKeyFields">主键字段</param>
        /// <returns></returns>
        public int UpdateDataTable(DataTable dataTable, string tableName, MyUpdateFields fields,string[] primaryKeyFields)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("dataTable");
            }
            if (dataTable.Rows.Count == 0 || dataTable.Columns.Count == 0)
            {
                return 0;
            }
            int num = 0;
            int result;
            try
            {
                foreach (object obj in dataTable.Rows)
                {
                    DataRow updateRow = (DataRow)obj;
                    num += this.UpdateDataRow(updateRow, tableName, fields, primaryKeyFields);
                }
                result = num;
            }
            catch
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 根据DataRow更新表数据
        /// </summary>
        /// <param name="updateRow">表行数据</param>
        /// <param name="tablename">表名</param>
        /// <param name="fields">特定字段</param>
        /// <param name="primaryKeyFields">主键字段</param>
        /// <returns></returns>
        public int UpdateDataRow(DataRow updateRow, string tablename, MyUpdateFields fields, string[] primaryKeyFields)
        {
            string str = GenerateUpdateDataRowSql(updateRow, tablename, fields, primaryKeyFields);
            if (string.IsNullOrEmpty(str))
            {
                return -1;
            }
            return this.ExecuteSql(str, null);
        }
        /// <summary>
        /// 根据DataRow生成更新语句
        /// </summary>
        /// <param name="updateRow">表行数据</param>
        /// <param name="tablename">表名</param>
        /// <param name="fields">特定字段</param>
        /// <param name="primaryKeyFields">主键字段</param>
        /// <returns></returns>
        protected string GenerateUpdateDataRowSql(DataRow updateRow, string tablename, MyUpdateFields fields, string[] primaryKeyFields) {
            return GenerateUpdateDataRowSql(updateRow, tablename, fields, primaryKeyFields,false);
        }
        /// <summary>
        /// 根据DataRow生成更新语句
        /// </summary>
        /// <param name="updateRow">表行数据</param>
        /// <param name="tablename">表名</param>
        /// <param name="fields">特定字段</param>
        /// <param name="primaryKeyFields">主键字段</param>
        /// <param name="appendSeparator">是否需要分隔符</param>
        /// <returns></returns>
        protected string GenerateUpdateDataRowSql(DataRow updateRow, string tablename, MyUpdateFields fields,string[] primaryKeyFields,bool appendSeparator)
        {
            if (updateRow == null)
            {
                throw new ArgumentNullException("insertRow");
            }
            if (string.IsNullOrEmpty(tablename))
            {
                throw new ArgumentNullException("tablename");
            }
            if (primaryKeyFields == null || primaryKeyFields.Length == 0)
            {
                throw new ArgumentNullException("primaryKeyFields");
            }
            if (updateRow.Table.Columns.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder sqlBuffer = new StringBuilder();
            //更新内容
            StringBuilder sqlUpdate = new StringBuilder();
            //更新条件
            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.AppendLine(" WHERE 1=1");
            DataTable table = updateRow.Table;
            int idx = 0;
            foreach (object obj in table.Columns)
            {
                DataColumn dataColumn = (DataColumn)obj;
                //指定更新字段时
                if (fields != null)
                {
                    if (fields.ContainsField(dataColumn.ColumnName))
                    {
                        if (fields.Option == MyUpdateFieldsOptions.ExcludeFields)
                        {
                            continue;
                        }
                        else
                        {
                            if (idx == 0)
                            {
                                sqlUpdate.Append(" SET ");
                            }
                            else
                            {
                                sqlUpdate.Append(" , ");
                            }
                            sqlUpdate.Append(dataColumn.ColumnName).Append(" = ");
                            string sqlValueString = this.GetSqlValueString(updateRow[dataColumn.ColumnName]);
                            if (sqlValueString == null)
                            {
                                throw new Exception("invalid value of" + dataColumn.ColumnName);
                            }
                            sqlUpdate.Append(sqlValueString);
                            idx++;
                        }
                    }
                   
                }
                else {
                    //不限定更新字段时，全字段更新
                    if (idx == 0)
                    {
                        sqlUpdate.Append(" SET ");
                    }
                    else
                    {
                        sqlUpdate.Append(" , ");
                    }
                    sqlUpdate.Append(dataColumn.ColumnName).Append(" = ");
                    string sqlValueString = this.GetSqlValueString(updateRow[dataColumn.ColumnName]);
                    if (sqlValueString == null)
                    {
                        throw new Exception("invalid value of" + dataColumn.ColumnName);
                    }
                    sqlUpdate.Append(sqlValueString);
                    idx++;
                }
                //更新条件
                if (primaryKeyFields.Contains(dataColumn.ColumnName)) {
                    sqlWhere.Append(" AND ").Append(dataColumn.ColumnName).Append(" = ");
                    string sqlValueString = this.GetSqlValueString(updateRow[dataColumn.ColumnName]);
                    if (sqlValueString == null)
                    {
                        throw new Exception("invalid value of" + dataColumn.ColumnName);
                    }
                    sqlWhere.Append(sqlValueString).AppendLine();
                }            
            }
            if (idx == 0)
            {
                return null;
            }
            sqlBuffer.Append("UPDATE ").Append(tablename);
            sqlBuffer.Append(sqlUpdate.ToString()).Append(sqlWhere.ToString());
            if (appendSeparator)
            {
                sqlBuffer.Append(";");
            }
            return sqlBuffer.ToString();
        }
        /// <summary>
        /// 根据非表对象更新数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objType"></param>
        /// <param name="properties"></param>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="primaryKeyFields"></param>
        /// <returns></returns>
        public int UpdateNonDataTable(object obj, Type objType, PropertyInfo[] properties, string tableName, MyUpdateFields fields, string[] primaryKeyFields)
        {
            string strSql = GenerateUpdateNonDataTableSql(obj, objType, properties, tableName, fields, primaryKeyFields);
            if (string.IsNullOrEmpty(strSql))
            {
                return -1;
            }
            return this.ExecuteSql(strSql, null);
        }
        /// <summary>
        /// 根据iList生成更新语句
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objType"></param>
        /// <param name="properties"></param>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="primaryKeyFields"></param>
        /// <returns></returns>
        protected string GenerateUpdateNonDataTableSql(object obj, Type objType, PropertyInfo[] properties, string tableName, MyUpdateFields fields, string[] primaryKeyFields) {
            return GenerateUpdateNonDataTableSql(obj, objType, properties, tableName, fields, primaryKeyFields,false);
        }
        /// <summary>
        /// 根据iList生成更新语句
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objType"></param>
        /// <param name="properties"></param>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="primaryKeyFields"></param>
        /// <param name="appendSeparator"></param>
        /// <returns></returns>
        protected string GenerateUpdateNonDataTableSql(object obj, Type objType, PropertyInfo[] properties, string tableName, MyUpdateFields fields, string[] primaryKeyFields, bool appendSeparator)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (objType == null)
            {
                throw new ArgumentNullException("type");
            }
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }
            if (string.IsNullOrEmpty(tableName))
            {
                throw new StringArgumentException("", "tableName");
            }
            if (primaryKeyFields == null || primaryKeyFields.Length == 0)
            {
                throw new ArgumentNullException("primaryKeyFields");
            }
            if (properties.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder sqlBuffer = new StringBuilder();
            StringBuilder sqlUpdate = new StringBuilder();
            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.AppendLine(" WHERE 1=1");
            try
            {
                int idx = 0;
                foreach (PropertyInfo propertyInfo in properties)
                {
                    if (fields != null)
                    {
                        if (fields.ContainsField(propertyInfo.Name))
                        {
                            if (fields.Option == MyUpdateFieldsOptions.ExcludeFields)
                            {
                                continue;
                            }
                            else
                            {
                                if (idx == 0)
                                {
                                    sqlUpdate.Append(" SET ");
                                }
                                else { sqlUpdate.Append(" , "); }
                                sqlUpdate.Append(propertyInfo.Name).Append(" = ");
                                string sqlValueString = this.GetSqlValueString(propertyInfo.GetValue(obj, null));
                                if (sqlValueString == null)
                                {
                                    throw new Exception("Invalid value of " + propertyInfo.Name);
                                }
                                sqlUpdate.Append(sqlValueString).AppendLine();
                            }
                        }
                    }
                    else
                    {
                        if (idx == 0)
                        {
                            sqlUpdate.Append(" SET ");
                        }
                        else { sqlUpdate.Append(" , "); }
                        sqlUpdate.Append(propertyInfo.Name).Append(" = ");
                        string sqlValueString = this.GetSqlValueString(propertyInfo.GetValue(obj, null));
                        if (sqlValueString == null)
                        {
                            throw new Exception("Invalid value of " + propertyInfo.Name);
                        }
                        sqlUpdate.Append(sqlValueString).AppendLine();
                    }
                    //更新条件
                    if (primaryKeyFields.Contains(propertyInfo.Name))
                    {
                        sqlWhere.Append(" AND ").Append(propertyInfo.Name).Append(" = ");
                        string sqlValueString = this.GetSqlValueString(propertyInfo.GetValue(obj, null));
                        if (sqlValueString == null)
                        {
                            throw new Exception("invalid value of" + propertyInfo.Name);
                        }
                        sqlWhere.Append(sqlValueString).AppendLine();
                    }
                }
                if (idx == 0)
                {
                    return null;
                }
                sqlBuffer.AppendLine(" UPDATE ").Append(tableName);
                sqlBuffer.Append(sqlUpdate).Append(sqlWhere);
                if (appendSeparator) {
                    sqlBuffer.Append(";");
                }
                return sqlBuffer.ToString();
            }
            catch
            {
                return null;
            }
        }
        #endregion 更新数据

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="dataModelObj">数据对象</param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKeyFields">主键字段</param>
        /// <returns></returns>
        public int Delete(object dataModelObj, string tableName, params string[] primaryKeyFields)
        {
            if (dataModelObj is DataRow)
            {
                return DeleteDataRow((DataRow)dataModelObj, tableName, primaryKeyFields);
            }
            if (dataModelObj is DataTable)
            {
                return DeleteDataTable((DataTable)dataModelObj, tableName, primaryKeyFields);
            }
            if (dataModelObj is IList)
            {
                IList list = dataModelObj as IList;
                int num = 0;
                if (list.Count == 0)
                {
                    return 0;
                }
                Type type = null;
                PropertyInfo[] properties = null;
                string b = null;
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] != null)
                        {
                            if (list[i] is IList || list[i] is DataTable || list[i] is DataRow || list[i] is DataSet)
                            {
                                num += this.Delete(list[i], tableName, primaryKeyFields);
                            }
                            else
                            {
                                if (type == null)
                                {
                                    type = list[i].GetType();
                                    properties = type.GetProperties();
                                    b = type.FullName;
                                }
                                else if (list[i].GetType().FullName != b)
                                {
                                    type = list[i].GetType();
                                    properties = type.GetProperties();
                                    b = type.FullName;
                                }
                                num += this.DeleteNonDataTable(list[i], type, properties, tableName, primaryKeyFields);
                            }
                        }
                    }
                    return num;
                }
                catch
                {
                    throw;
                }
            }
            return -1;
        }
        /// <summary>
        /// 根据表删除数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="tableName"></param>
        /// <param name="primaryKeyFields"></param>
        /// <returns></returns>
        public int DeleteDataTable(DataTable dataTable, string tableName, string[] primaryKeyFields)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("dataTable");
            }
            if (dataTable.Rows.Count == 0 || dataTable.Columns.Count == 0)
            {
                return 0;
            }
            int num = 0;
            int result;
            try
            {
                foreach (object obj in dataTable.Rows)
                {
                    DataRow updateRow = (DataRow)obj;
                    num += this.DeleteDataRow(updateRow, tableName, primaryKeyFields);
                }
                result = num;
            }
            catch
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 根据表行删除数据
        /// </summary>
        /// <param name="deleteRow"></param>
        /// <param name="tablename"></param>
        /// <param name="primaryKeyFields"></param>
        /// <returns></returns>
        public int DeleteDataRow(DataRow deleteRow, string tablename, string[] primaryKeyFields)
        {
            string str = GenerateDeleteDataRowSql(deleteRow, tablename, primaryKeyFields);
            if (string.IsNullOrEmpty(str))
            {
                return -1;
            }
            return this.ExecuteSql(str, null);
        }
        public int DeleteNonDataTable(object obj, Type objType, PropertyInfo[] properties, string tableName, string[] primaryKeyFields)
        {
            string strSql = GenerateDeleteNonDataTableSql(obj, objType, properties, tableName, primaryKeyFields);
            if (string.IsNullOrEmpty(strSql))
            {
                return -1;
            }
            return this.ExecuteSql(strSql, null);
        }
        /// <summary>
        /// 生成删除语句
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objType"></param>
        /// <param name="properties"></param>
        /// <param name="tableName"></param>
        /// <param name="primaryKeyFields"></param>
        /// <returns></returns>
        protected string GenerateDeleteNonDataTableSql(object obj, Type objType, PropertyInfo[] properties, string tableName, string[] primaryKeyFields) {
            return GenerateDeleteNonDataTableSql(obj, objType, properties, tableName, primaryKeyFields);
        }
        /// <summary>
        /// 生成删除语句
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objType"></param>
        /// <param name="properties"></param>
        /// <param name="tableName"></param>
        /// <param name="primaryKeyFields"></param>
        /// <param name="appendSeparator"></param>
        /// <returns></returns>
        protected string GenerateDeleteNonDataTableSql(object obj, Type objType, PropertyInfo[] properties, string tableName, string[] primaryKeyFields, bool appendSeparator)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (objType == null)
            {
                throw new ArgumentNullException("type");
            }
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }
            if (string.IsNullOrEmpty(tableName))
            {
                throw new StringArgumentException("", "tableName");
            }
            if (primaryKeyFields == null || primaryKeyFields.Length == 0)
            {
                throw new ArgumentNullException("primaryKeyFields");
            }
            if (properties.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder sqlBuffer = new StringBuilder();
            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.AppendLine(" WHERE 1=1");
            try
            {
                int idx = 0;
                foreach (PropertyInfo propertyInfo in properties)
                {                                                            
                    //更新条件
                    if (primaryKeyFields.Contains(propertyInfo.Name))
                    {
                        sqlWhere.Append(" AND ").Append(propertyInfo.Name).Append(" = ");
                        string sqlValueString = this.GetSqlValueString(propertyInfo.GetValue(obj, null));
                        if (sqlValueString == null)
                        {
                            throw new Exception("invalid value of" + propertyInfo.Name);
                        }
                        sqlWhere.Append(sqlValueString).AppendLine();
                    }
                }
                if (idx == 0)
                {
                    return null;
                }
                sqlBuffer.AppendLine(" DELETE FROM ").Append(tableName);
                sqlBuffer.Append(sqlWhere);
                if (appendSeparator)
                {
                    sqlBuffer.Append(";");
                }
                return sqlBuffer.ToString();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 根据表行数据生成删除语句
        /// </summary>
        /// <param name="deleteRow"></param>
        /// <param name="tablename"></param>
        /// <param name="primaryKeyFields"></param>
        /// <returns></returns>
        protected string GenerateDeleteDataRowSql(DataRow deleteRow, string tablename, string[] primaryKeyFields){
            return GenerateDeleteDataRowSql(deleteRow, tablename, primaryKeyFields,false);
        }
        /// <summary>
        /// 根据表行数据生成删除语句
        /// </summary>
        /// <param name="deleteRow"></param>
        /// <param name="tablename"></param>
        /// <param name="primaryKeyFields"></param>
        /// <param name="appendSeparator"></param>
        /// <returns></returns>
        protected string GenerateDeleteDataRowSql(DataRow deleteRow, string tablename, string[] primaryKeyFields, bool appendSeparator)
        {
            if (deleteRow == null)
            {
                throw new ArgumentNullException("insertRow");
            }
            if (string.IsNullOrEmpty(tablename))
            {
                throw new ArgumentNullException("tablename");
            }
            if (primaryKeyFields == null || primaryKeyFields.Length == 0)
            {
                throw new ArgumentNullException("primaryKeyFields");
            }
            if (deleteRow.Table.Columns.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder sqlBuffer = new StringBuilder();
            //更新内容
            StringBuilder sqlUpdate = new StringBuilder();
            //更新条件
            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.AppendLine(" WHERE 1=1");
            DataTable table = deleteRow.Table;
            int idx = 0;
            foreach (object obj in table.Columns)
            {
                DataColumn dataColumn = (DataColumn)obj;                
                //更新条件
                if (primaryKeyFields.Contains(dataColumn.ColumnName))
                {
                    sqlWhere.Append(" AND ").Append(dataColumn.ColumnName).Append(" = ");
                    string sqlValueString = this.GetSqlValueString(deleteRow[dataColumn.ColumnName]);
                    if (sqlValueString == null)
                    {
                        throw new Exception("invalid value of" + dataColumn.ColumnName);
                    }
                    sqlWhere.Append(sqlValueString).AppendLine();
                }
            }
            if (idx == 0)
            {
                return null;
            }
            sqlBuffer.Append("DELETE FROM ").Append(tablename);
            sqlBuffer.Append(sqlUpdate.ToString()).Append(sqlWhere.ToString());
            if (appendSeparator)
            {
                sqlBuffer.Append(";");
            }
            return sqlBuffer.ToString();
        }
        #endregion 删除数据

        #region 共通方法
        /// <summary>
        /// 根据值属性取得脚本字符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual string GetSqlValueString(object value)
        {
            return this.GetSqlValueString(value, null);
        }
        /// <summary>
        /// 根据值属性取得脚本字符
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dateFormat">需求格式</param>
        /// <returns></returns>
        public virtual string GetSqlValueString(object value, string dateFormat)
        {
            string result;
            if (value == null || value == DBNull.Value)
            {
                result = "NULL";
            }
            else if (value is DateTime)
            {
                result = this.GetDateTimeSqlString((DateTime)value, dateFormat);
            }
            else if (value is bool)
            {
                if ((bool)value)
                {
                    result = this.TrueValue.ToString();
                }
                else
                {
                    result = this.FalseValue.ToString();
                }
            }
            else if (value is decimal || value is short || value is int || value is long || value is double || value is byte || value is sbyte || value is float || value is ushort || value is uint || value is ulong)
            {
                result = value.ToString();
            }
            else if (value is string || value is char || value is Guid)
            {
                result = this.EndProcessStringSqlValueString("'" + value.ToString().Replace("'", "''") + "'");
            }
            else
            {
                result = null;
            }
            return result;
        }
        protected string EndProcessStringSqlValueString(string stringSqlValue)
        {
            return stringSqlValue;
        }
        protected string GetDateTimeSqlString(DateTime date, string format)
        {
            return "str_to_date('" + date.ToString(string.IsNullOrEmpty(format) ? "yyyyMMddHHmmss" : format) + "','%Y%m%d%H%i%s')";
        }
        protected  string DateFormatForDB
        {
            get
            {
                return "yyyyMMdd";
            }
        }
        /// <summary>
        /// 取得模糊查询字符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetLikeSqlValueString(object value) {
            return GetLikeSqlValueString(value, true, true);
        }
        /// <summary>
        /// 取得模糊查询字符
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isLeftLike">左like</param>
        /// <param name="isRightLike">右like</param>
        /// <returns></returns>
        protected string GetLikeSqlValueString(object value, bool isLeftLike, bool isRightLike) {
            string str = "'";
            if (isLeftLike) {
                str = str + "%";
            }
            str = str + value.ToString();
            if (isLeftLike)
            {
                str = str + "%";
            }
            str = str + "'";
            return str;
        }
        #endregion 共通方法
    }
}
