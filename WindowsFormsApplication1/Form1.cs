using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Services;
using WebService;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string mysqlCon = "server={0};port={1};user={2};password={3}; database={4};";
        public static string sqlServerCon = "server={0};uid={1};pwd={2};database={3}";
        private static string info = "选择表（共计{0}张表，已勾选{1}张）：";
        private SqlServerSetting sqlSetting = null;
        private MySqlServerSetting mysqlSetting = null;
        private SyncDataStructure sync;
        /// <summary>
        /// 画面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, System.EventArgs e)
        {
            sync = new SyncDataStructure();
            initForm();
        }      
        
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.chkTable.Items.Count; i++)
            {
                this.chkTable.SetItemChecked(i,this.chkAll.Checked);
            }
            this.lblTableInfo.Text = string.Format(info, this.chkTable.Items.Count, this.chkTable.CheckedItems.Count);
        }
        #region 数据源选择变化
        private void rbSql_CheckedChanged(object sender, EventArgs e)
        {
            SetBtnState(false);
        }

        private void rbMySql_CheckedChanged(object sender, EventArgs e)
        {
            SetBtnState(false);
        }
        #endregion 数据源选择变化

        
        /// <summary>
        /// 设置按钮状态
        /// </summary>
        private void SetBtnState(bool refreshDB) {
            //设置按钮状态
            if (this.rbSql.Checked)
            {
                btnToMySql.Enabled = true;
                btnToSqlServer.Enabled = false;
                rbMySql.Checked = false;
            }
            else
            {
                btnToMySql.Enabled = false;
                btnToSqlServer.Enabled = true;
                this.rbSql.Checked = false;
            }
            GetTables(refreshDB);
        }
        /// <summary>
        /// 取得table列表
        /// </summary>
        private void GetTables(bool refreshDB) {
            //绑定表列表
            if (refreshDB)
            {
                sync = new SyncDataStructure();
                string ConnectionString = string.Format(sqlServerCon, this.txtServer.Text, this.txtUid.Text, txtPwd.Text, txtDatabase.Text);
                string ConnectionString2 = string.Format(mysqlCon, this.txtHost.Text, txtPort.Text, txtUser.Text, txtPassword.Text, txtDbName.Text);
                sync.sqlServerCon = ConnectionString;
                sync.mysqlCon = ConnectionString2;
            }
            string sourceType = this.rbSql.Checked ? "1" : "2";
            string dbName = this.rbSql.Checked ? this.txtDatabase.Text : txtDbName.Text;
            DataTable dt2 = sync.GetAllTable(sourceType, dbName);
            if (dt2 == null) {
                dt2 = new DataTable();
                dt2.Columns.Add("name", typeof(string));
            }           
            this.chkTable.DataSource = dt2;
            this.chkTable.DisplayMember = "name";
            this.chkTable.ValueMember = "name";

            this.lblTableInfo.Text = string.Format(info, dt2.Rows.Count, 0);
        }
        /// <summary>
        /// 更新选中表个数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblTableInfo.Text = string.Format(info, this.chkTable.Items.Count, this.chkTable.CheckedItems.Count);
        }
        #region Sql Server按钮
        /// <summary>
        /// 更改设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //放开设置，冻结数据同步
            SetSyncStatus(true, "1");
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!TestSqlConnect()) {
                MessageBox.Show("当前设置导致连接失败，请检查并测试连接成功后再保存！");
                return;
            }
            //保存设置
            SetSqlSetting();
            //关闭设置，解冻数据同步
            SetSyncStatus(false, "1");
            if (rbSql.Checked) { SetBtnState(true);  }
        }
        /// <summary>
        /// 取消更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //回滚设置
            this.txtServer.Text = sqlSetting.server;
            this.txtDatabase.Text = sqlSetting.database;
            this.txtUid.Text = sqlSetting.uid;
            this.txtPwd.Text = sqlSetting.pwd;
            //关闭设置，解冻数据同步
            SetSyncStatus(false, "1");
        }
        /// <summary>
        /// 测试连接Sql Server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSqlServer_Click(object sender, EventArgs e)
        {
            if (!TestSqlConnect())
            {
                MessageBox.Show("连接失败！");
                return;
            }
            MessageBox.Show("连接成功。");
        }
        /// <summary>
        /// 同步到MySql
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToMySql_Click(object sender, EventArgs e)
        {
            if (this.chkTable.CheckedItems.Count == 0)
            {
                MessageBox.Show("请至少选择一张表进行数据同步！");
                return;
            }
            this.lblMsg.Visible = true;
            bool result = SyncData(); ;            
            if (result)
            {
                MessageBox.Show("同步成功。");
            }
            else
            {
                MessageBox.Show("同步失败。");
            }
            this.lblMsg.Visible = false;
            this.lblMsg.Text = "";
        }
        #endregion Sql Server按钮

        #region My Sql按钮
        /// <summary>
        /// 更改设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate2_Click(object sender, EventArgs e)
        {
            //放开设置，冻结数据同步
            SetSyncStatus(true, "2");
        }
        /// <summary>
        /// 取消设置更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel2_Click(object sender, EventArgs e)
        {
            //回滚设置
            this.txtHost.Text = mysqlSetting.server;
            this.txtPort.Text = mysqlSetting.port;
            this.txtDbName.Text = mysqlSetting.database;
            this.txtUser.Text = mysqlSetting.user;
            this.txtPassword.Text = mysqlSetting.password;
            //关闭设置，解冻数据同步
            SetSyncStatus(false, "2");
            
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave2_Click(object sender, EventArgs e)
        {
            if (!TestMysqlConnect())
            {
                MessageBox.Show("当前设置导致连接失败，请检查并测试连接成功后再保存！");
                return;
            }
            //保存设置
            SetMySqlSetting();
            //关闭设置，解冻数据同步
            SetSyncStatus(false, "2");
            if (rbMySql.Checked)
            {
                SetBtnState(true);
            }
        }
        /// <summary>
        /// 测试连接My Sql
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMySql_Click(object sender, EventArgs e)
        {
            if (!TestMysqlConnect()) { MessageBox.Show("连接失败。");
                return;
            }
            MessageBox.Show("连接成功。");
        }
        /// <summary>
        /// 同步到Sql Server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToSqlServer_Click(object sender, EventArgs e)
        {
            if (this.chkTable.CheckedItems.Count == 0)
            {
                MessageBox.Show("请至少选择一张表进行数据同步！");
                return;
            }
            this.lblMsg.Visible = true;
            bool result = SyncData();            
            if (result)
            {
                MessageBox.Show("同步成功。");
            }
            else
            {
                MessageBox.Show("同步失败。");
            }
            this.lblMsg.Visible = false;
            this.lblMsg.Text = "";
        }
        #endregion My Sql按钮
        /// <summary>
        /// 画面初始化
        /// </summary>
        private void initForm()
        {
            //数据同步类型
            DataTable dt = new DataTable();
            dt.Columns.Add("value", typeof(string));
            dt.Columns.Add("name", typeof(string));
            DataRow dr = dt.NewRow();
            dr["value"] = "01";
            dr["name"] = "表结构";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["value"] = "02";
            dr["name"] = "表数据";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["value"] = "03";
            dr["name"] = "表结构和数据";
            dt.Rows.Add(dr);
            cmbSyncDataType.DataSource = dt;
            cmbSyncDataType.DisplayMember = "name";
            cmbSyncDataType.ValueMember = "value";
            //设置按钮状态
            SetBtnState(true);
            //缓存设置
            SetSqlSetting();
            SetMySqlSetting();
        }
        /// <summary>
        /// 缓存Sql Server设置
        /// </summary>
        private void SetSqlSetting() {
            sqlSetting = new SqlServerSetting();
            sqlSetting.server = this.txtServer.Text;
            sqlSetting.database = this.txtDatabase.Text;
            sqlSetting.uid = this.txtUid.Text;
            sqlSetting.pwd = this.txtPwd.Text;
            sync.sqlServerCon = string.Format(sqlServerCon, this.txtServer.Text, this.txtUid.Text, txtPwd.Text, txtDatabase.Text);
        }
        /// <summary>
        /// 缓存My Sql设置
        /// </summary>
        private void SetMySqlSetting()
        {
            mysqlSetting = new MySqlServerSetting();
            mysqlSetting.server = this.txtHost.Text;
            mysqlSetting.port = this.txtPort.Text;
            mysqlSetting.database = this.txtDbName.Text;
            mysqlSetting.user = this.txtUser.Text;
            mysqlSetting.password = this.txtPassword.Text;
            if (sync == null) {
                sync = new SyncDataStructure();
            }          
            sync.mysqlCon = string.Format(mysqlCon, this.txtHost.Text, txtPort.Text, txtUser.Text, txtPassword.Text, txtDbName.Text);
        }
        /// <summary>
        /// 设置画面可编辑性
        /// </summary>
        /// <param name="isFrozen"></param>
        private void SetSyncStatus(bool isFrozen,string frozenType) {
            //数据同步模块是否冻结
            foreach (Control control in groupBox2.Controls)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.Button"
                    || control.GetType().ToString() == "System.Windows.Forms.RadioButton"
                    || control.GetType().ToString() == "System.Windows.Forms.CheckBox"
                    || control.GetType().ToString() == "System.Windows.Forms.CheckedListBox")
                {
                    control.Enabled = !isFrozen;
                }
            }
            if (frozenType == "1") {
                //sql server设置区域
                foreach (Control control in groupBox1.Controls)
                {
                    if (control.GetType().ToString() == "System.Windows.Forms.TextBox")
                    {
                        ((System.Windows.Forms.TextBox)control).ReadOnly = !isFrozen;
                    }
                }
                this.btnSave.Enabled = isFrozen;
                this.btnCancel.Enabled = isFrozen;
            }else if (frozenType == "2")
            {
                //sql server设置区域
                foreach (Control control in groupBox3.Controls)
                {
                    if (control.GetType().ToString() == "System.Windows.Forms.TextBox")
                    {
                        ((System.Windows.Forms.TextBox)control).ReadOnly = !isFrozen;
                    }
                }
                this.btnSave2.Enabled = isFrozen;
                this.btnCancel2.Enabled = isFrozen;
            }
        }
        /// <summary>
        /// 测试Sql Server连接
        /// </summary>
        /// <returns></returns>
        private bool TestSqlConnect() {
            if (string.IsNullOrEmpty(this.txtServer.Text))
            {
                MessageBox.Show("SqlServer数据库连接地址不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtDatabase.Text))
            {
                MessageBox.Show("SqlServer数据库名称不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtUid.Text))
            {
                MessageBox.Show("用户名不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtPwd.Text))
            {
                MessageBox.Show("密码不能为空！");
                return false;
            }
            SqlConnection Sqlcon = new SqlConnection(sqlServerCon);
            try
            {
                string ConnectionString = string.Format(sqlServerCon, this.txtServer.Text, this.txtUid.Text, txtPwd.Text, txtDatabase.Text);
                Sqlcon = new SqlConnection(ConnectionString);
                Sqlcon.Open();
                return true;
            }
            catch
            {
                return false;

            }
            finally { Sqlcon.Close(); }
        }
        /// <summary>
        /// 测试My Sql连接
        /// </summary>
        /// <returns></returns>
        private bool TestMysqlConnect() {
            if (string.IsNullOrEmpty(this.txtHost.Text))
            {
                MessageBox.Show("Host不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtPort.Text))
            {
                MessageBox.Show("Port不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtUser.Text))
            {
                MessageBox.Show("username不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtPassword.Text))
            {
                MessageBox.Show("password不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtDbName.Text))
            {
                MessageBox.Show("dbname不能为空！");
                return false;
            }
            string ConnectionString = string.Format(mysqlCon, this.txtHost.Text, txtPort.Text, txtUser.Text, txtPassword.Text, txtDbName.Text);
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                Console.WriteLine("已经建立连接");
                //MessageBox.Show("连接成功。");
                return true;
                //在这里使用代码对数据库进行增删查改
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show("连接失败！");
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 同步数据表结构
        /// </summary>
        /// <param name="p_mysqlCon"></param>
        /// <param name="p_sqlServerCon"></param>
        public bool SyncData()
        {
            //SyncDataStructure sync = new SyncDataStructure();
            //sync.sqlServerCon = string.Format(sqlServerCon, this.txtServer.Text, this.txtUid.Text, txtPwd.Text, txtDatabase.Text); 
            //sync.mysqlCon = string.Format(mysqlCon, this.txtHost.Text, txtPort.Text, txtUser.Text, txtPassword.Text, txtDbName.Text); 
            try
            {
                this.lblMsg.Text = "开始同步数据...";
                Application.DoEvents();
                string strSql = "";
                string tableName = "";
                List<string> syncSql = new List<string>();
                //循环表
                for (int i = 0; i < this.chkTable.CheckedItems.Count; i++)
                {
                    tableName = ((DataRowView)this.chkTable.CheckedItems[i]).Row["name"].ToString();
                    this.lblMsg.Text = "正在生成表【" + tableName + "】的脚本...";
                    Application.DoEvents();
                    if (rbSql.Checked)
                    {
                        //从Sql Server 到 MySql
                        if (cmbSyncDataType.SelectedValue.ToString().Equals("01")) {
                            //同步表结构
                            //先删除
                            strSql = "drop table if EXISTS " + tableName + ";";
                            syncSql.Add(strSql);
                            //再创建
                            strSql = sync.GetCreateSql(tableName);
                            if (!string.IsNullOrEmpty(strSql)) { syncSql.Add(strSql); }
                        }
                        else if (cmbSyncDataType.SelectedValue.ToString().Equals("02")) {
                            //同步表数据
                            List<string> singleList  = sync.SyncTableData("", tableName);
                            syncSql.AddRange(singleList);
                        }
                        else {
                            //同步表结构
                            //先删除
                            strSql = "drop table if EXISTS " + tableName + ";";
                            syncSql.Add(strSql);
                            //再创建
                            strSql = sync.GetCreateSql(tableName);
                            if (string.IsNullOrEmpty(strSql)) { continue; }
                            syncSql.Add(strSql);

                            //同步表数据
                            List<string> singleList = sync.SyncTableData("", tableName);
                            syncSql.AddRange(singleList);
                        }                        
                    }
                    else
                    {
                        //从My Sql 到 Sql Server
                        if (cmbSyncDataType.SelectedValue.ToString().Equals("01"))
                        {
                            //同步表结构
                            strSql = "if exists (select * from sys.objects where object_id = object_id(N'" + tableName + "') and type in (N'u'))";
                            strSql = strSql + "drop table " + tableName;
                            syncSql.Add(strSql);
                            //再创建
                            strSql = sync.GetCreateSql(this.txtDbName.Text, tableName);
                            if (!string.IsNullOrEmpty(strSql)) { syncSql.Add(strSql); }
                        }
                        else if (cmbSyncDataType.SelectedValue.ToString().Equals("02")) {
                            //同步表数据
                            List<string> singleList = sync.SyncTableData(this.txtDbName.Text, tableName);
                            syncSql.AddRange(singleList);
                        }
                        else if (cmbSyncDataType.SelectedValue.ToString().Equals("03")) {
                            //同步表结构
                            strSql = "if exists (select * from sys.objects where object_id = object_id(N'" + tableName + "') and type in (N'u'))";
                            strSql = strSql + "drop table " + tableName;
                            syncSql.Add(strSql);
                            //再创建
                            strSql = sync.GetCreateSql(this.txtDbName.Text, tableName);
                            if (string.IsNullOrEmpty(strSql)) { continue;  }
                            syncSql.Add(strSql);

                            //同步表数据
                            List<string> singleList = sync.SyncTableData(this.txtDbName.Text, tableName);
                            syncSql.AddRange(singleList);
                        }
                    }
                }

                this.lblMsg.Text = "开始同步数据到目标数据库...";
                Application.DoEvents();
                //建表语句写入文件
                if (this.chkFile.Checked)
                {
                    sync.WriteSqlTxt(syncSql);
                    this.lblMsg.Text = "同步数据到目标数据库结束。";
                    Application.DoEvents();
                    return true;
                }

                string sourceType = this.rbSql.Checked ? "1" : "2";
                int intResult = sync.ExecuteSql(syncSql, sourceType);
                this.lblMsg.Text = "同步数据到目标数据库结束。";
                Application.DoEvents();
                if (intResult >= 0) { return true; }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebService.WebService1 Client = new WebService.WebService1();
            string strResult = Client.Show("eyna");
            Console.WriteLine(strResult);
        }

        private void btnSHA_Click(object sender, EventArgs e)
        {
            string strResult  = SHAEncry.PasswordEncryption("1admin");
            Console.WriteLine(strResult);
            strResult = SHAEncry.PasswordEncryption("admin1");
            Console.WriteLine(strResult);
            strResult = SHAEncry.PasswordEncryption("1");
            Console.WriteLine(strResult);
        }
    }
}
