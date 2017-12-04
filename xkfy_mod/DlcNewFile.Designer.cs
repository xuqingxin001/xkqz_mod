namespace xkfy_mod
{
    partial class DlcNewFile
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
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.btnBuild = new System.Windows.Forms.Button();
            this.chkSel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DlcFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DlcFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.SuspendLayout();
            // 
            // dg1
            // 
            this.dg1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkSel,
            this.DlcFileName,
            this.DlcFilePath});
            this.dg1.Location = new System.Drawing.Point(12, 41);
            this.dg1.Name = "dg1";
            this.dg1.RowTemplate.Height = 23;
            this.dg1.Size = new System.Drawing.Size(1120, 420);
            this.dg1.TabIndex = 0;
            // 
            // btnBuild
            // 
            this.btnBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuild.Location = new System.Drawing.Point(1057, 12);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(75, 23);
            this.btnBuild.TabIndex = 1;
            this.btnBuild.Text = "创建";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // chkSel
            // 
            this.chkSel.HeaderText = "选择";
            this.chkSel.Name = "chkSel";
            // 
            // DlcFileName
            // 
            this.DlcFileName.DataPropertyName = "DlcFileName";
            this.DlcFileName.HeaderText = "文件名";
            this.DlcFileName.Name = "DlcFileName";
            this.DlcFileName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DlcFileName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DlcFilePath
            // 
            this.DlcFilePath.DataPropertyName = "DlcFilePath";
            this.DlcFilePath.HeaderText = "文件路径";
            this.DlcFilePath.Name = "DlcFilePath";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(976, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "创建2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DlcNewFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 473);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.dg1);
            this.Name = "DlcNewFile";
            this.Text = "DlcNewFile";
            this.Load += new System.EventHandler(this.DlcNewFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkSel;
        private System.Windows.Forms.DataGridViewTextBoxColumn DlcFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DlcFilePath;
        private System.Windows.Forms.Button button1;
    }
}