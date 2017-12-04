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
    public partial class RoutineNewDataEdit : Form
    {
        private ToolsHelper _tl = new ToolsHelper();
        private DataGridViewRow _dr = null;
        private readonly string _type;
        private readonly string _tableName;
        private readonly bool _isDlcFile = false;
        private readonly string _buffTable;

        public RoutineNewDataEdit(DataGridViewRow dr, string type)
        {
            _type = type;
            _dr = dr;
            _buffTable = "BattleCondition";
            _tableName = "RoutineNewData";
            InitializeComponent();
        }

        public RoutineNewDataEdit(DataGridViewRow dr, string type, string tableName)
        {
            _type = type;
            _dr = dr;
            this._tableName = tableName;
            _isDlcFile = true;
            _buffTable = "DLC_BattleCondition";
            InitializeComponent();
        }

        private void RoutineNewDataEdit_Load(object sender, EventArgs e)
        {
            CboData.BindiComboBox(comboBox1, CboData.WearAmspath);
            DataHelper.CopyRowToData(this, _dr);
            string path = Path.Combine(ToolsHelper.ExplainPath, _tableName+".xml");
            DataHelper.SetLabelTextBox(this, _dr, _type, path, btnUpdate, btnAdd);

            string[] condition = _dr.Cells["condition"].Value.ToString().Split(',');
            string buffName = string.Empty;
            for (int i = 0; i < condition.Length; i++)
            {
                if (condition[i] == "" || condition[i] == "0")
                    continue;
                buffName += DataHelper.GetValue(_buffTable, "CondName", "@ConditionID", condition[i]) + ",";
            }
            txtBuffName.Text = buffName.TrimEnd(new char[] { ',' }); ;
        }

        private void btnSelBuff_Click(object sender, EventArgs e)
        {
            string[] row = new string[3] { "@ConditionID", "CondName", "CondDesc" };
            RadioList rl = new RadioList(_buffTable, row, textBox20, txtBuffName, "2");
            rl.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ToolsHelper th = new ToolsHelper();
            th.AddData(this, _tableName);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请输入ID");
                return;
            }
            ToolsHelper tl = new ToolsHelper();
            tl.updateData(this, _dr);
        }
    }
}
