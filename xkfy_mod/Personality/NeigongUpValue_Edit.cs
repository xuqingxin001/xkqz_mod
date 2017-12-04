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
    public partial class NeigongUpValueEdit : Form
    {
        private string _id = string.Empty;
        private DataGridViewRow _dr = null;
        private DataRow _newDr = null;
        private string _type = string.Empty;
        public NeigongUpValueEdit(string id)
        {
            this._id = id;
            InitializeComponent();
        }

        public NeigongUpValueEdit()
        {
            InitializeComponent();
        }
        public NeigongUpValueEdit(DataGridViewRow dr,string type)
        {
            this._type = type;
            this._dr = dr;
            InitializeComponent();
        }

        private void NeigongUpValue_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                DataHelper.ExistTable("BattleNeigong");
                if (_type != "0")
                {
                    _newDr = DataHelper.XkfyData.Tables["NeigongUpValue"].NewRow();
                    if (_type == "2")
                    {
                        for (int i = 0; i < _dr.DataGridView.Columns.Count; i++)
                        {
                            string cName = _dr.DataGridView.Columns[i].Name;
                            _newDr[cName] = _dr.Cells[cName].Value;
                        }
                    }
                    _newDr["rowState"] = "1";
                    btnSelNeiGong.Visible = true;
                }
                else
                {
                    txtiid.Text = _dr.Cells["iID"].Value.ToString();
                    txtNgName.Text = DataHelper.GetValue("BattleNeigong", "name", "id", txtiid.Text);
                }
                cboLv.SelectedItem = "Lv1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] info = null;
            if (_type == "0")
            {
                info = _dr.Cells[cboLv.SelectedItem.ToString()].Value.ToString().Split(',');
            }
            else
            {
                info  = _newDr[cboLv.SelectedItem.ToString()].ToString().Split(',');
            }
            if (info.Length >= 6)
            {
                textBox1.Text = info[0];
                textBox2.Text = info[1];
                textBox3.Text = info[2];
                textBox4.Text = info[3];
                textBox5.Text = info[4];
                textBox6.Text = info[5];
            }
            else
            {
                if(_type != "1")
                    lblMsg.Text = "第[" + cboLv.SelectedItem.ToString() + "]级,数据有误,请检查";
            }
               
        }

        private void btnSelNeiGong_Click(object sender, EventArgs e)
        {
            string[] row = new string[3] { "ID", "name", "Desc" };
            RadioList rl = new RadioList("BattleNeigong", row, txtiid, txtNgName, "1");
            rl.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (string.IsNullOrEmpty(txtiid.Text) || txtiid.Text == "")
            {
                MessageBox.Show("请选择内功后在点击保存");
                return;
            }
            string rowValue = textBox1.Text + "," + textBox2.Text + "," + textBox3.Text + "," + textBox4.Text + "," + textBox5.Text + "," + textBox6.Text;

            if (ckbEqualAll.Checked)
            {
                for (int i = 0; i < cboLv.Items.Count; i++)
                {
                    if (_type != "0")
                    {
                        _newDr[cboLv.Items[i].ToString()] = rowValue;
                    }
                    else
                    {
                        _dr.Cells[cboLv.Items[i].ToString()].Value = rowValue;
                    }
                }
            }
            else
            {
                if (_type != "0")
                {
                    _newDr[cboLv.SelectedItem.ToString()] = rowValue;
                }
                else
                {
                    _dr.Cells[cboLv.SelectedItem.ToString()].Value = rowValue;
                }
                
            }

            if (_type != "0")
            {
                _newDr["iID"] = txtiid.Text;
                DataHelper.XkfyData.Tables["NeigongUpValue"].Rows.InsertAt(_newDr, 0);
            }
            
            lblMsg.ForeColor = Color.Blue;
            lblMsg.Text = "保存[" + txtNgName.Text + "]修炼[" + cboLv.SelectedItem.ToString() + "]级,修炼属性成功！";
            _dr.DataGridView.CurrentCell = null;
            this.Close();
        }

        private void ckbEqualAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEqualAll.Checked)
            {
                cboLv.Enabled = false;
            }
            else
            {
                cboLv.Enabled = true;
            }
        }
    }
}
