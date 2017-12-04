using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class AlmightyMap : DockContent
    {
        private string _tbName = null;
        private string _editForm = string.Empty;
        public AlmightyMap(string tbName)
        {
            this._tbName = tbName;
            InitializeComponent();
        }

        private void Almighty_Load(object sender, EventArgs e)
        {
            dg1.AllowUserToAddRows = false;
            if (_tbName.Substring(0, 3).ToUpper() == "MAP")
            {
                lbl1.Text = "iNpcID";
                lbl2.Text = "sCoordinate";
                txtID.Tag = "iNpcID";
                txtName.Tag = "sCoordinate";
            }
            else
            {
                lbl1.Text = "iNpcID";
                lbl2.Text = "sConduct";
                txtID.Tag = "iNpcID";
                txtName.Tag = "sConduct";
            }

            foreach (DataRow dr in DataHelper.MapData.Tables[_tbName].Rows)
            {
                if (dr["iNpcID"] == null)
                    continue;
                string iNpcId = dr["iNpcID"].ToString();
                if (iNpcId.Length >= 9)
                {
                    iNpcId = iNpcId.Substring(0, iNpcId.Length - 3);
                }
                dr["npcName"] = DataHelper.GetValue("NpcData", "sNpcName", "iID", iNpcId);
            }
            BulidSn(DataHelper.MapData.Tables[_tbName]);
            dg1.DataSource = DataHelper.MapData.Tables[_tbName].DefaultView;
        }

        private void BulidSn(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["indexSn"] = i;
            }
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
            if (dg1.Rows[e.RowIndex].Cells["rowState"].Value != null) {
                rowState = dg1.Rows[e.RowIndex].Cells["rowState"].Value.ToString();
                if (rowState == "0")
                {
                    dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PowderBlue;
                }
                else if(rowState == "1")
                {
                    dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string name = txtName.Text;
            string where = "1 = 1";
            if (!string.IsNullOrEmpty(id))
            {
                where += " and " + txtID.Tag + " like '%" + id + "%' ";
            }

            if (!string.IsNullOrEmpty(name))
            {
                where += " and " + txtName.Tag + " like '%" + name + "%' ";
            }
            DataView dv = dg1.DataSource as DataView;
            dv.RowFilter = where;
            this.dg1.DataSource = dv;
        }

        private void dg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (_tbName.Substring(0, 3).ToUpper() != "MAP")
            //    return;
            //StringBuilder sbExplain = new StringBuilder();
            //DataGridViewRow dv = dg1.CurrentRow;
            //if (dv.Cells["iNpcID"].Value.ToString() == "")
            //    return;
            //DataTable dt = DataHelper.XkfyData.Tables["Config"];
            //string sSCondition = dv.Cells["sSCondition"].Value.ToString();
            //string[] sCondition = sSCondition.Split(',');

            //string iNpcId = dv.Cells["iNpcID"].Value.ToString();
            //string npcName = DataHelper.GetValue("NpcData", "sNpcName", "iID", iNpcId.Substring(0, iNpcId.Length - 3));
            //switch (sCondition[0])
            //{
            //    case "2":
            //        sbExplain.AppendFormat("出现条件:【{0}】好感度{1}", npcName, sCondition[1]);
            //        break;
            //    case "6":
            //        btnDeBug.Visible = true;
            //        txtTalkId.Text = sCondition[1];
            //        sbExplain.AppendFormat("出现条件:完成前置事件【{0}】【{1}】好感度{2}", sCondition[1], npcName, sCondition[3]);
            //        break;
            //}
            //lblExplain.Text = sbExplain.ToString();

        }

        private void tsmInsertRow_Click(object sender, EventArgs e)
        {
            DataRow dr = (dg1.DataSource as DataView).Table.NewRow();
            (dg1.DataSource as DataView).Table.Rows.InsertAt(dr, dg1.CurrentRow.Index);
            dg1.Rows[dg1.CurrentRow.Index-1].DefaultCellStyle.BackColor = Color.MistyRose;
        }

        private void dg1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.DefaultCellStyle.BackColor = Color.MistyRose;
        }

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

        private DataRow _copyRow = null;
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

        private void tsmInsertCopyRow_Click(object sender, EventArgs e)
        {
            int sn = Convert.ToInt16(dg1.CurrentRow.Cells["indexSn"].Value);
            _copyRow["indexSn"] = (sn + 1).ToString();
            (dg1.DataSource as DataView).Table.Rows.InsertAt(_copyRow, sn + 1);
            _copyRow = null;
            BulidSn(DataHelper.MapData.Tables[_tbName]);
        }

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //修改
            ShowForm("0");
        }

        private void btnCopyAdd_Click(object sender, EventArgs e)
        {
            //复制新增
            ShowForm("2");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //新增
            ShowForm("1");
        }
        private void ShowForm(string type)
        {
            if (!string.IsNullOrEmpty(_editForm) && _editForm != "0")
            {
                DataGridViewRow dr = this.dg1.CurrentRow;

                //利用反射实例化窗口
                Type t = Type.GetType("xkfy_mod." + _editForm);//窗体名要加上程序集名称
                Form f = (Form)Activator.CreateInstance(t, new object[] { dr, type });
                f.ShowDialog();
            }
            else
            {
                DataGridViewRow dr = dg1.CurrentRow;
                AlmightyMapEdit ae = new AlmightyMapEdit(dr, _tbName,type);
                ae.ShowDialog();
            }
            dg1.DataSource = DataHelper.MapData.Tables[_tbName].DefaultView;
        }

        private void tsmDelRow_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = MessageBox.Show("是否确定要删除当前选择的行？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogR == DialogResult.Yes)
            {
                dg1.Rows.Remove(this.dg1.CurrentRow);
            }
        }

        private void btnDeBug_Click(object sender, EventArgs e)
        {
            TalkDeBug td = new TalkDeBug(txtTalkId.Text);
            td.Show();
        }

        private void dg1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PowderBlue;
        }
    }
}
