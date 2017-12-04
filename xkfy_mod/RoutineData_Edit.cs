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
    public partial class RoutineDataEdit : Form
    {
        private DataGridViewRow _dr = null;
        private string _type = string.Empty;
        public RoutineDataEdit(DataGridViewRow dr, string type)
        {
            this._dr = dr;
            this._type = type;
            InitializeComponent();
        }

        private void btnSel2_Click(object sender, EventArgs e)
        {
            ShowForm(txtSkill2ID, txtSkill2Name);
        }

        private void btnSel1_Click(object sender, EventArgs e)
        {
            ShowForm(txtSkill1ID, txtSkill1Name);
        }

        private void btnSel3_Click(object sender, EventArgs e)
        {
            ShowForm(txtSkill3ID, txtSkill3Name);
        }

        private void ShowForm(TextBox txtId, TextBox txtName)
        {
            string[] row = new string[3] { "SkillNo", "SkillName", "NeedToSelectTarget" };
            RadioList rl = new RadioList("BattleAbility", row, txtId, txtName,"1");
            rl.ShowDialog();
        }

        private void RoutineData_Edit_Load(object sender, EventArgs e)
        {
            CboData.BindiWearAmsType(cboiWearAmsType);
            switch (_type)
            {
                case "1":
                    btnAdd.Visible = true;
                    break;
                case "2":
                    btnAdd.Visible = true;
                    SetValue();
                    break;
                case "0":
                    btnUpdate.Visible = true;
                    SetValue();
                    break;
            }
        }
        private void SetValue()
        {
            DataHelper.CopyRowToData(this, _dr);

            if (!string.IsNullOrEmpty(txtSkill1ID.Text))
                txtSkill1Name.Text = GetSkillName(txtSkill1ID.Text);
            if (!string.IsNullOrEmpty(txtSkill2ID.Text))
                txtSkill2Name.Text = GetSkillName(txtSkill2ID.Text);
            if (!string.IsNullOrEmpty(txtSkill3ID.Text))
                txtSkill3Name.Text = GetSkillName(txtSkill3ID.Text);
            GetSpecialAdd(txtsSpecialAdd.Text);
        }

        private string GetSkillName(string skillNo)
        {
            return DataHelper.GetValue("BattleAbility", "SkillName", "SkillNo", skillNo);
        }

        private void GetSpecialAdd(string value)
        {
            string key = "";
            string[] str = value.Split(',');
            switch (str[0])
            {
                case "0":
                    key = "MB";
                    break;
                case "1":
                    key = "JBG";
                    break;
                case "2":
                    key = "ZS";
                    break;
                case "3":
                    key = "SHJN";
                    break;
                case "4":
                    key = "JY";
                    break;
            }
            Dictionary<string, string> dic = DataHelper.ExplainConfig[key];
            if (dic.ContainsKey(str[1]))
                txtTsName.Text = dic[str[1]];
        }

        private void btnSel4_Click(object sender, EventArgs e)
        {
            AttributeList al = new AttributeList("AttributeList.xml", txtsSpecialAdd, txtTsName);
            al.Location = btnSel4.Location;
            al.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ToolsHelper tl = new ToolsHelper();
            tl.updateData(this, _dr);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow[] drRd = DataHelper.XkfyData.Tables["RoutineData"].Select("iRoutineID='" + txtiRoutineID.Text + "'");
            if (drRd.Length > 0)
            {
                lblMsg.Text = string.Format("ID已经存在，为了避免游戏错误,不允许新增相同ID的数据");
                return;
            }
            ToolsHelper th = new ToolsHelper();
            th.AddData(this, "RoutineData");
        }

        private void btnSetValue_Click(object sender, EventArgs e)
        {
            FrmSetCbo fs = new FrmSetCbo( cboiWearAmsType, CboData.IWearAmspath);
            fs.Show();
        }
    }
}
