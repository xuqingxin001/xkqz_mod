using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Config;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class FrmMain : DockContent
    {
        private ToolsHelper _tl = new ToolsHelper();
        public FrmMain()
        {
            InitializeComponent();
        }

        private void MainWnd_Load(object sender, EventArgs e)
        {
        }

        #region 生成所有打开过的MOD文件
        /// <summary>
        /// 生成所有打开过的MOD文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsCreateMOD_Click(object sender, EventArgs e)
        {
            List<MyConfig> list = XmlHelper.XmlDeserializeFromFile<List<MyConfig>>(Application.StartupPath + "/工具配置文件/TableConfig.xml", Encoding.UTF8);
            foreach (MyConfig item in list)
            {
                if (!DataHelper.XkfyData.Tables.Contains(item.MainDtName))
                    continue;
                BuildModsFiles(item.MainDtName);
            }

            foreach (DataTable dt in DataHelper.MapData.Tables)
            {
                string txtName = dt.TableName + ".txt";
                StructureMapData(dt, txtName);
                _tl.BuildDataSetXmlMap(dt.TableName);
            }
            if (!string.IsNullOrEmpty(DataHelper.FilePath))
            {
                DialogResult dialogR = MessageBox.Show("Mod数据生成成功！是否打开生成的数据目录？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogR == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", DataHelper.FilePath);
                }
            }
        }
        #endregion

        #region Ctrl+S 保存当前文件
        private void tsmCreateCurr_Click(object sender, EventArgs e)
        {
            DockContent d = (DockContent)dockPanel1.ActiveDocument;
            if (d == null || d.Tag == null)
                return;
            string tbName = d.Tag.ToString();
            if (DataHelper.FormConfig.ContainsKey(tbName))
            {
                BuildModsFiles(tbName);
            }

            if (DataHelper.MapData.Tables.Contains(tbName))
            {
                DataTable dt = DataHelper.MapData.Tables[tbName];
                string txtName = dt.TableName + ".txt";
                StructureMapData(dt, txtName);
                _tl.BuildDataSetXmlMap(dt.TableName);
            }

            d.Text = d.Text.TrimEnd(new char[] { '*' });
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            //Ctrl+S 保存当前打开的Mod文件
            if (e.KeyCode == Keys.S && e.Control)
            {
                tsmCreateCurr_Click(null, null);
            }
        }
        #endregion

        #region 生成MOD文件

        /// <summary>
        /// 生成MOD文件
        /// </summary>
        /// <param name="tbName">窗口名字</param>
        private void BuildModsFiles(string tbName)
        {
            MyConfig item = DataHelper.FormConfig[tbName];

            string haveColumn = "YES";
            if (string.IsNullOrEmpty(item.HaveColumn) || item.HaveColumn.ToUpper() == "NO")
            {
                haveColumn = "NO";
            }
            switch (item.DtType)
            {
                case "1":
                    StructureData1(item.MainDtName, item.DetailDtName, item.TxtName, haveColumn);
                    if (item.IsCache == "1")
                    {
                        _tl.BuildDataSetXml(item.MainDtName);
                        _tl.BuildDataSetXml(item.DetailDtName);
                    }
                    break;
                case "2":
                    StructureData(item.MainDtName, item.TxtName, haveColumn);
                    if (item.IsCache == "1")
                    {
                        _tl.BuildDataSetXml(item.MainDtName);
                    }
                    break;
                case "3":
                    StructureData3(item.MainDtName, item.TxtName, haveColumn);
                    if (item.IsCache == "1")
                    {
                        _tl.BuildDataSetXml(item.MainDtName);
                    }
                    break;
            }
        }

        #endregion

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _c = new About();
            _c.MdiParent = this;
            _c.Show(this.dockPanel1);

        }

        #region 把字符串写入txt文件中
        /// <summary>
        /// 把字符串写入txt文件中
        /// </summary>
        /// <param name="txt">要写入的字符串</param>
        /// <param name="txtName">文件名</param>
        private void writeData(string txt, string txtName)
        {
            string path = DataHelper.DicFile[txtName];//Path.Combine(DataHelper.filePath, txtName);
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            //开始写入
            sw.Write(txt);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 把字符串写入txt文件中
        /// </summary>
        /// <param name="txt">要写入的字符串</param>
        /// <param name="txtName">文件名</param>
        /// <param name="path">文件路径</param>
        private void writeData(string txt, string txtName,string path)
        {
            File.SetAttributes(path, FileAttributes.Normal);
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            //开始写入
            sw.Write(txt);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
        #endregion

        #region 生成可以拆分为明细表的MOD文件
        /// <summary>
        /// 生成可以拆分为明细表的MOD文件
        /// </summary>
        /// <param name="mainDtName"></param>
        /// <param name="detailDtName"></param>
        /// <param name="txtName"></param>
        /// <param name="type"></param>
        private void StructureData1(string mainDtName, string detailDtName, string txtName,string haveColumn)
        {
            int maxRow = 0;
            StringBuilder sbTitle = new StringBuilder();
            StringBuilder sbConten = new StringBuilder();
            DataTable dtMain = DataHelper.XkfyData.Tables[mainDtName].Copy();
            DataTable dtDetail = DataHelper.XkfyData.Tables[detailDtName].Copy();
            dtMain.Columns.Remove("rowState");
            dtMain.Columns.Remove("indexSn");

            //循环主表的所有列
            foreach (DataRow dr in dtMain.Rows)
            {
                //把主表的列用/t组合成一个字符串
                sbConten.Append(string.Join("\t", dr.ItemArray));
                //用列名查询明细列
                DataRow[] dRow = dtDetail.Select(string.Format("{0}='{1}'", dtDetail.Columns[0].ColumnName, dr[dtMain.Columns[0].ColumnName].ToString()));
                //循环明细列记录
                foreach (DataRow row in dRow)
                {
                    //把明细列用\t组合成一个字符串
                    string strRow = string.Join("\t", row.ItemArray);
                    strRow = strRow.Substring(strRow.IndexOf("\t"));
                    sbConten.Append(strRow);
                }
                if (dRow.Length > maxRow)
                {
                    maxRow = dRow.Length;
                }
                sbConten.Append("\r\n");
            }
            
            List<string> list = new List<string>();
            dtDetail.Columns.Remove(dtDetail.Columns[0].ColumnName);
            for (int i = 0; i < dtMain.Columns.Count; i++)
            {
                list.Add(dtMain.Columns[i].ColumnName);
            }

            for (int j = 0; j < maxRow; j++)
            {
                for (int i = 0; i < dtDetail.Columns.Count; i++)
                {
                    list.Add(dtDetail.Columns[i].ColumnName);
                }
            }

            string content;
            content = sbConten.ToString();
            if (DataHelper.FormConfig[mainDtName].IsDlcFile.ToUpper() != "YES")
            {
                sbTitle.Append(string.Join("\t", list.ToArray()));
                content = sbTitle.ToString().Replace("@", "#") + "\r\n" + sbConten;
            }
            
            dtDetail.Dispose();
            dtMain.Dispose();
            writeData(content, txtName);
        }
        #endregion

        #region 生成map NPCdata MOD文件
        /// <summary>
        /// 生成map NOCdata MOD文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="txtName"></param>
        private void StructureMapData(DataTable dt, string txtName)
        {
            StringBuilder sbTitle = new StringBuilder();
            StringBuilder sbConten = new StringBuilder();
            DataTable dtMain = dt.Copy();
            if (dtMain == null)
                return;
            
            dtMain.Columns.Remove("rowState");
            dtMain.Columns.Remove("indexSn");
            foreach (DataRow dr in dtMain.Rows)
            {
                sbConten.Append(string.Join("\t", dr.ItemArray));
                sbConten.Append("\r\n");
            }

            List<string> list = new List<string>();
            for (int i = 0; i < dtMain.Columns.Count; i++)
            {
                list.Add(dtMain.Columns[i].ColumnName);
            }
            string columnsName = string.Join("\t", list.ToArray());
            sbTitle.Append(columnsName);
            dtMain.Dispose();
            //sbTitle.ToString() + "\r\n" +
            writeData(sbConten.ToString(), txtName, DataHelper.DicFile[txtName]);
        }
        #endregion

        #region 生成列名无重复的标准MOD文件
        /// <summary>
        /// 生成列名无重复的标准MOD文件
        /// </summary>
        /// <param name="mainDtName"></param>
        /// <param name="txtName"></param>
        /// <param name="type"></param>
        private void StructureData(string mainDtName, string txtName, string haveColumn)
        {
            string content = string.Empty;
            StringBuilder sbTitle = new StringBuilder();
            StringBuilder sbConten = new StringBuilder();
            DataTable dtMain = DataHelper.XkfyData.Tables[mainDtName].Copy();

            dtMain.Columns.Remove("rowState");
            dtMain.Columns.Remove("indexSn");

            if (mainDtName == "RoutineData")
            {
                dtMain.Columns["Plus"].ColumnName = " "; 
            }

            if (dtMain.Columns.Contains("Helper"))
            {
                dtMain.Columns.Remove("Helper");
            }

            foreach (DataRow dr in dtMain.Rows)
            {
                sbConten.Append(string.Join("\t", dr.ItemArray));
                sbConten.Append("\r\n");
            }

            if (DataHelper.FormConfig[mainDtName].IsDlcFile.ToUpper() != "YES")
            {
                //如果读取的时候有列,生成的时候也带列
                if (haveColumn == "YES")
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < dtMain.Columns.Count; i++)
                    {
                        list.Add(dtMain.Columns[i].ColumnName);
                    }

                    string columnsName = string.Join("\t", list.ToArray());
                    if (mainDtName == "JourneyData")
                    {
                        columnsName = columnsName.Replace("Column1", "").Replace("@", "#");
                    }
                    sbTitle.Append(columnsName);
                    content = sbTitle.ToString().Replace("@", "#") + "\r\n";
                }
            }
            content = content + sbConten;
            dtMain.Dispose();
            writeData(content, txtName);
        }
        #endregion

        #region 生成列名无规律重复的 MOD文件
        /// <summary>
        /// 生成列名无规律重复的 MOD文件
        /// </summary>
        /// <param name="mainDtName"></param>
        /// <param name="txtName"></param>
        /// <param name="type"></param>
        private void StructureData3(string mainDtName, string txtName, string haveColumn)
        {
            string content = string.Empty;
            StringBuilder sbTitle = new StringBuilder();
            StringBuilder sbConten = new StringBuilder();
            DataTable dtMain = DataHelper.XkfyData.Tables[mainDtName].Copy();

            dtMain.Columns.Remove("rowState");
            dtMain.Columns.Remove("indexSn");

            if (dtMain.Columns.Contains("Helper"))
            {
                dtMain.Columns.Remove("Helper");
            }

            foreach (DataRow dr in dtMain.Rows)
            {
                sbConten.Append(string.Join("\t", dr.ItemArray));
                sbConten.Append("\r\n");
            }

            if (DataHelper.FormConfig[mainDtName].IsDlcFile.ToUpper() != "YES")
            {
                //如果读取的时候有列,生成的时候也带列
                if (haveColumn == "YES")
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < dtMain.Columns.Count; i++)
                    {
                        string cname = dtMain.Columns[i].ColumnName;
                        if (cname.IndexOf("$") != -1)
                        {
                            cname = cname.Substring(0, cname.IndexOf("$"));
                        }
                        list.Add(cname);
                    }

                    sbTitle.Append(string.Join("\t", list.ToArray()));
                    content = sbTitle.ToString().Replace("@", "#") + "\r\n";
                }
            }
            dtMain.Dispose();
            content = content + sbConten;
            writeData(content, txtName);
        }
        #endregion

        private Dock _dock1 = null;
        private About _c = null;

        #region 联动树形菜单选择
        /// <summary>
        /// 联动树形菜单选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dockPanel1_ActiveDocumentChanged(object sender, EventArgs e)
        {
            DockPanel aa = (DockPanel)(sender);
            if (aa.ActiveDocument != null)
            {
                DockContent d = (DockContent)aa.ActiveDocument;
                if (d.Text == "自定义注释" || _dock1 == null)
                {
                    return;
                }
                _dock1.SetSelNode(d.Text.Replace("*","").Replace("[M]","").Replace("[N]",""));
            }
        }
        #endregion

        #region 开始
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="path"></param>
        private void Start(string path)
        {
            string endPath = path.Substring(0, path.IndexOf("xkfy.project"));
            List<ModConfig> list = XmlHelper.XmlDeserializeFromFile<List<ModConfig>>(path, Encoding.UTF8);
            
            foreach (ModConfig m in list)
            {
                if (m.Name == "ModFiles")
                {
                    DataHelper.FilePath = endPath + m.Path;
                    DataHelper.MapPath = endPath + m.Path;
                }
                if (m.Name == "ToolFiles")
                {
                    DataHelper.ToolFilesPath = endPath + m.Path;
                }
            }
            _dock1 = new Dock();
            
            _dock1.Show(this.dockPanel1, DockState.DockLeft);

            foreach (DockContent frm in this.dockPanel1.Contents)
            {
                if (frm.Text == "关于")
                {
                    frm.Activate();
                    return;
                }
            }
            _c = new About();
            _c.MdiParent = this;
            _c.Show(this.dockPanel1);
        }
        #endregion

        #region 新建解决方案
        /// <summary>
        /// 新建解决方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmCreate_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.SelectedPath = FilePathHelper.DefaultSelectFilePath;
            if (folder.ShowDialog() == DialogResult.OK)
            {
                ModConfig mc = new ModConfig();
                mc.Name = "ModFiles";
                mc.Path = "Config";//FilePathHelper.ModsFolder;

                ModConfig mc1 = new ModConfig();
                mc1.Name = "ToolFiles";
                mc1.Path = "ToolFiles";
                
                List<ModConfig> list = new List<ModConfig>();
                list.Add(mc);
                list.Add(mc1);
                string xmlFileName = folder.SelectedPath+ "\\xkfy.project";

                //保存mod方案文件
                XmlHelper.XmlSerializeToFile(list, xmlFileName, Encoding.UTF8);

                AppConfig ac = new AppConfig();
                ac.CreatePath = folder.SelectedPath;
                
                List<AppConfig> listAc = new List<AppConfig>();
                listAc.Add(ac);
                //保存Mod的路径，方便选择
                string appConfigPath = Application.StartupPath + "\\工具配置文件\\AppConfig.xml";
                XmlHelper.XmlSerializeToFile(listAc, appConfigPath, Encoding.UTF8);
                
                CopyFolderTo(FilePathHelper.FullModsFolder, Path.Combine(folder.SelectedPath, FilePathHelper.GameModsFolder));
                ClearData();
                Start(xmlFileName);
            }
        }
        #endregion

        #region 打开解决方案
        /// <summary>
        /// 打开解决方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            List<AppConfig> list = XmlHelper.XmlDeserializeFromFile<List<AppConfig>>(Application.StartupPath + "/工具配置文件/AppConfig.xml", Encoding.UTF8);
            if (list.Count > 0 && !string.IsNullOrEmpty(list[0].CreatePath))
            {
                fileDialog.InitialDirectory = list[0].CreatePath;
            }
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "Mod解决方案|*.project";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ClearData();
                string file = fileDialog.FileName;
                Start(file);
            }
        }
        #endregion

        #region 复制文件夹
        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="directorySource">源文件目录</param>
        /// <param name="directoryTarget">目标目录</param>
        private void CopyFolderTo(string directorySource, string directoryTarget)
        {
            //检查是否存在目的目录  
            if (!Directory.Exists(directorySource))
            {
                MessageBox.Show("无法从工具指定的路径\"" + directorySource + "\"获取到Mods文件\r\n请检查，程序根目录是否存在Config文件夹！");
                return;
            }
            //检查是否存在目的目录  
            if (!Directory.Exists(directoryTarget))
            {
                Directory.CreateDirectory(directoryTarget);
            }
            //先来复制文件  
            DirectoryInfo directoryInfo = new DirectoryInfo(directorySource);
            FileInfo[] files = directoryInfo.GetFiles();
            //复制所有文件  
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(directoryTarget, file.Name),true);
            }
            //最后复制目录  
            DirectoryInfo[] directoryInfoArray = directoryInfo.GetDirectories();
            foreach (DirectoryInfo dir in directoryInfoArray)
            {
                CopyFolderTo(Path.Combine(directorySource, dir.Name), Path.Combine(directoryTarget, dir.Name));
            }

        }
        #endregion

        #region 清除静态变量
        /// <summary>
        /// 清除静态变量
        /// </summary>
        private void ClearData()
        {
            DataHelper.MapPath = string.Empty;
            DataHelper.FilePath = string.Empty;
            DataHelper.ReadError.Clear();
            DataHelper.SelItem.Clear();
            DataHelper.FormConfig.Clear();
            DataHelper.ExplainConfig.Clear();

            DataHelper.XkfyData.Tables.Clear();
            DataHelper.MapData.Tables.Clear();
        }
        #endregion

        private void tsmOpenModFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", DataHelper.FilePath);
        }

        private void tsmWriteExplain_Click(object sender, EventArgs e)
        {
            FrmExplain fe = new FrmExplain();
            fe.MdiParent = this;
            fe.Show(this.dockPanel1);
        }

        #region 导入Mod文件
        private void tsmImportMod_Click(object sender, EventArgs e)
        {
            string modPath = Application.StartupPath + "\\Config";
            DeleteDirectory(modPath);

            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                CopyFolderTo(folder.SelectedPath, modPath);
                MessageBox.Show("已经将指定路径\"" + folder.SelectedPath + "\"所有内容，导入到了工具根目录的Config文件夹\r\n你可以新建解决方案了！");
            }
        }

        /// <summary>
        /// 删除非空文件夹
        /// </summary>
        /// <param name="path">要删除的文件夹目录</param>
        private void DeleteDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                DirectoryInfo[] childs = dir.GetDirectories();
                foreach (DirectoryInfo child in childs)
                {
                    child.Delete(true);
                }
            }
        }
        #endregion

        private void 重新加载当前文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContent d = (DockContent)dockPanel1.ActiveDocument;
            if (d == null || d.Tag == null)
                return;
            string fmName = d.Text.Replace("*","");
            string tbName = d.Tag.ToString();
            string fileName = string.Empty;
            string prefix = fmName.Substring(0, 3);
            string keyName = fmName.Substring(3);
            if (prefix == "[M]")
            {
                DataHelper.MapData.Tables.Remove(tbName);
                fileName = "MapInfo";
                //读取MAP文件
                ToolsHelper.ReadMapData(DataHelper.MapFile[keyName], tbName, fileName);
            }
            else if (prefix == "[N]")
            {
                DataHelper.MapData.Tables.Remove(tbName);
                fileName = "NpcProduct";
                //读取NpcConduct
                ToolsHelper.ReadMapData(DataHelper.MapFile[keyName], tbName, fileName);
            }
            else
            {
                if (DataHelper.FormConfig[tbName].DtType == "1")
                {
                    DataHelper.XkfyData.Tables.Remove(DataHelper.FormConfig[tbName].MainDtName);
                    DataHelper.XkfyData.Tables.Remove(DataHelper.FormConfig[tbName].DetailDtName);
                }
                else
                {
                    DataHelper.XkfyData.Tables.Remove(tbName);
                }
                DataHelper.ExistTable(tbName);
            }

            d.Close();

            

            //反射实例化窗体
            DockContent dc = (DockContent)Assembly.Load("xkfy_mod").CreateInstance("xkfy_mod." + tbName);
            //如果对象为空，则代表使用的是公共窗口
            if (dc == null)
            {
                if (prefix != "[M]" && prefix != "[N]")
                {
                    fileName = "other";
                }

                Almighty a = new Almighty(tbName, fileName);
                a.Text = fmName;
                a.Tag = tbName;
                a.Show(this.dockPanel1, DockState.Document);
            }
            else
            {
                dc.Text = fmName;
                dc.Tag = tbName;
                dc.Show(this.dockPanel1, DockState.Document);
            }
        }
       
        private void 窗体表格配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTbConfig ft = new FrmTbConfig();
            ft.Show();
        }

        private void 打开最近修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearData();
            string filePath = GetModFilePath();
            if(!string.IsNullOrEmpty(filePath))
                Start(GetModFilePath());
        }

        private void modToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test t = new Test();
            t.Show();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您确认退出吗？", "退出程序", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void dLC新增文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
//            string filePath = GetModFilePath();
//            if (!string.IsNullOrEmpty(filePath))
//            {
//
//            }
            DlcNewFile dnf = new DlcNewFile();
            dnf.Show();
        }

        private string GetModFilePath()
        {
            try
            {
                string path = Application.StartupPath + "/工具配置文件/AppConfig.xml";
                if (!File.Exists(path))
                {
                    MessageBox.Show("你还没有新建过方案，请先新建方案！");
                    return "";
                }
                List<AppConfig> list = XmlHelper.XmlDeserializeFromFile<List<AppConfig>>(path, Encoding.UTF8);
                if (list.Count > 0 && !string.IsNullOrEmpty(list[0].CreatePath))
                {
                    return list[0].CreatePath + "\\xkfy.project"; 
                }
                else
                {
                    MessageBox.Show("方案配置文件有误,请重新创建！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
            return "";
        }
    }
}
