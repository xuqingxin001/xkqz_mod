﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using xkfy_mod.EventEntity;

namespace xkfy_mod.Data
{
    public class ToolsHelper
    {
        /// <summary>
        /// 表列配置文件的路径
        /// </summary>
        public static string ExplainPath = Application.StartupPath + "\\工具配置文件\\TableExplain";
        public void AddData(Form form,string tbName)
        {
            DataRow dr = DataHelper.XkfyData.Tables[tbName].NewRow();
            DataHelper.CopyDataToRow(form, dr);
            
            if (string.IsNullOrEmpty(dr["rowState"].ToString()))
                dr["rowState"] = "1";
            DataHelper.XkfyData.Tables[tbName].Rows.InsertAt(dr, 0);
            form.Close();
        }

        public void updateData(Form form, DataGridViewRow dr)
        {
            DataHelper.CopyDataToRow(form, dr);
            if (string.IsNullOrEmpty(dr.Cells["rowState"].Value.ToString()))
                dr.Cells["rowState"].Value = "0";
            dr.DataGridView.CurrentCell = null;
            form.Close();
        }

        public void updateData(Form form, DataRow dr)
        {
            DataHelper.CopyDataToRow(form, dr);
            if (string.IsNullOrEmpty(dr["rowState"].ToString()))
                dr["rowState"] = "0";
            form.Close();
        }

        #region 返回中文字符数量
        /// <summary>
        /// 返回中文数量
        /// </summary>
        /// <param name="textboxTextStr">输入的字符串</param>
        /// <returns></returns>
        public int GetCnLength(string textboxTextStr)
        {
            int nLength = 0;
            for (int i = 0; i < textboxTextStr.Length; i++)
            {
                if (textboxTextStr[i] >= 0x3000 && textboxTextStr[i] <= 0x9FFF)
                    nLength++;
            }
            return nLength;
        }
        #endregion

        #region 规律创建控件
        /// <summary>
        /// 规律创建控件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="form"></param>
        /// <param name="dr"></param>
        public void CreateCtr(DataTable dt,Form form, DataGridViewRow dr, Dictionary<string, TableExplain> dlc, ToolTip toolTip)
        {
            int t = 0;
            int top = 15;
            int left = 15;
            int width = 100;
            int height = 21;
            int columnIndex = 0;
            int test = 0;

            string value = string.Empty;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                t = 0;
                string tipsName = dt.Columns[i].ColumnName;
                if (tipsName == "rowState" || tipsName == "Helper" || tipsName == "key" || tipsName == "indexSn")
                    continue;
                Boolean dh = false;
                if (dr != null)
                {
                    if (dr.Cells[tipsName].Value == null)
                        continue;

                    value = dr.Cells[tipsName].Value.ToString();
                    int cnStrLen = GetCnLength(value);
                    int enStrLen = value.Length;

                    if (cnStrLen <= 5 && enStrLen <= 10)
                    {
                        width = 100;
                    }
                    else if (cnStrLen <= 10 && enStrLen <= 20)
                    {
                        width = 530;// 315;
                    }
                    else if (cnStrLen <= 20 && enStrLen <= 40)
                    {
                        width = 745;
                    }
                    else if (cnStrLen <= 30 && enStrLen <= 60)
                    {
                        width = 745;
                        dh = true;
                        height = 42;
                        t = 21;
                    }
                    else
                    {
                        width = 745;
                        dh = true;
                        height = 63;
                        t = 42;
                    }
                }

                if (900 - left - 100 < width || t > 0)
                {
                    left = 15;
                    top = top + 30;
                    columnIndex = 0;
                }
                
                Label tips = new Label();
                tips.Text = tipsName;
                tips.Top = top + 4;
                tips.Left = left;
                tips.Width = 95;
                

                //如果填写了文字就显示
                if (dlc.ContainsKey(tipsName))
                {
                    TableExplain te = dlc[tipsName];

                    //把lable文字修改为中文显示
                    if (!string.IsNullOrEmpty(te.Text))
                    {
                        tips.Text = te.Text;
                    }
                    else if (!string.IsNullOrEmpty(te.Column))
                    {
                        tips.Text = te.Column;
                    }
                    else
                    {
                        tips.Text = te.ToolsColumn;
                    }

                    toolTip.SetToolTip(tips, te.Explain);
                }

                left += 100;//tips.Width;
                tips.TextAlign = ContentAlignment.TopRight;

                TextBox tb = new TextBox();
                tb.Tag = tipsName;
                tb.Text = value;
                tb.Top = top;
                tb.Left = left;// + 100 + columnIndex * 200 + columnIndex * 15;
                tb.Width = width; 

                if (dh)
                {
                    tb.Multiline = true;
                    tb.Height = height;
                }
                left += tb.Width + 15;
                top = top + t;
                columnIndex++;
                form.Controls.Add(tb);
                form.Controls.Add(tips);
            }
            form.Height = top + 135;
        }

