namespace CopyProduct {
    partial class Form1 {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aPI设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGetDetail = new System.Windows.Forms.Button();
            this.txtSourceProductID = new System.Windows.Forms.TextBox();
            this.lblSourceAccountProductID = new System.Windows.Forms.Label();
            this.txtSourceAccountPwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSourceAccountUser = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.dgDestAccounts = new System.Windows.Forms.DataGridView();
            this.ppgProduct = new System.Windows.Forms.PropertyGrid();
            this.btnPublish = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDestAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(922, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aPI设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // aPI设置ToolStripMenuItem
            // 
            this.aPI设置ToolStripMenuItem.Name = "aPI设置ToolStripMenuItem";
            this.aPI设置ToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.aPI设置ToolStripMenuItem.Text = "API 设置";
            this.aPI设置ToolStripMenuItem.Click += new System.EventHandler(this.aPI设置ToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGetDetail);
            this.groupBox1.Controls.Add(this.txtSourceProductID);
            this.groupBox1.Controls.Add(this.lblSourceAccountProductID);
            this.groupBox1.Controls.Add(this.txtSourceAccountPwd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSourceAccountUser);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 244);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 129);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "源账户";
            // 
            // btnGetDetail
            // 
            this.btnGetDetail.Location = new System.Drawing.Point(9, 97);
            this.btnGetDetail.Name = "btnGetDetail";
            this.btnGetDetail.Size = new System.Drawing.Size(288, 23);
            this.btnGetDetail.TabIndex = 6;
            this.btnGetDetail.Text = "获取产品信息";
            this.btnGetDetail.UseVisualStyleBackColor = true;
            this.btnGetDetail.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtSourceProductID
            // 
            this.txtSourceProductID.Location = new System.Drawing.Point(53, 70);
            this.txtSourceProductID.Name = "txtSourceProductID";
            this.txtSourceProductID.Size = new System.Drawing.Size(244, 21);
            this.txtSourceProductID.TabIndex = 5;
            // 
            // lblSourceAccountProductID
            // 
            this.lblSourceAccountProductID.AutoSize = true;
            this.lblSourceAccountProductID.Location = new System.Drawing.Point(7, 75);
            this.lblSourceAccountProductID.Name = "lblSourceAccountProductID";
            this.lblSourceAccountProductID.Size = new System.Drawing.Size(41, 12);
            this.lblSourceAccountProductID.TabIndex = 4;
            this.lblSourceAccountProductID.Text = "产品ID";
            // 
            // txtSourceAccountPwd
            // 
            this.txtSourceAccountPwd.Location = new System.Drawing.Point(53, 42);
            this.txtSourceAccountPwd.Name = "txtSourceAccountPwd";
            this.txtSourceAccountPwd.PasswordChar = '*';
            this.txtSourceAccountPwd.Size = new System.Drawing.Size(244, 21);
            this.txtSourceAccountPwd.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码";
            // 
            // txtSourceAccountUser
            // 
            this.txtSourceAccountUser.Location = new System.Drawing.Point(53, 14);
            this.txtSourceAccountUser.Name = "txtSourceAccountUser";
            this.txtSourceAccountUser.Size = new System.Drawing.Size(244, 21);
            this.txtSourceAccountUser.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddAccount);
            this.groupBox2.Controls.Add(this.dgDestAccounts);
            this.groupBox2.Location = new System.Drawing.Point(12, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 210);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "目标账户";
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.Location = new System.Drawing.Point(9, 179);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(294, 23);
            this.btnAddAccount.TabIndex = 1;
            this.btnAddAccount.Text = "添加目标账户";
            this.btnAddAccount.UseVisualStyleBackColor = true;
            this.btnAddAccount.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgDestAccounts
            // 
            this.dgDestAccounts.AllowUserToAddRows = false;
            this.dgDestAccounts.AllowUserToDeleteRows = false;
            this.dgDestAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDestAccounts.Location = new System.Drawing.Point(7, 21);
            this.dgDestAccounts.Name = "dgDestAccounts";
            this.dgDestAccounts.RowTemplate.Height = 23;
            this.dgDestAccounts.Size = new System.Drawing.Size(296, 151);
            this.dgDestAccounts.TabIndex = 0;
            this.dgDestAccounts.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDestAccounts_CellContentDoubleClick);
            // 
            // ppgProduct
            // 
            this.ppgProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ppgProduct.Location = new System.Drawing.Point(327, 29);
            this.ppgProduct.Name = "ppgProduct";
            this.ppgProduct.Size = new System.Drawing.Size(583, 567);
            this.ppgProduct.TabIndex = 4;
            // 
            // btnPublish
            // 
            this.btnPublish.Location = new System.Drawing.Point(19, 379);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(290, 23);
            this.btnPublish.TabIndex = 5;
            this.btnPublish.Text = "发布到目标账户";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 602);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(898, 128);
            this.txtLog.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 742);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnPublish);
            this.Controls.Add(this.ppgProduct);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDestAccounts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aPI设置ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSourceAccountUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSourceAccountPwd;
        private System.Windows.Forms.Label lblSourceAccountProductID;
        private System.Windows.Forms.TextBox txtSourceProductID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgDestAccounts;
        private System.Windows.Forms.Button btnAddAccount;
        private System.Windows.Forms.Button btnGetDetail;
        private System.Windows.Forms.PropertyGrid ppgProduct;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.TextBox txtLog;
    }
}

