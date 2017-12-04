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
    public partial class BattleAreaDataEdit : Form
    {
        private ToolsHelper _tl = new ToolsHelper();
        private DataGridViewRow _dr = null;
        private string _type = string.Empty;
        private readonly string _tableName;
        private readonly bool _isDlcFile = false;

        public BattleAreaDataEdit(DataGridViewRow dr, string type)
        {
            this._dr = dr;
            this._type = type;
            this._tableName = "BattleAreaData";
            InitializeComponent();
        }

        public BattleAreaDataEdit(DataGridViewRow dr, string type,string tableName)
        {
            this._dr = dr;
            this._type = type;
            this._tableName = tableName;
            _isDlcFile = true;
            InitializeComponent();
        }

        

        private void BattleAreaData_Edit_Load(object sender, EventArgs e)
        {
            Dictionary<string, TableExplain> dlc = new Dictionary<string, TableExplain>();

            string path = Path.Combine(ToolsHelper.ExplainPath, _tableName+".xml");

            if (File.Exists(path))
            {
                dlc = DataHelper.GetTableExplainList(path);
            }

            ToolsHelper tl = new ToolsHelper();
            tl.CreateCtr(DataHelper.XkfyData.Tables[_tableName], gbDrop, _dr, dlc, 12);

            if (_type == "Modify")
            {
                //修改
                DataHelper.CopyRowToData(this, _dr);
                btnUpdate.Visible = true;
            }
            else if (_type == "Add")
            {
                //新增
                btnAdd.Visible = true;
            }
            else
            {
                //复制新增
                DataHelper.CopyRowToData(this, _dr);
                btnAdd.Visible = true;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnMustJoin_Click(object sender, EventArgs e)
        {
            string[] row = new string[3] { "@iID", "sNpcName", "sIntroduction" };
            string tbName = "NpcData";
            if (_isDlcFile)
            {
                tbName = "DLC_NpcData";
            }
            RadioList rl = new RadioList(tbName, row, txtMustJoin,null, "2");
            rl.ShowDialog();
        }

        private void radioButton6_Click(object sender, EventArgs e)
        {
            cboHuihe.Text = ((RadioButton)sender).Text;
        }

        private void btnZuhe_Click(object sender, EventArgs e)
        {
            string huihe = cboHuihe.Text;
            if (string.IsNullOrEmpty(huihe))
            {
                MessageBox.Show("请选择回合！");
            }
            
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(txtMustJoinStaff.Text))
            {
                sb.Append("*");
            }
            
            string[] mustJoin = txtMustJoin.Text.Split(',');
            for (int i = 0; i < mustJoin.Length; i++)
            {
                sb.AppendFormat("({0},{1},{2})*", huihe, mustJoin[i], 0);
            }
           
            if (rdo1.Checked)
            {
                txtMustJoinStaff.Text += sb.ToString().TrimEnd('*');
            }
            else if (rdo2.Checked)
            {
                txtsJoinStaff.Text += sb.ToString().TrimEnd('*');
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] row = new string[2] { "iRID", "sRewardData" };
            string tbName = "RewardData";
            if (_isDlcFile)
            {
                tbName = "DLC_RewardData";
            }

            RadioList rl = new RadioList(tbName, row, txtsRewardWin, null, "1");
            rl.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] row = new string[2] { "iRID", "sRewardData" };
            string tbName = "RewardData";
            if (_isDlcFile)
            {
                tbName = "DLC_RewardData";
            }
            RadioList rl = new RadioList(tbName, row, txtRewardFale, null, "1");
            rl.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _tl.AddData(this, _tableName);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _tl.updateData(this, _dr);
        }
    }
}
