using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Config;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class BattleConditionEdit : Form
    {
        private ToolsHelper _tl = new ToolsHelper();
        private readonly DataGridViewRow _dr = null;
        private readonly string _type;
        private string _initialId = string.Empty;

        private readonly string _tableName;
        private readonly string _tableNameD;

        private readonly DataTable _battleConditionD = null;
        private readonly DataTable _conditionD = null;
        private Dictionary<string, List<Condition>> _presetConditions;

        public BattleConditionEdit(DataGridViewRow dr, string type)
        {
            this._dr = dr;
            this._type = type;
            this._tableName = "BattleCondition";
            this._tableNameD = "BattleCondition_D";
            _battleConditionD = DataHelper.XkfyData.Tables[_tableNameD];
            _conditionD = DataHelper.XkfyData.Tables[_tableNameD].Clone();
            InitializeComponent();
        }

        public BattleConditionEdit(DataGridViewRow dr, string type, string tableName)
        {
            this._dr = dr;
            this._type = type;
            this._tableName = tableName;
            this._tableNameD = tableName + "_D";
            _battleConditionD = DataHelper.XkfyData.Tables[_tableNameD];
            _conditionD = DataHelper.XkfyData.Tables[_tableNameD].Clone();
            InitializeComponent();
        }
        
        private void BattleCondition_Edit_Load(object sender, EventArgs e)
        {
            //增加 预设下拉框新增 功能2016年10月23日 07:39:48
            List<BattleCondition> conditions = DataHelper.ReadXmlToList<BattleCondition>(CboData.PresetConditionPath);
            //下拉框数据源
            Dictionary<string,string> dropdownSource = new Dictionary<string, string>();
            dropdownSource.Add("SELADD", "选择新增");
            //与下拉框关联的预设效果
            _presetConditions = new Dictionary<string, List<Condition>>();
            foreach (BattleCondition condition in conditions)
            {
                if(dropdownSource.ContainsKey(condition.Key))
                    continue;
                dropdownSource.Add(condition.Key,condition.Value);
                _presetConditions.Add(condition.Key, condition.Conditions);
            }
            
            DataHelper.BinderComboBox(cboPresetCondition, dropdownSource);
            cboPresetCondition.SelectedIndex = 0;
            this.cboPresetCondition.SelectedIndexChanged += new System.EventHandler(this.cboPresetCondition_SelectedIndexChanged);

            //2016年7月7日 20:57:50 修改为统一的下拉框读取
            CboData.BindiComboBox(cboEffectType1, CboData.BattleConditionPath);
            CboData.BindiComboBox(cboAccumulate, CboData.AccumulatePath);

            if (_type != "Add")
            {
                DataHelper.CopyRowToData(groupBox1, _dr);
                _initialId = txtConditionID.Text;
                SetdgXiaoGuo();
            }
            this.dgXiaoGuo.DataSource = _conditionD;
        }

        #region 绑定内功效果的DataTable的值到页面控件上
        /// <summary>
        /// 绑定内功效果的DataTable的值到页面控件上
        /// </summary>
        public void SetdgXiaoGuo()
        {
            DataRow[] bnD = _battleConditionD.Select("@ConditionID='" + txtConditionID.Text + "'");
            foreach (DataRow item in bnD)
            {
                _conditionD.ImportRow(item);
            }
        }
        #endregion

        private void dg1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dgXiaoGuo.RowHeadersWidth - 4,
                e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgXiaoGuo.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgXiaoGuo.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void btnAddXg_Click(object sender, EventArgs e)
        {
            DefaultValue();
            DataRow dr = _conditionD.NewRow();
            dr["@ConditionID"] = txtConditionID.Text;
            DataHelper.CopyDataToRow(groupBox2, dr);
            _conditionD.Rows.Add(dr);
        }

        DataGridViewRow _dgvDrc = null;
        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CleatState();
            txtXgName.Text = "";
            txtXgName2.Text = "";
            txtXgName.Visible = false;
            txtXgName.Visible = false;
            btnSelBattleCondition2.Visible = false;
            btnSelBattleCondition.Visible = false;

            _dgvDrc = this.dgXiaoGuo.CurrentRow;
            string effectType = _dgvDrc.Cells["EffectType"].Value.ToString();
            //if (string.IsNullOrEmpty(effectType))
            //    return;
            //if (DataHelper.selItem["BdEffectType"].ContainsKey(effectType))
            //{
                
            //}

            cboEffectType1.SelectedValue = effectType;

            txtValue1.Text = _dgvDrc.Cells["Value1"].Value.ToString();
            txtValue2.Text = _dgvDrc.Cells["Value2"].Value.ToString();
            txtValueLimit.Text = _dgvDrc.Cells["ValueLimit"].Value.ToString();

            cboAccumulate.SelectedValue = _dgvDrc.Cells["Accumulate"].Value.ToString();

            //if (DataHelper.selItem["BattleNeigong.Accumulate"].ContainsKey(dgvDrc.Cells["Accumulate"].Value.ToString()))
            //{
                
            //}

            if (_dgvDrc.Cells["Percent"].Value.ToString() == "1")
                rdoPercentA.Checked = true;
            else
                rdoPercentB.Checked = true;
            btnAddXg.Visible = false;
            btnSaveXg.Visible = true;

            if (txtValue1.Text.Length > 5)
            {
                txtXgName.Visible = true;
                btnSelBattleCondition.Visible = true;
                txtXgName.Text = DataHelper.GetValue(_tableName, "CondName", "@ConditionID", txtValue1.Text);
            }

            if (txtValue2.Text.Length > 5)
            {
                txtXgName.Visible = true;
                btnSelBattleCondition2.Visible = true;
                txtXgName2.Text = DataHelper.GetValue(_tableName, "CondName", "@ConditionID", txtValue2.Text);
            }
        }

        private void btnSaveXg_Click(object sender, EventArgs e)
        {
            DefaultValue();
            //修改页面表格的值
            if (_dgvDrc.DataGridView != null)
            {
                DataHelper.CopyDataToRow(groupBox2, _dgvDrc);
            }
            dgXiaoGuo.CurrentCell = null;
            //恢复控件的初始状态
            CleatState();
            lblMsg.ForeColor = Color.Blue;
            lblMsg.Text = "修改选中的效果成功！";
        }

        private void DefaultValue()
        {
            if (string.IsNullOrEmpty(txtValue1.Text))
                txtValue1.Text = "0";
            if (string.IsNullOrEmpty(txtValue2.Text))
                txtValue2.Text = "0";
            if (string.IsNullOrEmpty(txtValueLimit.Text))
                txtValueLimit.Text = "0";
        }

        private void CleatState()
        {
            cboAccumulate.SelectedIndex = 0;
            cboEffectType1.SelectedIndex = 0;
            txtValue1.Text = "";
            txtValue2.Text = "";
            txtValueLimit.Text = "";
            btnAddXg.Visible = true;
            btnSaveXg.Visible = false;

            txtXgName.Text = "";
            txtXgName2.Text = "";
            txtXgName.Visible = false;
            txtXgName.Visible = false;
            btnSelBattleCondition2.Visible = false;
            btnSelBattleCondition.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblMsg.ForeColor = Color.Red;
            if (string.IsNullOrEmpty(txtConditionID.Text))
            {
                lblMsg.Text = "特效ID已经存在，请修改！";
                return;
            }

            if (btnSaveXg.Visible)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("你当前有未保存的状态修改,是否确定不保存离开?", "退出状态修改", messButton);

                if (dr != DialogResult.OK) //如果点击“确定”按钮
                {
                    return;
                }
            }

            string ngId = txtConditionID.Text;
            if (_type == "Modify")
            {
                if (_initialId != ngId)
                {
                    DataRow[] bnDr = _battleConditionD.Select("@ConditionID='" + ngId + "'");
                    if (bnDr.Length > 0)
                    {
                        lblMsg.Text = "特效ID已经存在，请修改！";
                        return;
                    }
                }
                DataRow[] mdRow = _battleConditionD.Select("@ConditionID='" + ngId + "'");
                foreach (DataRow item in mdRow)
                {
                    _battleConditionD.Rows.Remove(item);
                }

                DataHelper.XkfyData.Tables[_tableNameD].Merge(_conditionD);
                DataHelper.CopyDataToRow(groupBox1, _dr);
                if (string.IsNullOrEmpty(_dr.Cells["rowState"].Value.ToString()))
                    _dr.Cells["rowState"].Value = "0";
            }
            else
            {
                DataRow[] bnDr = DataHelper.XkfyData.Tables[_tableName].Select("@ConditionID='" + ngId + "'");
                if (bnDr.Length > 0)
                {
                    lblMsg.Text = "特效ID已经存在，请修改！！";
                    return;
                }
                DataRow bnNewRow = DataHelper.XkfyData.Tables[_tableName].NewRow();

                DataHelper.CopyDataToRow(groupBox1, bnNewRow);
                if (string.IsNullOrEmpty(bnNewRow["rowState"].ToString()))
                    bnNewRow["rowState"] = "1";
                DataHelper.XkfyData.Tables[_tableName].Rows.InsertAt(bnNewRow, 0);
                DataHelper.XkfyData.Tables[_tableNameD].Merge(_conditionD);
            }
            _dr.DataGridView.CurrentCell = null;
            this.Close();
        }

        private void dgXiaoGuo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CleatState();

            _dgvDrc = this.dgXiaoGuo.CurrentRow;
            if (_dgvDrc == null)
                return;
            string effectType = _dgvDrc.Cells["EffectType"].Value.ToString();
            //if (string.IsNullOrEmpty(effectType))
            //    return;
            //if (DataHelper.selItem["BdEffectType"].ContainsKey(effectType))
            //{

            //}
            cboEffectType1.SelectedValue = effectType;

            txtValue1.Text = _dgvDrc.Cells["Value1"].Value.ToString();
            txtValue2.Text = _dgvDrc.Cells["Value2"].Value.ToString();
            txtValueLimit.Text = _dgvDrc.Cells["ValueLimit"].Value.ToString();

            //if (DataHelper.selItem["BattleNeigong.Accumulate"].ContainsKey(dgvDrc.Cells["Accumulate"].Value.ToString()))
            //{
                
            //}
            cboAccumulate.SelectedValue = _dgvDrc.Cells["Accumulate"].Value.ToString();

            if (_dgvDrc.Cells["Percent"].Value.ToString() == "1")
                rdoPercentA.Checked = true;
            else
                rdoPercentB.Checked = true;
            btnAddXg.Visible = false;
            btnSaveXg.Visible = true;

            if (txtValue1.Text.Length > 5)
            {
                txtXgName.Visible = true;
                btnSelBattleCondition.Visible = true;
                txtXgName.Text = DataHelper.GetValue(_tableName, "CondName", "@ConditionID", txtValue1.Text);
            }

            if (txtValue2.Text.Length > 5)
            {
                txtXgName.Visible = true;
                btnSelBattleCondition2.Visible = true;
                txtXgName2.Text = DataHelper.GetValue(_tableName, "CondName", "@ConditionID", txtValue2.Text);
            }
            lblMsg.Text = "当前修改第" + (this.dgXiaoGuo.CurrentRow.Index + 1) + "行数据";
        }

        private void txtConditionID_TextChanged(object sender, EventArgs e)
        {
            if (dgXiaoGuo.DataSource != null)
            {
                foreach (DataRow dr in (dgXiaoGuo.DataSource as DataTable).Rows)
                {
                    dr[0] = txtConditionID.Text;
                }
            }
        }

        private void cboEffectType1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSelBattleCondition_Click(object sender, EventArgs e)
        {
            RadioList rl = new RadioList(_tableName, row, txtValue1, txtXgName, "1");
            rl.ShowDialog();
        }

        string[] row = new string[3] { "@ConditionID", "CondName", "CondDesc" };
        private void btnSelBattleCondition2_Click(object sender, EventArgs e)
        {
            RadioList rl = new RadioList(_tableName, row, txtValue2, txtXgName2, "1");
            rl.ShowDialog();
        }

        private void cboAccumulate_SelectedIndexChanged(object sender, EventArgs e)
        {
            label17.Text = "value1【值1】";
            label11.Text = "value1【值2】";
            btnSelBattleCondition.Visible = false;
            btnSelBattleCondition2.Visible = false;
            txtXgName.Visible = false;
            txtXgName2.Visible = false;
            if (cboAccumulate.SelectedValue == null)
                return;
            switch (cboAccumulate.SelectedValue.ToString())
            {
                case "7":
                case "10":
                    btnSelBattleCondition.Visible = true;
                    btnSelBattleCondition2.Visible = true;
                    txtXgName.Visible = true;
                    txtXgName2.Visible = true;

                    label17.Text = "对自己";
                    label11.Text = "对敌人";
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CleatState();
        }

        private void cboPresetCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = cboPresetCondition.SelectedValue.ToString();
            if (key != "SELADD")
            {
                List<Condition> conditions = _presetConditions[key];
                foreach (Condition cd in conditions)
                {
                    DataRow dr = _conditionD.NewRow();
                    dr["@ConditionID"] = txtConditionID.Text;
                    dr["EffectType"] = cd.EffectType;
                    dr["Accumulate"] = cd.Accumulate;
                    dr["Percent"] = cd.Percent;
                    dr["Value1"] = cd.Value1;
                    dr["Value2"] = cd.Value2;
                    dr["ValueLimit"] = cd.ValueLimit;
                    _conditionD.Rows.Add(dr);
                }
            }
        }

        private void 修改招式效果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetCbo fs = new FrmSetCbo(cboEffectType1, CboData.BattleConditionPath);
            fs.Show();
        }

        private void 修改生效方式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetCbo fs = new FrmSetCbo(cboAccumulate, CboData.AccumulatePath);
            fs.Show();
        }

        private void 预设效果ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            _conditionD.Clear();
            SetdgXiaoGuo();
        }
    }
}
