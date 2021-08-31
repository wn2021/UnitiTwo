namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbSyncDataType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSqlServer = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chkFile = new System.Windows.Forms.CheckBox();
            this.lblTableInfo = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.rbMySql = new System.Windows.Forms.RadioButton();
            this.rbSql = new System.Windows.Forms.RadioButton();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.chkTable = new System.Windows.Forms.CheckedListBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnToSqlServer = new System.Windows.Forms.Button();
            this.btnToMySql = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCancel2 = new System.Windows.Forms.Button();
            this.btnSave2 = new System.Windows.Forms.Button();
            this.btnUpdate2 = new System.Windows.Forms.Button();
            this.btnMySql = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSHA = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbSyncDataType
            // 
            this.cmbSyncDataType.FormattingEnabled = true;
            this.cmbSyncDataType.Location = new System.Drawing.Point(23, 63);
            this.cmbSyncDataType.Name = "cmbSyncDataType";
            this.cmbSyncDataType.Size = new System.Drawing.Size(140, 23);
            this.cmbSyncDataType.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据同步类型：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnSqlServer);
            this.groupBox1.Controls.Add(this.txtPwd);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtUid);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDatabase);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1183, 143);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sql Server数据库";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(1044, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(127, 40);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消更改";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(1044, 80);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 40);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存配置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(911, 30);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(127, 40);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "更改配置";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSqlServer
            // 
            this.btnSqlServer.Location = new System.Drawing.Point(911, 80);
            this.btnSqlServer.Name = "btnSqlServer";
            this.btnSqlServer.Size = new System.Drawing.Size(127, 40);
            this.btnSqlServer.TabIndex = 8;
            this.btnSqlServer.Text = "测试连接";
            this.btnSqlServer.UseVisualStyleBackColor = true;
            this.btnSqlServer.Click += new System.EventHandler(this.btnSqlServer_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(368, 99);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.ReadOnly = true;
            this.txtPwd.Size = new System.Drawing.Size(194, 25);
            this.txtPwd.TabIndex = 7;
            this.txtPwd.Text = "eyna1990";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(280, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "password：";
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(115, 99);
            this.txtUid.Name = "txtUid";
            this.txtUid.ReadOnly = true;
            this.txtUid.Size = new System.Drawing.Size(152, 25);
            this.txtUid.TabIndex = 5;
            this.txtUid.Text = "sa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "username：";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(115, 64);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.ReadOnly = true;
            this.txtDatabase.Size = new System.Drawing.Size(251, 25);
            this.txtDatabase.TabIndex = 3;
            this.txtDatabase.Text = "BelraySC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "dbname：";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(115, 29);
            this.txtServer.Name = "txtServer";
            this.txtServer.ReadOnly = true;
            this.txtServer.Size = new System.Drawing.Size(511, 25);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "DESKTOP-ISQR65D\\SQLEXPRESS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Server:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSHA);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.chkFile);
            this.groupBox2.Controls.Add(this.lblTableInfo);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.rbMySql);
            this.groupBox2.Controls.Add(this.rbSql);
            this.groupBox2.Controls.Add(this.chkAll);
            this.groupBox2.Controls.Add(this.chkTable);
            this.groupBox2.Controls.Add(this.lblMsg);
            this.groupBox2.Controls.Add(this.btnToSqlServer);
            this.groupBox2.Controls.Add(this.btnToMySql);
            this.groupBox2.Controls.Add(this.cmbSyncDataType);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 276);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1183, 465);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据同步";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1008, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkFile
            // 
            this.chkFile.AutoSize = true;
            this.chkFile.Location = new System.Drawing.Point(994, 140);
            this.chkFile.Name = "chkFile";
            this.chkFile.Size = new System.Drawing.Size(134, 19);
            this.chkFile.TabIndex = 11;
            this.chkFile.Text = "仅生成脚本文件";
            this.chkFile.UseVisualStyleBackColor = true;
            // 
            // lblTableInfo
            // 
            this.lblTableInfo.AutoSize = true;
            this.lblTableInfo.Location = new System.Drawing.Point(182, 17);
            this.lblTableInfo.Name = "lblTableInfo";
            this.lblTableInfo.Size = new System.Drawing.Size(236, 15);
            this.lblTableInfo.TabIndex = 10;
            this.lblTableInfo.Text = "选择表(共计{0}张表，已选{1})：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 15);
            this.label11.TabIndex = 9;
            this.label11.Text = "数据源：";
            // 
            // rbMySql
            // 
            this.rbMySql.AutoSize = true;
            this.rbMySql.Location = new System.Drawing.Point(25, 153);
            this.rbMySql.Name = "rbMySql";
            this.rbMySql.Size = new System.Drawing.Size(68, 19);
            this.rbMySql.TabIndex = 8;
            this.rbMySql.Text = "MySql";
            this.rbMySql.UseVisualStyleBackColor = true;
            this.rbMySql.CheckedChanged += new System.EventHandler(this.rbMySql_CheckedChanged);
            // 
            // rbSql
            // 
            this.rbSql.AutoSize = true;
            this.rbSql.Checked = true;
            this.rbSql.Location = new System.Drawing.Point(25, 127);
            this.rbSql.Name = "rbSql";
            this.rbSql.Size = new System.Drawing.Size(108, 19);
            this.rbSql.TabIndex = 7;
            this.rbSql.TabStop = true;
            this.rbSql.Text = "Sql Server";
            this.rbSql.UseVisualStyleBackColor = true;
            this.rbSql.CheckedChanged += new System.EventHandler(this.rbSql_CheckedChanged);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(919, 17);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(59, 19);
            this.chkAll.TabIndex = 6;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // chkTable
            // 
            this.chkTable.FormattingEnabled = true;
            this.chkTable.Location = new System.Drawing.Point(185, 39);
            this.chkTable.Name = "chkTable";
            this.chkTable.Size = new System.Drawing.Size(793, 384);
            this.chkTable.TabIndex = 5;
            this.chkTable.SelectedIndexChanged += new System.EventHandler(this.chkTable_SelectedIndexChanged);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(26, 425);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(86, 19);
            this.lblMsg.TabIndex = 4;
            this.lblMsg.Text = "label11";
            this.lblMsg.Visible = false;
            // 
            // btnToSqlServer
            // 
            this.btnToSqlServer.Location = new System.Drawing.Point(994, 258);
            this.btnToSqlServer.Name = "btnToSqlServer";
            this.btnToSqlServer.Size = new System.Drawing.Size(183, 37);
            this.btnToSqlServer.TabIndex = 3;
            this.btnToSqlServer.Text = "同步到Sql Server";
            this.btnToSqlServer.UseVisualStyleBackColor = true;
            this.btnToSqlServer.Click += new System.EventHandler(this.btnToSqlServer_Click);
            // 
            // btnToMySql
            // 
            this.btnToMySql.Location = new System.Drawing.Point(994, 188);
            this.btnToMySql.Name = "btnToMySql";
            this.btnToMySql.Size = new System.Drawing.Size(183, 37);
            this.btnToMySql.TabIndex = 2;
            this.btnToMySql.Text = "同步到MySql";
            this.btnToMySql.UseVisualStyleBackColor = true;
            this.btnToMySql.Click += new System.EventHandler(this.btnToMySql_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCancel2);
            this.groupBox3.Controls.Add(this.btnSave2);
            this.groupBox3.Controls.Add(this.btnUpdate2);
            this.groupBox3.Controls.Add(this.btnMySql);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtUser);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtPort);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtDbName);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtHost);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1183, 133);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "My Sql数据库";
            // 
            // btnCancel2
            // 
            this.btnCancel2.Enabled = false;
            this.btnCancel2.Location = new System.Drawing.Point(1044, 31);
            this.btnCancel2.Name = "btnCancel2";
            this.btnCancel2.Size = new System.Drawing.Size(127, 40);
            this.btnCancel2.TabIndex = 13;
            this.btnCancel2.Text = "取消更改";
            this.btnCancel2.UseVisualStyleBackColor = true;
            this.btnCancel2.Click += new System.EventHandler(this.btnCancel2_Click);
            // 
            // btnSave2
            // 
            this.btnSave2.Enabled = false;
            this.btnSave2.Location = new System.Drawing.Point(1044, 81);
            this.btnSave2.Name = "btnSave2";
            this.btnSave2.Size = new System.Drawing.Size(127, 40);
            this.btnSave2.TabIndex = 12;
            this.btnSave2.Text = "保存配置";
            this.btnSave2.UseVisualStyleBackColor = true;
            this.btnSave2.Click += new System.EventHandler(this.btnSave2_Click);
            // 
            // btnUpdate2
            // 
            this.btnUpdate2.Location = new System.Drawing.Point(911, 31);
            this.btnUpdate2.Name = "btnUpdate2";
            this.btnUpdate2.Size = new System.Drawing.Size(127, 40);
            this.btnUpdate2.TabIndex = 11;
            this.btnUpdate2.Text = "更改配置";
            this.btnUpdate2.UseVisualStyleBackColor = true;
            this.btnUpdate2.Click += new System.EventHandler(this.btnUpdate2_Click);
            // 
            // btnMySql
            // 
            this.btnMySql.Location = new System.Drawing.Point(911, 81);
            this.btnMySql.Name = "btnMySql";
            this.btnMySql.Size = new System.Drawing.Size(127, 40);
            this.btnMySql.TabIndex = 10;
            this.btnMySql.Text = "测试连接";
            this.btnMySql.UseVisualStyleBackColor = true;
            this.btnMySql.Click += new System.EventHandler(this.btnMySql_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(368, 96);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.ReadOnly = true;
            this.txtPassword.Size = new System.Drawing.Size(194, 25);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.Text = "eyna1990";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(280, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 15);
            this.label10.TabIndex = 8;
            this.label10.Text = "password：";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(115, 96);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(152, 25);
            this.txtUser.TabIndex = 7;
            this.txtUser.Text = "root";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 15);
            this.label9.TabIndex = 6;
            this.label9.Text = "username：";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(695, 26);
            this.txtPort.Name = "txtPort";
            this.txtPort.ReadOnly = true;
            this.txtPort.Size = new System.Drawing.Size(100, 25);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "3306";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(644, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "port：";
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(115, 61);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.ReadOnly = true;
            this.txtDbName.Size = new System.Drawing.Size(251, 25);
            this.txtDbName.TabIndex = 3;
            this.txtDbName.Text = "BelraySC";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "dbname：";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(115, 26);
            this.txtHost.Name = "txtHost";
            this.txtHost.ReadOnly = true;
            this.txtHost.Size = new System.Drawing.Size(511, 25);
            this.txtHost.TabIndex = 1;
            this.txtHost.Text = "localhost";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "host：";
            // 
            // btnSHA
            // 
            this.btnSHA.Location = new System.Drawing.Point(1008, 390);
            this.btnSHA.Name = "btnSHA";
            this.btnSHA.Size = new System.Drawing.Size(75, 23);
            this.btnSHA.TabIndex = 13;
            this.btnSHA.Text = "SHA加密";
            this.btnSHA.UseVisualStyleBackColor = true;
            this.btnSHA.Click += new System.EventHandler(this.btnSHA_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 741);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库数据同步";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }        

        #endregion

        private System.Windows.Forms.ComboBox cmbSyncDataType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnToSqlServer;
        private System.Windows.Forms.Button btnToMySql;
        private System.Windows.Forms.Button btnSqlServer;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMySql;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckedListBox chkTable;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rbMySql;
        private System.Windows.Forms.RadioButton rbSql;
        private System.Windows.Forms.Label lblTableInfo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCancel2;
        private System.Windows.Forms.Button btnSave2;
        private System.Windows.Forms.Button btnUpdate2;
        private System.Windows.Forms.CheckBox chkFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSHA;
    }
}

