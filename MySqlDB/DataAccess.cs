using BFC.SDK;
using BFC.SDK.Diagnostics;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDB
{
    public class DataAccess
    {
        private Type GetRealDataType(Type t)
        {
            return DevReflection.GetNullableOriginalType(t);
        }     
        protected int DataTableToList(IList resultList, Type t, DataTable reader)
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
            //取得所有列
            DataColumnCollection cols = reader.Columns;
            //设置属性数组
            PropertyInfo[] array2 = new PropertyInfo[cols.Count];
            //取得LIST属性数组
            PropertyInfo[] properties = t.GetProperties();
            //标志是否需要转换数据类型
            bool[] array3 = new bool[cols.Count];
            //记录列类型
            Type[] array4 = new Type[cols.Count];
           
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
           
            foreach (PropertyInfo propertyInfo in properties)
            {
                int i = 0;
                while (i < cols.Count)
                {
                    if (string.Compare(propertyInfo.Name, cols[i].ColumnName, true, CultureInfo.InvariantCulture) == 0)
                    {
                        array2[i] = propertyInfo;
                        array4[i] = this.GetRealDataType(propertyInfo.PropertyType);
                        if (!array4[i].Equals(cols[i].DataType))
                        {
                            array3[i] = true;
                            break;
                        }
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            int num = -1;
            DevDebug.WriteTraceInfo(this, "DataReaderToList() DataReader 数据扫描开始...");
            while (num< reader.Rows.Count)
            {
                num++;
                DataRow dr = reader.Rows[num];
                object obj = Activator.CreateInstance(t);
                for (int m = 0; m < array2.Length; m++)
                {
                    if (array2[m] != null && array2[m].CanWrite)
                    {
                        try
                        {
                            if (dr[m]==null)
                            {
                                array2[m].SetValue(obj, null, null);
                            }
                            else if (array3[m])
                            {
                                array2[m].SetValue(obj, Convert.ChangeType(dr[m], array4[m]), null);
                            }
                            else
                            {
                                array2[m].SetValue(obj, reader.Columns[m], null);
                            }
                        }
                        catch (Exception innerException)
                        {
                            throw new InvalidCastException(string.Format("属性设置不对{0}", new object[]
                            {
                                array2[m].Name
                            }), innerException);
                        }
                    }
                }
                resultList.Add(obj);
                break;
            }
            DevDebug.WriteTraceInfo(this, "DataReaderToList() DataReader 数据扫描结束。");
            return num + 1;
        }
    }
}