        private int GetStrLength(string text)
        {
            int length = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (Regex.IsMatch(text[i].ToString(), @"[\u4e00-\u9fbb]"))
                    length = length + 2;
                else
                    length++;
            }
            return length;
        }

        public void CreateCtr(DataTable dt, Control form, DataGridViewRow dr, Dictionary<string, TableExplain> dlc,int startIndex)
        {
            int t = 0;
            int top = 15;
            int left = 15;
            int width = 80;
            int height = 21;
            int columnIndex = 0;

            string value = string.Empty;
            for (int i = startIndex; i < dt.Columns.Count; i++)
            {
                t = 0;
                string tipsName = dt.Columns[i].ColumnName;
                if (tipsName == "rowState" || tipsName == "Helper" || tipsName == "key" || tipsName == "indexSn")
                    continue;
                Boolean dh = false;
                if (dr != null)
                {
                    if (dr.Cells[tipsName].Value == null)
                        continue;

                    value = dr.Cells[tipsName].Value.ToString();
                    int cnStrLen = GetCnLength(value);
                    int enStrLen = value.Length;

                    if (cnStrLen <= 5 && enStrLen <= 10)
                    {
                        width = 60;
                    }
                    else if (cnStrLen <= 10 && enStrLen <= 20)
                    {
                        width = 530;// 315;
                    }
                    else if (cnStrLen <= 20 && enStrLen <= 40)
                    {
                        width = 745;
                    }
                    else if (cnStrLen <= 30 && enStrLen <= 60)
                    {
                        width = 745;
                        dh = true;
                        height = 42;
                        t = 21;
                    }
                    else
                    {
                        width = 745;
                        dh = true;
                        height = 63;
                        t = 42;
                    }
                }

                if (700 - left - 100 < width || t > 0)
                {
                    left = 15;
                    top = top + 30;
                    columnIndex = 0;
                }

                Label tips = new Label();
                tips.Text = tipsName;
                tips.Top = top + 4;
                tips.Left = left;

                tips.Width = 67;
                left += tips.Width;
                //鼠标移动到Lable上显示的文字
                string tipsText = string.Empty;
                //如果填写了文字就显示
                if (dlc.ContainsKey(tipsName))
                {
                    TableExplain te = dlc[tipsName];

                    //把lable文字修改为中文显示
                    if (!string.IsNullOrEmpty(te.Text))
                    {
                        tips.Text = te.Text;
                    }
                    else if (!string.IsNullOrEmpty(te.Column))
                    {
                        tips.Text = te.Column;
                    }
                    else
                    {
                        tips.Text = te.ToolsColumn;
                    }
                }

                TextBox tb = new TextBox();
                tb.Tag = tipsName;
                tb.Text = value;
                tb.Top = top;
                tb.Left = left;// + 100 + columnIndex * 200 + columnIndex * 15;
                tb.Width = width;
                if (dh)
                {
                    tb.Multiline = true;
                    tb.Height = height;
                }
                left += tb.Width + 15;
                top = top + t;
                columnIndex++;
                form.Controls.Add(tips);
                form.Controls.Add(tb);
            }
            form.Height = top + 135;
        }

        #endregion

        #region 解释物品奖励
        /// <summary>
        /// 解释物品奖励
        /// </summary>
        /// <param name="sRewardData">列</param>
        /// <param name="dt">数据源</param>
        /// <returns></returns>
        public string ExplainRewardData(string[] sRewardData, DataTable dt)
        {
            DataHelper.ExistTable("String_table");
            DataTable strT = DataHelper.XkfyData.Tables["String_table"];

            StringBuilder sbExplain = new StringBuilder();

            foreach (string rw in sRewardData)
            {
                string[] reward = rw.Replace(")", "").Replace("(", "").Split(',');
                if (reward.Length < 4)
                {
                    sbExplain.Append("本行数据有误,请检查!");
                    continue;
                }

                string jl = reward[0];
                string jlType = reward[1];
                string value = reward[2];
                string strMsg = reward[3];

                DataRow[] driJl = dt.Select("Type='Reward' and Value='" + jl + "'");
                if (driJl.Length == 0)
                {
                    sbExplain.Append("未在配置文件中找到对应的配置,请联系作者");
                    continue;
                }

                if (driJl.Length > 1)
                {
                    sbExplain.Append("配置文件有误，请联系作者！");
                    continue;
                }

                string tempStr = string.Empty;
                switch (jl)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "24":
                    case "30":
                    case "32":
                        DataRow[] drjlType = dt.Select("Type='" + driJl[0]["ChildType"].ToString() + "' and Value='" + jlType + "'");
                        if (drjlType.Length == 0)
                            continue;
                        DataRow drStr = strT.Select("iID='" + strMsg + "'")[0];
                        tempStr = drStr["sString"].ToString();
                        sbExplain.AppendFormat(tempStr + "", value);
                        break;
                    case "6":
                        sbExplain.AppendFormat("【{0}】对你好感增加了{1}点  ", DataHelper.GetValue("NpcData", "sNpcName", "iID", jlType), value);
                        break;
                    case "7":
                        sbExplain.AppendFormat("获得物品【{0}】", DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", jlType));
                        break;
                    case "13":
                        sbExplain.AppendFormat("获得称号【{0}】", DataHelper.GetValue("TitleData", "sTitle", "iID", jlType));
                        break;
                    case "16":
                        sbExplain.AppendFormat("获得套路【{0}】", DataHelper.GetValue("RoutineData", "sRoutineName", "iRoutineID", jlType));
                        break;
                    case "17":
                        sbExplain.AppendFormat("获得内功【{0}】", DataHelper.GetValue("BattleNeigong", "Name", "ID", jlType));
                        break;
                    case "18":
                        sbExplain.AppendFormat("增加经历【{0}】", DataHelper.GetValue("JourneyData", "sTip", "iID", jlType));
                        break;
                    case "19":
                        sbExplain.AppendFormat("增加【{0}】说明", DataHelper.GetValue("AbilityBookData", "sAbilityID", "iID", jlType));
                        break;
                    case "20":
                        sbExplain.AppendFormat("触发动画{0}", rw);
                        break;
                    case "21":
                        sbExplain.AppendFormat("触发场景对话,对话ID为【{0}】", value);
                        break;
                    case "14":
                    case "27":
                    case "28":
                        sbExplain.AppendFormat("【{0}】增加了【{1}】", driJl[0]["text"], value);
                        break;
                    case "31":
                        string hh = value+"回合对应的年月未配置，请联系作者补全！";
                        if (DataHelper.ExplainConfig["HuiHe"].ContainsKey(value))
                        {
                            hh = DataHelper.ExplainConfig["HuiHe"][value];
                        }
                        sbExplain.AppendFormat("回到逍遥谷，{0}", hh);
                        break;
                    case "38":
                        sbExplain.AppendFormat("开启前传章节【{0}】", strMsg);
                        break;
                    case "39":
                        sbExplain.AppendFormat("时间过去【{0}】分钟", jlType);
                        break;
                    case "40":
                        sbExplain.AppendFormat("【{0}】加入队伍", DataHelper.GetValue("BattleCharacterData", "Name", "CharID", value));
                        break;
                    case "42":
                        sbExplain.AppendFormat("内功【{0}】升到10级", DataHelper.GetValue("BattleNeigong", "Name", "ID", jlType));
                        break;
                    default:
                        sbExplain.AppendFormat("弱鸡作者不理解{0}代表什么意思", rw);
                        break;
                }
                sbExplain.Append("\r\n");
            }
            return sbExplain.ToString();
        }
        #endregion

        #region 解释对话
        /// <summary>
        /// 解释对话
        /// </summary>
        /// <param name="dv">DataGridViewRow 类型的列</param>
        /// <returns></returns>
        public string ExplainTalkManager(DataGridViewRow dv)
        {
            DataHelper.ExistTable("String_table");
            DataTable dt = DataHelper.XkfyData.Tables["Config"];
            StringBuilder sbExplain1 = new StringBuilder();
            DataTable strT = DataHelper.XkfyData.Tables["String_table"];
            for (int i = 1; i < 4; i++)
            {
                string iGtype = dv.Cells["iGtype" + i].Value.ToString();
                string sGArg = dv.Cells["sGArg" + i].Value.ToString();
                string iAmount = dv.Cells["iAmount" + i].Value.ToString();
                string sStringId = dv.Cells["sStringID" + i].Value.ToString();

                if (string.IsNullOrEmpty(iGtype) || (iGtype == "0" && sGArg == "0" && iAmount == "0" && sStringId == "0"))
                    continue;

                DataRow[] driGtype = dt.Select("Type='iGtype' and Value='" + iGtype + "'");
                if (driGtype.Length == 0)
                {
                    sbExplain1.AppendFormat("iGtype{0}的值,未在配置文件中找到对应的配置！\r\n", i);
                    continue;
                }

                if (driGtype.Length > 1)
                {
                    sbExplain1.AppendFormat("iGtype{0}的配置文件有误，请联系作者！\r\n", i);
                    continue;
                }

                sbExplain1.AppendFormat("【{0}】  ", driGtype[0]["Text"]);
                DataRow[] drsGArg = null;
                switch (iGtype)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "24":
                    case "30":
                        drsGArg = dt.Select("Type='" + driGtype[0]["ChildType"].ToString() + "' and Value='" + sGArg + "'");
                        if (drsGArg.Length == 0)
                            continue;
                        string tempStr = string.Empty;
                        if (sStringId != "0")
                        {
                            tempStr = DataHelper.GetValue("String_table", "sString", "iID", sStringId);
                            sbExplain1.AppendFormat(tempStr, iAmount, drsGArg[0]["Text"].ToString(), iAmount);
                        }
                        else
                        {
                            sbExplain1.AppendFormat("{0}{1}", drsGArg[0]["Text"].ToString(), iAmount);
                        }
                        break;
                    case "6":
                        sbExplain1.AppendFormat("【{0}】对你好感增加了{1}点  ", DataHelper.GetValue("NpcData", "sNpcName", "iID", sGArg), iAmount);
                        break;
                    case "7":
                        sbExplain1.AppendFormat("获得物品【{0}】", DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", sGArg));
                        break;
                    case "13":
                        sbExplain1.AppendFormat("获得称号【{0}】", DataHelper.GetValue("TitleData", "sTitle", "iID", sGArg));
                        break;
                    case "14":
                    case "26":
                    case "29":
                        tempStr = DataHelper.GetValue("String_table", "sString", "iID", sStringId);
                        sbExplain1.AppendFormat(tempStr, iAmount);
                        break;
                    case "16":
                        sbExplain1.AppendFormat("获得套路 【{0}】", DataHelper.GetValue("RoutineData", "sRoutineName", "iRoutineID", sGArg));
                        break;
                    case "17":
                        sbExplain1.AppendFormat("获得内功 【{0}】", DataHelper.GetValue("BattleNeigong", "Name", "ID", sGArg));
                        break;
                    case "18":
                        sbExplain1.AppendFormat("开启新经历 【{0}】", DataHelper.GetValue("JourneyData", "sTip", "iID", sGArg));
                        break;
                    case "19":
                        sbExplain1.AppendFormat("【{0}】", DataHelper.GetValue("AbilityBookData", "sAbilityID", "iID", sGArg));
                        break;
                    case "34":
                        sbExplain1.AppendFormat("{0}", DataHelper.GetValue("DevelopBtnData", "xRemark", "iBtnID", sGArg));
                        break;
                    default:
                        sbExplain1.AppendFormat("弱鸡作者不理解iGtype={0}的意思", iGtype);
                        break;
                }
                sbExplain1.Append("\r\n");
            }
            return sbExplain1.ToString();
        }

        /// <summary>
        /// 解释对话
        /// </summary>
        /// <param name="dv">Datarow 类型的列</param>
        /// <returns></returns>
        public string ExplainTalkManager(DataRow dv)
        {
            DataTable dt = DataHelper.XkfyData.Tables["Config"];
            StringBuilder sbExplain1 = new StringBuilder();
            for (int i = 1; i < 4; i++)
            {
                string iGtype = dv["iGtype" + i].ToString();
                string sGArg = dv["sGArg" + i].ToString();
                string iAmount = dv["iAmount" + i].ToString();
                string sStringId = dv["sStringID" + i].ToString();

                if (string.IsNullOrEmpty(iGtype) || (iGtype == "0" && sGArg == "0" && iAmount == "0" && sStringId == "0"))
                    continue;

                DataRow[] driGtype = dt.Select("Type='iGtype' and Value='" + iGtype + "'");
                if (driGtype.Length == 0)
                {
                    sbExplain1.AppendFormat("iGtype{0}的值,未在配置文件中找到对应的配置！\r\n", i);
                    continue;
                }

                if (driGtype.Length > 1)
                {
                    sbExplain1.AppendFormat("iGtype{0}的配置文件有误，请联系作者！\r\n", i);
                    continue;
                }

                sbExplain1.AppendFormat("【{0}】  ", driGtype[0]["Text"]);
                DataRow[] drsGArg = null;
                switch (iGtype)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "24":
                    case "30":
                        drsGArg = dt.Select("Type='" + driGtype[0]["ChildType"].ToString() + "' and Value='" + sGArg + "'");
                        if (drsGArg.Length == 0)
                            continue;
                        string tempStr = string.Empty;
                        if (sStringId != "0")
                        {
                            tempStr = DataHelper.GetValue("String_table", "sString", "iID", sStringId);
                            sbExplain1.AppendFormat(tempStr, iAmount, drsGArg[0]["Text"].ToString(), iAmount);
                        }
                        else
                        {
                            sbExplain1.AppendFormat("{0}{1}", drsGArg[0]["Text"].ToString(), iAmount);
                        }
                        break;
                    case "6":
                        sbExplain1.AppendFormat("【{0}】对你好感增加了{1}点  ", DataHelper.GetNpcName(sGArg), iAmount);
                        break;
                    case "7":
                        sbExplain1.AppendFormat("获得物品【{0}】", DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", sGArg));
                        break;
                    case "13":
                        sbExplain1.AppendFormat("获得称号【{0}】", DataHelper.GetValue("TitleData", "sTitle", "iID", sGArg));
                        break;
                    case "14":
                    case "26":
                    case "29":
                        tempStr = DataHelper.GetValue("String_table", "sString", "iID", sStringId);
                        sbExplain1.AppendFormat(tempStr, iAmount);
                        break;
                    case "16":
                        sbExplain1.AppendFormat("获得套路 【{0}】", DataHelper.GetValue("RoutineData", "sRoutineName", "iRoutineID", sGArg));
                        break;
                    case "17":
                        sbExplain1.AppendFormat("获得内功 【{0}】", DataHelper.GetValue("BattleNeigong", "Name", "ID", sGArg));
                        break;
                    case "18":
                        sbExplain1.AppendFormat("开启新经历 【{0}】", DataHelper.GetValue("JourneyData", "sTip", "iID", sGArg));
                        break;
                    case "19":
                        sbExplain1.AppendFormat("【{0}】", DataHelper.GetValue("AbilityBookData", "sAbilityID", "iID", sGArg));
                        break;
                    case "34":
                        sbExplain1.AppendFormat("{0}", DataHelper.GetValue("DevelopBtnData", "xRemark", "iBtnID", sGArg));
                        break;
                    default:
                        sbExplain1.AppendFormat("弱鸡作者不理解iGtype={0}的意思", iGtype);
                        break;
                }
                sbExplain1.Append("\r\n");
            }
            return sbExplain1.ToString();
        }
        #endregion

        #region 解释游戏事件
        /// <summary>
        /// 解释游戏事件
        /// </summary>
        /// <param name="striCondition"></param>
        /// <param name="striType"></param>
        /// <param name="striArg1"></param>
        /// <param name="striArg2"></param>
        /// <returns></returns>
        public string[] ExplainDevelopQuest(string[] striCondition,string[] striType, string[] striArg1, string[] striArg2)
        {
            string[] rtValue = new string[2];

            string explain = "";
            DataTable dt = DataHelper.XkfyData.Tables["Config"];
            Dictionary<string, string> diciType = DataHelper.ExplainConfig["DevelopQuest"];
            

            if (!diciType.ContainsKey(striType[0]))
            {
                explain = string.Format("弱鸡作者不明白{0}代表的意思", striType[0]);
            }
            else
            {
                explain = diciType[striType[0]];
            }

            
            string explain1 = "";
            StringBuilder sbExplain1 = new StringBuilder();
            if (striCondition.Length == striArg1.Length && striArg1.Length == striArg2.Length)
            {

                for (int i = 0; i < striCondition.Length; i++)
                {
                    string iCondition = striCondition[i];
                    string iArg1 = striArg1[i];
                    string iArg2 = striArg2[i];
                    DataRow[] drCon = dt.Select("Type='isNull' and value='" + iCondition + "'");
                    if (drCon.Length == 0)
                        continue;
                    explain1 += drCon[0]["Text"].ToString();
                    //sbExplain1.Append(drCon[0]["Text"].ToString() + "  ");
                    switch (striCondition[i])
                    {
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                            DataRow drAgr1 = dt.Select("Type='" + drCon[0]["ChildType"] + "' and value='" + iArg1 + "'")[0];
                            sbExplain1.AppendFormat("{0}:{1}  ", drAgr1["Text"].ToString(), striArg2[i]);
                            break;
                        case "7":
                            sbExplain1.AppendFormat("最早触发回合:{0} 最晚触发回合:{1}  ", DataHelper.GetHuiHe(striArg1[i]), DataHelper.GetHuiHe(striArg2[i]));
                            break;
                        case "2":
                            sbExplain1.AppendFormat("{1}好感度:{0}  ", iArg2, DataHelper.GetValue("NpcData", "sNpcName", "iID", iArg1));
                            break;
                        case "9":
                        case "14":
                        case "13":
                        case "113":
                            sbExplain1.AppendFormat("{0}【{1}】  ", drCon[0]["Text"], iArg1);
                            break;
                        case "8":
                        case "10":
                            sbExplain1.AppendFormat("{0}【{1}】  ", drCon[0]["Text"], iArg2);
                            break;
                        case "11":
                            sbExplain1.AppendFormat("拥有物品【{0}】  ", DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", iArg1));
                            break;
                        default:
                            sbExplain1.AppendFormat("弱鸡作者不明白【{0}】的意思  ", striCondition[i]);
                            break;
                    }

                }
                rtValue[0] = "触发地点 : " + explain;
                rtValue[1] = "触发条件 : " + sbExplain1.ToString();
            }
            else
            {
                MessageBox.Show("数据格式有错误,触发条件和值不匹配");
            }

            return rtValue;
        }
        #endregion

        #region 解释内功代码
        public string ExplainNeiGong(string id)
        {
            DataRow[] row = DataHelper.XkfyData.Tables["BattleNeigong_D"].Select("ID='" + id + "'");
            StringBuilder sbExplain = new StringBuilder();

            foreach (DataRow item in row)
            {
                string explain = "";
                string tempExplain = string.Empty;
                string strAccumulate = string.Empty;

                string accumulate = item["Accumulate"].ToString();
                string effectType = item["EffectType"].ToString();
                string value1 = item["value1"].ToString();
                string value2 = item["value2"].ToString();

                //通过key从Dictionary读取出对应的Value
                if (DataHelper.SelItem["BattleNeigong.EffectType"].ContainsKey(effectType))
                    tempExplain = DataHelper.SelItem["BattleNeigong.EffectType"][effectType];
                else
                    tempExplain = "";

                //通过key从Dictionary读取出对应的Value
                //if (DataHelper.selItem["Accumulate"].ContainsKey(item["Accumulate"].ToString()))
                //    tempExplain1 = DataHelper.selItem["Accumulate"][item["Accumulate"].ToString()];
                //else
                //    tempExplain1 = "";

                //如果前四项为0，代表没有效果
                if (effectType == "" || (effectType == "0" && accumulate == "0" && value1 == "0" && value2 == "0"))
                {
                    continue;
                }
                string percent = "点";
                if (item["percent"].ToString() == "1")
                    percent = "%";


                string conditionId = "";
                switch (accumulate)
                {
                    case "0":
                        strAccumulate = "固定增加";
                        break;
                    case "1":
                        strAccumulate = "累进增加";
                        break;
                    case "4":
                        strAccumulate = "";
                        break;
                    case "5":
                        strAccumulate = string.Format("提高周遭{0}格友军【{1}】{2}{3}", item["ValueLimit"].ToString(), tempExplain, value1, percent);
                        break;
                    case "7":
                        conditionId = (value1 == "0" || string.IsNullOrEmpty(value1)) ? value2 : value1;
                        strAccumulate = string.Format("发动【{0}】后，触发【{1}】", tempExplain, GetCondition(conditionId));
                        sbExplain.AppendFormat("{0}\r\n", strAccumulate);
                        continue;
                    case "9":
                        strAccumulate = string.Format("降低周遭{0}格敌军【{1}】{2}{3}", item["ValueLimit"].ToString(), tempExplain, value1, percent);
                        sbExplain.AppendFormat("{0}\r\n", strAccumulate);
                        continue;
                    case "10":
                        conditionId = (value1 == "0" || string.IsNullOrEmpty(value1)) ? value2 : value1;
                        string strGd = (value1 == "0" || string.IsNullOrEmpty(value1)) ? "高于" : "低于";
                        strAccumulate = string.Format("【{0}】{4}{1}{2}触发【{3}】", tempExplain, item["ValueLimit"].ToString(), percent, GetCondition(conditionId), strGd);
                        sbExplain.AppendFormat("{0}\r\n", strAccumulate);
                        continue;
                }

                string tempa = item["ValueLimit"].ToString();
                tempa = tempa != "0" ? tempa + "个" : "所有";
                switch (item["EffectType"].ToString())
                {
                    case "0":
                    case "1":
                        strAccumulate = "每回合恢复";
                        explain = strAccumulate + item["value1"].ToString() + percent + "【" + tempExplain + "】  最多" + item["ValueLimit"].ToString() + percent;
                        break;
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                        if (accumulate == "5")
                        {
                            explain = strAccumulate;
                            break;
                        }
                        explain = strAccumulate + item["value1"].ToString() + percent + "【" + tempExplain + "】  最多" + item["ValueLimit"].ToString() + percent;
                        break;
                    case "8":
                    case "24":
                    case "25":
                    case "29":
                    case "30":
                        explain = string.Format("【{0}】", tempExplain);
                        break;
                    case "9":
                        explain = "移动范围 +" + item["ValueLimit"].ToString() + "";
                        break;
                    case "15":
                    case "16":
                    case "17":
                    case "18":
                        explain = string.Format("【{0}】{1}{2}  最多{3}{4}", tempExplain, value1, percent, item["ValueLimit"].ToString(), percent);
                        break;
                    case "20":
                        explain = string.Format("每回合解除【{0}】负面状态", tempa);
                        break;
                    case "22":
                        explain = string.Format("行动等级 +{0} 神行", item["ValueLimit"].ToString());
                        break;
                    case "23":
                        explain = "连斩：击杀敌人後可再行动";
                        break;
                    case "26":
                        strAccumulate = string.Format("保护周遭{0}格同伴", item["ValueLimit"].ToString());
                        sbExplain.Append(strAccumulate);
                        break;
                    case "31":
                        explain = "毒体：百毒不侵";
                        break;
                    case "32":
                        sbExplain.AppendFormat("减少{0}{1} - {2}{3}伤害",value1, percent,value2, percent);
                        break;
                    default:
                        sbExplain.AppendFormat("弱鸡作者不明白EffectType={0} 是什么意思！", item["EffectType"].ToString());
                        break;

                }
                sbExplain.Append(explain);
                sbExplain.Append("\r\n");
            }
            return sbExplain.ToString();
        }

        private string GetCondition(string conid)
        {
            return DataHelper.GetValue("BattleCondition", "CondName", "ConditionID", conid);
        }
        #endregion

        #region 解释招式伤害范围
        public string ExplainRoutineData(DataGridViewRow dv)
        {
            DataHelper.ExistTable("BattleAbility");
            StringBuilder sbExplain = new StringBuilder();
            string where = "SkillNo = '" + dv.Cells["iLinkSkill1"].Value + "'";
            where += "or SkillNo = '" + dv.Cells["iLinkSkill2"].Value + "'";
            where += "or SkillNo = '" + dv.Cells["iLinkSkill3"].Value + "'";

            DataRow[] row = DataHelper.XkfyData.Tables["BattleAbility"].Select(where);
            int index = 1;
            foreach (DataRow dr in row)
            {
                sbExplain.AppendFormat("第{0}招:{1}  ", index, dr["skillname"]);
                sbExplain.AppendFormat("最小伤害{0}  ", dr["mindamage"]);
                sbExplain.AppendFormat("最大伤害：{0}  ", dr["maxdamage"]);
                sbExplain.AppendFormat("内力消耗：{0}  ", dr["useap"]);
                sbExplain.AppendFormat("CD时间：{0}  ", dr["cd"]);
                string ttName = "";
                string targetarea = dr["targetarea"].ToString();
                switch (targetarea)
                {
                    case "0":
                        ttName = "单体";
                        break;
                    case "2":
                        ttName = "自身";
                        break;
                    case "1":
                        ttName = "直线";
                        break;
                    case "3":
                        ttName = "扇形";
                        break;
                }

                string[] condition = dr["condition"].ToString().Split(',');
                string buffName = string.Empty;
                for (int i = 0; i < condition.Length; i++)
                {
                    if (condition[i] == "" || condition[i] == "0")
                        continue;
                    buffName += DataHelper.GetValue("BattleCondition", "CondName", "ConditionID", condition[i]) + ",";
                }

                sbExplain.AppendFormat("攻击方式：{0} 攻击距离:{1} 溅射：{2}  ", ttName, dr["range"], dr["aoe"]);
                sbExplain.AppendFormat("附加效果：{0}", buffName.TrimEnd(new char[] { ',' }));
                sbExplain.Append("\r\n\r\n");
                index++;
            }
            return sbExplain.ToString();
        }
        #endregion

        #region 解释大地图事件
        public string ExplainQuestDataCon(string[] sOConditions, DataTable dt)
        {
            Dictionary<string, string> dicCon = null;
            if (!DataHelper.ExplainConfig.ContainsKey("QuestDataCon"))
            {
                string path = Application.StartupPath + "/工具配置文件/";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<SelectItem> dicList = XmlHelper.XmlDeserializeFromFile<List<SelectItem>>(path + "QuestDataManager.xml", Encoding.UTF8);
                foreach (SelectItem item in dicList)
                {
                    dic.Add(item.Value, item.Text);
                }
                DataHelper.ExplainConfig.Add("QuestDataCon", dic);
                dicCon = dic;
            }
            else
            {
                dicCon = DataHelper.ExplainConfig["QuestDataCon"];
            }

            StringBuilder sbExplain = new StringBuilder();
            foreach (string rw in sOConditions)
            {
                string[] reward = rw.Replace(")", "").Replace("(", "").Split(',');
                if (reward.Length < 3)
                {
                    sbExplain.Append("本行数据有误,请检查!");
                    continue;
                }

                string bigClass = reward[0];
                string smallClass = reward[1];
                string value = reward[2];
                switch (bigClass)
                {
                    case "1":
                    case "2":
                    case "4":
                    case "5":
                    case "12":
                        sbExplain.AppendFormat("{0}{1}", DataHelper.ExplainConfig[dicCon[bigClass]][smallClass], value);
                        break;
                    case "6":
                        sbExplain.AppendFormat("【{0}】好感达到{1}点  ", DataHelper.GetValue("NpcData", "sNpcName", "iID", smallClass), value);
                        break;
                    case "7":
                        sbExplain.AppendFormat("拥有物品【{0}】*{1}", DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", smallClass), value);
                        break;
                    case "9":
                        sbExplain.AppendFormat("完成前置剧情 {0}", smallClass);
                        break;
                    case "10":
                        sbExplain.AppendFormat("未完成前置剧情 {0}",smallClass);
                        break;
                    case "11":
                        sbExplain.AppendFormat("拥有金钱{0}",value);
                        break;
                }
                sbExplain.Append("\r\n");
            }
            return sbExplain.ToString();
        }

        public string ExplainQuestDataFinish(string[] sFinish)
        {
            StringBuilder sbExplain = new StringBuilder();
            foreach (string rw in sFinish)
            {
                string[] reward = rw.Replace(")", "").Replace("(", "").Split(',');
                if (reward.Length < 3)
                {
                    sbExplain.Append("本行数据有误,请检查!");
                    continue;
                }

                if (reward[0] == "1")
                    continue;

                string bigClass = reward[0];
                string smallClass = reward[1];
                string value = reward[2];
                switch (bigClass)
                {
                    case "2":
                        sbExplain.AppendFormat("失去物品【{0}】", DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", smallClass));
                        break;
                    case "3":
                        sbExplain.AppendFormat("金钱减少【{0}】", value);
                        break;
                    case "4":
                        sbExplain.AppendFormat("人物作出动作【{0}】", value);
                        break;
                    case "6":
                        sbExplain.AppendFormat("触发动画【{0}】", value);
                        break;
                    case "9":
                        sbExplain.AppendFormat("前置剧情【{0}】", value);
                        break;
                }
                sbExplain.Append("\r\n");
            }
            return sbExplain.ToString();
        }
        #endregion

        #region 解释战斗补充文件
        public string ExplainBattleSchedule(DataGridViewRow dr)
        {
            StringBuilder sb = new StringBuilder();
            int triggerType = Convert.ToInt32(dr.Cells["triggerType"].Value);
            string triggerData = dr.Cells["TriggerData"].Value.ToString();

            string npcName = string.Empty;
            if (triggerType != 7&&triggerType != 8 && triggerType != 9 && triggerType != 0 && triggerType != 6)
            {
                npcName = DataHelper.GetNpcName(triggerData);
            }

            string zhenYin = string.Empty;
            sb.Append("【触发时间】");
            switch (triggerType)
            {
                case 0:
                    sb.AppendFormat("战斗开始");
                    break;
                case 1:
                    sb.AppendFormat("{0}攻击时", npcName);
                    break;
                case 2:
                    sb.AppendFormat("{0}攻击后", npcName);
                    break;
                case 3:
                    sb.AppendFormat("{0}受伤前", npcName);
                    break;
                case 4:
                    sb.AppendFormat("{0}受伤后", npcName);
                    break;
                case 5:
                    sb.AppendFormat("{0}死亡后", npcName);
                    break;
                case 6:
                    sb.AppendFormat("第{0}回合", triggerData);
                    break;
                case 7:
                    sb.AppendFormat("战斗胜利");
                    break;
                case 8:
                    sb.AppendFormat("战斗失败");
                    break;
                case 9:
                    zhenYin = string.Empty;
                    if (triggerData == "0")
                        zhenYin = "我方阵营";
                    if (triggerData == "2")
                        zhenYin = "中立阵营";
                    if (triggerData == "1")
                        zhenYin = "敌对阵营";
                    sb.AppendFormat("{0}全阵亡", zhenYin);
                    break;
            }
            sb.Append("\r\n");

            int requireType = Convert.ToInt32(dr.Cells["RequireType"].Value);
            int requireEqual = Convert.ToInt32(dr.Cells["RequireEqual"].Value); 
            string value1 = dr.Cells["iRequireValue1"].Value.ToString();
            string value2 = dr.Cells["iRequireValue2"].Value.ToString();
            string panDuan = string.Empty;
            switch (requireEqual)
            {
                case 0:
                    panDuan = "等于";
                    break;
                case 1:
                    panDuan = "大于";
                    break;
                case 2:
                    panDuan = "小于";
                    break;
                case 3:
                    panDuan = "大于等于";
                    break;
                case 4:
                    panDuan = "小于等于";
                    break;
                case 5:
                    panDuan = "不等于";
                    break;
            }

            sb.Append("【判定条件】");
            switch (requireType)
            {
                case 0:
                    sb.AppendFormat("无条件");
                    break;
                case 1:
                    sb.AppendFormat("血量{0}{1}", panDuan, value1=="0"?value2+"%":value1);
                    break;
                case 2:
                    sb.AppendFormat("Npc好感{0}", npcName);
                    break;
                case 3:
                    sb.AppendFormat("仇恨值{0}", npcName);
                    break;
                case 4:
                    sb.AppendFormat("{0}阵营,人物数量{1}{2}", value1, panDuan, value2);
                    break;
                case 5:
                    sb.AppendFormat("攻击目标{0}", DataHelper.GetNpcName(value1));
                    break;
                case 6:
                    sb.AppendFormat("被{0}攻击", DataHelper.GetNpcName(value1));
                    break;
                case 7:
                    sb.AppendFormat("进行中任务");
                    break;
                case 8:
                    sb.AppendFormat("完成任务");
                    break;
                case 9:
                    sb.AppendFormat("养成");
                    break;
            }

            sb.Append("\r\n");
            int triggerEvent = Convert.ToInt32(dr.Cells["TriggerEvent"].Value);
            string teArg1 = dr.Cells["iTriggerEventArg1"].Value.ToString();
            string teArg2 = dr.Cells["iTriggerEventArg2"].Value.ToString();
            string teArg3 = dr.Cells["iTriggerEventArg3"].Value.ToString();

            if (teArg2 == "0")
                zhenYin = "我方阵营";
            if (teArg2 == "2")
                zhenYin = "中立阵营";
            if (teArg2 == "1")
                zhenYin = "敌对阵营";

            switch (triggerEvent)
            {
                case 0:
                    sb.Append("【显示对话】"+ DataHelper.GetMapTalk(teArg1, teArg2));
                    break;
                case 1:
                    sb.AppendFormat("{1}在位置{0} 增援", teArg3, zhenYin);
                    npcName = DataHelper.GetNpcName(teArg1);
                    sb.Append(npcName);
                    break;
                case 2:
                    sb.AppendFormat("转换【{0}】到{1}", DataHelper.GetNpcName(teArg1), zhenYin);
                    break;
                case 3:
                    string npc1 = DataHelper.GetNpcName(teArg1);
                    string jn = DataHelper.GetRoutineName(teArg2);
                    string npc2 = DataHelper.GetNpcName(teArg3);
                    sb.AppendFormat("{0} 向 {1}发动【{2}】", npc1, npc2, jn);
                    break;
                case 4:
                    npc1 = DataHelper.GetNpcName(teArg1);
                    sb.AppendFormat("{0}{1}获得状态【{2}】", zhenYin, npc1, DataHelper.GetConditionName(teArg2));
                    break;
                case 5:
                    sb.AppendFormat("获得进行中的任务：{0}", teArg1);
                    break;
                case 6:
                    sb.AppendFormat("获得已完成的任务：{0}", teArg1);
                    break;
                case 7:
                    sb.AppendFormat("{0}离开战场", DataHelper.GetNpcName(teArg1));
                    break;
                case 8:
                    sb.Append("播放背景音乐："+ teArg1);
                    break;
                case 9:
                    sb.Append("战斗结束,获胜阵营：" + teArg1);
                    break;
                case 10:
                    sb.AppendFormat("转换AI,具体什么意思,有待实验,如果有知情者,可联系作者！");
                    break;
            }


            return sb.ToString();
        }
        #endregion

        #region 把Map文件 读取到DataTable里去
        /// <summary>
        /// 把Map文件 读取到DataTable里去
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="dtName">表名</param>
        /// <param name="xmlName">XML文件名称</param>
        public static void ReadMapData(string path, string dtName,string xmlName)
        {
            string xmlPath = DataHelper.ToolFilesPath + "\\" + dtName + ".xml";
            //如果XML文件存在读取XML文件
            if (File.Exists(xmlPath))
            {
                DataHelper.MapData.Tables.Add(dtName);
                DataHelper.MapData.Tables[dtName].ReadXml(xmlPath);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.TableName = dtName;
                string columnPath = Path.Combine(ToolsHelper.ExplainPath, xmlName + ".xml");
                if (File.Exists(columnPath))
                {
                    List<TableExplain> teList = DataHelper.ReadXmlToList<TableExplain>(columnPath);
                    foreach (TableExplain te in teList)
                    {
                        if (!string.IsNullOrEmpty(te.Column))
                        {
                            dt.Columns.Add(te.Column);
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

                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        string readStr = sr.ReadLine();//读取一行数据
                                                       //string[] strs = readStr.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                        string[] strs = readStr.Split('\t');//将读取的字符串按"制表符/t“和””“分割成数组

                        if (strs.Length <= 0)
                            continue;

                        if (strs[0] == "#iNpcID")
                            continue;

                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < strs.Length; i++)
                        {
                            if (i >= dt.Columns.Count)
                            {
                                //readError.AppendFormat("文件{0}.txt 第{1} 行数据格式有误,数据长度超越了表头,只按表头长度来处理数据\r\n", dt.TableName, index + 1);
                                break;
                            }
                            dr[i] = strs[i];
                        }
                        dt.Rows.Add(dr);
                    }
                    
                }
                DataHelper.MapData.Tables.Add(dt);
            }
        }
        #endregion

        #region 把Map文件 读取到DataTable里去
        /// <summary>
        /// 把Map文件 读取到DataTable里去
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="dt">表名</param>
        public static void ReadNpcData(string path, string dtName)
        {
            string xmlPath = DataHelper.ToolFilesPath + "\\" + dtName + ".xml";
            //如果XML文件存在读取XML文件
            if (File.Exists(xmlPath))
            {
                DataHelper.MapData.Tables.Add(dtName);
                DataHelper.MapData.Tables[dtName].ReadXml(xmlPath);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.TableName = dtName;
                dt.Columns.Add("iNpcID");
                dt.Columns.Add("sConduct");
                dt.Columns.Add("iTime");
                dt.Columns.Add("rowState");
                dt.Columns.Add("npcName");
                dt.Columns.Add("indexSn", typeof(int));

                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        string readStr = sr.ReadLine();//读取一行数据
                                                       //string[] strs = readStr.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                        string[] strs = readStr.Split('\t');//将读取的字符串按"制表符/t“和””“分割成数组

                        if (strs.Length <= 0)
                            continue;

                        if (strs[0] == "#iNpcID")
                            continue;

                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < strs.Length; i++)
                        {
                            if (i >= dt.Columns.Count)
                            {
                                //readError.AppendFormat("文件{0}.txt 第{1} 行数据格式有误,数据长度超越了表头,只按表头长度来处理数据\r\n", dt.TableName, index + 1);
                                break;
                            }
                            dr[i] = strs[i];
                        }
                        dt.Rows.Add(dr);
                    }
                }
                DataHelper.XkfyData.Tables.Add(dt);
            }
        }
        #endregion

        
        #region 清除窗体文本框的值
        public void ClearData(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
            }
        }
        #endregion

        #region 将容器里不包括下拉框的值赋给Dr
        /// <summary>
        /// 将容器里不包括下拉框的值赋给Dr
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dr"></param>
        public void CopyRowToData(Control control, DataGridViewRow dr)
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
            }
        }

        /// <summary>
        /// 将容器里不包括下拉框的值赋给Dr
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dr"></param>
        public void CopyRowToData(Control control, DataRow dr)
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

                if (c is TextBox)
                {
                    dr[rowName] = ((TextBox)c).Text;
                }
            }
        }
        #endregion

        #region 搜索文件夹中的文件
        /// <summary>
        /// //搜索文件夹中的文件
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        public void GetAllFile(DirectoryInfo dir, Dictionary<string, string> dic)
        {
            FileInfo[] allFile = dir.GetFiles();
            foreach (FileInfo fi in allFile)
            {
                if (dic.ContainsKey(Path.GetFileNameWithoutExtension(fi.FullName)))
                    continue;
                dic.Add(Path.GetFileNameWithoutExtension(fi.FullName), fi.FullName);
            }
            
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                GetAllFile(d, dic);
            }
        }
        #endregion

        #region 把DataSet里的数据生成到xml文件中去
        public void BuildDataSetXml(string tbName)
        {
            string path = DataHelper.ToolFilesPath + "\\" + tbName + ".xml";
            //检查是否存在目的目录  
            if (!Directory.Exists(DataHelper.ToolFilesPath))
            {
                Directory.CreateDirectory(DataHelper.ToolFilesPath);
            }
            DataHelper.XkfyData.Tables[tbName].WriteXml(path, XmlWriteMode.WriteSchema);
        }

        /// <summary>
        /// 生成数据到XML
        /// </summary>
        /// <param name="tbName"></param>
        public void BuildDataSetXmlMap(string tbName)
        {
            string path = DataHelper.ToolFilesPath + "\\" + tbName + ".xml";
            //检查是否存在目的目录  
            if (!Directory.Exists(DataHelper.ToolFilesPath))
            {
                Directory.CreateDirectory(DataHelper.ToolFilesPath);
            }
            DataHelper.MapData.Tables[tbName].WriteXml(path, XmlWriteMode.WriteSchema);
        }

        public void ReadDataSetXml(string tbName)
        {
            string path = DataHelper.ToolFilesPath + "\\" + tbName + ".xml";
            //检查是否存在目的目录  
            if (!Directory.Exists(DataHelper.ToolFilesPath))
            {
                Directory.CreateDirectory(DataHelper.ToolFilesPath);
            }
            DataHelper.XkfyData.Tables[tbName].WriteXml(path, XmlWriteMode.WriteSchema);
        }
        #endregion

        public bool CheckFormIsOpen(string forms)
        {
            bool bResult = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == forms)
                {
                    bResult = true;
                    break;
                }
            }
            return bResult;
        }

        /// <summary>
        /// 通用字符串
        /// </summary>
        /// <param name="id">显示ID的TextBox</param>
        /// <param name="name">显示Text的TextBox</param>
        public void GetStringTable(TextBox id, TextBox name)
        {
            string[] row = new string[3] { "@iID", "sString", "xUse" };
            RadioList rl = new RadioList("String_table", row, id, name, "1");
            rl.ShowDialog();
        }
        
    }
}
