using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class DlcNewFile : Form
    {
        public DlcNewFile()
        {
            InitializeComponent();
        }

        private void DlcNewFile_Load(object sender, EventArgs e)
        {
            List<NewFileInof> newFiles = new List<NewFileInof>();

            foreach (KeyValuePair<string, string> files in DataHelper.DicFile)
            {
                string startName = files.Key.Substring(0, 3).ToUpper();
                if (startName == "NPC" || startName == "MAP")
                {
                    continue; 
                }

                if (!DataHelper.ConfigFile.ContainsKey(files.Key))
                {
                    NewFileInof nfi = new NewFileInof();
                    nfi.DlcFileName = files.Key;
                    nfi.DlcFilePath = files.Value;
                    newFiles.Add(nfi);
                }
            }

            BindingList<NewFileInof> bl = new BindingList<NewFileInof>(newFiles);

            dg1.DataSource = bl;

        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\工具配置文件\\TableConfig.xml";
            List<MyConfig> menuList = XmlHelper.XmlDeserializeFromFile<List<MyConfig>>(path, Encoding.UTF8);

            int rowIndex = 1;
            foreach (DataGridViewRow row in dg1.Rows)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell) row.Cells["chkSel"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag)
                {
                    string dlcFileName = row.Cells["DlcFileName"].Value.ToString();
                    string dlcFilePath = row.Cells["DlcFilePath"].Value.ToString();
                    string tbName = dlcFileName.Substring(0, dlcFileName.IndexOf("."));

                    string configFileName = tbName + ".xml";
                    string configRawFileName = tbName.Substring(4, tbName.Length - 4) + ".xml";
                    string configFilepath = Path.Combine(Application.StartupPath + "\\工具配置文件\\TableExplain", configFileName);
                    string configRawFilepath = Path.Combine(Application.StartupPath + "\\工具配置文件\\TableExplain", configRawFileName);

                    if (!File.Exists(configRawFilepath))
                        continue;


                    MyConfig mc = new MyConfig();
                    mc.TxtName = dlcFileName;
                    mc.Classify = "Dlc";
                    mc.Notes = dlcFileName;
                    mc.HaveColumn = "NO";
                    mc.IsCache = "0";
                    mc.DtType = "3";
                    mc.BasicCritical = "";
                    mc.EffectCritical = "";
                    mc.HaveHelperColumn = "";
                    mc.DetailDtName = "";
                    mc.MainDtName = tbName;
                    menuList.Add(mc);

                    List<TableExplain> configFileList = new List<TableExplain>();


                    int index = 0;
                    using (StreamReader sr = new StreamReader(dlcFilePath, Encoding.Default))
                    {
                        string names = string.Empty;
                        string ids = string.Empty;
                        while (index < 2)
                        {
                            if (index == 0)
                            {
                                names = sr.ReadLine(); //读取一行数据
                            }
                            else
                            {
                                ids = sr.ReadLine();
                            } 
                            index++;
                        }
                        string[] rowIds = ids.Split('\t');
                        string[] rowNames = names.Split('\t');

                        List<TableExplain> teList = XmlHelper.XmlDeserializeFromFile<List<TableExplain>>(configRawFilepath, Encoding.UTF8);
                        for (int i = 0; i < rowIds.Length; i++)
                        {
                            TableExplain te = new TableExplain();
                            if (i < 3)
                            {
                                te.IsSelect = "1";
                            }
                            te.Column = teList[i].Column;
                            te.Explain = teList[i].Explain;
                            te.Text = rowNames[i];
                            te.ToolsColumn = teList[i].ToolsColumn;
                            configFileList.Add(te);
                            rowIndex++;
                        }
                        rowIndex = rowIndex + 10;
                    }


                    XmlHelper.XmlSerializeToFile(configFileList, configFilepath, Encoding.UTF8);
                }
            }

            XmlHelper.XmlSerializeToFile(menuList, path, Encoding.UTF8);
            MessageBox.Show("修改成功！");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\工具配置文件\\TableConfig.xml";
            string path2 = Path.Combine(Application.StartupPath + "\\工具配置文件\\TableExplain","ExplainKey");

            List<MyConfig> menuList = XmlHelper.XmlDeserializeFromFile<List<MyConfig>>(path, Encoding.UTF8);

            List<TableExplain> dlcColumn = new List<TableExplain>();

            int rowIndex = 1;
            foreach (DataGridViewRow row in dg1.Rows)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)row.Cells["chkSel"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag)
                {
                    string dlcFileName = row.Cells["DlcFileName"].Value.ToString();
                    string dlcFilePath = row.Cells["DlcFilePath"].Value.ToString();
                    string tbName = dlcFileName.Substring(0, dlcFileName.IndexOf("."));
                    MyConfig mc = new MyConfig();
                    mc.TxtName = dlcFileName;
                    mc.Classify = "Dlc";
                    mc.Notes = dlcFileName;
                    mc.HaveColumn = "NO";
                    mc.IsCache = "0";
                    mc.DtType = "3";
                    mc.BasicCritical = "";
                    mc.EffectCritical = "";
                    mc.HaveHelperColumn = "";
                    mc.DetailDtName = "";
                    mc.MainDtName = tbName;
                    menuList.Add(mc);

                    List<TableExplain> configFileList = new List<TableExplain>();

                    string configFileName = tbName + ".xml";
                    string configRawFileName = tbName.Substring(4, tbName.Length - 4) + ".xml";
                    string configFilepath = Path.Combine(Application.StartupPath + "\\工具配置文件\\TableExplain", configFileName);
                    string configRawFilepath = Path.Combine(Application.StartupPath + "\\工具配置文件\\TableExplain", configRawFileName);

                    int index = 0;
                    using (StreamReader sr = new StreamReader(dlcFilePath, Encoding.Default))
                    {
                        string names = string.Empty;
                        string ids = string.Empty;
                        while (index < 2)
                        {
                            if (index == 0)
                            {
                                names = sr.ReadLine(); //读取一行数据
                            }
                            else
                            {
                                ids = sr.ReadLine();
                            }
                            index++;
                        }
                        string[] rowIds = ids.Split('\t');
                        string[] rowNames = names.Split('\t');

                        //List<TableExplain> teList = XmlHelper.XmlDeserializeFromFile<List<TableExplain>>(configRawFilepath, Encoding.UTF8);
                        for (int i = 0; i < rowIds.Length; i++)
                        {
                            TableExplain te = new TableExplain();
                            if (i < 3)
                            {
                                te.IsSelect = "1";
                            }
                            //te.Column = rowIds[i] + "$" + i;
                            te.Column = rowIds[i];
                            te.Explain = "";
                            te.Text = rowNames[i];
                            te.ToolsColumn = "TxzColumn" + rowIndex;
                            configFileList.Add(te);
                            rowIndex++;

                            TableExplain dlcTe = new TableExplain();
                            dlcTe.ToolsColumn = "TxzColumn" + rowIndex;
                            dlcTe.Column = rowIds[i];
                            dlcTe.Text = configFileName;
                            dlcColumn.Add(dlcTe);
                        }
                        rowIndex = rowIndex + 10;
                    }


                    XmlHelper.XmlSerializeToFile(configFileList, configFilepath, Encoding.UTF8);
                    XmlHelper.XmlSerializeToFile(dlcColumn, path2, Encoding.UTF8);
                }
            }

            XmlHelper.XmlSerializeToFile(menuList, path, Encoding.UTF8);
            MessageBox.Show("修改成功！");
        }
    }

    public class NewFileInof
    {
        public string DlcFileName { get; set; }
        public string DlcFilePath { get; set; }
    }
}
