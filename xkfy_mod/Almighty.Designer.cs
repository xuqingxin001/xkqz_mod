﻿namespace xkfy_mod
{
    partial class Almighty
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
            this.indexSn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rowState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dg1RightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmCopyRow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInsertCopyRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmDelRow = new System.Windows.Forms.ToolStripMenuItem();
            this.gbQueryCon = new System.Windows.Forms.GroupBox();
            this.lblTag = new System.Windows.Forms.Label();
            this.btnCopyAdd = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblExplainRed = new System.Windows.Forms.Label();
            this.gbZhanDou = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFale = new System.Windows.Forms.TextBox();
            this.lblSl = new System.Windows.Forms.TextBox();
            this.txtWin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExplain = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbError = new System.Windows.Forms.GroupBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnToVisible = new System.Windows.Forms.Button();
            this.txtError = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.dg1RightMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbZhanDou.SuspendLayout();
            this.gbError.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg1
            // 
            this.dg1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexSn,
            this.rowState});
            this.dg1.ContextMenuStrip = this.dg1RightMenu;
            this.dg1.Location = new System.Drawing.Point(5, 103);
            this.dg1.Name = "dg1";
            this.dg1.RowTemplate.Height = 23;
            this.dg1.Size = new System.Drawing.Size(980, 377);
            this.dg1.TabIndex = 0;
            this.dg1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellDoubleClick);
            this.dg1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellValueChanged);
            this.dg1.CurrentCellChanged += new System.EventHandler(this.dg1_CurrentCellChanged);
            this.dg1.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dg1_DefaultValuesNeeded);
            this.dg1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dg1_RowPostPaint);
            // 
            // indexSn
            // 
            this.indexSn.DataPropertyName = "indexSn";
            this.indexSn.HeaderText = "工具排序用";
            this.indexSn.Name = "indexSn";
            this.indexSn.Visible = false;
            this.indexSn.Width = 60;
            // 
            // rowState
            // 
            this.rowState.DataPropertyName = "rowState";
            this.rowState.HeaderText = "rowState";
            this.rowState.Name = "rowState";
            this.rowState.Visible = false;
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
            // gbQueryCon
            // 
            this.gbQueryCon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbQueryCon.Location = new System.Drawing.Point(5, 0);
            this.gbQueryCon.Name = "gbQueryCon";
            this.gbQueryCon.Size = new System.Drawing.Size(896, 70);
            this.gbQueryCon.TabIndex = 1;
            this.gbQueryCon.TabStop = false;
            // 
            // lblTag
            // 
            this.lblTag.AutoSize = true;
            this.lblTag.Location = new System.Drawing.Point(3, 83);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(467, 12);
            this.lblTag.TabIndex = 7;
            this.lblTag.Text = "点击序号，选中整行后，在不使用查询的情况下，可以使用方向键↑ ↓调整数据的顺序";
            // 
            // btnCopyAdd
            // 
            this.btnCopyAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyAdd.Location = new System.Drawing.Point(920, 35);
            this.btnCopyAdd.Name = "btnCopyAdd";
            this.btnCopyAdd.Size = new System.Drawing.Size(64, 23);
            this.btnCopyAdd.TabIndex = 6;
            this.btnCopyAdd.Text = "复制新增";
            this.btnCopyAdd.UseVisualStyleBackColor = true;
            this.btnCopyAdd.Visible = false;
            this.btnCopyAdd.Click += new System.EventHandler(this.btnCopyAdd_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(920, 64);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(920, 6);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(64, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lblExplainRed);
            this.panel1.Controls.Add(this.gbZhanDou);
            this.panel1.Controls.Add(this.txtExplain);
            this.panel1.Location = new System.Drawing.Point(5, 486);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 193);
            this.panel1.TabIndex = 2;
            // 
            // lblExplainRed
            // 
            this.lblExplainRed.AutoSize = true;
            this.lblExplainRed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblExplainRed.Location = new System.Drawing.Point(277, 11);
            this.lblExplainRed.Name = "lblExplainRed";
            this.lblExplainRed.Size = new System.Drawing.Size(0, 12);
            this.lblExplainRed.TabIndex = 9;
            // 
            // gbZhanDou
            // 
            this.gbZhanDou.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbZhanDou.Controls.Add(this.textBox1);
            this.gbZhanDou.Controls.Add(this.label5);
            this.gbZhanDou.Controls.Add(this.lblFale);
            this.gbZhanDou.Controls.Add(this.lblSl);
            this.gbZhanDou.Controls.Add(this.txtWin);
            this.gbZhanDou.Controls.Add(this.label1);
            this.gbZhanDou.Controls.Add(this.label3);
            this.gbZhanDou.Controls.Add(this.label2);
            this.gbZhanDou.Location = new System.Drawing.Point(522, 7);
            this.gbZhanDou.Name = "gbZhanDou";
            this.gbZhanDou.Size = new System.Drawing.Size(453, 178);
            this.gbZhanDou.TabIndex = 8;
            this.gbZhanDou.TabStop = false;
            this.gbZhanDou.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(103, 123);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(344, 49);
            this.textBox1.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "失败：";
            // 
            // lblFale
            // 
            this.lblFale.Location = new System.Drawing.Point(103, 41);
            this.lblFale.Name = "lblFale";
            this.lblFale.Size = new System.Drawing.Size(344, 21);
            this.lblFale.TabIndex = 9;
            // 
            // lblSl
            // 
            this.lblSl.Location = new System.Drawing.Point(103, 14);
            this.lblSl.Name = "lblSl";
            this.lblSl.Size = new System.Drawing.Size(344, 21);
            this.lblSl.TabIndex = 8;
            // 
            // txtWin
            // 
            this.txtWin.Location = new System.Drawing.Point(103, 68);
            this.txtWin.Multiline = true;
            this.txtWin.Name = "txtWin";
            this.txtWin.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtWin.Size = new System.Drawing.Size(344, 49);
            this.txtWin.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "我方胜利条件：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "胜利：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DeepPink;
            this.label2.Location = new System.Drawing.Point(8, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "我方失败条件：";
            // 
            // txtExplain
            // 
            this.txtExplain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExplain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExplain.Location = new System.Drawing.Point(7, 7);
            this.txtExplain.Multiline = true;
            this.txtExplain.Name = "txtExplain";
            this.txtExplain.ReadOnly = true;
            this.txtExplain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExplain.Size = new System.Drawing.Size(509, 178);
            this.txtExplain.TabIndex = 10;
            // 
            // gbError
            // 
            this.gbError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbError.Controls.Add(this.lblTime);
            this.gbError.Controls.Add(this.label4);
            this.gbError.Controls.Add(this.btnToVisible);
            this.gbError.Controls.Add(this.txtError);
            this.gbError.Location = new System.Drawing.Point(5, 103);
            this.gbError.Name = "gbError";
            this.gbError.Size = new System.Drawing.Size(980, 377);
            this.gbError.TabIndex = 8;
            this.gbError.TabStop = false;
            this.gbError.Visible = false;
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(860, 109);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(89, 12);
            this.lblTime.TabIndex = 3;
            this.lblTime.Text = "30秒后自动隐藏";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.OrangeRed;
            this.label4.Location = new System.Drawing.Point(9, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(329, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "读取文件出错了，以下是具体错误信息，忽略请点击右侧按钮";
            // 
            // btnToVisible
            // 
            this.btnToVisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToVisible.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnToVisible.Location = new System.Drawing.Point(856, 46);
            this.btnToVisible.Name = "btnToVisible";
            this.btnToVisible.Size = new System.Drawing.Size(113, 56);
            this.btnToVisible.TabIndex = 1;
            this.btnToVisible.Text = "朕知道了";
            this.btnToVisible.UseVisualStyleBackColor = true;
            this.btnToVisible.Click += new System.EventHandler(this.btnToVisible_Click);
            // 
            // txtError
            // 
            this.txtError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtError.Location = new System.Drawing.Point(7, 46);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtError.Size = new System.Drawing.Size(843, 317);
            this.txtError.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Almighty
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(992, 683);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCopyAdd);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbQueryCon);
            this.Controls.Add(this.dg1);
            this.Controls.Add(this.gbError);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Name = "Almighty";
            this.Text = "Almighty";
            this.Load += new System.EventHandler(this.Almighty_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Almighty_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.dg1RightMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbZhanDou.ResumeLayout(false);
            this.gbZhanDou.PerformLayout();
            this.gbError.ResumeLayout(false);
            this.gbError.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbQueryCon;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnCopyAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbZhanDou;
        private System.Windows.Forms.ContextMenuStrip dg1RightMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyRow;
        private System.Windows.Forms.ToolStripMenuItem tsmInsertCopyRow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmDelRow;
        private System.Windows.Forms.Label lblExplainRed;
        private System.Windows.Forms.TextBox txtExplain;
        private System.Windows.Forms.TextBox txtWin;
        public System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowState;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexSn;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TextBox lblFale;
        private System.Windows.Forms.TextBox lblSl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbError;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnToVisible;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTime;
    }
}