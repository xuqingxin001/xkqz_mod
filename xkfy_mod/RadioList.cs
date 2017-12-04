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
    public partial class RadioList : Form
    {
        private string _tbName;
        private string _selType;
        private string[] _row;
        private TextBox _txtId;
        private TextBox _txtName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tbName">存储TXT文件的DataTable的名称</param>
        /// <param name="row">要显示的列</param>
        /// <param name="txtId">显示ID的控件</param>
        /// <param name="txtName">显示名称的控件</param>
        /// <param name="selType">类型 1，单选；2,多选</param>
        public RadioList(string tbName, string[] row,TextBox txtId, TextBox txtName,string selType)
        {
            this._tbName = tbName;
            this._selType = selType;
            this._row = row;
            this._txtId = txtId;
            this._txtName = txtName;
            InitializeComponent();
        }

        private void RadioList_Load(object sender, EventArgs e)
        {
            if (_selType == "2")
                btnOK.Visible = true;
            label1.Text = _row[0];
            label2.Text = _row[1];
            textBox1.Tag = _row[0];
            textBox2.Tag = _row[1];
            dg1.AllowUserToAddRows = false;
            DataHelper.ExistTable(_tbName);
            dg1.DataSource = DataHelper.XkfyData.Tables[_tbName].Copy().DefaultView.ToTable(true, _row).DefaultView;
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
        }

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = string.Empty;
            string name = string.Empty;
            if (!string.IsNullOrEmpty(_txtId.Text))
                id = ",";
            if (!string.IsNullOrEmpty(_txtId.Text))
                name = ",";
            id += this.dg1.CurrentRow.Cells[0].Value.ToString();
            name += this.dg1.CurrentRow.Cells[1].Value.ToString();

            if (_selType == "2")
            {
                if (_txtId != null)
                    _txtId.Text += id;
                if (_txtName != null)
                    _txtName.Text += name;
            }
            else
            {
                if (_txtId != null)
                    _txtId.Text = this.dg1.CurrentRow.Cells[0].Value.ToString(); ;
                if (_txtName != null)
                    _txtName.Text = this.dg1.CurrentRow.Cells[1].Value.ToString(); ;
            }
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string name = textBox2.Text;
            string where = "1 = 1";
            if (!string.IsNullOrEmpty(id))
            {
                where += " and " + textBox1.Tag + " like '%" + id + "%' ";
            }

            if (!string.IsNullOrEmpty(name))
            {
                where += " and " + textBox2.Tag + " like '%" + name + "%' ";
            }
            DataView dv = dg1.DataSource as DataView;
            dv.RowFilter = where;
            this.dg1.DataSource = dv;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string id = string.Empty;
            string name = string.Empty;
            if (!string.IsNullOrEmpty(_txtId.Text))
                id = ",";
            if (!string.IsNullOrEmpty(_txtId.Text))
                name = ",";

            foreach (DataGridViewRow row in dg1.SelectedRows)
            {
                id += row.Cells[0].Value + ",";
                name += row.Cells[1].Value + ",";
            }
            if(_txtId != null)
                _txtId.Text += id.TrimEnd(new char[] { ',' });

            if (_txtName != null)
                _txtName.Text += name.TrimEnd(new char[] { ',' });
            this.Close();
        }
    }
}
