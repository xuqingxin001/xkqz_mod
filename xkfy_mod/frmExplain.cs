using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class FrmExplain : DockContent
    {
        /// <summary>
        /// 存储表格类型
        /// </summary>
        private Dictionary<string, string> _tbConfig = null;
        public FrmExplain()
        {
            InitializeComponent();
            tvMenu.HideSelection = false;
            //自已绘制
            this.tvMenu.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.tvMenu.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);
        }

        private void formExplain_Load(object sender, EventArgs e)
        {
            List<MyConfig> list = XmlHelper.XmlDeserializeFromFile<List<MyConfig>>(Application.StartupPath + "/工具配置文件/TableConfig.xml", Encoding.UTF8);
            List<Menu> menuList = XmlHelper.XmlDeserializeFromFile<List<Menu>>(Application.StartupPath + "/工具配置文件/MenuConfig.xml", Encoding.UTF8);

            string filePath = DataHelper.FilePath;

            foreach (Menu m in menuList)
            {
                TreeNode node = new TreeNode();
                node.Text = m.MenuText;
                node.Tag = m.MenuTag;
                node.Name = m.MenuName;
                tvMenu.Nodes.Add(node);
            }

            _tbConfig = new Dictionary<string, string>();
            foreach (MyConfig item in list)
            {
                TreeNode chldNode = new TreeNode();
                chldNode.Text = item.Notes + "(" + item.TxtName + ")";
                chldNode.Tag = item.MainDtName;
                tvMenu.Nodes[item.Classify].Nodes.Add(chldNode);
                _tbConfig.Add(item.MainDtName, item.DtType);
            }
        }

        private void tvMenu_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode currentNode = e.Node;
            if (currentNode.Parent == null)
            {
                return;
            }

            string fileName = e.Node.Tag.ToString()+".xml";
            _path = Path.Combine(Application.StartupPath+ "\\工具配置文件\\TableExplain", fileName);
            _menuList = XmlHelper.XmlDeserializeFromFile<List<TableExplain>>(_path, Encoding.UTF8);
            BindingList<TableExplain> bl = new BindingList<TableExplain>(_menuList);

            dg1.DataSource = bl;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        string _path = string.Empty;
        List<TableExplain> _menuList;
        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlHelper.XmlSerializeToFile(_menuList, _path, Encoding.UTF8);
            MessageBox.Show("修改成功！");
        }

        #region 单机更改颜色
        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                //显示为玫瑰红
                e.Graphics.FillRectangle(Brushes.MistyRose, e.Node.Bounds);

                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.Black, Rectangle.Inflate(e.Bounds, 2, 0));
            }
            else
            {
                e.DrawDefault = true;
            }

            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusBounds = e.Node.Bounds;
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }

        }
        #endregion
    }


}
