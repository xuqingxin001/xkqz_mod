using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class Almighty : DockContent
    {
        private string _tbName = null;
        private string _fileType = null;
        private string _editForm = string.Empty;
        private MyConfig _myConfig;

        private ToolsHelper _tl = new ToolsHelper();
        public Almighty(string tbName,string fileType)
        {
            this._tbName = tbName;
            this._fileType = fileType;
            InitializeComponent();
        }

        private void Almighty_Load(object sender, EventArgs e)
        {
            if (DateTime.Now.Second%2 == 0)
            {
                lblTag.Text = "点击序号，选中整行后，在不使用查询的情况下，可以使用方向键↑ ↓调整数据的顺序";
            }
            else
            {
                lblTag.Text = "如果当前表格不小心修改错了,可以通过点击顶部菜单【重新加载】按钮重新加载文件！";
            }

            if (DataHelper.ReadError.ToString() != "")
            {
                timer1.Start();
                gbError.Visible = true;
                txtError.Text = DataHelper.ReadError.ToString();
            }

            dg1.AllowUserToAddRows = false;
            try
            {
                string path = string.Empty;
                DataTable dt;

                string haveHelperColumn = string.Empty;
                if (DataHelper.FormConfig.ContainsKey(_tbName))
                {
                    _myConfig = DataHelper.FormConfig[_tbName];
                    _editForm = _myConfig.EditFormName;

                    if (_editForm != "" && _editForm != "0")
                    {
                        btnAdd.Visible = true;
                        btnCopyAdd.Visible = true;
                    }
                }

                //绑定数据源
                if (_fileType == "other")
                {
                    BulidSn(DataHelper.XkfyData.Tables[_tbName]);
                    dt = DataHelper.XkfyData.Tables[_tbName];
                }
                else
                {
                    BulidSn(DataHelper.MapData.Tables[_tbName]);
                    dt = DataHelper.MapData.Tables[_tbName];
                }
                dg1.DataSource = dt.DefaultView;


                Dictionary<string, TableExplain> dlc = new Dictionary<string, TableExplain>();
                if (_fileType == "MapInfo")
                {
                    path = Path.Combine(ToolsHelper.ExplainPath, "MapInfo.xml");
                }
                else if (_fileType == "NpcProduct")
                {
                    path = Path.Combine(ToolsHelper.ExplainPath, "NpcProduct.xml");
                }
                else
                {
                    path = Path.Combine(ToolsHelper.ExplainPath, _tbName + ".xml");
                }
                
                if (File.Exists(path))
                {
                    int top = 10;
                    int left = 7;
                    int width = 122;
                    int columnIndex = 0;

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
                        #region 生成查询控件
                        if (!string.IsNullOrEmpty(item.IsSelect) && item.IsSelect == "1")
                        {
                            if (columnIndex == 3)
                            {
                                top = 40;
                                left = 7;
                            }
                            Label tips = new Label();
                            tips.Top = top + 4;
                            tips.Left = left;
                            tips.Width = width;
                            left += tips.Width;

                            TextBox tb = new TextBox();
                            if (!string.IsNullOrEmpty(item.Column))
                            {
                                tb.Tag = item.Column;
                            }
                            else
                            {
                                tb.Tag = item.ToolsColumn;
                            }

                            tb.Top = top;
                            tb.Left = left;
                            tb.Width = width;

                            left += tb.Width + 15;
                            gbQueryCon.Controls.Add(tips);
                            gbQueryCon.Controls.Add(tb);


                            //把lable文字修改为中文显示
                            if (!string.IsNullOrEmpty(item.Text))
                            {
                                tips.Text = item.Text;
                            }
                            else if (!string.IsNullOrEmpty(item.Column))
                            {
                                tips.Text = item.Column;
                            }
                            else
                            {
                                tips.Text = item.ToolsColumn;
                            }
                            toolTip.SetToolTip(tips, item.Explain);
                            columnIndex++;
                        }
                        #endregion
                    }

                    CheckBox ck = new CheckBox();
                    ck.Tag = "rowState";
                    ck.Top = top;
                    ck.Left = left;
                    ck.Width = width;
                    ck.Text = "只显示我修改的";
                    gbQueryCon.Controls.Add(ck);
                }
                else
                {
                    MessageBox.Show("缺少列配置文件,文件路径"+ path);
                }


                foreach (DataGridViewColumn dgvCol in dg1.Columns)
                {
                    if (dlc.ContainsKey(dgvCol.Name) && dlc[dgvCol.Name] != null)
                    {
                        TableExplain te = dlc[dgvCol.Name];
                        if (!string.IsNullOrEmpty(te.Text))
                        {
                            dgvCol.HeaderText = te.Text;
                        }
                        else if (!string.IsNullOrEmpty(te.Column))
                        {
                            dgvCol.HeaderText = te.Column;
                        }
                        else
                        {
                            dgvCol.HeaderText = te.ToolsColumn;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dg1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dg1.RowHeadersWidth - 4,
                e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dg1.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dg1.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
            string rowState = "";

            if (dg1.Rows[e.RowIndex].Cells["rowState"].Value != null)
            {
                rowState = dg1.Rows[e.RowIndex].Cells["rowState"].Value.ToString();
                if (rowState == "0")
                {
                    dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PowderBlue;
                }
                else if (rowState == "1")
                {
                    dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
        }

        private void BulidSn(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["indexSn"] = i;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string text = string.Empty;
                string tag = string.Empty;
                string where = "1 = 1";

                foreach (Control c in gbQueryCon.Controls)
                {
                    if (c.Tag == null)
                    {
                        continue;
                    }

                    if (c is TextBox)
                    {
                        tag = ((TextBox)c).Tag.ToString();
                        text = ((TextBox)c).Text;

                        if (!string.IsNullOrEmpty(text))
                        {
                            where += " and " + tag.Replace("#","@") + " like '%" + text + "%' ";
                        }
                    }else if (c is CheckBox)
                    {
                        tag = ((CheckBox)c).Tag.ToString();
                        if (((CheckBox)c).Checked)
                        {
                            where += " and (rowState = 1 or rowState = 0)";
                        }
                    }
                }

                DataView dv = dg1.DataSource as DataView;
                dv.RowFilter = where;
                this.dg1.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 解释战斗信息
        private void ExplainBattleArea(DataGridViewRow dv)
        {
            StringBuilder sbExplain = new StringBuilder();
            StringBuilder sbExplainRed = new StringBuilder();
            StringBuilder slExplain = new StringBuilder();
            StringBuilder faleExplain = new StringBuilder();
            
            string[] sMustJoinStaff = dv.Cells["sMustJoinStaff$2"].Value.ToString().Split('*');
            int i = 0;
            foreach (string str in sMustJoinStaff)
            {
                if (string.IsNullOrEmpty(str.Trim()))
                {
                    continue;
                }
                if (str == "(0,0,0)")
                {
                    sbExplain.AppendFormat("自定义上场人物{0}\r\n",i);
                    i++;
                    continue;
                }
                string[] mustJoinStaff = str.Replace(")", "").Replace("(", "").Split(',');
                string huihe = mustJoinStaff[0];
                string npcid = mustJoinStaff[1];
                string zhenyin = mustJoinStaff[2];

                string npcName = DataHelper.GetNpcName(npcid);
                sbExplain.AppendFormat("我方上场【{0}】\r\n", npcName);
            }

            sMustJoinStaff = dv.Cells["sJoinStaff$3"].Value.ToString().Split('*');
            i = 0;
            foreach (string str in sMustJoinStaff)
            {
                if (string.IsNullOrEmpty(str.Trim()))
                {
                    continue;
                }
                if (str == "(0,0,0)")
                {
                    sbExplain.AppendFormat("自定义上场人物{0}\r\n", i);
                    continue;
                }
                string[] mustJoinStaff = str.Replace(")", "").Replace("(", "").Split(',');
                string huihe = mustJoinStaff[0];
                string npcid = mustJoinStaff[1];
                string zhenyin = mustJoinStaff[2];

                string npcName = DataHelper.GetNpcName(npcid);
                sbExplainRed.AppendFormat("敌方上场【{0}】\r\n", npcName);
            }

            
            //胜利条件
            i = 0;
            string[] iVictory = dv.Cells["iVictory$4"].Value.ToString().Split('*');
            foreach (string str in iVictory)
            {
                if (i != 0)
                {
                    slExplain.Append(" 或 ");
                }
                i++;

                if (str == "(0,0,0)")
                {
                    slExplain.Append("对方全部阵亡");
                    continue;
                }

                string[] victory = str.Replace(")", "").Replace("(", "").Split(',');

                if ((victory[0] == "1" || victory[0] == "0") && victory[1] != "0" && victory[2] == "0")
                {
                    slExplain.AppendFormat("坚持{0}回合！", victory[1]);
                }
                else if (victory[0] == "2" && victory[1] != "0" && victory[2] == "0")
                {
                    slExplain.AppendFormat("击败{0}！", DataHelper.GetNpcName(victory[1]));
                }
                else
                {
                    slExplain.AppendFormat("弱鸡作者不明白{0}的意思", str);
                }
            }

            //失败条件
            i = 0;
            string[] iFale = dv.Cells["iFale$5"].Value.ToString().Split('*');
            foreach (string str in iFale)
            {
                if (i != 0)
                {
                    faleExplain.Append(" 或 ");
                }
                i++;

                if (str == "(0,0,0)")
                {
                    faleExplain.Append("我方全部阵亡\r\n");
                    continue;
                }

                string[] victory = str.Replace(")", "").Replace("(", "").Split(',');
                if ((victory[0] == "1" || victory[0] == "0") && victory[1] != "0" && victory[2] == "0")
                {
                    faleExplain.AppendFormat("对方坚持了{0}回合！", victory[1]);
                }
                else if (victory[0] == "2" && victory[1] != "0" && victory[2] == "0")
                {
                    faleExplain.AppendFormat("{0}阵亡！", DataHelper.GetNpcName(victory[1]));
                }
                else
                {
                    faleExplain.AppendFormat("弱鸡作者不明白{0}的意思", str);
                }
            }
            lblExplainRed.Text = sbExplainRed.ToString();
            lblSl.Text = slExplain.ToString();
            txtExplain.Text = sbExplain.ToString();
            lblFale.Text = faleExplain.ToString();

            DataRow[] drBa = DataHelper.XkfyData.Tables["RewardData"].Select($"TxzColumn1='{dv.Cells["sReward$6"].Value}'");

            if (drBa.Length == 0 || drBa.Length > 1)
            {
                txtWin.Text = "数据有误,无法解析iRID="+dv.Cells["sReward$6"].Value.ToString();
                return;
            }

            string[] sRewardData = drBa[0]["TxzColumn2"].ToString().Split('*');

            txtWin.Text = ExplainHelper.ExplainRewardData(sRewardData);

            drBa = DataHelper.XkfyData.Tables["RewardData"].Select($"TxzColumn1='{dv.Cells["sReward$7"].Value}'");
            sRewardData = drBa[0]["TxzColumn2"].ToString().Split('*');

            textBox1.Text = ExplainHelper.ExplainRewardData(sRewardData);
        }
        #endregion

        private void tsmInsertRow_Click(object sender, EventArgs e)
        {
            if (dg1.CurrentCell == null)
                return;
            DataRow dr = (dg1.DataSource as DataView).Table.NewRow();
            dr["rowState"] = "1";
            (dg1.DataSource as DataView).Table.Rows.InsertAt(dr, dg1.CurrentCell.RowIndex);
        }

        private void dg1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.DefaultCellStyle.BackColor = Color.MistyRose;
        }

        private void tsmCopyRow_Click(object sender, EventArgs e)
        {
            DataRow dr = (dg1.DataSource as DataView).Table.NewRow();
            for (int i = 0; i < dg1.Columns.Count; i++)
            {
                dr[dg1.Columns[i].Name] = dg1.CurrentRow.Cells[dg1.Columns[i].Name].Value;
            }
            dr["rowState"] = "1";
            _copyRow = dr;
        }

        private DataRow _copyRow = null;
        private void dg1RightMenu_Opening(object sender, CancelEventArgs e)
        {
            if (_copyRow == null)
            {
                tsmInsertCopyRow.Enabled = false;
            }
            else
            {
                tsmInsertCopyRow.Enabled = true;
            }

            if (dg1.CurrentCell == null)
            {
                //tsmInsertRow.Enabled = false;
                tsmCopyRow.Enabled = false;
            }
            else
            {
                tsmCopyRow.Enabled = true;
                //tsmInsertRow.Enabled = true;
            }
        }

        private void tsmInsertCopyRow_Click(object sender, EventArgs e)
        {
            int sn = Convert.ToInt16(dg1.CurrentRow.Cells["indexSn"].Value);
            _copyRow["indexSn"] = (sn + 1).ToString();
            (dg1.DataSource as DataView).Table.Rows.InsertAt(_copyRow, sn+1);
            _copyRow = null;
            BulidSn(DataHelper.XkfyData.Tables[_tbName]);
        }

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //修改
            ShowForm("Modify");
        }

        private void btnCopyAdd_Click(object sender, EventArgs e)
        {
            //复制新增
            ShowForm("CopyAdd");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //新增
            ShowForm("Add");
        }
        private void ShowForm(string type)
        {
            if (this.dg1.CurrentRow == null)
            {
                MessageBox.Show("请先选择一行");
                return;
            }
              
            if (!string.IsNullOrEmpty(_editForm) && _editForm != "0")
            {
                string tableName = _myConfig.MainDtName;
                DataGridViewRow dr = this.dg1.CurrentRow;

                //利用反射实例化窗口
                Type t = Type.GetType("xkfy_mod." + _editForm);//窗体名要加上程序集名称
                if (t == null)
                {
                    return;
                }

                Form form;
                if (!string.IsNullOrEmpty(_myConfig.IsDlcFile) && _myConfig.IsDlcFile == "YES")
                {
                   
                    form = (Form)Activator.CreateInstance(t, new object[] { dr, type, tableName });
                }
                else
                {
                    form = (Form)Activator.CreateInstance(t, new object[] { dr, type });
                }

                form.Text = "编辑" + _tbName;
                form.ShowDialog();
            }
            else
            {
                DataGridViewRow dr = this.dg1.CurrentRow;
                AlmightyEdit ae = new AlmightyEdit(dr, _tbName, type, _fileType);
                ae.Text = "编辑" + _tbName;
                ae.Show();
            }
        }

        private void tsmDelRow_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = MessageBox.Show("是否确定要删除当前选择的行？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogR == DialogResult.Yes)
            {
                dg1.Rows.Remove(this.dg1.CurrentRow);
            }
        }

        private void dg1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (string.IsNullOrEmpty(dg1.Rows[e.RowIndex].Cells["rowState"].Value.ToString()))
                dg1.Rows[e.RowIndex].Cells["rowState"].Value = "0";
            //dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PowderBlue;

            if (this.Text.IndexOf("*") == -1)
            {
                this.Text = this.Text + "*";
            }
        }

        private void dg1_CurrentCellChanged(object sender, EventArgs e)
        {
            txtExplain.Text = "";
            if (dg1.CurrentRow == null)
                return;
            DataGridViewRow dv = dg1.CurrentRow;
            if (dv.Cells[1].Value == null)
                return;
            string explain = string.Empty;
            DataTable dt = DataHelper.XkfyData.Tables["Config"];
            switch (_tbName)
            {
                //DLC内功
                case "DLC_NeigongData":
                    txtExplain.Text = ExplainHelper.ExplainNeiGong(dv);
                    break;
                case "CharacterData":
                    txtExplain.Text = ExplainHelper.GetCharacterData(dv);
                    break;
                //战斗奖励
                case "RewardData":
                    string[] sRewardData = dv.Cells[1].Value.ToString().Split('*');
                    txtExplain.Text = ExplainHelper.ExplainRewardData(sRewardData);
                    break;
                //战斗结果
                case "BattleAreaData":
                    gbZhanDou.Visible = true;
                    DataHelper.ExistTable("RewardData");
                    ExplainBattleArea(dv);
                    break;
                //招式
                case "RoutineData":
                    txtExplain.Text = _tl.ExplainRoutineData(dv); ;
                    break;
                //战斗补充文件
                case "BattleSchedule":
                    txtExplain.Text = _tl.ExplainBattleSchedule(dv); ;
                    break;
            }
        }

        private void Almighty_KeyDown(object sender, KeyEventArgs e)
        {
            //if (dg1.CurrentRow == null)
            //    return;
            //int cellIndex = dg1.CurrentRow.Index;
            //int indexSn = Convert.ToInt16(dg1.CurrentRow.Cells["indexSn"].Value);
            //if (e.KeyCode == Keys.Up)
            //{
            //    if (dg1.CurrentRow != null && dg1.CurrentRow.Selected)
            //    {
            //        if (indexSn - 1 < 0)
            //            return;
            //        DataTable dt = (dg1.DataSource as DataView).Table;
            //        DataRow dr = dt.NewRow();
                    
            //        dr.ItemArray = dt.Rows[indexSn - 1].ItemArray;
            //        dt.Rows[indexSn - 1].ItemArray = dt.Rows[cellIndex].ItemArray;
            //        dt.Rows[indexSn].ItemArray = dr.ItemArray;
            //    }
            //    BulidSn(DataHelper.XkfyData.Tables[_tbName]);
            //}
            //else if (e.KeyCode == Keys.Down)
            //{
            //    if (dg1.CurrentRow != null && dg1.CurrentRow.Selected)
            //    {
            //        if (indexSn + 1 >= dg1.Rows.Count)
            //            return;
            //        DataTable dt = (dg1.DataSource as DataView).Table;
            //        DataRow dr = dt.NewRow();
            //        dr.ItemArray = dt.Rows[indexSn + 1].ItemArray;
            //        dt.Rows[indexSn + 1].ItemArray = dt.Rows[cellIndex].ItemArray;
            //        dt.Rows[indexSn].ItemArray = dr.ItemArray;
            //    }
            //    BulidSn(DataHelper.XkfyData.Tables[_tbName]);
            //}
            
        }

        private void btnToVisible_Click(object sender, EventArgs e)
        {
            gbError.Visible = false;
        }

        private int time = 30;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time == 0)
            {
                gbError.Visible = false;
                timer1.Stop();
            }
            lblTime.Text = time + "秒后自动隐藏！";
            time--;
        }
    }
}
