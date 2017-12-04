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
    public partial class AttributeList : Form
    {
        private string _fileName = string.Empty;
        private TextBox _txtId = null;
        private TextBox _txtName = null;
        public AttributeList(string fileName,TextBox txtId,TextBox txtName)
        {
            this._txtId = txtId;
            this._txtName = txtName;
            this._fileName = fileName;
            InitializeComponent();
        }

        private void AttributeList_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "/工具配置文件/";
            List<Menu> list = XmlHelper.XmlDeserializeFromFile<List<Menu>>(path + _fileName, Encoding.UTF8);
            foreach (Menu ls in list)
            {
                TreeNode node = new TreeNode();
                node.Text = ls.MenuText;
                node.Tag = ls.MenuTag;
                foreach (KeyValuePair<string, string> dic in DataHelper.ExplainConfig[ls.MenuName])
                {
                    TreeNode chldNode = new TreeNode();
                    chldNode.Text = dic.Value;
                    chldNode.Tag = dic.Key;
                    node.Nodes.Add(chldNode);
                }
                treeView1.Nodes.Add(node);
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode currentNode = e.Node;
            if (e.Node.Parent != null)
            {
                _txtId.Text = currentNode.Parent.Tag + "," + currentNode.Tag;
                _txtName.Text = currentNode.Text;
                this.Close();
            }
        }
    }
}
