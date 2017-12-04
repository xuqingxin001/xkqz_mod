using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class AlmightyEdit : Form
    {
        private ToolsHelper _tl = new ToolsHelper();
        private DataGridViewRow _dr = null;
        private string _tbName = null;
        private string _type = string.Empty;
        private string _fileType = string.Empty;
        public AlmightyEdit(DataGridViewRow dr, string tbName,string type, string fileType)
        {
            this._dr = dr;
            this._tbName = tbName;
            this._type = type;
            _fileType = fileType;
            InitializeComponent();
        }
        public static int GetTextBoxLength(string textboxTextStr)
        {
            int nLength = 0;
            for (int i = 0; i < textboxTextStr.Length; i++)
            {
                if (textboxTextStr[i] >= 0x3000 && textboxTextStr[i] <= 0x9FFF)
                    nLength++;
            }
            return nLength;
        }

        private void Almighty_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                if (_fileType == "other")
                {
                    dt = DataHelper.XkfyData.Tables[_tbName];
                }
                else
                {
                    dt = DataHelper.MapData.Tables[_tbName];
                }

                Dictionary<string, TableExplain> dlc = new Dictionary<string, TableExplain>();

                string path = string.Empty;
                if (_fileType == "MapInfo")
                {
                    path = Path.Combine(ToolsHelper.ExplainPath, "MapInfo.xml");
                }
                else if (_fileType == "NpcProduct")
                {
                    path = Path.Combine(ToolsHelper.ExplainPath, "NpcProduct.xml");
                }
                else
                {
                    path = Path.Combine(ToolsHelper.ExplainPath, _tbName + ".xml");
                }

                if (File.Exists(path))
                {
                    dlc = DataHelper.GetTableExplainList(path);
                }

                ToolsHelper tl = new ToolsHelper();
                tl.CreateCtr(dt, this, _dr, dlc, toolTip);


                if (_type == "Modify")
                {
                    btnUpdate.Visible = true;
                }
                else
                {
                    btnAdd.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _tl.updateData(this, _dr);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _tl.AddData(this, _tbName);
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void BtnSel(string[] row,TextBox txtValue, TextBox txtName)
        {
            RadioList rl = new RadioList(_tbName, row, txtValue, txtName, "1");
            rl.ShowDialog();
        }
    }
}
