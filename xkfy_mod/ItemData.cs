using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class ItemData : DockContent
    {
        public ItemData()
        {
            InitializeComponent();
        }

        private void ItemData_Load(object sender, EventArgs e)
        {
            dg1.DataSource = DataHelper.XkfyData.Tables["ItemData"].DefaultView;
            ToolsHelper tl = new ToolsHelper();
            tl.UpdateCellName("ItemData", dg1);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string name = txtName.Text;
            string npcId = txtNpcId.Text;
            string where = "1 = 1";
            if (!string.IsNullOrEmpty(id))
            {
                where += " and iItemID$0 like '%" + id + "%' ";
            }

            if (!string.IsNullOrEmpty(name))
            {
                where += " and sItemName$1 like '%" + name + "%' ";
            }

            if (!string.IsNullOrEmpty(npcId))
            {
                where += " and sNpcLike$28 like '%" + npcId + "%' ";
            }
            DataView dv = dg1.DataSource as DataView;
            dv.RowFilter = where;
            this.dg1.DataSource = dv;
        }

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.dg1.CurrentRow;
            ItemDataEdit ie = new ItemDataEdit(dr,"0");
            ie.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ItemDataEdit ie = new ItemDataEdit("1");
            ie.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = this.dg1.CurrentRow;
            ItemDataEdit ie = new ItemDataEdit(dr,"2");
            ie.ShowDialog();
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

        private void dg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private DataRow _copyRow = null;
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

        private void tsmInsertCopyRow_Click(object sender, EventArgs e)
        {
            (dg1.DataSource as DataView).Table.Rows.InsertAt(_copyRow, dg1.CurrentRow.Index);
            dg1.Rows[dg1.CurrentRow.Index - 1].DefaultCellStyle.BackColor = Color.MistyRose;
            _copyRow = null;
        }

        private void tsmDelRow_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = MessageBox.Show("是否确定要删除当前选择的行？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogR == DialogResult.Yes)
            {
                dg1.Rows.Remove(this.dg1.CurrentRow);
            }
        }

        private void tsmInsertRow_Click(object sender, EventArgs e)
        {
            DataRow dr = (dg1.DataSource as DataView).Table.NewRow();
            (dg1.DataSource as DataTable).Rows.InsertAt(dr, dg1.CurrentCell.RowIndex);
            dg1.Rows[dg1.CurrentRow.Index - 1].DefaultCellStyle.BackColor = Color.MistyRose;
        }

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
        }

        private void dg1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (string.IsNullOrEmpty(dg1.Rows[e.RowIndex].Cells["rowState"].Value.ToString()))
                dg1.Rows[e.RowIndex].Cells["rowState"].Value = "0";
            dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PowderBlue;
        }

        private void dg1_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (dg1.CurrentRow == null)
            //    return;
            //StringBuilder sbExplain = new StringBuilder();
            //DataGridViewRow dv = dg1.CurrentRow;
            //if (dv.Cells[0].Value.ToString() == "")
            //    return;
            //string snpclike = dv.Cells["sNpcLike$28"].Value.ToString();
            //if (snpclike != "0" && snpclike != "")
            //{
            //    string[] snpclikeArray = snpclike.Split('*');
            //    for (int i = 0; i < snpclikeArray.Length; i++)
            //    {
            //        string[] str = snpclikeArray[i].Replace("(", "").Replace(")", "").Split(',');
            //        sbExplain.AppendFormat("送给【{0}】增加{1}点好感  ", DataHelper.GetValue("NpcData", "sNpcName", "iID", str[0]), str[1]);
            //        sbExplain.Append("\r\n");
            //    }
            //}
            //sbExplain.Append("\r\n");
            //string sUseLimit = dv.Cells["sUseLimit$29"].Value.ToString();
            //if (sUseLimit != "0" && !string.IsNullOrEmpty(sUseLimit))
            //{
            //    string[] useLimit = sUseLimit.Replace("(", "").Replace(")", "").Split(',');
            //    if (useLimit.Length < 3)
            //        return;
            //    string name = string.Empty;
            //    switch (useLimit[0])
            //    {
            //        case "0":
            //            name = DataHelper.GetDicValue("MB", useLimit[1]);
            //            break;
            //        case "1":
            //            name = DataHelper.GetDicValue("JBG", useLimit[1]);
            //            break;
            //        case "2":
            //            name = DataHelper.GetDicValue("ZS", useLimit[1]);
            //            break;
            //        case "4":
            //            name = DataHelper.GetDicValue("JY", useLimit[1]);
            //            break;
            //    }
            //    sbExplain.AppendFormat("使用条件【{0}】{1}", name, useLimit[2]);
            //}
            //txtExplain.Text = sbExplain.ToString();
        }
    }
}
