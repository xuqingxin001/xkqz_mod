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
    public partial class FrmRound : Form
    {
        private TextBox _txtId;
        private TextBox _txtName;
        public FrmRound(TextBox txtId, TextBox txtName)
        {
            this._txtId = txtId;
            this._txtName = txtName;
            InitializeComponent();
        }

        private void frmRound_Load(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            dg1.DataSource = bs;
            bs.DataSource = DataHelper.ExplainConfig["HuiHe"];
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            
        }

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_txtId != null)
            {
                _txtId.Text = this.dg1.CurrentRow.Cells[0].Value.ToString(); ;
                _txtName.Text = this.dg1.CurrentRow.Cells[1].Value.ToString(); ;
                this.Close();
            }
        }
    }
}
