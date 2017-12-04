using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xkfy_mod
{
    public partial class Test : Form
    {
        private DataTable _dt = null;
        public Test()
        {
            InitializeComponent();
        }

        private void test_Load(object sender, EventArgs e)
        {
            string _path = Application.StartupPath + "\\工具配置文件\\TableConfig.xml";
            List<MyConfig> _menuList = XmlHelper.XmlDeserializeFromFile<List<MyConfig>>(_path, Encoding.UTF8);
            BindingList<MyConfig> bl = new BindingList<MyConfig>(_menuList);

            dg1.DataSource = bl;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
            //string rowState = "";
            //if (dg1.Rows[e.RowIndex].Cells["rowState"].Value != null)
            //{
            //    rowState = dg1.Rows[e.RowIndex].Cells["rowState"].Value.ToString();
            //    if (rowState == "0")
            //    {
            //        dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PowderBlue;
            //    }
            //    else if (rowState == "1")
            //    {
            //        dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
            //    }
            //}
        }

        private void dg1_MouseDown(object sender, MouseEventArgs e)
        {
            //调用DoDragDrop方法
            if (this.dg1.SelectedRows != null)
            {
                this.dg1.DoDragDrop(this.dg1.SelectedRows, DragDropEffects.Copy);
            }
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            DataGridViewSelectedRowCollection item = (DataGridViewSelectedRowCollection)e.Data.GetData(e.Data.GetFormats()[0]);

            textBox1.Text = item.ToString();
        }
    }
}
