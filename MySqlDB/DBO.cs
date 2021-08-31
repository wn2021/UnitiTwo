using BFC.SDK;
using BFC.SDK.Data;
using BFC.SDK.Data.DataAccess;
using BFC.SDK.Diagnostics;
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
    public class DBO
    {
        public MySqlConnection myConnection;
        private int TrueValue=1;
        private int FalseValue=0;

        public DBO()
        {
            myConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString());
        }

        #region 执行SQL语句
        public int ExecuteSql(string strSql, MySqlParameter[] myPar)
        {
            try
            {
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand(strSql, myConnection);
                if (myPar != null)
                {
                    foreach (MySqlParameter spar in myPar)
                    {
                        cmd.Parameters.Add(spar);
                    }
                }
                int result = cmd.ExecuteNonQuery();
                myConnection.Close();
                return result;
            }
            catch
            {
                if (myConnection.State != ConnectionState.Closed) { myConnection.Close(); }
                return 0;
            }
        }
        
        #endregion

        #region 查询 返回DataSet
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
        #endregion
        private MySqlParameter CreateParameter(string name, object value)
        {
            MySqlParameter para = new MySqlParameter();
            para.ParameterName = name;
            para.Value = value;
            return para;
        }
        private MySqlParameter[] NameValueArrayToParamValueArray(params object[] parameterNameAndValues)
        {
            if (parameterNameAndValues == null && parameterNameAndValues.Length == 0)
            {
                return null;
            }
            if (parameterNameAndValues.Length % 2 != 0)
            {
                throw new ArgumentException("缺少参数");
            }
            MySqlParameter[] array = new MySqlParameter[parameterNameAndValues.Length / 2];
            for (int i = 0; i < parameterNameAndValues.Length; i += 2)
            {
                if (!(parameterNameAndValues[i] is string))
                {
                    throw new ArgumentException("参数必须为字符串");
                }
                array[i / 2] = this.CreateParameter((string)parameterNameAndValues[i], parameterNameAndValues[i + 1]);
            }
            return array;
        }
        #region 查询数据到List
        public void FillQuery<T>(IList<T> list, string selectSql, params object[] parameterNameAndValues) where T : class, new()
        {
            this.FillQuery(list as IList, typeof(T), selectSql, parameterNameAndValues);
        }
        public void FillQuery(IList list, Type t, string selectSql, params object[] parameterNameAndValues)
        {
            this.FillQuery(list, t, selectSql, this.NameValueArrayToParamValueArray(parameterNameAndValues));
        }
        public void FillQuery<T>(IList<T> list, string selectSql, MySqlParameter[] commandParameters) where T : class, new()
        {
            this.FillQuery(list as IList, typeof(T), selectSql, commandParameters);
        }
        public void FillQuery(IList list, Type t, string selectSql, MySqlParameter[] commandParameters)
        {
            this.ExecuteQueryImpl(list, t, CommandType.Text, selectSql, commandParameters);
        }
        private void ExecuteQueryImpl(IList resultList, Type t, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            if (resultList == null)
            {
                throw new ArgumentNullException("resultList");
            }
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }            
            MySqlCommand dbCommand = null;
            MySqlDataReader dataReader = null;
            try
            {
                myConnection.Open();
                dbCommand = new MySqlCommand(commandText, myConnection);
                dbCommand.CommandType = commandType;
                dbCommand.Parameters.AddRange(commandParameters);
                dataReader = dbCommand.ExecuteReader();
                this.DataReaderToList(resultList, t, dataReader, 0, null, null);
                myConnection.Close();
            }
            catch (Exception ex)
            {
                if (myConnection.State != ConnectionState.Closed) { myConnection.Close(); }
                //this.WriteException(ex, commandText, commandParameters);
                throw;
            }
            finally
            {
                if (dataReader != null)
                {
                    try
                    {
                        dataReader.Close();
                        dataReader.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (dbCommand != null)
                {
                    try
                    {
                        dbCommand.Parameters.Clear();
                        dbCommand.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
        }
        protected int DataReaderToList(IList resultList, Type t, MySqlDataReader reader, int startIndex, int? endIndex, Dictionary<string, decimal> sumFields)
        {
            return this.DataReaderToList(resultList, t, reader, startIndex, endIndex, sumFields, 0);
        }
        protected int DataReaderToList(IList resultList, Type t, MySqlDataReader reader, int startIndex, int? endIndex, Dictionary<string, decimal> sumFields, int returnRowCountMaxValue)
        {
            if (resultList == null)
            {
                throw new ArgumentNullException("resultList");
            }
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (startIndex < 0)
            {
                //throw new ArgumentOutOfRangeException("startIndex", this.GetStringResource("StartRowIndexMustMoreThan0", Array.Empty<object>()));
            }
            if (endIndex != null && endIndex != null && endIndex < 0)
            {
                //throw new ArgumentOutOfRangeException("endIndex", this.GetStringResource("EndRowIndexMustMoreThan0", Array.Empty<object>()));
            }
            if (endIndex != null && endIndex != null && returnRowCountMaxValue > 0 && endIndex >= returnRowCountMaxValue)
            {
                throw new ArgumentOutOfRangeException("returnRowCountMaxValue", "参数值要么为0，否则必须大于 endIndex 参数的值。");
            }
            string[] array = new string[reader.FieldCount];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = reader.GetName(i);
            }
            PropertyInfo[] array2 = new PropertyInfo[array.Length];
            PropertyInfo[] properties = t.GetProperties();
            bool[] array3 = new bool[array.Length];
            Type[] array4 = new Type[array.Length];
            bool flag = sumFields != null && sumFields.Count > 0;
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            if (flag)
            {
                foreach (KeyValuePair<string, decimal> keyValuePair in sumFields)
                {
                    bool flag2 = false;
                    if (string.IsNullOrEmpty(keyValuePair.Key) || string.IsNullOrEmpty(keyValuePair.Key.Trim()))
                    {
                        throw new ArgumentException("指定的需要合计的列没有提供字段名称。", "sumFields");
                    }
                    for (int j = 0; j < array.Length; j++)
                    {
                        if (string.Compare(keyValuePair.Key, array[j], true, CultureInfo.InvariantCulture) == 0)
                        {
                            dictionary.Add(keyValuePair.Key, j);
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        throw new ArgumentException("需要合计的列“" + keyValuePair.Key + "”在查询的数据结果中不存在。", "sumFields");
                    }
                }
                foreach (KeyValuePair<string, int> keyValuePair2 in dictionary)
                {
                    sumFields[keyValuePair2.Key] = 0m;
                }
            }
            foreach (PropertyInfo propertyInfo in properties)
            {
                int l = 0;
                while (l < array.Length)
                {
                    if (string.Compare(propertyInfo.Name, array[l], true, CultureInfo.InvariantCulture) == 0)
                    {
                        array2[l] = propertyInfo;
                        array4[l] = this.GetRealDataType(propertyInfo.PropertyType);
                        if (!array4[l].Equals(reader.GetFieldType(l)))
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
            DevDebug.WriteTraceInfo(this, "DataReaderToList() DataReader 数据扫描开始...");
            while (reader.Read())
            {
                num++;
                if (flag)
                {
                    using (Dictionary<string, int>.Enumerator enumerator2 = dictionary.GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            KeyValuePair<string, int> keyValuePair3 = enumerator2.Current;
                            object value = reader.GetValue(keyValuePair3.Value);
                            if (value != DBNull.Value)
                            {
                                string key = keyValuePair3.Key;
                                sumFields[key] += Convert.ToDecimal(value);
                            }
                        }
                        goto IL_375;
                    }
                    goto IL_367;
                }
                goto IL_367;
                IL_375:
                if (startIndex > num)
                {
                    continue;
                }
                if (startIndex == num)
                {
                    DevDebug.WriteTraceInfo(this, "DataReaderToList() 要获取的页面数据读取开始...");
                }
                if (endIndex != null && endIndex != null && endIndex.Value < num)
                {
                    continue;
                }
                object obj = Activator.CreateInstance(t);
                for (int m = 0; m < array2.Length; m++)
                {
                    if (array2[m] != null && array2[m].CanWrite)
                    {
                        try
                        {
                            if (reader.IsDBNull(m))
                            {
                                array2[m].SetValue(obj, null, null);
                            }
                            else if (array3[m])
                            {
                                array2[m].SetValue(obj, Convert.ChangeType(reader.GetValue(m), array4[m]), null);
                            }
                            else
                            {
                                array2[m].SetValue(obj, reader.GetValue(m), null);
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
                    DevDebug.WriteTraceInfo(this, "DataReaderToList() 要获取的页面数据读取结束。");
                    continue;
                }
                continue;
                IL_367:
                if (returnRowCountMaxValue <= 0 || num < returnRowCountMaxValue)
                {
                    goto IL_375;
                }
                break;
            }
            DevDebug.WriteTraceInfo(this, "DataReaderToList() DataReader 数据扫描结束。");
            return num + 1;
        }
        private Type GetRealDataType(Type t)
        {
            return DevReflection.GetNullableOriginalType(t);
        }
        #endregion 查询数据到List

        #region 批量插入
        public int Insert(object dataModelObj, string tableName)
        {
            return this.Insert(dataModelObj, tableName, null);
        }
        public int Insert(object dataModelObj, string tableName, UpdateFields fields)
        {
            if (dataModelObj == null)
            {
                throw new ArgumentNullException("dataModelObj");
            }
            if (dataModelObj is DataRow)
            {
                return this.InsertDataRow((DataRow)dataModelObj, tableName, fields);
            }
            if (dataModelObj is DataTable)
            {
                return this.InsertDataTable((DataTable)dataModelObj, tableName, fields);
            }
            if (dataModelObj is DataSet)
            {
                return this.InsertDataSet((DataSet)dataModelObj, fields);
            }
            if (dataModelObj is DataView)
            {
                return this.InsertDataTable(((DataView)dataModelObj).Table, tableName, fields);
            }
            if (dataModelObj is DataRowView)
            {
                return this.InsertDataRow(((DataRowView)dataModelObj).Row, tableName, fields);
            }
            if (dataModelObj is IDataReader)
            {
                return this.BatchInsertDataReader((MySqlDataReader)dataModelObj, tableName, fields, 1);
            }
            if (dataModelObj is IList)
            {
                int num = 0;
                IList list = dataModelObj as IList;
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
            Type type2 = dataModelObj.GetType();
            PropertyInfo[] properties2 = dataModelObj.GetType().GetProperties();
            return this.InsertNonDataTable(dataModelObj, type2, properties2, tableName, fields);
        }
        private int InsertNonDataTable(object obj, Type objType, PropertyInfo[] properties, string tableName, UpdateFields fields)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ArrayList arrayList = new ArrayList();
            int num = 0;
            if (!this.GenerateInsertNonDataTableSql(stringBuilder, arrayList, 0, obj, objType, properties, tableName, fields))
            {
                return 0;
            }
            if (arrayList == null || arrayList.Count == 0)
            {
                num += this.ExecuteNonQuery(stringBuilder.ToString(), Array.Empty<object>());
            }
            else
            {
                num += this.ExecuteNonQuery(stringBuilder.ToString(), (IDataParameter[])arrayList.ToArray(typeof(IDataParameter)));
            }
            return num;
        }
        private bool GenerateInsertNonDataTableSql(StringBuilder sqlBuffer, ArrayList parameterList, int startParamIndex, object obj, Type objType, PropertyInfo[] properties, string tableName, UpdateFields fields)
        {
            return this.GenerateInsertNonDataTableSql(sqlBuffer, parameterList, startParamIndex, obj, objType, properties, tableName, fields, false);
        }
        private bool GenerateInsertNonDataTableSql(StringBuilder sqlBuffer, ArrayList parameterList, int startParamIndex, object obj, Type objType, PropertyInfo[] properties, string tableName, UpdateFields fields, bool appendSeparator)
        {
            if (sqlBuffer == null)
            {
                throw new ArgumentNullException("sqlBuffer");
            }
            if (parameterList == null)
            {
                throw new ArgumentNullException("parameterList");
            }
            if (startParamIndex < 0)
            {
                startParamIndex = 0;
            }
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
                return false;
            }
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            bool result;
            try
            {
                bool flag = obj is UpdatableDataModelBase;
                int num = startParamIndex;
                stringBuilder.Append("INSERT INTO " + tableName);
                foreach (PropertyInfo propertyInfo in properties)
                {
                    if (propertyInfo.CanRead && (!flag || ((UpdatableDataModelBase)obj).CheckIsSetValue(propertyInfo)))
                    {
                        ModelUpdatableAttribute modelUpdatableAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ModelUpdatableAttribute)) as ModelUpdatableAttribute;
                        if (modelUpdatableAttribute == null || !modelUpdatableAttribute.IsIdentity)
                        {
                            if (fields != null)
                            {
                                if (fields.ContainsField(propertyInfo.Name))
                                {
                                    if (fields.Option == UpdateFieldsOptions.ExcludeFields)
                                    {
                                        goto IL_221;
                                    }
                                }
                                else if (fields.Option == UpdateFieldsOptions.IncludeFields)
                                {
                                    goto IL_221;
                                }
                            }
                            if (stringBuilder2.Length == 0)
                            {
                                stringBuilder2.Append("(");
                            }
                            else
                            {
                                stringBuilder2.Append(",");
                            }
                            stringBuilder2.Append(propertyInfo.Name);
                            if (stringBuilder3.Length == 0)
                            {
                                stringBuilder3.Append("(");
                            }
                            else
                            {
                                stringBuilder3.Append(",");
                            }
                            string sqlValueString = this.GetSqlValueString(propertyInfo.GetValue(obj, null));
                            if (sqlValueString != null)
                            {
                                stringBuilder3.Append(sqlValueString);
                            }
                            else
                            {
                                num++;
                                string tmp = "@p" + num.ToString();
                                stringBuilder3.Append(tmp);
                                parameterList.Add(this.CreateParameter(tmp, propertyInfo.GetValue(obj, null)));
                            }
                        }
                    }
                    IL_221:;
                }
                if (stringBuilder2.Length == 0)
                {
                    stringBuilder2.Remove(0, stringBuilder2.Length);
                    stringBuilder3.Remove(0, stringBuilder3.Length);
                    stringBuilder.Remove(0, stringBuilder.Length);
                    result = false;
                }
                else
                {
                    stringBuilder2.Append(") ");
                    stringBuilder3.Append(")");
                    stringBuilder.Append(" " + stringBuilder2.ToString() + " VALUES " + stringBuilder3.ToString());
                    sqlBuffer.Append(stringBuilder.ToString());
                    if (appendSeparator)
                    {
                        sqlBuffer.Append(";");
                    }
                    result = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                stringBuilder2.Remove(0, stringBuilder2.Length);
                stringBuilder3.Remove(0, stringBuilder3.Length);
                stringBuilder.Remove(0, stringBuilder.Length);
            }
            return result;
        }
        private bool GenerateInsertDataReaderSql(StringBuilder sqlBuffer, ArrayList parameterList, int startParamIndex, MySqlDataReader insertReader, string tableName, UpdateFields fields, bool appendSeparator)
        {
            if (sqlBuffer == null)
            {
                throw new ArgumentNullException("sqlBuffer");
            }
            if (parameterList == null)
            {
                throw new ArgumentNullException("parameterList");
            }
            Argument.CheckStringParameter(tableName, "tableName");
            if (startParamIndex < 0)
            {
                startParamIndex = 0;
            }
            if (insertReader == null)
            {
                throw new ArgumentNullException("insertRow");
            }
            if (insertReader.FieldCount == 0)
            {
                return false;
            }
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            int num = startParamIndex;
            bool result;
            try
            {
                stringBuilder.Append("INSERT INTO " + tableName);
                //new string[insertReader.FieldCount];
                int i = 0;
                while (i < insertReader.FieldCount)
                {
                    string name = insertReader.GetName(i);
                    if (fields == null)
                    {
                        goto IL_CB;
                    }
                    if (fields.ContainsField(name))
                    {
                        if (fields.Option != UpdateFieldsOptions.ExcludeFields)
                        {
                            goto IL_CB;
                        }
                    }
                    else if (fields.Option != UpdateFieldsOptions.IncludeFields)
                    {
                        goto IL_CB;
                    }
                    IL_19E:
                    i++;
                    continue;
                    IL_CB:
                    if (stringBuilder2.Length == 0)
                    {
                        stringBuilder2.Append("(");
                    }
                    else
                    {
                        stringBuilder2.Append(",");
                    }
                    stringBuilder2.Append(name);
                    if (stringBuilder3.Length == 0)
                    {
                        stringBuilder3.Append("(");
                    }
                    else
                    {
                        stringBuilder3.Append(",");
                    }
                    string sqlValueString = this.GetSqlValueString(insertReader.GetValue(i));
                    if (sqlValueString != null)
                    {
                        stringBuilder3.Append(sqlValueString);
                        goto IL_19E;
                    }
                    num++;
                    string tmp= "@p" + num.ToString();
                    stringBuilder3.Append(tmp);
                    parameterList.Add(this.CreateParameter(tmp, insertReader.GetValue(i)));
                    goto IL_19E;
                }
                if (stringBuilder2.Length == 0)
                {
                    result = false;
                }
                else
                {
                    stringBuilder2.Append(") ");
                    stringBuilder3.Append(")");
                    stringBuilder.Append(" " + stringBuilder2.ToString() + " VALUES " + stringBuilder3.ToString());
                    sqlBuffer.Append(stringBuilder.ToString());
                    if (appendSeparator)
                    {
                        sqlBuffer.Append(";");
                    }
                    result = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                stringBuilder.Remove(0, stringBuilder.Length);
                stringBuilder2.Remove(0, stringBuilder2.Length);
                stringBuilder3.Remove(0, stringBuilder3.Length);
            }
            return result;
        }
        private int BatchInsertDataReader(MySqlDataReader reader, string tableName, UpdateFields fields, int buffer)
        {
            Argument.CheckParameterNull(reader, "reader");
            Argument.CheckStringParameter(tableName, "tableName");
            if (reader.IsClosed)
            {
                throw new ArgumentException("提供的 System.Data.IDataReader 对象处于关闭状态。");
            }
            if (buffer <= 0)
            {
                buffer = 1;
            }
            bool flag = buffer > 1;
            int num = 0;
            StringBuilder stringBuilder = new StringBuilder();
            ArrayList arrayList = new ArrayList();
            int num2 = 0;
            int result;
            try
            {
                while (reader.Read())
                {
                    if (this.GenerateInsertDataReaderSql(stringBuilder, arrayList, arrayList.Count, reader, tableName, fields, flag))
                    {
                        num2++;
                        if (num2 == buffer)
                        {
                            if (flag)
                            {
                                stringBuilder.Insert(0, "");
                                stringBuilder.Append(";");
                            }
                            if (arrayList.Count > 0)
                            {
                                num += this.ExecuteNonQuery(stringBuilder.ToString(), (IDataParameter[])arrayList.ToArray(typeof(IDataParameter)));
                            }
                            else
                            {
                                num += this.ExecuteNonQuery(stringBuilder.ToString(), Array.Empty<object>());
                            }
                            num2 = 0;
                            arrayList.Clear();
                            stringBuilder.Remove(0, stringBuilder.Length);
                        }
                    }
                }
                if (num2 > 0)
                {
                    if (flag)
                    {
                        stringBuilder.Insert(0, "");
                        stringBuilder.Append(";");
                    }
                    if (arrayList.Count > 0)
                    {
                        num += this.ExecuteNonQuery(stringBuilder.ToString(), (IDataParameter[])arrayList.ToArray(typeof(IDataParameter)));
                    }
                    else
                    {
                        num += this.ExecuteNonQuery(stringBuilder.ToString(), Array.Empty<object>());
                    }
                    arrayList.Clear();
                    stringBuilder.Remove(0, stringBuilder.Length);
                }
                result = num;
            }
            catch
            {
                throw;
            }
            finally
            {
                try
                {
                    if (arrayList != null)
                    {
                        arrayList.Clear();
                        arrayList = null;
                    }
                    if (stringBuilder != null)
                    {
                        stringBuilder.Remove(0, stringBuilder.Length);
                        stringBuilder = null;
                    }
                }
                catch
                {
                }
            }
            return result;
        }
        public int InsertDataSet(DataSet dataSet, UpdateFields fields)
        {
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            if (dataSet.Tables.Count == 0 || dataSet.Tables.Count == 0)
            {
                return 0;
            }
            int num = 0;
            int result;
            try
            {
                foreach (object obj in dataSet.Tables)
                {
                    DataTable dataTable = (DataTable)obj;
                    num += this.InsertDataTable(dataTable, dataTable.TableName, fields);
                }
                result = num;
            }
            catch
            {
                throw;
            }
            return result;
        }
        public int InsertDataTable(DataTable dataTable, string tableName, UpdateFields fields)
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
        public int InsertDataRow(DataRow insertRow, string tableName)
        {
            return this.InsertDataRow(insertRow, null, null);
        }
        public int InsertDataRow(DataRow insertRow, UpdateFields fields)
        {
            return this.InsertDataRow(insertRow, null, fields);
        }
        public int InsertDataRow(DataRow insertRow, string tableName, UpdateFields fields)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ArrayList arrayList = new ArrayList();
            int num = 0;
            if (!this.GenerateInsertDataRowSql(stringBuilder, arrayList, arrayList.Count, insertRow, tableName, fields))
            {
                return 0;
            }
            if (arrayList == null || arrayList.Count == 0)
            {
                num += this.ExecuteNonQuery(stringBuilder.ToString(), Array.Empty<object>());
            }
            else
            {
                num += this.ExecuteNonQuery(stringBuilder.ToString(), (MySqlParameter[])arrayList.ToArray(typeof(MySqlParameter)));
            }
            return num;
        }
        public int ExecuteNonQueryImpl(CommandType cmType,string strSql, MySqlParameter[] myPar)
        {
            try
            {
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand(strSql, myConnection);
                cmd.CommandType = cmType;
                if (myPar != null)
                {
                    foreach (MySqlParameter spar in myPar)
                    {
                        cmd.Parameters.Add(spar);
                    }
                }
                int result = cmd.ExecuteNonQuery();
                myConnection.Close();
                return result;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        public int ExecuteNonQuery(string nonQuerySql, params object[] parameterNameAndValues)
        {
            return this.ExecuteNonQuery(nonQuerySql, this.NameValueArrayToParamValueArray(parameterNameAndValues));
        }
        public int ExecuteNonQuery(string nonQuerySql, MySqlParameter[] commandParameters)
        {
            return this.ExecuteNonQueryImpl(CommandType.Text, nonQuerySql, commandParameters);
        }
        private bool GenerateInsertDataRowSql(StringBuilder sqlBuffer, ArrayList parameterList, int startParamIndex, DataRow insertRow, string tableName, UpdateFields fields)
        {
            return this.GenerateInsertDataRowSql(sqlBuffer, parameterList, startParamIndex, insertRow, tableName, fields, false);
        }
        private bool GenerateInsertDataRowSql(StringBuilder sqlBuffer, ArrayList parameterList, int startParamIndex, DataRow insertRow, string tableName, UpdateFields fields, bool appendSeparator)
        {
            if (sqlBuffer == null)
            {
                throw new ArgumentNullException("sqlBuffer");
            }
            if (parameterList == null)
            {
                throw new ArgumentNullException("parameterList");
            }
            if (startParamIndex < 0)
            {
                startParamIndex = 0;
            }
            if (insertRow == null)
            {
                throw new ArgumentNullException("insertRow");
            }
            if (insertRow.Table.Columns.Count == 0)
            {
                return false;
            }
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            int num = startParamIndex;
            bool result;
            try
            {
                DataTable table = insertRow.Table;
                string tableName2 = string.IsNullOrEmpty(tableName) ? table.TableName : tableName;
                stringBuilder.Append("INSERT INTO " + (string.IsNullOrEmpty(tableName) ? table.TableName : tableName));
                foreach (object obj in table.Columns)
                {
                    DataColumn dataColumn = (DataColumn)obj;
                    if (fields != null)
                    {
                        if (fields.ContainsField(dataColumn.ColumnName))
                        {
                            if (fields.Option == UpdateFieldsOptions.ExcludeFields)
                            {
                                continue;
                            }
                        }
                        else if (fields.Option == UpdateFieldsOptions.IncludeFields)
                        {
                            continue;
                        }
                    }
                    if (stringBuilder2.Length == 0)
                    {
                        stringBuilder2.Append("(");
                    }
                    else
                    {
                        stringBuilder2.Append(",");
                    }
                    stringBuilder2.Append(dataColumn.ColumnName);
                    if (stringBuilder3.Length == 0)
                    {
                        stringBuilder3.Append("(");
                    }
                    else
                    {
                        stringBuilder3.Append(",");
                    }
                    string sqlValueString = this.GetSqlValueString(insertRow[dataColumn.ColumnName]);
                    if (sqlValueString != null)
                    {
                        stringBuilder3.Append(sqlValueString);
                    }
                    else
                    {
                        num++;
                        stringBuilder3.Append("@p" + num.ToString());
                        parameterList.Add(this.CreateParameter("@p" + num.ToString(), insertRow[dataColumn.ColumnName]));
                    }
                }
                if (stringBuilder2.Length == 0)
                {
                    result = false;
                }
                else
                {
                    stringBuilder2.Append(") ");
                    stringBuilder3.Append(")");
                    stringBuilder.Append(" " + stringBuilder2.ToString() + " VALUES " + stringBuilder3.ToString());
                    sqlBuffer.Append(stringBuilder.ToString());
                    if (appendSeparator)
                    {
                        sqlBuffer.Append(";");
                    }
                    result = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                stringBuilder.Remove(0, stringBuilder.Length);
                stringBuilder2.Remove(0, stringBuilder2.Length);
                stringBuilder3.Remove(0, stringBuilder3.Length);
            }
            return result;
        }
        #endregion 批量插入

        private PropertyInfo FindProperty(PropertyInfo[] properties, string findName)
        {
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }
            if (findName == null)
            {
                throw new ArgumentNullException("findName");
            }
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (string.Compare(propertyInfo.Name, findName, true, CultureInfo.InvariantCulture) == 0)
                {
                    return propertyInfo;
                }
            }
            return null;
        }
        private int DeleteNonDataTable(object obj, Type objType, PropertyInfo[] properties, string tableName, params string[] primaryKeyProperties) {
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
                throw new ArgumentException("Value cannot be null or empty.", "tableName");
            }
            if (primaryKeyProperties == null || primaryKeyProperties.Length == 0)
            {
                throw new ArgumentException("The array length of parameter value cannot be 0.", "primaryKeyProperties");
            }
            if (properties.Length == 0)
            {
                return 0;
            }
            ArrayList arrayList = null;
            int num = 0;
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            int num2 = 0;
            stringBuilder.Append("DELETE FROM " + tableName + " WHERE ");
            foreach (string text in primaryKeyProperties)
            {
                if (string.IsNullOrEmpty(text))
                {
                    throw new ArgumentException("primaryKeyProperties cannot include null or empty.", "primaryKeyProperties");
                }
                if (stringBuilder2.Length > 0)
                {
                    stringBuilder2.Append(" AND ");
                }
                PropertyInfo propertyInfo = this.FindProperty(properties, text);
                if (propertyInfo == null)
                {
                    throw new ArgumentException("Property(" + text + ") is not exist in provied type.", "primaryKeyProperties");
                }
                string sqlValueString = this.GetSqlValueString(propertyInfo.GetValue(obj, null));
                if (sqlValueString != null)
                {
                    if (string.Compare(sqlValueString, "null", true) == 0)
                    {
                        stringBuilder2.Append(text + " IS " + sqlValueString);
                    }
                    else
                    {
                        stringBuilder2.Append(text + " = "+ sqlValueString);
                    }
                }
                else
                {
                    num++;
                    stringBuilder2.Append(text + " = " + "@p" + num.ToString());
                    if (arrayList == null)
                    {
                        arrayList = new ArrayList();
                    }
                    arrayList.Add(this.CreateParameter("@p" + num.ToString(),propertyInfo.GetValue(obj, null)));
                }
            }
            stringBuilder.Append(stringBuilder2);
            stringBuilder.Append(" ");
            if (stringBuilder.Length > 0)
            {
                if (arrayList == null || arrayList.Count == 0)
                {
                    num2 += this.ExecuteNonQuery(stringBuilder.ToString(), Array.Empty<object>());
                }
                else
                {
                    num2 += this.ExecuteNonQuery(stringBuilder.ToString(), (IDataParameter[])arrayList.ToArray(typeof(IDataParameter)));
                }
            }
            return num2;
        }
        public int Delete(object dataModelObj, string tableName, params string[] primaryKeyFields) {
            if (dataModelObj == null)
            {
                throw new ArgumentNullException("dataModelObj");
            }
            string[] array = primaryKeyFields;
            if (array == null || array.Length == 0) {
                return 0;
            }
            //if (dataModelObj is DataRow)
            //{
            //    return this.DeleteDataRow((DataRow)dataModelObj, tableName, primaryKeyFields);
            //}
            //if (dataModelObj is DataTable)
            //{
            //    return this.DeleteDataTable((DataTable)dataModelObj, tableName, primaryKeyFields);
            //}
            //if (dataModelObj is DataSet)
            //{
            //    return this.DeleteDataSet((DataSet)dataModelObj);
            //}
            //if (dataModelObj is DataView)
            //{
            //    return this.DeleteDataTable(((DataView)dataModelObj).Table, tableName, primaryKeyFields);
            //}
            //if (dataModelObj is DataRowView)
            //{
            //    return this.DeleteDataRow(((DataRowView)dataModelObj).Row, tableName, primaryKeyFields);
            //}
            if (dataModelObj is IList)
            {
                int num = 0;
                IList list = dataModelObj as IList;
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
            Type type2 = dataModelObj.GetType();
            PropertyInfo[] properties2 = dataModelObj.GetType().GetProperties();
            
            return this.DeleteNonDataTable(dataModelObj, type2, properties2, tableName, primaryKeyFields);
        }
    
        public virtual string GetSqlValueString(object value)
        {
            return this.GetSqlValueString(value, null);
        }
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
        protected  string EndProcessStringSqlValueString(string stringSqlValue)
        {
            return stringSqlValue;
        }
        protected  string GetDateTimeSqlString(DateTime date, string format)
        {
            return "str_to_date('" + date.ToString(string.IsNullOrEmpty(format) ? "yyyyMMddHHmmss" : format) + "','%Y%m%d%H%i%s')";
        }
    }
}
