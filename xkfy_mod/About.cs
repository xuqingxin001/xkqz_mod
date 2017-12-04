using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class About : DockContent
    {
        public About()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(((Label)sender).Text);
        }

        private void About_Load(object sender, EventArgs e)
        {
            txtFileMsg.Text = DataHelper.newFilesInfo.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://tieba.baidu.com/home/main?un=%E7%BA%B1%E5%B8%83RaBbiT&fr=pb&ie=utf-8");
        }
    }
}
