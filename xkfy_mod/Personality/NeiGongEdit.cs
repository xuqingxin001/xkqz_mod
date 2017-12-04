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
    public partial class NeiGongEdit : DockContent
    {
        private ToolsHelper _tl = new ToolsHelper();
        private DataGridViewRow _dr = null;
        private string _type = string.Empty;
        public NeiGongEdit(DataGridViewRow dr, string type)
        {
            InitializeComponent();
            _type = type;
            _dr = dr;
        }

        private void NeiGongEdit_Load(object sender, EventArgs e)
        {
            string path = Path.Combine(ToolsHelper.ExplainPath, "NeigongData.xml");
            DataHelper.SetLabelTextBox(this,_dr,_type,path,btnUpdate,btnAdd);

            textBox17.Text = DataHelper.GetConditionName(textBox9.Text);
            textBox16.Text = DataHelper.GetConditionName(textBox11.Text);
            textBox18.Text = DataHelper.GetConditionName(textBox13.Text);
            textBox19.Text = DataHelper.GetConditionName(textBox15.Text);

            Dictionary<string, string> ngLevel = new Dictionary<string, string>();
            for (int i = 1; i <= 10; i++)
            {
                string key = $"(10,0,{textBox1.Text},{i})";
                ngLevel.Add(key, "第"+i+"层");
            }
            DataHelper.BinderComboBox(comboBox1, ngLevel);
            DataHelper.BinderComboBox(comboBox2, ngLevel);
            DataHelper.BinderComboBox(comboBox3, ngLevel);

            CboData.BindiComboBox(comboBox4, CboData.NeiGongPath);
            CboData.BindiComboBox(comboBox5, CboData.NeiGongPath);

            DataHelper.SetDicConfig("NeiGong.Attribute", CboData.NeiGongPath);
            textBox20.Text = DataHelper.GetNeiGongLevelAttribute(textBox5.Text);
            textBox23.Text = DataHelper.GetNeiGongLevelAttribute(textBox6.Text);

            comboBox1.SelectedValue = textBox10.Text;
            comboBox2.SelectedValue = textBox12.Text;
            comboBox3.SelectedValue = textBox14.Text;

            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
        }

        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            _tl.AddData(this, "NeigongData");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _tl.updateData(this, _dr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataHelper.SelCondition(textBox9, textBox17);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataHelper.SelCondition(textBox11, textBox16);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataHelper.SelCondition(textBox13, textBox18);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataHelper.SelCondition(textBox15, textBox19);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataHelper.SetCboTextBox(this, sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox21.Text) && DataHelper.validateNum(textBox21.Text))
            {
                string attribute = $"({comboBox4.SelectedValue},{textBox21.Text})";
                if (!string.IsNullOrEmpty(textBox5.Text))
                {
                    attribute = textBox5.Text + "*" + attribute;
                }
                textBox5.Text = attribute;
            }
            else
            {
                MessageBox.Show("输入字符非数字");  
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmSetCbo fs = new FrmSetCbo(comboBox4, CboData.NeiGongPath);
            fs.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox22.Text) && DataHelper.validateNum(textBox22.Text))
            {
                string attribute = $"({comboBox5.SelectedValue},{textBox22.Text})";
                if (!string.IsNullOrEmpty(textBox6.Text))
                {
                    attribute = textBox6.Text + "*" + attribute;
                }
                textBox6.Text = attribute;
            }
            else
            {
                MessageBox.Show("输入字符非数字");
            }
        }
    }
}
