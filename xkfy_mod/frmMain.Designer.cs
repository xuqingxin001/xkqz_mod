namespace xkfy_mod
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.手动选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开最近修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCreateMOD = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMods = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpenModFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmImportMod = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCreateCurr = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExplain = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmWriteExplain = new System.Windows.Forms.ToolStripMenuItem();
            this.窗体表格配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dLC新增文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新加载当前文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCreate,
            this.tsmOpen,
            this.tsCreateMOD,
            this.tsmMods,
            this.tsmCreateCurr,
            this.tsmExplain,
            this.重新加载当前文件ToolStripMenuItem,
            this.帮助ToolStripMenuItem,
            this.modToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1306, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmCreate
            // 
            this.tsmCreate.Image = global::xkfy_mod.Properties.Resources._3;
            this.tsmCreate.Name = "tsmCreate";
            this.tsmCreate.Size = new System.Drawing.Size(84, 21);
            this.tsmCreate.Text = "新建方案";
            this.tsmCreate.Click += new System.EventHandler(this.tsmCreate_Click);
            // 
            // tsmOpen
            // 
            this.tsmOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.手动选择ToolStripMenuItem,
            this.打开最近修改ToolStripMenuItem});
            this.tsmOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsmOpen.Image")));
            this.tsmOpen.Name = "tsmOpen";
            this.tsmOpen.Size = new System.Drawing.Size(84, 21);
            this.tsmOpen.Text = "打开方案";
            // 
            // 手动选择ToolStripMenuItem
            // 
            this.手动选择ToolStripMenuItem.Name = "手动选择ToolStripMenuItem";
            this.手动选择ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.手动选择ToolStripMenuItem.Text = "手动选择";
            this.手动选择ToolStripMenuItem.Click += new System.EventHandler(this.tsmOpen_Click);
            // 
            // 打开最近修改ToolStripMenuItem
            // 
            this.打开最近修改ToolStripMenuItem.Name = "打开最近修改ToolStripMenuItem";
            this.打开最近修改ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.打开最近修改ToolStripMenuItem.Text = "打开最近修改";
            this.打开最近修改ToolStripMenuItem.Click += new System.EventHandler(this.打开最近修改ToolStripMenuItem_Click);
            // 
            // tsCreateMOD
            // 
            this.tsCreateMOD.Image = global::xkfy_mod.Properties.Resources._5;
            this.tsCreateMOD.Name = "tsCreateMOD";
            this.tsCreateMOD.Size = new System.Drawing.Size(91, 21);
            this.tsCreateMOD.Text = "生成MOD";
            this.tsCreateMOD.Click += new System.EventHandler(this.tsCreateMOD_Click);
            // 
            // tsmMods
            // 
            this.tsmMods.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmOpenModFolder,
            this.tsmImportMod});
            this.tsmMods.Image = global::xkfy_mod.Properties.Resources._5;
            this.tsmMods.Name = "tsmMods";
            this.tsmMods.Size = new System.Drawing.Size(64, 21);
            this.tsmMods.Text = "Mod";
            // 
            // tsmOpenModFolder
            // 
            this.tsmOpenModFolder.Name = "tsmOpenModFolder";
            this.tsmOpenModFolder.Size = new System.Drawing.Size(164, 22);
            this.tsmOpenModFolder.Text = "打开Mod文件夹";
            this.tsmOpenModFolder.Click += new System.EventHandler(this.tsmOpenModFolder_Click);
            // 
            // tsmImportMod
            // 
            this.tsmImportMod.Name = "tsmImportMod";
            this.tsmImportMod.Size = new System.Drawing.Size(164, 22);
            this.tsmImportMod.Text = "导入基准Mod";
            this.tsmImportMod.Click += new System.EventHandler(this.tsmImportMod_Click);
            // 
            // tsmCreateCurr
            // 
            this.tsmCreateCurr.Name = "tsmCreateCurr";
            this.tsmCreateCurr.Size = new System.Drawing.Size(80, 21);
            this.tsmCreateCurr.Text = "保存Ctrl+S";
            this.tsmCreateCurr.Click += new System.EventHandler(this.tsmCreateCurr_Click);
            // 
            // tsmExplain
            // 
            this.tsmExplain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmWriteExplain,
            this.窗体表格配置ToolStripMenuItem,
            this.dLC新增文件ToolStripMenuItem});
            this.tsmExplain.Image = ((System.Drawing.Image)(resources.GetObject("tsmExplain.Image")));
            this.tsmExplain.Name = "tsmExplain";
            this.tsmExplain.Size = new System.Drawing.Size(60, 21);
            this.tsmExplain.Text = "设置";
            // 
            // tsmWriteExplain
            // 
            this.tsmWriteExplain.Name = "tsmWriteExplain";
            this.tsmWriteExplain.Size = new System.Drawing.Size(148, 22);
            this.tsmWriteExplain.Text = "自定义注释";
            this.tsmWriteExplain.Click += new System.EventHandler(this.tsmWriteExplain_Click);
            // 
            // 窗体表格配置ToolStripMenuItem
            // 
            this.窗体表格配置ToolStripMenuItem.Name = "窗体表格配置ToolStripMenuItem";
            this.窗体表格配置ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.窗体表格配置ToolStripMenuItem.Text = "文件读取配置";
            this.窗体表格配置ToolStripMenuItem.Click += new System.EventHandler(this.窗体表格配置ToolStripMenuItem_Click);
            // 
            // dLC新增文件ToolStripMenuItem
            // 
            this.dLC新增文件ToolStripMenuItem.Name = "dLC新增文件ToolStripMenuItem";
            this.dLC新增文件ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.dLC新增文件ToolStripMenuItem.Text = "DLC新增文件";
            this.dLC新增文件ToolStripMenuItem.Click += new System.EventHandler(this.dLC新增文件ToolStripMenuItem_Click);
            // 
            // 重新加载当前文件ToolStripMenuItem
            // 
            this.重新加载当前文件ToolStripMenuItem.Name = "重新加载当前文件ToolStripMenuItem";
            this.重新加载当前文件ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.重新加载当前文件ToolStripMenuItem.Text = "重新加载";
            this.重新加载当前文件ToolStripMenuItem.Click += new System.EventHandler(this.重新加载当前文件ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("关于ToolStripMenuItem.Image")));
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // modToolStripMenuItem
            // 
            this.modToolStripMenuItem.ForeColor = System.Drawing.Color.LimeGreen;
            this.modToolStripMenuItem.Name = "modToolStripMenuItem";
            this.modToolStripMenuItem.Size = new System.Drawing.Size(133, 21);
            this.modToolStripMenuItem.Text = "交流Q群 496198287";
            this.modToolStripMenuItem.Click += new System.EventHandler(this.modToolStripMenuItem_Click);
            // 
            // dockPanel1
            // 
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DockBackColor = System.Drawing.SystemColors.Control;
            this.dockPanel1.DockLeftPortion = 300D;
            this.dockPanel1.Location = new System.Drawing.Point(0, 25);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(1306, 631);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.dockPanel1.Skin = dockPanelSkin1;
            this.dockPanel1.TabIndex = 0;
            this.dockPanel1.ActiveDocumentChanged += new System.EventHandler(this.dockPanel1_ActiveDocumentChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 656);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "侠客.前传.支持DLC V1.4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.MainWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsCreateMOD;
        private System.Windows.Forms.ToolStripMenuItem tsmCreate;
        private System.Windows.Forms.ToolStripMenuItem tsmOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmMods;
        private System.Windows.Forms.ToolStripMenuItem tsmExplain;
        private System.Windows.Forms.ToolStripMenuItem tsmWriteExplain;
        public WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private System.Windows.Forms.ToolStripMenuItem modToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmCreateCurr;
        private System.Windows.Forms.ToolStripMenuItem tsmOpenModFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmImportMod;
        private System.Windows.Forms.ToolStripMenuItem 重新加载当前文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 窗体表格配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 手动选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开最近修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dLC新增文件ToolStripMenuItem;
    }
}

