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
    public partial class FrmSetUp : Form
    {
        string _path = string.Empty;
        List<DicConfig> _menuList;
        public FrmSetUp(string path)
        {
            this._path = path;
            InitializeComponent();
        }

        private void frmSetUp_Load(object sender, EventArgs e)
        {
            _menuList = XmlHelper.XmlDeserializeFromFile<List<DicConfig>>(_path, Encoding.UTF8);
            BindingList<DicConfig> bl = new BindingList<DicConfig>(_menuList);

            dg1.DataSource = bl;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlHelper.XmlSerializeToFile(_menuList, _path, Encoding.UTF8);
            MessageBox.Show("修改成功！");
        }
    }
}
