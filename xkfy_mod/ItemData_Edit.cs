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
    public partial class ItemDataEdit : Form
    {
        private ToolsHelper _tl = new ToolsHelper();
        public DataGridViewRow Dr = null;
        private readonly string _type;
        private readonly string _tableName;
        private readonly bool _isDlcFile = false;

        public ItemDataEdit(DataGridViewRow dr,string type)
        {
            this.Dr = dr;
            this._type = type;
            this._tableName = "ItemData";
            InitializeComponent();
        }

        public ItemDataEdit(DataGridViewRow dr, string type, string tableName)
        {
            this.Dr = dr;
            this._type = type;
            this._tableName = tableName;
            _isDlcFile = true;
            InitializeComponent();
        }

        public ItemDataEdit(string type)
        {
            this._type = type;
            InitializeComponent();
        }

        Dictionary<string, List<DicConfig>> _Arg = new Dictionary<string, List<DicConfig>>();
        private void ItemData_Edit_Load(object sender, EventArgs e)
        {
            List<UnionDropDown> list = DataHelper.ReadXmlToList<UnionDropDown>(CboData.Recover);
            foreach (UnionDropDown dc in list)
            {
                if (dc.TwoLevel.Count > 0)
                {
                    _Arg.Add(dc.Key, dc.TwoLevel);
                }
            }
            DataHelper.BinderComboBox(cboiRecover1,list);
            DataHelper.BinderComboBox(cboiRecover2, list);
            DataHelper.BinderComboBox(cboiRecover3, list);

            Dictionary<string, string> iItemKint = new Dictionary<string, string>();
            iItemKint.Add("0", "未分类");
            iItemKint.Add("1", "武器");
            iItemKint.Add("2", "防具");
            iItemKint.Add("3", "饰品");
            iItemKint.Add("4", "补药");
            iItemKint.Add("5", "任务");
            iItemKint.Add("6", "秘籍");
            DataHelper.BinderComboBox(cboiItemKint, iItemKint);

            //绑定武器类型
            //CboData.BindiWearAmsType(comboBox1);
            

            Dictionary<string, string> iAddType = new Dictionary<string, string>();
            iAddType.Add("2", "可装备");
            iAddType.Add("0", "其他");
            iAddType.Add("1", "营养鸡汤");
            DataHelper.BinderComboBox(cboiAddType, iAddType);

            Dictionary<string, string> iUseTime = new Dictionary<string, string>();
            iUseTime.Add("0", "无");
            iUseTime.Add("1", "非战斗");
            iUseTime.Add("2", "战斗中");
            iUseTime.Add("3", "无法使用");
            DataHelper.BinderComboBox(cboiUseTime, iUseTime);
            
            if (_type == "Add")
            {
                btnUpdate.Visible = false;
                btnSave.Visible = true;
            }
            else if (_type == "CopyAdd")
            {
                SetValue();
                btnUpdate.Visible = false;
                btnSave.Visible = true;
            }
            else
            {
                SetValue();
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }

            string path = Path.Combine(ToolsHelper.ExplainPath, _tableName + ".xml");
            DataHelper.SetLabelTextBox(this, Dr, _type, path, btnUpdate, btnSave,"");
        }

        public void SetValue()
        {
            DataHelper.CopyRowToData(this, Dr);
            DataHelper.CopyRowToData(gbCondition, Dr);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemID.Text))
            {
                lblMsg.Text = "请输入ID";
                return;
            }

            DataRow[] addItem = DataHelper.XkfyData.Tables[_tableName].Select("@iItemID$0='" + txtItemID.Text + "'");
            if (addItem.Length > 0)
            {
                lblMsg.Text = "此ID在源数据中已经存在,请修改ID，并确保在该文件此ID是唯一的";
                return;
            }
            


            DataRow dr1 = DataHelper.XkfyData.Tables[_tableName].NewRow();
            _tl.CopyRowToData(gbCondition, dr1);
            DataHelper.CopyDataToRow(this, dr1);
            if (string.IsNullOrEmpty(dr1["rowState"].ToString()))
                dr1["rowState"] = "1";
            DataHelper.XkfyData.Tables[_tableName].Rows.InsertAt(dr1, 0);
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemID.Text))
            {
                lblMsg.Text = "请输入ID";
                return;
            }
            if (txtItemID.Text != Dr.Cells["@iItemID$0"].Value.ToString())
            {
                DataRow[] addItem = DataHelper.XkfyData.Tables[_tableName].Select("@iItemID$0='" + txtItemID.Text + "'");
                if (addItem.Length > 0)
                {
                    lblMsg.Text = "此ID在源数据中已经存在,请修改ID，并确保在该文件是唯一的";
                    return;
                }
            }
            _tl.CopyRowToData(gbCondition, Dr);
            _tl.updateData(this, Dr);
        }

        private void cboiRecover1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboiArg1.Visible = true;
                cboiArg2.Visible = true;
                cboiArg3.Visible = true;

                string iRecover = ((ComboBox)sender).SelectedValue.ToString();
                string tag = ((ComboBox)sender).Tag.ToString();
                switch (iRecover)
                {
                    case "1":
                        if (_Arg.ContainsKey(iRecover))
                        {
                            switch (tag)
                            {
                                case "iRecover1$13":
                                    DataHelper.BinderComboBox(cboiArg1, _Arg[iRecover]);
                                    break;
                                case "iRecover2$17":
                                    DataHelper.BinderComboBox(cboiArg2, _Arg[iRecover]);
                                    break;
                                case "iRecover3$21":
                                    DataHelper.BinderComboBox(cboiArg3, _Arg[iRecover]);
                                    break;
                            }
                        }
                        break;
                    default:
                        switch (tag)
                        {
                            case "iRecover1$13":
                                cboiArg1.Visible = false;
                                break;
                            case "iRecover2$17":
                                cboiArg2.Visible = false;
                                break;
                            case "iRecover3$21":
                                cboiArg3.Visible = false;
                                break;
                        }
                        break;

                }

                switch (tag)
                {
                    case "iRecover1$13":
                        txtiRecover1.Text = iRecover;
                        break;
                    case "iRecover2$17":
                        txtiRecover2.Text = iRecover;
                        break;
                    case "iRecover3$21":
                        txtiRecover3.Text = iRecover;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboiArg1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string iArg = ((ComboBox)sender).SelectedValue.ToString();
            string tag = ((ComboBox)sender).Tag.ToString();
            switch (tag)
            {
                case "iArg1$14":
                    txtiArg1.Text = iArg;
                    break;
                case "iArg2$18":
                    txtiArg2.Text = iArg;
                    break;
                case "iArg3$22":
                    txtiArg3.Text = iArg;
                    break;
            }
        }

        private void btnSel1_Click(object sender, EventArgs e)
        {
            _tl.GetStringTable(textBox19,null);
        }

        private void btnSel2_Click(object sender, EventArgs e)
        {
            _tl.GetStringTable(textBox25, null); 
        }

        private void btnSel3_Click(object sender, EventArgs e)
        {
            _tl.GetStringTable(textBox21, null);
        }
    }
}
