using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Data;
using System.Collections;
using xkfy_mod.Config;
using System.Configuration;

namespace xkfy_mod
{
    public partial class Dock : DockContent
    {
        public Dock()
        {
            InitializeComponent();
            MenuTree.HideSelection = false;
            //自已绘制
            this.MenuTree.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.MenuTree.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);
        }

        private void Dock_Load(object sender, EventArgs e)
        {
            try
            {
                //如果文件目录不存在提示出错直接返回
                if (!Directory.Exists(DataHelper.FilePath) || !Directory.Exists(DataHelper.FilePath))
                {
                    MessageBox.Show(DataHelper.FilePath + "目录不存在！");
                    return;
                }

                //加载所有表配置
                List<MyConfig> list = XmlHelper.XmlDeserializeFromFile<List<MyConfig>>(Application.StartupPath + "/工具配置文件/TableConfig.xml", Encoding.UTF8);
                //加载左侧菜单数据
                List<Menu> menuList = XmlHelper.XmlDeserializeFromFile<List<Menu>>(Application.StartupPath + "/工具配置文件/MenuConfig.xml", Encoding.UTF8);

                ToolsHelper tl = new ToolsHelper();
                string filePath = DataHelper.FilePath;
                DirectoryInfo di = new DirectoryInfo(filePath);
                DataHelper.GetAllFile(di, DataHelper.DicFile);

                foreach (Menu m in menuList)
                {
                    TreeNode node = new TreeNode();
                    node.Text = m.MenuText;
                    node.Tag = m.MenuTag;
                    node.Name = m.MenuName;
                    MenuTree.Nodes.Add(node);
                }

                
                #region 循环窗体配置，并且保存到静态变量中去
                //循环窗体配置
                foreach (MyConfig item in list)
                {
                    
                    if (!DataHelper.DicFile.ContainsKey(item.TxtName))
                    {
                        if (item.TxtName == "NpcProduct.txt" || item.TxtName == "MapInfo.txt")
                        {
                            continue;
                        }
                        DataHelper.newFilesInfo.AppendFormat("文件名：{0}没有对应的配置文件!\r\n", item.TxtName);
                        continue;
                    }
                    TreeNode chldNode = new TreeNode();
                    chldNode.Text = item.Notes + "(" + item.TxtName + ")";
                    chldNode.Tag = item.MainDtName;
                    chldNode.Name = item.Notes + "(" + item.TxtName + ")";
                    MenuTree.Nodes[item.Classify].Nodes.Add(chldNode);

                    //保存窗体配置文件
                    DataHelper.FormConfig.Add(item.MainDtName, item);
                    DataHelper.ConfigFile.Add(item.TxtName,item.TxtName);
                }
                #endregion

                //foreach (KeyValuePair<string, string> files in DataHelper.DicFile)
                //{
                //    if(!DataHelper.ConfigFile.ContainsKey(files.Key))
                //    {
                //        DataHelper.newFilesInfo.AppendFormat("文件名：{0}没有对应的配置文件!\r\n", files.Value);
                //    }
                //}

                if (DataHelper.DicFile.ContainsKey("MapID.txt"))
                {
                    string mapId = DataHelper.DicFile["MapID.txt"];
                    //读取地图文件,以ID为KEY的字典
                    DataHelper.readConfig(mapId, "mapId");
                    //循环所有地图,加入到左侧菜单
                    foreach (KeyValuePair<string, string> map in DataHelper.SelItem["mapId"])
                    {
                        TreeNode node = new TreeNode();
                        node.Text = map.Value;
                        node.Tag = "map";
                        node.Name = map.Key;
                        MenuTree.Nodes["map"].Nodes.Add(node);
                    }

                    //读取所有地图文件
                    DirectoryInfo dfMap = new DirectoryInfo(DataHelper.MapPath);
                    DataHelper.GetMapIconFile(dfMap, DataHelper.MapFile);

                    //循环所有地图文件
                    foreach (KeyValuePair<string, string> map in DataHelper.MapFile)
                    {
                        //截取文件名称的关键信息，和大地图匹配
                        string key = map.Key.Substring(9, 8);
                        

                        //保存文件名称，作为数据名称
                        string tableName = map.Key.Substring(0, map.Key.LastIndexOf('.'));

                        //判断地图集合是否存在地图文件
                        if (!DataHelper.SelItem["mapId"].ContainsKey(key))
                        {
                            //截取NPC信息
                            key = map.Key.Substring(11, 8);
                            if (!DataHelper.SelItem["mapId"].ContainsKey(key))
                            {
                                continue;
                            }
                        }
                        

                        TreeNode chldNode = new TreeNode();
                        chldNode.Text = map.Key;
                        chldNode.Tag = tableName;
                        chldNode.Name = map.Key;
                        MenuTree.Nodes["map"].Nodes[key].Nodes.Add(chldNode);
                    }
                }
                else
                {
                    MessageBox.Show("mod文件夹中缺少Mapid.txt文件，无法正确加载地图文件！");
                }

                //读取下拉框联动配置信息
                DataHelper.readConfig();
                //设置琴棋书画等数值
                DataHelper.SetDicValue();
                //设置回合对应的年月日
                DataHelper.SetHuiHeDicValue();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Menu_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode currentNode = e.Node;
                //如果双击的节点的父节点是空
                if (currentNode.Parent == null || currentNode.Parent.Text == "地图信息")
                {
                    return;
                }

                //循环已经打开过的串口
                foreach (DockContent frm in this.DockPanel.Contents)
                {
                    //如果当前窗口已经打开过了
                    if (frm.Text == currentNode.Text)
                    {
                        //激活窗口
                        frm.Activate();
                        return;
                    }
                }
                string tbName = currentNode.Tag.ToString();
                //反射实例化窗体
                DockContent dc = (DockContent)Assembly.Load("xkfy_mod").CreateInstance("xkfy_mod." + tbName);
                //如果对象为空，则代表使用的是公共窗口
                if (dc == null)
                {
                    //如果节点的父节点的tag等于map代表，是地图文件和NPC动作文件
                    if (currentNode.Parent.Tag.ToString() == "map")
                    {
                        string fileName = string.Empty;
                        string preFix = string.Empty;
                        //如果是第一次打开窗口,读取文件
                        if (!DataHelper.MapData.Tables.Contains(tbName))
                        {
                            string typeName = currentNode.Text.Substring(0, 3);
                            //如果文件名称的前3个字符等于MAP
                            if (typeName.ToUpper() == "MAP")
                            {
                                fileName = "MapInfo";
                                preFix = "[M]";
                                //读取MAP文件
                                ToolsHelper.ReadMapData(DataHelper.MapFile[currentNode.Text], tbName, fileName);
                            }
                            else
                            {
                                fileName = "NpcProduct";
                                preFix = "[N]";
                                //读取NpcConduct
                                ToolsHelper.ReadMapData(DataHelper.MapFile[currentNode.Text], tbName, fileName);
                            }
                        }
                        
                        Almighty a = new Almighty(tbName, fileName);
                        a.Text = preFix + currentNode.Text;
                        a.Tag = tbName;
                        a.Show(this.DockPanel, DockState.Document);
                    }
                    else
                    {
                        DataHelper.ExistTable(tbName);
                        Almighty a = new Almighty(tbName,"other");
                        a.Text = currentNode.Text;
                        a.Tag = tbName;
                        a.Show(this.DockPanel, DockState.Document);
                    }
                }
                else
                {
                    DataHelper.ExistTable(tbName);
                    //显示窗口
                    dc.Text = currentNode.Text;
                    dc.Tag = tbName;
                    dc.Show(this.DockPanel, DockState.Document);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        public void SetSelNode(string text)
        {
            TreeNode[] tn = MenuTree.Nodes.Find(text, true);
            if (tn.Length > 0)
            {
                MenuTree.SelectedNode = tn[0];
            }
        }
    }
}
