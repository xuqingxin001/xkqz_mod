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
    public partial class AlmightyMapEdit : Form
    {
        private DataGridViewRow _dr = null;
        private string _tbName = null;
        private string _type = null;
        public AlmightyMapEdit(DataGridViewRow dr, string tbName,string type)
        {
            this._dr = dr;
            this._tbName = tbName;
            this._type = type;
            InitializeComponent();
        }

        private void AlmightyMap_Edit_Load(object sender, EventArgs e)
        {
            DataTable dt = DataHelper.MapData.Tables[_tbName];
            
            string path = Path.Combine(ToolsHelper.ExplainPath,"Map_icon.xml");
            Dictionary<string, TableExplain> dlc = new Dictionary<string, TableExplain>();

            if (File.Exists(path))
            {
                List<TableExplain> list = DataHelper.ReadXmlToList<TableExplain>(path);
                foreach (TableExplain item in list)
                {
                    if (!dlc.ContainsKey(item.ToolsColumn))
                    {
                        dlc.Add(item.ToolsColumn, item);
                    }
                }
            }

            _tl.CreateCtr(dt, this, _dr, dlc, toolTip);
            if (_type == "0")
            {
                btnUpdate.Visible = true;
            }
            else
            {
                btnAdd.Visible = true;
            }
        }

        private ToolsHelper _tl = new ToolsHelper();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            _tl.AddData(this, _tbName);
            //DataRow dr = DataHelper.mapData.Tables[tbName].NewRow();
            //DataHelper.CopyDataToRow(this, dr);
            //if (string.IsNullOrEmpty(dr["rowState"].ToString()))
            //    dr["rowState"] = "1";
            //DataHelper.mapData.Tables[tbName].Rows.InsertAt(dr, 0);

            //this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _tl.updateData(this, _dr);
            //DataHelper.CopyDataToRow(this, dr);
            //if (string.IsNullOrEmpty(dr.Cells["rowState"].Value.ToString()))
            //    dr.Cells["rowState"].Value = "0";
            //this.Close();
        }
    }
}
