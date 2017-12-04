using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace xkfy_mod.Data
{
    public class DataHelper
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public static string FilePath = string.Empty;

        /// <summary>
        /// 地图文件路径
        /// </summary>
        public static string MapPath = string.Empty;

        /// <summary>
        /// 工具保存的XML文件路径
        /// </summary>
        public static string ToolFilesPath = string.Empty;

        /// <summary>
        /// 读取文件的错误信息
        /// </summary>
        public static StringBuilder ReadError = new StringBuilder();

        /// <summary>
        /// 存放所有TXT数据转化的DataTable
        /// </summary>
        public static DataSet XkfyData = new DataSet();

        /// <summary>
        /// 存放所有map.txt数据转化的DataTable
        /// </summary>
        public static DataSet MapData = new DataSet();

        /// <summary>
        /// 存储所有的MOD文件信息
        /// </summary>
        public static Dictionary<string, string> DicFile = new Dictionary<string, string>();

        /// <summary>
        /// 存储地图文件
        /// </summary>
        public static Dictionary<string, string> MapFile = new Dictionary<string, string>();

        /// <summary>
        /// 存放所有下拉框的数据源
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> SelItem = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 存储窗体配置信息
        /// </summary>
        public static Dictionary<string, MyConfig> FormConfig = new Dictionary<string, MyConfig>();

        /// <summary>
        /// 存储解释配置数字需要用到的数据
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> ExplainConfig = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 新增的文件信息
        /// </summary>
        public static StringBuilder newFilesInfo = new StringBuilder();

        /// <summary>
        /// 配置文件
        /// </summary>
        public static Dictionary<string, string> ConfigFile = new Dictionary<string, string>();

        #region 设置Table数据
        /// <summary>
        /// 设置有重复列的TXT文件的数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="tableName"></param>
        /// <param name="type"></param>
        public static void setMainDt(string path,string tableName,string type)
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            readConfig(path, dt, "");
            XkfyData.Tables.Add(dt);
        }

        /// <summary>
        /// 设置可以分为明细表的TXT文件的数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mainDtName"></param>
        /// <param name="detailDtName"></param>
        /// <param name="basicCritical"></param>
        /// <param name="effectCritical"></param>
        public static void setMainDt(string path, string mainDtName,string detailDtName, int basicCritical, int effectCritical)
        {
            DataTable mainDt = new DataTable();
            mainDt.TableName = mainDtName;
            DataTable detailDt = new DataTable();
            detailDt.TableName = detailDtName;
            readConfig(path, basicCritical, effectCritical, mainDt, detailDt);
            
            XkfyData.Tables.Add(mainDt);
            XkfyData.Tables.Add(detailDt);
        }


        /// <summary>
        /// 设置没有重复列的TXT文件的数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="tableName"></param>
        public static void setMainDt(string path, string tableName)
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            readConfig(path, dt);
            XkfyData.Tables.Add(dt);
        }
        #endregion

        #region 从配置文件中读取配置信息,绑定到相应的下拉框
        /// <summary>
        /// 从配置文件中读取配置信息,绑定到相应的下拉框
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="cb">要绑定的ComboBox名称</param>
        public static void SetConfig(string path, string dtName)
        {
            DataTable dt = new DataTable();
            dt.TableName = dtName;
            XkfyData.Tables.Add(dt);
            readConfig(path, dt);
        }
        #endregion

        #region 绑定数据到下拉框
        /// <summary>
        /// 绑定数据到下拉框
        /// </summary>
        /// <param name="cb">下拉框对象</param>
        /// <param name="dt">数据源</param>
        public static void BinderComboBox(ComboBox cb,DataTable dt)
        {
            cb.DisplayMember = "text";
            cb.ValueMember = "value";
            cb.DataSource = dt;
        }

        /// <summary>
        /// 绑定数据到下拉框
        /// </summary>
        /// <param name="cb">下拉框对象</param>
        /// <param name="dt">数据源</param>
        public static void BinderComboBox(ComboBox cb, string key)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = DataHelper.SelItem[key];
            cb.DataSource = bs;
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }

        /// <summary>
        /// 绑定数据到下拉框
        /// </summary>
        /// <param name="cb">下拉框对象</param>
        /// <param name="dic">数据源</param>
        public static void BinderComboBox(ComboBox cb, IDictionary<string,string> dic)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dic;
            cb.DataSource = bs;
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }

        /// <summary>
        /// 绑定数据到下拉框
        /// </summary>
        /// <param name="cb">下拉框对象</param>
        /// <param name="dt">数据源</param>
        public static void BinderComboBox<T>(ComboBox cb, List<T> list)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = list;
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
            cb.DataSource = bs;
        }
        #endregion

        #region 通用读取xml配置文件的方法
        /// <summary>
        /// 通用读取xml配置文件的方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <returns>返回传入类型</returns>
        public static List<T> ReadXmlToList<T>(String path)
        {
            try
            {
                List<T> list = XmlHelper.XmlDeserializeFromFile<List<T>>(path, Encoding.UTF8);
                return list;
            }
            catch (Exception ex)
            {
                return default(List<T>);
            }
        }
        #endregion

        #region 把TXT文件读取到 Dictionary ,适合下拉框
        /// <summary>
        ///  把TXT文件读取到 Dictionary ,适合下拉框，地图文件专属
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="key">key值</param>
        public static void readConfig(string path, string key)
        {
            if (SelItem.ContainsKey(key))
                return;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            int index = 0;
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string readStr = sr.ReadLine();//读取一行数据
                    string[] strs = readStr.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                    if (index == 0)
                    {
                        index++;
                        continue;
                    }
                    dic.Add(strs[0], strs[1]);
                }
            }
            SelItem.Add(key, dic);
        }

        public static void ReadSelItem(string path, string key)
        {
            if (SelItem.ContainsKey(key))
                return;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            List<SelectItem> dicList = XmlHelper.XmlDeserializeFromFile<List<SelectItem>>(path, Encoding.UTF8);
            foreach (SelectItem item in dicList)
            {
                dic.Add(item.Value, item.Text);
            }
            SelItem.Add(key, dic);
        }
        #endregion

        #region 把可以分为明细表的TXT文件 读取到DataTable里去,有重复列用
        /// <summary>
        /// 把TXT文件 读取到DataTable里去,有重复列用
        /// </summary>
        /// <param name="path"></param>
        /// <param name="basicCritical">基本信息列下标</param>
        /// <param name="effectCritical">重复的明细信息列</param>
        /// <param name="dtMain">主表</param>
        /// <param name="dtDetail">明细表</param>
        public static void readConfig(string path, int basicCritical, int effectCritical, DataTable dtMain, DataTable dtDetail)
        {
            int index = 0;
            //存放表头信息
            string[] tabTitle = null;

            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    try
                    {
                        string readStr = sr.ReadLine(); //读取一行数据
                        //string[] strs = readStr.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                        string[] strs = readStr.Split('\t');
                        if (index == 0)
                        {
                            #region 解决DLC文件两行表头的问题
                            if (DataHelper.FormConfig[dtMain.TableName].IsDlcFile.ToUpper() == "YES")
                            {
                                string columnPath = Path.Combine(ToolsHelper.ExplainPath, dtMain.TableName + ".xml");
                                if (File.Exists(columnPath))
                                {
                                    List<TableExplain> teList = ReadXmlToList<TableExplain>(columnPath);
                                    foreach (TableExplain te in teList)
                                    {
                                        if (!string.IsNullOrEmpty(te.Column))
                                        {
                                            dtMain.Columns.Add(te.Column.Replace("#", "@"));
                                        }
                                        else
                                        {
                                            dtMain.Columns.Add(te.ToolsColumn);
                                        }
                                    }
                                    dtMain.Columns.Add("rowState");
                                }
                                else
                                {
                                    throw new Exception("列配置文件未找到！\r\n路径：" + columnPath);
                                }

                                string detailPath = Path.Combine(ToolsHelper.ExplainPath, dtMain.TableName + "_D.xml");
                                if (File.Exists(detailPath))
                                {
                                    List<TableExplain> teList = ReadXmlToList<TableExplain>(detailPath);
                                    foreach (TableExplain te in teList)
                                    {
                                        if (!string.IsNullOrEmpty(te.Column))
                                        {
                                            dtDetail.Columns.Add(te.Column.Replace("#", "@"));
                                        }
                                        else
                                        {
                                            dtDetail.Columns.Add(te.ToolsColumn);
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception("列配置文件未找到！\r\n路径：" + columnPath);
                                } 
                            }
                            else
                            {
                                tabTitle = strs;
                                //循环读取主表的表头
                                for (int i = 0; i < basicCritical; i++)
                                {
                                    dtMain.Columns.Add(strs[i].Replace("#", "@"));
                                    if (i == 0)
                                    {
                                        dtDetail.Columns.Add(strs[i].Replace("#", "@"));
                                    }
                                }
                                dtMain.Columns.Add("rowState");
                                //循环读取明细表的表头
                                for (int i = basicCritical; i < effectCritical; i++)
                                {
                                    dtDetail.Columns.Add(strs[i]);
                                }
                                index++;
                                continue;
                            }
                            #endregion
                        }

                        //小于这个数目，数据肯定不正确了，也不需要继续执行了
                        if (strs.Length < basicCritical)
                        {
                            ReadError.AppendFormat("文件{0}.txt 第{1} 行数据格式有误,可能是多余的空行没有删除，也可能是数据格式不准确,程序将忽略这行数据\r\n",
                                dtMain.TableName, index + 1);
                            continue;
                        }
                        string id = string.Empty;
                        //7列之前是基本信息
                        DataRow dr = dtMain.NewRow();
                        for (int i = 0; i < basicCritical; i++)
                        {
                            dr[i] = strs[i];
                            if (i == 0)
                            {
                                id = strs[i];
                            }
                        }

                        DataRow dr1 = dtDetail.NewRow();
                        for (int i = basicCritical; i < strs.Length; i++)
                        {
                            int postion = (i - basicCritical)%6;
                            switch (postion)
                            {
                                case 0:
                                    dr1 = dtDetail.NewRow();
                                    dr1["EffectType"] = strs[i];
                                    break;
                                case 1:
                                    dr1["Accumulate"] = strs[i];
                                    break;
                                case 2:
                                    dr1["Percent"] = strs[i];
                                    break;
                                case 3:
                                    dr1["Value1"] = strs[i];
                                    break;
                                case 4:
                                    dr1["Value2"] = strs[i];
                                    break;
                                case 5:
                                    dr1["ValueLimit"] = strs[i];
                                    break;
                            }

                            if ((i - basicCritical)%6 == 0)
                            {
                                dr1[0] = id;
                                dtDetail.Rows.Add(dr1);
                            }
                        }
                        dtMain.Rows.Add(dr);

                        index++;
                    }
                    catch (Exception ex)
                    {
                        ReadError.Append(ex.Message + "\r\n");
                    }
                }
                dtMain.Columns.Add("indexSn", typeof(int));
            }
        }
        #endregion

        #region 把列名无重复的TXT文件 读取到DataTable里去
        /// <summary>
        /// 把TXT文件 读取到DataTable里去,下拉框无重复列用
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="dt">表名</param>
        public static void readConfig(string path,DataTable dt)
        {
            int index = 0;
            MyConfig item = DataHelper.FormConfig[dt.TableName];
            //以手动文件作为列名
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    try
                    {
                        string readStr = sr.ReadLine(); //读取一行数据

                        if (string.IsNullOrEmpty(readStr))
                        {
                            continue;
                        }
                        string[] strs = readStr.Split('\t');

                        //如果是首行,并且列配置为有表头,跳过这一行
                        if (index == 0)
                        {
                            index++;
                            //如果没有Mod文件没有表头
                            if (string.IsNullOrEmpty(item.HaveColumn) || item.HaveColumn.ToUpper() == "NO")
                            {
                                string columnPath = Path.Combine(ToolsHelper.ExplainPath, dt.TableName + ".xml");
                                if (File.Exists(columnPath))
                                {
                                    List<TableExplain> teList = ReadXmlToList<TableExplain>(columnPath);
                                    foreach (TableExplain te in teList)
                                    {
                                        if (!string.IsNullOrEmpty(te.Column))
                                        {
                                            dt.Columns.Add(te.Column.Replace("#", "@"));
                                        }
                                        else
                                        {
                                            dt.Columns.Add(te.ToolsColumn);
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception("列配置文件未找到！\r\n路径：" + columnPath);
                                }
                                //dt.Columns.Add("TxzKey", typeof(int));
                                dt.Columns.Add("indexSn", typeof(int));
                                dt.Columns.Add("rowState");
                                //dt.PrimaryKey = new DataColumn[] { dt.Columns["TxzKey"] };
                            }
                            else
                            {
                                for (int i = 0; i < strs.Length; i++)
                                {
                                    dt.Columns.Add(strs[i].Replace("#", "@"));
                                }

                                if (dt.TableName == "RoutineData")
                                {
                                    dt.Columns[strs.Length - 1].ColumnName = "plus";
                                }
                                //dt.Columns.Add("TxzKey", typeof(int));
                                dt.Columns.Add("indexSn", typeof(int));
                                dt.Columns.Add("rowState");
                                //dt.PrimaryKey = new DataColumn[] { dt.Columns["TxzKey"] };
                                continue;
                            }
                        }

                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < strs.Length; i++)
                        {
                            if (i >= dt.Columns.Count-2)
                            {
                                ReadError.AppendFormat("文件{0}.txt 第{1} 行数据格式有误,数据长度超越了表头,只按表头长度来处理数据\r\n",
                                    dt.TableName, index + 1);
                                break;
                            }
                            dr[i] = strs[i];
                        }
                        //dr["TxzKey"] = index;
                        dt.Rows.Add(dr);
                        index++;
                    }
                    catch (Exception e)
                    {
                        ReadError.Append(e.Message+"\r\n");
                    }
                }
            }
        }
        #endregion

        #region 把列名有重复无规律的TXT文件 读取到DataTable里去

        /// <summary>
        /// 把TXT文件 读取到DataTable里去,下拉框无重复列用
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="dt">表名</param>
        public static void readConfig(string path, DataTable dt, string type)
        {
            int index = 0;
            MyConfig item = DataHelper.FormConfig[dt.TableName];
            //以手动文件作为列名
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    try
                    {
                        string readStr = sr.ReadLine(); //读取一行数据

                        if (string.IsNullOrEmpty(readStr))
                        {
                            continue;
                        }
                        string[] strs = readStr.Split('\t');

                        //如果是首行,并且列配置为有表头,跳过这一行
                        if (index == 0)
                        {
                            index++;
                            //如果没有Mod文件没有表头
                            if (string.IsNullOrEmpty(item.HaveColumn) || item.HaveColumn.ToUpper() == "NO")
                            {
                                string columnPath = Path.Combine(ToolsHelper.ExplainPath, dt.TableName + ".xml");
                                if (File.Exists(columnPath))
                                {
                                    List<TableExplain> teList = ReadXmlToList<TableExplain>(columnPath);
                                    foreach (TableExplain te in teList)
                                    {
                                        if (!string.IsNullOrEmpty(te.Column))
                                        {
                                            dt.Columns.Add(te.Column.Replace("#","@"));
                                        }
                                        else
                                        {
                                            dt.Columns.Add(te.ToolsColumn);
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception("列配置文件未找到！\r\n路径：" + columnPath);
                                }
                                //dt.Columns.Add("TxzKey", typeof(int));
                                dt.Columns.Add("indexSn", typeof(int));
                                dt.Columns.Add("rowState");
                                //dt.PrimaryKey = new DataColumn[] { dt.Columns["TxzKey"] };
                            }
                            else
                            {
                                for (int i = 0; i < strs.Length; i++)
                                {
                                    dt.Columns.Add(strs[i].Replace("#", "@") + "$" + i);
                                }
                                //dt.Columns.Add("TxzKey", typeof(int));
                                dt.Columns.Add("indexSn", typeof(int));
                                dt.Columns.Add("rowState");
                                //dt.PrimaryKey = new DataColumn[] { dt.Columns["TxzKey"] };
                                continue;
                            }
                        }
                        
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < strs.Length; i++)
                        {
                            if (i >= dt.Columns.Count-2)
                            {
                                ReadError.AppendFormat("文件{0}.txt 第{1} 行数据格式有误,数据长度超越了表头,只按表头长度来处理数据\r\n",
                                    dt.TableName, index + 1);
                                break;
                            }
                            dr[i] = strs[i];
                        }
                        dr["indexSn"] = index;
                        dt.Rows.Add(dr);
                        index++;
                    }
                    catch (Exception e)
                    {
                        ReadError.Append(e.Message + "\r\n");
                    }

                }
            }
            
        }

        #endregion

        #region 将窗体上的控件赋值给DataRow
        /// <summary>
        /// 将窗体上的控件赋值给DataRow
        /// </summary>
        /// <param name="control">窗体</param>
        /// <param name="dr">要接受值的DataRow</param>
        public static void CopyDataToRow(Control control,DataRow dr)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Tag == null)
                {
                    continue;
                }

                string rowName = c.Tag.ToString();
                if (!dr.Table.Columns.Contains(rowName))
                    continue;

                if (c is TextBox)
                {
                    dr[rowName] = ((TextBox)c).Text;
                }
                else if (c is ComboBox)
                {
                    //判断下拉框样式，如果是可选可输入，绑定文本框
                    ComboBox com = ((ComboBox)c);
                    if (com.DropDownStyle == ComboBoxStyle.DropDown)
                    {
                        dr[rowName] = com.Text;
                    }
                    else
                    {
                        //如果是只能选择，绑定下拉框的数据源里选中项
                        dr[rowName] = com.SelectedValue;
                    }
                }
                else if (c is RadioButton)
                {
                    if (((RadioButton)c).Checked)
                    {
                        dr[rowName] = GetRdoValue(((RadioButton)c).Text);
                    }
                }
                else if (c is Panel || c is GroupBox)
                {
                    CopyDataToRow(c, dr);
                }
            }
        }

        /// <summary>
        /// 将窗体上控件的值转换为DataGridViewRow对象
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dr"></param>
        public static void CopyDataToRow(Control control, DataGridViewRow dr)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Tag == null)
                {
                    continue;
                }

                string rowName = c.Tag.ToString();
                if (!dr.DataGridView.Columns.Contains(rowName))
                {
                    continue;
                }

                if (c is TextBox)
                {
                    dr.Cells[rowName].Value = ((TextBox)c).Text;
                }
                else if (c is ComboBox)
                {
                    //判断下拉框样式，如果是可选可输入，绑定文本框
                    ComboBox com = ((ComboBox)c);
                    if (com.DropDownStyle == ComboBoxStyle.DropDown)
                    {
                        dr.Cells[rowName].Value = com.Text;
                    }
                    else
                    {
                        //如果是只能选择，绑定下拉框的数据源里选中项
                        dr.Cells[rowName].Value = com.SelectedValue;
                    }
                }
                else if (c is RadioButton)
                {
                    if (((RadioButton)c).Checked)
                    {
                        dr.Cells[rowName].Value = GetRdoValue(((RadioButton)c).Text);
                    }
                }
                else if (c is Panel || c is GroupBox)
                {
                    CopyDataToRow(c, dr);
                }
            }
        }
        #endregion

        #region 将dataRow的值赋给窗体控件
        /// <summary>
        /// 将DataRow对象的值赋给窗体控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dt"></param>
        /// <param name="dr"></param>
        public static void CopyRowToData(Control control,DataRow dr)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Tag == null)
                {
                    continue;
                }

                string rowName = c.Tag.ToString();
                if (!dr.Table.Columns.Contains(rowName))
                {
                    continue;
                }

                string value = dr[rowName].ToString();

                if (c is TextBox)
                {
                    ((TextBox)c).Text = value;
                }
                else if (c is ComboBox)
                {
                    //判断下拉框样式，如果是可选可输入，绑定文本框
                    ComboBox com = ((ComboBox)c);
                    if (com.DropDownStyle == ComboBoxStyle.DropDown)
                    {
                        com.Text = value;
                    }
                    else 
                    {
                        //如果是只能选择，绑定下拉框的数据源里选中项
                        com.SelectedValue = value;
                    }
                }
                else if (c is RadioButton)
                {
                    if (((RadioButton)c).Text.IndexOf(value) == -1)
                    {
                        continue;
                    }
                    ((RadioButton)c).Checked = true;
                }
                else if (c is Panel || c is GroupBox)
                {
                    CopyRowToData(c, dr);
                }
            }
        }

        /// <summary>
        /// 将DataGridViewRow对象的值赋给窗体控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dt"></param>
        /// <param name="dr"></param>
        public static void CopyRowToData(Control control,DataGridViewRow dr)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Tag == null)
                {
                    continue;
                }

                string rowName = c.Tag.ToString();
                if (!dr.DataGridView.Columns.Contains(rowName))
                {
                    continue;
                }

                string value = dr.Cells[rowName].Value.ToString();

                if (c is TextBox)
                {
                    ((TextBox)c).Text = value;
                }
                else if (c is ComboBox)
                {
                    //判断下拉框样式，如果是可选可输入，绑定文本框
                    ComboBox com = ((ComboBox)c);
                    if (com.DropDownStyle == ComboBoxStyle.DropDown)
                    {
                        com.Text = value;
                    }
                    else
                    {
                        //如果是只能选择，绑定下拉框的数据源里选中项
                        com.SelectedValue = value;
                    }
                }
                else if (c is RadioButton)
                {
                    if (((RadioButton)c).Text.IndexOf(value) == -1)
                    {
                        continue;
                    }
                    ((RadioButton)c).Checked = true;
                }
                else if (c is Panel || c is GroupBox)
                {
                    CopyRowToData(c, dr);
                }
            }
        }
        #endregion

        #region 判断是否已经加载过X表
        /// <summary>
        /// 判断是否已经加载过X表
        /// </summary>
        /// <param name="tbName">表名称</param>
        public static void ExistTable(string tbName)
        {
            if (!XkfyData.Tables.Contains(tbName))
            {
                ReadTbConfig(tbName);
            }
        }
        #endregion

        #region 根据表配置信息,调用对应的方法加载数据
        /// <summary>
        /// 根据表配置信息,调用对应的方法加载数据
        /// </summary>
        /// <param name="tbName"></param>
        private static void ReadTbConfig(string tbName)
        {
            ReadError.Clear();
            if (!DataHelper.FormConfig.ContainsKey(tbName))
                return;
            MyConfig item = DataHelper.FormConfig[tbName];
            string dtType = item.DtType;
            string isCache = item.IsCache;

            string path = DataHelper.ToolFilesPath + "\\" + tbName + ".xml";
            //如果开启了缓存文件，并且XML文件存在读取XML文件
            if (isCache == "1" && File.Exists(path))
            {
                XkfyData.Tables.Add(tbName);
                XkfyData.Tables[tbName].ReadXml(path);
                //如果类型=1 代表有明细表
                if (dtType == "1")
                {
                    XkfyData.Tables.Add(item.DetailDtName);
                    string path1 = DataHelper.ToolFilesPath + "\\" + item.DetailDtName + ".xml";
                    XkfyData.Tables[item.DetailDtName].ReadXml(path1);
                }
            }
            else
            {
                switch (dtType)
                {
                    case "1":
                        //拆分明细表
                        DataHelper.setMainDt(DicFile[item.TxtName], item.MainDtName, item.DetailDtName, int.Parse(item.BasicCritical), int.Parse(item.EffectCritical));
                        break;
                    case "2":
                        //列名无重复
                        DataHelper.setMainDt(DicFile[item.TxtName], item.MainDtName);
                        break;
                    case "3":
                        //列名有重复
                        DataHelper.setMainDt(DicFile[item.TxtName], item.MainDtName, "");
                        break;
                }

                if (item.HaveHelperColumn == "YES")
                {
                    if (!XkfyData.Tables[tbName].Columns.Contains("Helper"))
                    {
                        XkfyData.Tables[tbName].Columns.Add("Helper").SetOrdinal(0);
                    }
                    SetHelperColumn(XkfyData.Tables[tbName], tbName);
                }
            }
        }

        private static void SetHelperColumn(DataTable dt,string tableName)
        {
            foreach (DataRow dr in dt.Rows)
            {
                switch (tableName)
                {
                    case "CharacterData":
                        dr["Helper"] = DataHelper.GetNpcName(dr["@CharID$0"].ToString());
                        break;
                    case "MapTalkManager":
                        dr["Helper"] = DataHelper.GetNpcName(dr["iNpcID"].ToString());
                        break;
                }

            }
        }
        #endregion

        #region 返回Name等中文名字
        /// <summary>
        /// 返回Name等中文名字
        /// </summary>
        /// <param name="tbName">DataTable的名称</param>
        /// <param name="rtRow">返回的列名</param>
        /// <param name="selRow">查询的列名</param>
        /// <param name="iId">条件</param>
        /// <returns></returns>
        public static string GetValue(string tbName,string rtRow,string selRow, string iId)
        {
            ExistTable(tbName);
            DataRow[] dr = XkfyData.Tables[tbName].Select(string.Format("{0}='{1}'", selRow, iId));
            if (dr.Length > 1)
                return string.Format("{0}.txt 文件中{2}【{1}】出现了多次", tbName, iId, selRow);
            else if (dr.Length == 0)
                return string.Format("{0}.txt 文件中{2}【{1}】未找到", tbName, iId, selRow); ;
            return dr[0][rtRow].ToString();
        }

        //返回对话专用
        public static string GetMapTalk(string iId, string iOrder)
        {
            string tbName = "MapTalkManager";
            ExistTable(tbName);
            DataRow[] dr = XkfyData.Tables[tbName].Select(string.Format("iOrder='{0}' and @sGroupID ='{1}'", iOrder, iId));
            if (dr.Length > 1)
                return string.Format("{0}.txt 文件中{2}【{1}】出现了多次", tbName, iId, iOrder);
            else if (dr.Length == 0)
                return string.Format("{0}.txt 文件中{2}【{1}】未找到", tbName, iId, iOrder); ;

            string npcName = DataHelper.GetNpcName(dr[0]["iNpcID"].ToString());
            
            return npcName + ":" + dr[0]["sManager"].ToString();
        }
        #endregion

        /// <summary>
        /// 读取联动信息的配置文件
        /// </summary>
        public static void readConfig()
        {
            //读取下拉框联动信息
            List<SelectItem> list = XmlHelper.XmlDeserializeFromFile<List<SelectItem>>(Application.StartupPath + "/工具配置文件/DevelopQuestData_Config.xml", Encoding.UTF8);
            DataTable dt = new DataTable();
            dt.TableName = "Config";
            dt.Columns.Add("Type");
            dt.Columns.Add("ChildType");
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (SelectItem dc in list)
            {
                if (dc.Type == "itype")
                {
                    dic.Add(dc.Value, dc.Text);
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["Type"] = dc.Type;
                    dr["ChildType"] = dc.ChildType;
                    dr["Text"] = dc.Text;
                    dr["Value"] = dc.Value;
                    dt.Rows.Add(dr);
                }
            }
            ExplainConfig.Add("DevelopQuest", dic);
            XkfyData.Tables.Add(dt);
        }

        #region 设置字典的值
        /// <summary>
        /// 设置字典的值
        /// </summary>
        public static void SetDicValue()
        {
            //读取DicConfig.xml 
            //循环读取招式,技艺,等数据
            string path = Application.StartupPath + "/工具配置文件/";
            List<DicConfig> list = XmlHelper.XmlDeserializeFromFile<List<DicConfig>>(path + "DicConfig.xml", Encoding.UTF8);
            foreach (DicConfig dc in list)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<SelectItem> dicList = XmlHelper.XmlDeserializeFromFile<List<SelectItem>>(path + dc.Value, Encoding.UTF8);
                foreach (SelectItem item in dicList)
                {
                    dic.Add(item.Value, item.Text);
                }
                ExplainConfig.Add(dc.Key, dic);
            }
        }

        public static void SetHuiHeDicValue()
        {
            string path = Application.StartupPath + "/工具配置文件/";
            List<DicConfig> list = XmlHelper.XmlDeserializeFromFile<List<DicConfig>>(path + "HuiHe.xml", Encoding.UTF8);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DicConfig dc in list)
            {
                dic.Add(dc.Key, dc.Value);   
            }
            ExplainConfig.Add("HuiHe", dic);
        }

        /// <summary>
        /// 所有可输入可选择的下拉框,统一读取文件数据的方法
        /// </summary>
        public static void SetDicConfig(string key, string path)
        {
            if (ExplainConfig.ContainsKey(key))
                return;
            List<DicConfig> list = ReadXmlToList<DicConfig>(CboData.NeiGongPath);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DicConfig dc in list)
            {
                dic.Add(dc.Key, dc.Value);
            }
            ExplainConfig.Add(key, dic);
        }
        
        /// <summary>
        /// 所有可输入可选择的下拉框,统一读取文件数据的方法,窗体关闭会重新加载数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <param name="read"></param>
        public static void setDicConfig(string key, string path,bool read)
        {
            if (SelItem.ContainsKey(key))
            {
                SelItem.Remove(key);
            }
            path = Application.StartupPath + "/" + path;
            List<DicConfig> list = XmlHelper.XmlDeserializeFromFile<List<DicConfig>>(path, Encoding.UTF8);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DicConfig dc in list)
            {
                dic.Add(dc.Key, dc.Value);
            }
            SelItem.Add(key, dic);
        }

        public static string GetHuiHe(string key)
        {
            if (DataHelper.ExplainConfig["HuiHe"].ContainsKey(key))
                return DataHelper.ExplainConfig["HuiHe"][key];
            return "回合数超过了180，如有需要请自行配置HuiHe.xml文件";
        }
        #endregion

        public static string GetDicValue(string key,string value)
        {
            if (ExplainConfig[key].ContainsKey(value))
            {
                return ExplainConfig[key][value];
            }
            return "";
        }

        #region 使用正则表达式截取##中的各类ID
        /// <summary>
        /// 使用正则表达式截取##中的各类ID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetId(string str)
        {
            MatchCollection m = Regex.Matches(str, @"(?<=\#)[^\[\]]+(?=\#)");//正则
            if (m.Count > 0)
            {
                return m[0].Value;
            }
            return "";
        }

        /// <summary>
        /// 使用正则表达式截取[]中的各类ID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetRdoValue(string str)
        {
            MatchCollection m = Regex.Matches(str, @"(?<=\[)[^\[\]]+(?=\])");//正则
            if (m.Count > 0)
            {
                return m[0].Value;
            }
            return "";
        }
        #endregion
        

        public static Dictionary<string, TableExplain> GetTableExplainList(string path)
        {
            Dictionary<string, TableExplain> dlc = new Dictionary<string, TableExplain>();

            List<TableExplain> list = DataHelper.ReadXmlToList<TableExplain>(path);
            foreach (TableExplain item in list)
            {
                if (!string.IsNullOrEmpty(item.Column))
                {
                    dlc.Add(item.Column.Replace("#", "@"), item);
                }
                else
                {
                    dlc.Add(item.ToolsColumn, item);
                }
            }

            return dlc;
        }

        #region 使用配置文件修改控件Text值
        /// <summary>
        /// 使用配置文件修改控件Text值
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dr"></param>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <param name="btnUpdate"></param>
        /// <param name="btnAdd"></param>
        public static void SetLabelTextBox(Control control, DataGridViewRow dr, string type, string path,Button btnUpdate,Button btnAdd)
        {
            List<TableExplain> list = DataHelper.ReadXmlToList<TableExplain>(path);
            int i = 1;
            foreach (TableExplain item in list)
            {
                string lblText = string.Empty;
                string tag = string.Empty;
                Control[] label = control.Controls.Find("label" + i, false);
                Control[] textBox = control.Controls.Find("textBox" + i, false);

                if (label.Length > 0)
                {
                    if (!string.IsNullOrEmpty(item.Text))
                    {
                        lblText = item.Text;
                    }
                    else if (!string.IsNullOrEmpty(item.Column))
                    {
                        lblText = item.Column.Replace("#", "@");
                    }
                    else
                    {
                        lblText = item.ToolsColumn;
                    }
                    label[0].Text = lblText;
                }

                if (textBox.Length > 0)
                {
                    if (!string.IsNullOrEmpty(item.Column))
                    {
                        tag = item.Column.Replace("#", "@");
                    }
                    else
                    {
                        tag = item.ToolsColumn;
                    }
                    
                    if (dr.DataGridView.Columns.Contains(tag))
                    {
                        textBox[0].Tag = tag;
                        if (type != "Add")
                        {
                            textBox[0].Text = dr.Cells[tag].Value.ToString();
                        }
                    }
                }
                i++;
            }

            if (type == "Modify")
            {
                //修改
                btnUpdate.Visible = true;
            }
            else
            {
                //新增
                btnAdd.Visible = true;
            }
        }
        public static void SetLabelTextBox(Control control, DataGridViewRow dr, string type, string path, Button btnUpdate, Button btnAdd,string onlyLabel)
        {
            List<TableExplain> list = DataHelper.ReadXmlToList<TableExplain>(path);
            int i = 1;
            foreach (TableExplain item in list)
            {
                string lblText = string.Empty;
                string tag = string.Empty;
                Control[] label = control.Controls.Find("label" + i, false);
                Control[] textBox = control.Controls.Find("textBox" + i, false);

                if (label.Length > 0)
                {
                    if (!string.IsNullOrEmpty(item.Column))
                    {
                        string columnText = item.Column;
                        if (columnText.IndexOf("$") != -1)
                        {
                            columnText = columnText.Substring(0, columnText.IndexOf("$"));
                        }
                        lblText = columnText.Replace("#", "@") + "【" + item.Text + "】";
                    }
                    else
                    {
                        lblText = item.ToolsColumn;
                    }
                    label[0].Text = lblText;
                } 
                i++;
            }

            if (type == "Modify")
            {
                //修改
                btnUpdate.Visible = true;
            }
            else
            {
                //新增
                btnAdd.Visible = true;
            }
        }

        #endregion

        /// <summary>
        /// 弹出选择状态的窗口
        /// </summary>
        /// <param name="txtId">ID框</param>
        /// <param name="txtName">文字框</param>
        public static void SelCondition(TextBox txtId, TextBox txtName)
        {
            string[] row = new string[3] { "@ConditionID", "CondName", "CondDesc" };
            RadioList rl = new RadioList("BattleCondition", row, txtId, txtName, "1");
            rl.ShowDialog();
        }

        /// <summary>
        /// 返回效果名称
        /// </summary>
        /// <param name="id">效果ID</param>
        /// <returns></returns>
        public static string GetConditionName(string id)
        {
           return DataHelper.GetValue("BattleCondition", "CondName", "@ConditionID", id);
        }

        public static void SetCboTextBox(Control control,object sender)
        {
            ComboBox cb = ((ComboBox)sender);
            string name = ((ComboBox)sender).Tag.ToString();

            Control[] text = control.Controls.Find(name, false);
            if (text.Length > 0)
            {
                text[0].Text = cb.SelectedValue .ToString();
            }
        }

        public static string GetNeiGongLevelAttribute(string attText)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string[] attributes = attText.Split('*');
                for (int i = 0; i < attributes.Length; i++)
                {
                    if (string.IsNullOrEmpty(attributes[i]))
                        continue;
                    string[] attribute = attributes[i].Replace("(", "").Replace(")", "").Split(',');
                    sb.AppendFormat("【{0}】+{1}", ExplainConfig["NeiGong.Attribute"][attribute[0]], attribute[1]);
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #region 验证文本框输入为整数
        /// <summary>
        /// 验证文本框输入为整数
        /// </summary>
        /// <param name="strNum">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool validateNum(string strNum)
        {
            return Regex.IsMatch(strNum, "^[0-9]*$");
        }
        #endregion

        public static string GetNpcName(string id)
        {
            return DataHelper.GetValue("NpcData", "sNpcName", "@iID", id);
        }

        /// <summary>
        /// 物品名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetItemName(string id)
        {
            return DataHelper.GetValue("ItemData", "sItemName$1", "@iItemID$0", id);
        }

        /// <summary>
        /// 天赋文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetTalentName(string id)
        {
            return DataHelper.GetValue("TalentNewData", "sTalenName$1", "@iTalenID$0", id);
        }

        /// <summary>
        /// 内功名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetNeiGongName(string id)
        {
            return DataHelper.GetValue("NeigongData", "Name$1", "@ID$0", id);
        }

        /// <summary>
        /// 招式名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetRoutineName(string id)
        {
            return DataHelper.GetValue("RoutineNewData", "sRoutineName", "@iRoutineID", id);
        }

        /// <summary>
        /// 通用提示字符串
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetStringTab(string id)
        {
            return DataHelper.GetValue("String_table", "sString", "@iID", id);
        }

        public static string GetQuestDataManager(string id)
        {
            return DataHelper.GetValue("QuestDataManager", "sQuestName", "@sID", id);
        }

        /// <summary>
        /// NPC武学信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetCharacterData(string id)
        {
            return DataHelper.GetValue("CharacterData", "Helper", "@CharID", id);
        }

        /// <summary>
        /// 对话
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetMapTalkManager(string id)
        {
            return DataHelper.GetValue("CharacterData", "Helper", "@CharID", id);
        }

        /// <summary>
        /// 返回内功状态对应的说明
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetNeiGong(string id)
        {
            return DataHelper.GetValue("DLC_BattleCondition", "CondDesc", "@ConditionID", id);
        }


        public static string GetText(string source, string id)
        {
            string text = string.Empty;
            switch (source)
            {
                case "ItemData":
                    text = GetItemName(id);
                    break;
                case "NpcData":
                    text = GetNpcName(id);
                    break;
                case "Talent":
                    text = GetTalentName(id);
                    break;
                case "NeiGong":
                    text = GetNeiGongName(id);
                    break;
                case "RoutineData":
                    text = GetRoutineName(id);
                    break;
            }
            return text;
        }

        #region 搜索文件夹中的文件
        /// <summary>
        /// //搜索文件夹中的文件
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        public static void GetAllFile(DirectoryInfo dir, Dictionary<string, string> dic)
        {
            FileInfo[] allFile = dir.GetFiles();
            foreach (FileInfo fi in allFile)
            {
                if (dic.ContainsKey(fi.Name))
                {
                    DataHelper.ReadError.Append("存在多个\"" + fi.Name + "\"文件！\r\n文件路径1：" + dic[fi.Name] + "\r\n文件路径2：" + fi.FullName + "\r\n请手动去掉重复的文件,不然工具无法识别要读取哪个文件修改");
                    continue;
                }
                dic.Add(fi.Name, fi.FullName);
            }

            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                GetAllFile(d, dic);
            }
        }
        #endregion

        #region 搜索MAP文件夹中的文件
        /// <summary>
        /// //搜索文件夹中的文件
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        public static void GetMapIconFile(DirectoryInfo dir, Dictionary<string, string> dic)
        {
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                if (d.Name.ToUpper().Equals("MAPICON"))
                {
                    FileInfo[] allFile = d.GetFiles();
                    foreach (FileInfo fi in allFile)
                    {
                        dic.Add(fi.Name, fi.FullName);
                    }
                    break;
                }
                else
                {
                    GetMapIconFile(d, dic);
                }
            }
        }
        #endregion
    }
}
