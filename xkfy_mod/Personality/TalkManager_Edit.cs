using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class TalkManagerEdit : Form
    {
        private ToolsHelper _tl = new ToolsHelper();
        private DataGridViewRow _dr = null;
        private DataRow _datarow = null;
        private string _type = string.Empty;
        public TalkManagerEdit(DataGridViewRow dr, string type)
        {
            this._dr = dr;
            this._type = type;
            InitializeComponent();
        }

        public TalkManagerEdit(string type)
        {
            this._type = type;
            InitializeComponent();
        }

        private void TalkManager_Edit_Load(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            DataView dv = DataHelper.XkfyData.Tables["Config"].DefaultView;
            dv.RowFilter = "Type='iGtype'";
            txtiGType1.DataSource = dv;

            DataView dv1 = dv.Table.Copy().DefaultView;
            dv1.RowFilter = "Type='iGtype'";
            txtiGType2.DataSource = dv1;

            DataView dv2 = dv.Table.Copy().DefaultView;
            dv2.RowFilter = "Type='iGtype'";
            txtiGType3.DataSource = dv2;

            string path = "\\工具配置文件\\TalkManager\\NpcImg.xml";
            DataHelper.setDicConfig("NpcImg",path,true);
            DataHelper.BinderComboBox(comboBox1, "NpcImg");
            DataHelper.BinderComboBox(comboBox2, "NpcImg");
            DataHelper.BinderComboBox(comboBox3, "NpcImg");
            DataHelper.BinderComboBox(comboBox4, "NpcImg");
            DataHelper.BinderComboBox(comboBox5, "NpcImg");
            DataHelper.BinderComboBox(comboBox6, "NpcImg");
            DataHelper.BinderComboBox(comboBox7, "NpcImg");
            DataHelper.BinderComboBox(comboBox8, "NpcImg");

            if (_type == "0")
            {
                //修改
                DataHelper.CopyRowToData(this, _dr);
                btnUpdate.Visible = true;
            }
            else if (_type == "1")
            {
                //新增
                btnAdd.Visible = true;
            }
            else if (_type == "2")
            {
                //复制新增
                DataHelper.CopyRowToData(this, _dr);
                btnAdd.Visible = true;
            }
            else if (_type == "debug")
            {
                //bindData(datarow);
                btnUpdate.Visible = true;
            }
        }

        public void BindData(DataRow row)
        {
            this._datarow = row;
            DataHelper.CopyRowToData(this, row);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ToolsHelper tl = new ToolsHelper();
            if (_type == "debug")
            {
                lblMsg.Visible = true;
                if (string.IsNullOrEmpty(txtIndexSn.Text.Trim()))
                    txtIndexSn.Text = "0";
                DataHelper.CopyDataToRow(this, _datarow);
                if (string.IsNullOrEmpty(_datarow["rowState"].ToString()))
                    _datarow["rowState"] = "0";
            }
            else
            {
                tl.updateData(this, _dr);
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;
            if (!string.IsNullOrEmpty(txtIndexSn.Text))
            {
                try
                {
                    rowIndex = Convert.ToInt16(txtIndexSn.Text);
                }
                catch
                {
                    MessageBox.Show("必须填入数字");
                    return;
                }
            }
            ToolsHelper th = new ToolsHelper();
            //th.addData(this, "TalkManager");
            DataRow dr = DataHelper.XkfyData.Tables["TalkManager"].NewRow();
            DataHelper.CopyDataToRow(this, dr);

            if (string.IsNullOrEmpty(dr["rowState"].ToString()))
                dr["rowState"] = "1";
            DataHelper.XkfyData.Tables["TalkManager"].Rows.InsertAt(dr, rowIndex);
            this.Close();
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_dr.ToString());
        }

        private DataTable _dt = new DataTable();
        private void cboiGType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                ComboBox cb = ((ComboBox)sender);
                string name = ((ComboBox)sender).Tag.ToString();
                string iGtype = ((ComboBox)sender).SelectedValue.ToString();

                DataRow[] driGtype = (cb.DataSource as DataView).Table.Select("Type='iGtype' and Value='" + iGtype + "'");
                if (driGtype.Length != 1)
                {
                    return;
                }

                Control[] text = groupBox1.Controls.Find(name, false);
                if (text.Length > 0)
                {
                    text[0].Text = cb.SelectedValue.ToString();
                }

                Control[] cbo = groupBox1.Controls.Find("cbo" + name, false);
                DataView dv = (cb.DataSource as DataView).Table.Copy().DefaultView;
                if (driGtype[0]["ChildType"].ToString() != "isNull")
                {
                    
                    dv.RowFilter = "Type='" + driGtype[0]["ChildType"].ToString() + "'";
                    if (cbo.Length > 0)
                    {
                        ((ComboBox)cbo[0]).DataSource = dv;
                    }
                }
                else
                {
                    if (cbo.Length > 0)
                    {
                        DataView dvNull = (cb.DataSource as DataView).Table.Clone().DefaultView;
                        ((ComboBox)cbo[0]).DataSource = dvNull;
                    }
                }
                Control[] btn = groupBox1.Controls.Find("btn" + name, false);
                btn[0].Visible = false;
                switch (iGtype)
                {
                    case "6":
                    case "7":
                    case "13":
                    case "16":
                    case "17":
                    case "19":
                        if (cbo.Length > 0)
                        {
                            btn[0].Visible = true;
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenForm(string iGtype,TextBox txtSelValue2,TextBox txtSelName2)
        {
            string[] row = null;
            string tbName = string.Empty;

            switch (iGtype)
            {
                case "6":
                    row = new string[3] { "iID", "sNpcName", "sIntroduction" };
                    tbName = "NpcData";
                    break;
                case "7":
                    row = new string[3] { "iItemID$0", "sItemName$1", "sTip$5" };
                    tbName = "ItemData";
                    break;
                case "16":
                    row = new string[3] { "iRoutineID", "sRoutineName", "sModelName" };
                    tbName = "RoutineData";
                    break;
                case "17":
                    row = new string[3] { "ID", "Name", "Desc" };
                    tbName = "BattleNeigong";
                    break;
                case "13":
                    row = new string[3] { "iID", "sTitle", "sTip" };
                    tbName = "TitleData";
                    break;
                case "19":
                    row = new string[3] { "iID", "sAbilityID", "sBookTip" };
                    tbName = "AbilityBookData";
                    break;
            }
            if (!string.IsNullOrEmpty(tbName))
            {
                RadioList rl = new RadioList(tbName, row, txtSelValue2, txtSelName2, "1");
                rl.ShowDialog();
            }
        }

        private void cboiGType1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                ComboBox cb = ((ComboBox)sender);
                string name = ((ComboBox)sender).Tag.ToString();
                string sGArg1 = ((ComboBox)sender).SelectedValue.ToString();

                Control[] text = groupBox1.Controls.Find(name, false);
                if (text.Length > 0)
                {
                    text[0].Text = cb.SelectedValue.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btniGType2_Click(object sender, EventArgs e)
        {
            OpenForm(txtiGType2.SelectedValue.ToString(), sGArg2, txtWn);
        }

        private void btniGType1_Click(object sender, EventArgs e)
        {
            OpenForm(txtiGType1.SelectedValue.ToString(), sGArg1, txtWn);
        }

        private void btniGType3_Click(object sender, EventArgs e)
        {
            OpenForm(txtiGType3.SelectedValue.ToString(), sGArg3, txtWn);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Button cb = ((Button)sender);
            string name = ((Button)sender).Tag.ToString();

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = Application.StartupPath + "\\Images";
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            string file = "";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                file = fileDialog.SafeFileName;
            }

            if (!string.IsNullOrEmpty(file))
            {
                file = file.Substring(0, file.IndexOf("."));
                Control[] text = this.Controls.Find(name, false);
                if (text.Length > 0)
                {
                    text[0].Text = file;
                }
            }
        }

        Label _oldlab = null;
        private void label34_Click(object sender, EventArgs e)
        {
            Label cb = ((Label)sender);
            cb.ForeColor = Color.Red;
            textBox63.Text = cb.Tag.ToString();
            if (_oldlab != null)
                _oldlab.ForeColor = Color.Black;
            _oldlab = cb;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cb = ((ComboBox)sender);
                string name = ((ComboBox)sender).Tag.ToString();
                if (((ComboBox)sender).SelectedValue != null)
                {
                    string sGArg1 = ((ComboBox)sender).SelectedValue.ToString();

                    Control[] text = this.Controls.Find(name, false);
                    if (text.Length > 0)
                    {
                        text[0].Text = cb.SelectedValue.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\工具配置文件\\TalkManager\\NpcImg.xml";
            FrmSetUp fs = new FrmSetUp(path);
            fs.Show();
        }
    }
}
