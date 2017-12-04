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
    public partial class FrmSetCbo : Form
    {
        string _path = string.Empty;
        List<DicConfig> _menuList;
        private ComboBox _cb;
        public FrmSetCbo(ComboBox cb, string path)
        {
            this._path = path;
            this._cb = cb;
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
            CboData.BindiComboBox(_cb, _path);
            MessageBox.Show("修改成功！");
            this.Close();
        }
    }
}
