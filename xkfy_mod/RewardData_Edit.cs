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
    public partial class RewardDataEdit : Form
    {
        private ToolsHelper _tl = new ToolsHelper();
        private DataGridViewRow _dr = null;
        private string _type = string.Empty;
        private readonly string _tableName;
        private readonly bool _isDlcFile = false;

        public RewardDataEdit(DataGridViewRow dr, string type)
        {
            this._dr = dr;
            this._type = type;
            this._tableName = "RewardData";
            InitializeComponent();
        }

        public RewardDataEdit(DataGridViewRow dr, string type, string tableName)
        {
            this._dr = dr;
            this._type = type;
            this._tableName = tableName;
            _isDlcFile = true;
            InitializeComponent();
        }

        private void RewardData_Edit_Load(object sender, EventArgs e)
        {
            List<DicConfig> list = DataHelper.ReadXmlToList<DicConfig>(CboData.RewardData);
            DataHelper.BinderComboBox(cboItem, list);

            if (_type == "Modify")
            {
                //修改
                DataHelper.CopyRowToData(this, _dr);
                btnUpdate.Visible = true;
            }
            else if (_type == "Add")
            {
                //新增
                btnAdd.Visible = true;
            }
            else
            {
                //复制新增
                DataHelper.CopyRowToData(this, _dr);
                btnAdd.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToolsHelper tl = new ToolsHelper();
            string[] sRewardData = txtsRewardData.Text.Split('*');
            lblExplain.Text = ExplainHelper.ExplainRewardData(sRewardData);
        }

        private void cboItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lbl = string.Empty;
            btnSel2.Visible = false;
            textBox5.Text = "0";
            lbl4.Text = "提示语";
            lbl3.Text = "值";
            lbl5.Text = "第五个值";
            btnSelMsgStr2.Visible = false;
            txtSelValue2.Text = "";

            switch (cboItem.SelectedValue.ToString())
            {
                case "2":
                case "3":
                case "4":
                case "6":
                case "7":
                case "15":
                case "18":
                    lbl = "NPC名称";
                    btnSel2.Visible = true;
                    break;
                case "5":
                case "21":
                    lbl = "物品名称";
                    btnSel2.Visible = true;
                    break;
                case "12":
                    lbl = "对话ID";
                    break;
                case "19":
                    lbl = "事件ID";
                    btnSel2.Visible = true;
                    break;
            }
            
            switch (cboItem.SelectedValue.ToString())
            {
                case "2":
                    txtMsgStr.Text = "0";
                    textBox5.Text = "1";
                    lbl5.Text = "层数？";
                    btnSelMsgStr2.Visible = true;
                    break;
                case "3":
                    txtMsgStr.Text = "0";
                    textBox5.Text = "1";
                    lbl5.Text = "层数？";
                    btnSelMsgStr2.Visible = true;
                    break;
                case "4":
                    txtMsgStr.Text = "0";
                    break;
                case "6":
                case "7":
                    break;
                case "9":
                    txtMsgStr.Text = "100206";
                    txtMsgStrText.Text = "你的阅历增长了{0}点";
                    break; 
                case "10":
                    txtSelValue2.Text = "0";
                    txtMsgStr.Text = "210040";
                    txtMsgStrText.Text = "全体队员修练经验增加{0}点";
                    break;
                case "15":
                    btnSelMsgStr2.Visible = true;
                    break;
                case "18":
                    break;
                case "5":
                    lbl3.Text = "数量";
                    txtMsgStrText.Text = "获得 {0} {1}个";
                    break;
                case "21":
                    
                    break;
                case "12":
                    break;
                case "19":
                    break;
            }


            lbl1.Text = lbl;
            txtType.Text = cboItem.SelectedValue.ToString();
        }

        private void SetSel1()
        {
            btnSel2.Visible = false;
            txtSelValue2.Enabled = false;
            txtSelValue2.Text = "0";
        }

        private void btnSel2_Click(object sender, EventArgs e)
        {
            string[] row = null;
            string tbName = string.Empty;
            switch (cboItem.SelectedValue.ToString())
            {
                case "2":
                case "3":
                case "4":
                case "6":
                case "7":
                case "15":
                case "18":
                    row = new string[4] { "@CharID$0", "Helper", "vHp$6", "vSp$7" };
                    tbName = "CharacterData";
                    if (_isDlcFile)
                    {
                        tbName = "DLC_CharacterData";
                    }
                    break;
                case "5":
                case "21":
                    row = new string[2] { "@iItemID$0", "sItemName$1"};
                    tbName = "ItemData";
                    if (_isDlcFile)
                    {
                        tbName = "DLC_ItemData";
                    }
                    break;
                case "19":
                    row = new string[2] { "@sID", "sQuestName" };
                    tbName = "QuestDataManager";
                    //if (_isDlcFile)
                    //{
                    //    tbName = "DLC_ItemData";
                    //}
                    break;
            }
            if (row != null)
            {
                RadioList rl = new RadioList(tbName, row, txtSelValue2, txtSelName2, "1");
                rl.ShowDialog();
            }
        }

        private void btnAddSel2_Click(object sender, EventArgs e)
        {
            StringBuilder sbItem = new StringBuilder();
            if (!string.IsNullOrEmpty(txtsRewardData.Text))
            {
                sbItem.Append("*");
            }
            sbItem.AppendFormat("({0},{1},{2},{3},{4})", cboItem.SelectedValue,txtSelValue2.Text, txtValue2.Text, txtMsgStr.Text, textBox5.Text);
            txtsRewardData.Text += sbItem.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _tl.AddData(this, _tableName);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _tl.updateData(this, _dr);
        }

        private void btnSelMsgStr2_Click(object sender, EventArgs e)
        {
            string[] row = null;
            string tbName = string.Empty;
            switch (cboItem.SelectedValue.ToString())
            {
                case "2":
                    row = new string[2] { "@iRoutineID", "sRoutineName" };
                    tbName = "RoutineNewData";
                    if (_isDlcFile)
                    {
                        tbName = "DLC_RoutineNewData";
                    }
                    break;
                case "3":
                    row = new string[2] { "@ID$0", "sNpcName"};
                    tbName = "NeigongData";
                    if (_isDlcFile)
                    {
                        tbName = "DLC_NeigongData";
                    }
                    break;
                case "15":
                    row = new string[2] { "sTalenName$1", "@iTalenID$0" };
                    tbName = "TalentNewData";
                    if (_isDlcFile)
                    {
                        tbName = "DLC_TalentNewData";
                    }
                    break;
            }
            if (row != null)
            {
                RadioList rl = new RadioList(tbName, row, txtValue2, txtValue2Text, "1");
                rl.ShowDialog();
            }
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            string[] row = new string[2] { "@iID", "sString" };
            RadioList rl = new RadioList("String_table", row, txtMsgStr, txtMsgStrText, "1");
            rl.ShowDialog();
        }
    }
}
