using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xkfy_mod
{
    public partial class FrmTbConfig : Form
    {
        public FrmTbConfig()
        {
            InitializeComponent();
        }

        string _path = string.Empty;
        List<MyConfig> _menuList;
        private void FrmTbConfig_Load(object sender, EventArgs e)
        {
            _path = Application.StartupPath + "\\工具配置文件\\TableConfig.xml";
            _menuList = XmlHelper.XmlDeserializeFromFile<List<MyConfig>>(_path, Encoding.UTF8);
            BindingList<MyConfig> bl = new BindingList<MyConfig>(_menuList);

            dg1.DataSource = bl;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlHelper.XmlSerializeToFile(_menuList, _path, Encoding.UTF8);
            MessageBox.Show("修改成功！");
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string fileName = txtFileName.Text;
            string menuType = txtMenuType.Text;
            string where = "1 = 1";
            if (!string.IsNullOrEmpty(fileName))
            {
                where += " and " + txtFileName.Tag + " like '%" + fileName + "%' ";
            }

            if (!string.IsNullOrEmpty(menuType))
            {
                where += " and " + txtMenuType.Tag + " like '%" + menuType + "%' ";
            }
            DataView dv = dg1.DataSource as DataView;
            dv.RowFilter = where;
            this.dg1.DataSource = dv;
        }
    }
}
