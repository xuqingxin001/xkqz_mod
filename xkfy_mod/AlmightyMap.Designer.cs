namespace xkfy_mod
{
    partial class AlmightyMap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.dg1RightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmCopyRow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInsertCopyRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmDelRow = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCopyAdd = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.lbl2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTalkId = new System.Windows.Forms.TextBox();
            this.btnDeBug = new System.Windows.Forms.Button();
            this.lblExplain = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.rowState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.npcName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.dg1RightMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg1
            // 
            this.dg1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg1.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rowState,
            this.npcName});
            this.dg1.ContextMenuStrip = this.dg1RightMenu;
            this.dg1.Location = new System.Drawing.Point(13, 85);
            this.dg1.Name = "dg1";
            this.dg1.ReadOnly = true;
            this.dg1.RowTemplate.Height = 23;
            this.dg1.Size = new System.Drawing.Size(1010, 383);
            this.dg1.TabIndex = 0;
            this.dg1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellClick);
            this.dg1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellDoubleClick);
            this.dg1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellValueChanged);
            this.dg1.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dg1_DefaultValuesNeeded);
            this.dg1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dg1_RowPostPaint);
            // 
            // dg1RightMenu
            // 
            this.dg1RightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCopyRow,
            this.tsmInsertCopyRow,
            this.toolStripSeparator1,
            this.tsmDelRow});
            this.dg1RightMenu.Name = "dg1RightMenu";
            this.dg1RightMenu.Size = new System.Drawing.Size(137, 76);
            this.dg1RightMenu.Opening += new System.ComponentModel.CancelEventHandler(this.dg1RightMenu_Opening);
            // 
            // tsmCopyRow
            // 
            this.tsmCopyRow.Name = "tsmCopyRow";
            this.tsmCopyRow.Size = new System.Drawing.Size(136, 22);
            this.tsmCopyRow.Text = "复制行";
            this.tsmCopyRow.Click += new System.EventHandler(this.tsmCopyRow_Click);
            // 
            // tsmInsertCopyRow
            // 
            this.tsmInsertCopyRow.Name = "tsmInsertCopyRow";
            this.tsmInsertCopyRow.Size = new System.Drawing.Size(136, 22);
            this.tsmInsertCopyRow.Text = "插入复制行";
            this.tsmInsertCopyRow.Click += new System.EventHandler(this.tsmInsertCopyRow_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // tsmDelRow
            // 
            this.tsmDelRow.Name = "tsmDelRow";
            this.tsmDelRow.Size = new System.Drawing.Size(136, 22);
            this.tsmDelRow.Text = "删除行";
            this.tsmDelRow.Click += new System.EventHandler(this.tsmDelRow_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnCopyAdd);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.lbl2);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.lbl1);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1010, 66);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnCopyAdd
            // 
            this.btnCopyAdd.Location = new System.Drawing.Point(918, 28);
            this.btnCopyAdd.Name = "btnCopyAdd";
            this.btnCopyAdd.Size = new System.Drawing.Size(75, 23);
            this.btnCopyAdd.TabIndex = 6;
            this.btnCopyAdd.Text = "复制新增";
            this.btnCopyAdd.UseVisualStyleBackColor = true;
            this.btnCopyAdd.Click += new System.EventHandler(this.btnCopyAdd_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(837, 28);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(262, 15);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(29, 12);
            this.lbl2.TabIndex = 3;
            this.lbl2.Text = "名称";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(264, 30);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(195, 21);
            this.txtName.TabIndex = 2;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(23, 15);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(17, 12);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(25, 30);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(182, 21);
            this.txtID.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.txtTalkId);
            this.panel1.Controls.Add(this.btnDeBug);
            this.panel1.Controls.Add(this.lblExplain);
            this.panel1.Controls.Add(this.lblMsg);
            this.panel1.Location = new System.Drawing.Point(13, 474);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1010, 172);
            this.panel1.TabIndex = 2;
            // 
            // txtTalkId
            // 
            this.txtTalkId.Location = new System.Drawing.Point(785, 23);
            this.txtTalkId.Name = "txtTalkId";
            this.txtTalkId.Size = new System.Drawing.Size(100, 21);
            this.txtTalkId.TabIndex = 3;
            // 
            // btnDeBug
            // 
            this.btnDeBug.Location = new System.Drawing.Point(704, 23);
            this.btnDeBug.Name = "btnDeBug";
            this.btnDeBug.Size = new System.Drawing.Size(75, 23);
            this.btnDeBug.TabIndex = 2;
            this.btnDeBug.Text = "模拟事件";
            this.btnDeBug.UseVisualStyleBackColor = true;
            this.btnDeBug.Visible = false;
            this.btnDeBug.Click += new System.EventHandler(this.btnDeBug_Click);
            // 
            // lblExplain
            // 
            this.lblExplain.AutoSize = true;
            this.lblExplain.Location = new System.Drawing.Point(23, 28);
            this.lblExplain.Name = "lblExplain";
            this.lblExplain.Size = new System.Drawing.Size(0, 12);
            this.lblExplain.TabIndex = 1;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(23, 7);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 12);
            this.lblMsg.TabIndex = 0;
            // 
            // rowState
            // 
            this.rowState.DataPropertyName = "rowState";
            this.rowState.HeaderText = "状态";
            this.rowState.Name = "rowState";
            this.rowState.ReadOnly = true;
            // 
            // npcName
            // 
            this.npcName.DataPropertyName = "NPCNAME";
            this.npcName.HeaderText = "NCP名称";
            this.npcName.Name = "npcName";
            this.npcName.ReadOnly = true;
            // 
            // AlmightyMap
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1035, 658);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dg1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "AlmightyMap";
            this.Text = "地图信息编辑";
            this.Load += new System.EventHandler(this.Almighty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.dg1RightMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnCopyAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblExplain;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.ContextMenuStrip dg1RightMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyRow;
        private System.Windows.Forms.ToolStripMenuItem tsmInsertCopyRow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmDelRow;
        private System.Windows.Forms.Button btnDeBug;
        private System.Windows.Forms.TextBox txtTalkId;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowState;
        private System.Windows.Forms.DataGridViewTextBoxColumn npcName;
    }
}