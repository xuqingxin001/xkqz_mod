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
    public partial class BattleAbilityEdit : Form
    {
        private DataGridViewRow _dr = null;
        private string _type = string.Empty;
        public BattleAbilityEdit(DataGridViewRow dr,string type)
        {
            this._dr = dr;
            this._type = type;
            InitializeComponent();
        }

        public BattleAbilityEdit(string type)
        {
            this._type = type;
            InitializeComponent();
        }

        private void BattleAbility_Edit_Load(object sender, EventArgs e)
        {
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
                    //txtSkillNo.Enabled = false;
                    btnUpdate.Visible = true;
                    SetValue();
                    break;
            }
        }

        private void SetValue()
        {
            DataHelper.CopyRowToData(this, _dr);
            string[] condition = _dr.Cells["condition"].Value.ToString().Split(',');
            string buffName = string.Empty;
            for (int i = 0; i < condition.Length; i++)
            {
                if (condition[i] == "" || condition[i] == "0")
                    continue;
                buffName += DataHelper.GetValue("BattleCondition", "CondName", "@ConditionID", condition[i]) + ",";
            }
            txtBuffName.Text = buffName.TrimEnd(new char[] { ',' }); ;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSkillNo.Text))
            {
                lblMsg.Text = "请输入ID";
                return;
            }
            ToolsHelper tl = new ToolsHelper();
            tl.updateData(this, _dr);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ToolsHelper th = new ToolsHelper();
            th.AddData(this, "BattleAbility");
        }

        private void btnSelBuff_Click(object sender, EventArgs e)
        {
            string[] row = new string[3] { "ConditionID", "CondName", "CondDesc" };
            RadioList rl = new RadioList("BattleCondition", row, txtBuffID, txtBuffName, "2");
            rl.ShowDialog();
        }
    }
}
