using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class SyncDataStructure
    {
        public string mysqlCon { get; set; }
        public string sqlServerCon { get; set; }
        /// <summary>
        /// 同步数据表结构
        /// </summary>
        /// <param name="p_mysqlCon"></param>
        /// <param name="p_sqlServerCon"></param>
        public bool SyncDataStruct(string p_mysqlCon, string p_sqlServerCon)
        {
            mysqlCon = p_mysqlCon;
            sqlServerCon = p_sqlServerCon;
            try
            {
                //取得所有表
                DataTable dtTables = GetAllTable();
                if (dtTables != null && dtTables.Rows.Count > 0)
                {
                    string strSql = "";
                    string tableName = "";
                    List<string> syncSql = new List<string>();
                    //循环表
                    foreach (DataRow dr in dtTables.Rows)
                    {
                        tableName = dr["name"].ToString();
                        //先删除
                        strSql = "drop table if EXISTS " + tableName + ";";
                        syncSql.Add(strSql);
                        //再创建
                        strSql = GetCreateSql(dr["name"].ToString());
                        if (!string.IsNullOrEmpty(strSql)) { syncSql.Add(strSql); }
                    }
                    //建表语句写入文件
                    //WriteSqlTxt(syncSql);
                    ExecuteSql(syncSql,"1");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 同步脚本写入文件
        /// </summary>
        /// <param name="syncSql"></param>
        public void WriteSqlTxt(List<string> syncSql)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmsss") + ".txt";
            fileName = Path.Combine(@"D:\Learn\SqlFile", fileName);
            using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.Default))
            {
                foreach (string sql in syncSql)
                {
                    sw.WriteLine(sql);
                }
            }
            FileInfo fi = new FileInfo(fileName);
            if (fi.Length > 900000)
            {
                byte[] buff = new byte[1000];

                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    fs.Seek((int)fi.Length - 1000, SeekOrigin.Begin);
                    fs.Read(buff, 0, 1000);
                    fs.Close();
                    using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default))
                    {
                        sw.Write(Encoding.Default.GetString(buff, 0, buff.Length));
                    }
                }
            }
        }
        /// <summary>
        /// 取得所有表
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public DataTable GetAllTable(string dataSource,string dbName)
        {
            if (dataSource == "1") { return GetAllTable(); }
            return GetMySqlAllTable(dbName);
        }
        /// <summary>
        /// 取得Sql Server所有表名
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTable()
        {
            try
            {
                string sqlStr = "select name from sysobjects where xtype='U'and name !='sysdiagrams' order by name";
                SqlConnection Sqlcon = new SqlConnection(sqlServerCon);
                SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(sqlStr, Sqlcon);
                DataSet ds = new DataSet();  //创建数据集对象
                sqlDataAdapter1.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 取得MySql所有表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMySqlAllTable(string dbname)
        {
            try
            {
                string sqlStr = "select table_name as name from information_schema.tables where (table_schema='"+ dbname+"' and table_type='base table') order by table_name";
                MySqlConnection Sqlcon = new MySqlConnection(mysqlCon);
                MySqlDataAdapter sqlDataAdapter1 = new MySqlDataAdapter(sqlStr, Sqlcon);
                DataSet ds = new DataSet();  //创建数据集对象
                sqlDataAdapter1.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 根据表名取得所有列名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetAllColumns(string tableName)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT syscolumns.name,systypes.name as datatype,syscolumns.isnullable, syscolumns.length, syscolumns.xprec  ,syscolumns.xscale,syscolumns.colstat");
                str.AppendLine("FROM syscolumns, systypes");
                str.AppendLine("WHERE syscolumns.xusertype = systypes.xusertype  ");
                str.AppendLine("AND syscolumns.id = object_id('" + tableName + "')");
                SqlConnection Sqlcon = new SqlConnection(sqlServerCon);
                SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(str.ToString(), Sqlcon);
                DataSet ds = new DataSet();  //创建数据集对象
                sqlDataAdapter1.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 取得表主键
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetPrimaryKeys(string tableName)
        {
            try
            {
                string sqlStr = "SELECT  COLUMN_NAME  FROM  INFORMATION_SCHEMA.KEY_COLUMN_USAGE  WHERE  TABLE_NAME= '" + tableName + "'";
                SqlConnection Sqlcon = new SqlConnection(sqlServerCon);
                SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(sqlStr, Sqlcon);
                DataSet ds = new DataSet();  //创建数据集对象
                sqlDataAdapter1.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 拼接建表语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetCreateSql(string tableName)
        {
            StringBuilder str = new StringBuilder();
            DataTable dtCos = GetAllColumns(tableName);
            if (dtCos == null || dtCos.Rows.Count == 0)
            { return str.ToString(); }

            string tmp = "";
            int len = 0;
            str.Append("CREATE TABLE IF NOT EXISTS " + tableName + "(").AppendLine();
            foreach (DataRow dr in dtCos.Rows)
            {
                //列名
                str.Append(dr["name"].ToString());
                tmp = dr["datatype"].ToString();
                //数据类型
                if (dr["length"].ToString() == "-1")
                {
                    str.Append(" text");
                }
                else
                {
                    str.Append(" ").Append(tmp);                   
                    if (tmp.Equals("varchar"))
                    {
                        str.Append("(").Append(dr["length"].ToString()).Append(")");
                    }
                    else if (tmp.Equals("nvarchar"))
                    {
                        len = int.Parse(dr["length"].ToString());
                        len = len * 2;
                        str.Append("(").Append(len.ToString()).Append(")");
                    }
                    else if (tmp.Equals("decimal"))
                    {
                        len = int.Parse(dr["xprec"].ToString());
                        str.Append("(").Append(len.ToString());
                        len = int.Parse(dr["xscale"].ToString());
                        str.Append(",").Append(len.ToString()).Append(")");
                    }
                }
                //标志列
                if (dr["colstat"].ToString().Equals("1"))
                {
                    str.Append(" UNSIGNED AUTO_INCREMENT");
                }
                //不为空列
                if (dr["isnullable"].ToString().Equals("0"))
                {
                    str.Append(" NOT NULL");
                }
                str.Append(",").AppendLine();
            }
            //主键
            DataTable dtKeys = GetPrimaryKeys(tableName);
            if (dtKeys != null && dtKeys.Rows.Count > 0)
            {
                int idx = 1;
                str.Append("PRIMARY KEY (");
                foreach (DataRow pk in dtKeys.Rows)
                {
                    if (idx < dtKeys.Rows.Count)
                    {
                        str.Append(pk["COLUMN_NAME"].ToString()).Append(",");
                    }
                    else { str.Append(pk["COLUMN_NAME"].ToString()).Append(")").AppendLine(); }
                    idx++;
                }
            }
            else {
                string strResult = str.ToString();
                strResult = strResult.Substring(0, strResult.LastIndexOf(','));
                return strResult + ")ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            }
            str.AppendLine(")ENGINE=InnoDB DEFAULT CHARSET=utf8;");
            return str.ToString();
        }
        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="syncSql"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public int ExecuteSql(List<string> syncSql,string sourceType)
        {
            int result = 0;
            string exeSql = "";
            if (sourceType == "1")
            {
                MySqlConnection myConnection = new MySqlConnection(mysqlCon);
                try
                {

                    myConnection.Open();
                    foreach (string strSql in syncSql)
                    {
                        exeSql = strSql;
                        MySqlCommand cmd = new MySqlCommand(strSql, myConnection);

                        result += cmd.ExecuteNonQuery();
                    }
                    myConnection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("执行以下语句报错："+ exeSql);
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                    if (myConnection.State != ConnectionState.Closed) { myConnection.Close(); }
                    return -1;
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection(sqlServerCon))
                {
                    SqlTransaction transaction = null;
                    try
                    {
                        con.Open();
                        transaction = con.BeginTransaction("SampleTransaction");
                        SqlCommand cmd = con.CreateCommand();
                        cmd.Connection = con;
                        cmd.Transaction = transaction;
                        foreach (string strSql in syncSql)
                        {
                            cmd.CommandText = strSql;
                            result += cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        con.Close();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        Console.WriteLine("  Message: {0}", ex.Message);
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                            Console.WriteLine("  Message: {0}", ex2.Message);
                        }
                        //if (con.State != ConnectionState.Closed) { con.Close(); }
                        return -1;
                    }
                }
            }
        }

        /// <summary>
        /// 根据表名取得所有列名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetAllColumns(string dbname, string tableName)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("select column_name,is_nullable,data_type,column_type,column_key,extra");
                str.AppendLine("from  information_schema.columns");
                str.AppendLine("where table_schema='"+ dbname + "'  ");
                str.AppendLine("and table_name ='" + tableName + "'");
                MySqlConnection Sqlcon = new MySqlConnection(mysqlCon);
                MySqlDataAdapter sqlDataAdapter1 = new MySqlDataAdapter(str.ToString(), Sqlcon);
                DataSet ds = new DataSet();  //创建数据集对象
                sqlDataAdapter1.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 拼接Sql Server建表语句
        /// </summary>
        /// <param name="dbname"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetCreateSql(string dbname, string tableName)
        {
            StringBuilder str = new StringBuilder();
            DataTable dtCos = GetAllColumns(dbname,tableName);
            if (dtCos == null || dtCos.Rows.Count == 0)
            { return str.ToString(); }
            string tmp = "";
            str.AppendLine("CREATE TABLE ").Append(tableName).Append("(").AppendLine();
            foreach (DataRow dr in dtCos.Rows) {
                //列名
                str.Append(dr["column_name"].ToString());
                //字段类型
                tmp = dr["data_type"].ToString();
                if (tmp.Equals("int"))
                {
                    str.Append(" ").Append(tmp);
                    //自增长
                    if (dr["extra"] != null && dr["extra"].ToString().Equals("auto_increment"))
                    {
                        str.Append(" IDENTITY(1,1)");
                    }
                }
                else if (tmp.Equals("bit")) { str.Append(" ").Append(tmp); }
                else if (tmp.Equals("date"))
                {
                    //日期一律转化datetime
                    str.Append(" datetime");
                }
                else
                {
                    str.Append(" ").Append(dr["column_type"].ToString());
                } 
                //是否可为空                              
                if (dr["is_nullable"].ToString().Equals("NO")) { str.Append(" NOT NULL"); }
                str.Append(" ,").AppendLine();
            }
            //主键
            DataRow[] keys = dtCos.Select("column_key ='PRI'");
            if (keys != null && keys.Length > 0)
            {
                str.Append("CONSTRAINT[PK_" + tableName + "] PRIMARY KEY CLUSTERED").AppendLine();
                str.Append("(").AppendLine();
                for (int i = 0; i < keys.Length; i++)
                {
                    if (i < keys.Length - 1)
                    {
                        str.Append(keys[i]["column_name"].ToString()).Append(",").AppendLine();
                    }
                    else
                    {
                        str.Append(keys[i]["column_name"].ToString()).AppendLine();
                    }
                }
                str.Append(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 100) ON [PRIMARY]").AppendLine();
                str.Append(") ON[PRIMARY]").AppendLine();
            }
            else {
                //str.Remove(str.ToString().LastIndexOf(','), 1).Append(")"); 
                string strResult = str.ToString();
                strResult = strResult.Substring(0, strResult.LastIndexOf(','));
                strResult = strResult + ")";
                return strResult;
            }
            return str.ToString();
        }
        /// <summary>
        /// 创建同步表数据脚本
        /// </summary>
        /// <param name="dbname"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public List<string> SyncTableData(string dbname,string tablename) {
            List<string> insertList = new List<string>();
            if (string.IsNullOrEmpty(dbname))
            {
                //Sql Server To MySql
                //取不到列直接返回
                DataTable columns = this.GetAllColumns(tablename);
                if (columns == null || columns.Rows.Count == 0)
                {
                    return insertList;
                }
                //取不到数据直接返回
                DataTable data = GetTableData("1",tablename);
                if (data == null || data.Rows.Count == 0)
                {
                    return insertList;
                }
                Dictionary<string, string> dic = new Dictionary<string, string>();
                //共通插入语句
                string strCol = "insert into " + tablename + "(";
                int idx = 1;
                int colCnt = columns.Rows.Count;
                string strValue = "";
                foreach (DataRow dr in columns.Rows)
                {
                    //自增长的列不用插入
                    if (dr["colstat"].ToString().Equals("0"))
                    {
                        dic.Add(dr["name"].ToString(), dr["datatype"].ToString());
                        if (idx < colCnt)
                        {
                            strCol = strCol + dr["name"].ToString() + ",";
                        }
                        else { strCol = strCol + dr["name"].ToString(); }
                    }
                    idx++;
                }
                strCol = strCol + ") values(";
                StringBuilder str = new StringBuilder();

                //先删除表脚本
                str.Append("delete from ").Append(tablename).Append(";");
                insertList.Add(str.ToString());
                //循环插入行数据脚本
                foreach (DataRow row in data.Rows)
                {
                    str.Remove(0, str.Length);
                    str.Append(strCol);
                    idx = 1;
                    foreach (string key in dic.Keys)
                    {
                        strValue = GetMySqlFormatValue(key, dic[key], row);
                        if (idx > 1)
                        {

                            str.Append(",").Append(strValue);
                        }
                        else
                        {
                            str.Append(strValue);
                        }
                        idx++;
                    }
                    str.Append(");");
                    insertList.Add(str.ToString());
                }
                return insertList;
            }
            else {
                //MySql To Sql Server
                //取不到列直接返回空
                DataTable columns = this.GetAllColumns(dbname, tablename);
                if (columns == null || columns.Rows.Count == 0)
                {
                    return insertList;
                }
                //取不到数据直接返回空
                DataTable data = GetTableData("2", tablename);
                if (data == null || data.Rows.Count == 0)
                {
                    return insertList;
                }
                Dictionary<string, string> dic = new Dictionary<string, string>();
                //插入的共通语句
                string strCol = "insert into " + tablename + "(";
                int idx = 1;
                int colCnt = columns.Rows.Count;
                string strValue = "";
                foreach (DataRow dr in columns.Rows)
                {
                    //自增长的列不用插入
                    if (dr["extra"] != null && dr["extra"].ToString().Equals("auto_increment"))
                    {

                    }
                    else {
                        dic.Add(dr["column_name"].ToString(), dr["data_type"].ToString());
                        if (idx < colCnt)
                        {
                            strCol = strCol + dr["column_name"].ToString() + ",";
                        }
                        else { strCol = strCol + dr["column_name"].ToString(); }
                    }
                    idx++;
                }
                strCol = strCol + ") values(";
                //先删除表脚本
                StringBuilder str = new StringBuilder();
                str.Append("delete from ").Append(tablename).Append(";");
                insertList.Add(str.ToString());
                //循环插入行数据脚本
                foreach (DataRow row in data.Rows)
                {
                    str.Remove(0, str.Length);
                    str.Append(strCol);
                    idx = 1;
                    foreach (string key in dic.Keys)
                    {
                        strValue = GetSqlFormatValue(key, dic[key], row);
                        if (idx > 1)
                        {

                            str.Append(",").Append(strValue);
                        }
                        else
                        {
                            str.Append(strValue);
                        }
                        idx++;
                    }
                    str.Append(");");
                    insertList.Add(str.ToString());
                }
                return insertList;
            }
        }
        /// <summary>
        /// 格式化成MySql数据格式
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetMySqlFormatValue(string columnName,string dataType,DataRow dr) {
            //空时为null
            if (dr[columnName] == null || dr[columnName].ToString() == "") { return "null"; }
            //if (string.IsNullOrEmpty(dr[columnName].ToString())) { return "null"; }
            //日期格式
            if (dataType.Equals("date") || dataType.Equals("datetime")) {
                if (dr[columnName] == null || dr[columnName].ToString()=="") {
                    return "null";
                }
                DateTime dataValue = (DateTime)dr[columnName];
                return "str_to_date('" + dataValue.ToString("yyyyMMddHHmmss") + "','%Y%m%d%H%i%s')";
            } else if (dataType.Equals("int")
                || dataType.Equals("bigint")
                || dataType.Equals("decimal")
                || dataType.Equals("numeric")
                //|| dataType.Equals("bit")
                || dataType.Equals("float")) {
                //数值格式
                return dr[columnName].ToString();
            } else if (dataType.Equals("bit")){
                return dr[columnName].ToString() == "True" ? "1":"0";
            }
            //字符格式
            return "'"+dr[columnName].ToString()+"'";
        }
        /// <summary>
        /// 格式化成SqlSql数据格式
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetSqlFormatValue(string columnName, string dataType, DataRow dr)
        {
            //为空
            if (dr[columnName] == null) { return "null"; }
            //日期格式
            if (dataType.Equals("date") || dataType.Equals("datetime"))
            {
                DateTime dataValue = (DateTime)dr[columnName];
                return "'" + dataValue.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            else if (dataType.Equals("int")
              || dataType.Equals("bigint")
              || dataType.Equals("decimal")
              || dataType.Equals("numeric")
              || dataType.Equals("double"))
            {
                //数值格式
                return dr[columnName].ToString();
            }
            //字符格式
            return "'" + dr[columnName].ToString() + "'";
        }
        /// <summary>
        /// 取得表数据
        /// </summary>
        public DataTable GetTableData(string dataSource,string tableName)
        {
            string sqlStr = "SELECT  *  FROM  " + tableName;
            if (dataSource == "1")
            {
                //从Sql Server取得
                try
                {
                    
                    SqlConnection Sqlcon = new SqlConnection(sqlServerCon);
                    SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(sqlStr, Sqlcon);
                    DataSet ds = new DataSet();  //创建数据集对象
                    sqlDataAdapter1.Fill(ds);
                    return ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            else {
                //从MySql取得
                try
                {                   
                    MySqlConnection Sqlcon = new MySqlConnection(mysqlCon);
                    MySqlDataAdapter sqlDataAdapter1 = new MySqlDataAdapter(sqlStr, Sqlcon);
                    DataSet ds = new DataSet();  //创建数据集对象
                    sqlDataAdapter1.Fill(ds);
                    return ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
    }
}
