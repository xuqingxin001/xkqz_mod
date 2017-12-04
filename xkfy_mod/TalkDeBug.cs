using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class TalkDeBug : Form
    {
        private string _id = null;
        private int _index = 0;
        private ToolsHelper _tl = new ToolsHelper();
        public TalkDeBug(string id)
        {
            this._id = id;
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); //双缓冲
        }

        /// <summary>
        /// 存储对话
        /// </summary>
        private DataRow[] _drTalk;

        /// <summary>
        /// 存储事件
        /// </summary>
        private DataRow[] _drDq;
        private DataRow[] _drBa;
        /// <summary>
        /// 事件结束触发对话
        /// </summary>
        private string _sEndAdd;

        /// <summary>
        /// 文件路径
        /// </summary>
        public static Dictionary<string, string> PicFile = new Dictionary<string, string>();

        private void TalkDeBug_Load(object sender, EventArgs e)
        {
            DataHelper.ExistTable("BattleAreaData");
            DataHelper.ExistTable("RewardData");
            DataHelper.ExistTable("TalkManager");
            DataHelper.ExistTable("DevelopQuestData");
            
            if (PicFile.Count == 0)
            {
                string filePath = Application.StartupPath + "\\Images\\";
                DirectoryInfo di = new DirectoryInfo(filePath);
                _tl.GetAllFile(di, PicFile);
            }
            SetTalkDr(_id);
        }

        private void SetTalkDr(string iId)
        {
            //查询事件
            try
            {
                _drDq = DataHelper.XkfyData.Tables["DevelopQuestData"].Select("iID='" + iId + "'");
                if (_drDq.Length > 1)
                {
                    lblMsg.Text = string.Format("iID为【{0}】的数据在DevelopQuestData.txt 文件出现了多次,默认取第一条数据",iId);
                }
                //事件类型
                string iType = _drDq[0]["iType"].ToString();
                if (iType == "3")
                {
                    Satisfy();
                }
                else
                {
                    //下一事件ID
                    _sEndAdd = _drDq[0]["sEndAdd"].ToString();
                    //查询对话
                    _drTalk = DataHelper.XkfyData.Tables["TalkManager"].Select("iQGroupID='" + _drDq[0]["iArg3"] + "'");
                    _index = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetTalkDr2(string iId)
        {
            //查询事件
            try
            {
                DataRow[] drNewDq = DataHelper.XkfyData.Tables["DevelopQuestData"].Select("iID='" + iId + "'");
                if (drNewDq.Length == 0)
                {
                    _drTalk = DataHelper.XkfyData.Tables["TalkManager"].Select("iQGroupID='" + iId + "'");
                    _index = 0;
                    _sEndAdd = "0";
                }
                else
                {
                    _drDq = drNewDq;
                    if (_drDq.Length > 1)
                    {
                        lblMsg.Text = string.Format("iID为【{0}】的数据在DevelopQuestData.txt 文件出现了多次");
                    }

                    //事件类型
                    string iType = _drDq[0]["iType"].ToString();
                    if (iType == "3")
                    {
                        Satisfy();
                    }
                    else
                    {
                        //下一事件ID
                        _sEndAdd = _drDq[0]["sEndAdd"].ToString();
                        //查询对话
                        _drTalk = DataHelper.XkfyData.Tables["TalkManager"].Select("iQGroupID='" + _drDq[0]["iArg3"] + "'");
                        _index = 0;
                    }
                    //sEndAdd = drDq[0]["sEndAdd"].ToString();
                    ////查询对话
                    //drTalk = DataHelper.xkfyData.Tables["TalkManager"].Select("iQGroupID='" + drDq[0]["iArg3"] + "'");
                    //index = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Satisfy()
        {
            string[] striType = _drDq[0]["iType"].ToString().Split(',');
            string[] striArg1 = _drDq[0]["iArg1"].ToString().Split(',');
            string[] striArg2 = _drDq[0]["iArg2"].ToString().Split(',');
            string[] striCondition = _drDq[0]["iCondition"].ToString().Split(',');
            ToolsHelper tl = new ToolsHelper();
            string[] explain = tl.ExplainDevelopQuest(striCondition, striType, striArg1, striArg2);
            txtCondition.Text = explain[1];
            gbCondition.Visible = true;
        }

        private void SetTalk(int iTm)
        {
            try
            {
                //如果序号是最后一段对话
                if (iTm >= _drTalk.Length)
                {
                    //如果有后续对话
                    if (_sEndAdd != "0" || string.IsNullOrEmpty(_sEndAdd))
                    {
                        //用第一个事件的结尾查找另一个事件的开端
                        _drDq = DataHelper.XkfyData.Tables["DevelopQuestData"].Select("iID='" + _sEndAdd + "'");
                        if (_drDq.Length == 0)
                        {
                            //一般而言不会执行到这段
                            return;
                        }

                        string iType = _drDq[0]["iType"].ToString();
                        if (iType == "3")
                        {
                            Satisfy();
                            return;
                        }
                        else
                        {
                            //触发战斗,优先执行战斗
                            if (_drDq[0]["iDevelopType"].ToString() == "3")
                            {
                                _drBa = DataHelper.XkfyData.Tables["BattleAreaData"].Select("iID$0='" + _drDq[0]["iArg3"].ToString() + "'");
                                gbZd.Visible = true;
                                return;
                            }
                            //无条件就直接执行
                            SetTalkDr(_drDq[0]["iArg3"].ToString());
                            iTm = 0;
                        }
                    }
                    else
                    {
                        //事件结束
                        txtNext.Text = "结束";
                        //txtNext.Enabled = false;
                        return;
                    }
                }
                StringBuilder sbMsg = new StringBuilder();
                string sBackground = _drDq[0]["sBackground"].ToString();
                string path = "";
                if (PicFile.ContainsKey(sBackground))
                {
                    path = PicFile[sBackground];
                }

                //画出背景图
                if (File.Exists(path))
                {
                    Image img = Image.FromFile(path);
                    panelBackground.BackgroundImage = img;
                }
                else
                {
                    sbMsg.AppendFormat("没有找到名称为【{0}】的背景图片文件！", _drDq[0]["sBackground"]);
                }

                //判断是否有选项
                if (_drTalk[iTm]["bInFields"].ToString() == "1")
                {
                    for (int i = 1; i < 5; i++)
                    {
                        string sButtonName = _drTalk[iTm]["sButtonName" + i].ToString();
                        if (sButtonName == "0" || string.IsNullOrEmpty(sButtonName))
                            continue;

                        switch (i)
                        {
                            case 1:
                                btnA.Visible = true;
                                btnA.Text = sButtonName;
                                btnA.Tag = _drTalk[iTm]["sBArg" + i].ToString();
                                break;
                            case 2:
                                btnB.Visible = true;
                                btnB.Text = sButtonName;
                                btnB.Tag = _drTalk[iTm]["sBArg" + i].ToString();
                                break;
                            case 3:
                                btnC.Visible = true;
                                btnC.Text = sButtonName;
                                btnC.Tag = _drTalk[iTm]["sBArg" + i].ToString();
                                break;
                            case 4:
                                btnD.Visible = true;
                                btnD.Text = sButtonName;
                                btnD.Tag = _drTalk[iTm]["sBArg" + i].ToString();
                                break;
                        }
                    }
                }

                //显示对话内容
                txtTalk.Text = _drTalk[iTm]["sManager"].ToString();
                _curIndex = iTm;
                int iMasgPlace = int.Parse(_drTalk[iTm]["iMasgPlace"].ToString());

                //画出人物位置
                Graphics g = panel1.CreateGraphics();//pictureBox1.CreateGraphics();
                g.Clear(panel1.BackColor);
                for (int i = 1; i < 9; i++)
                {
                    string sNpcQName = _drTalk[iTm]["sNpcQName" + i].ToString();
                    if (sNpcQName == "0" || string.IsNullOrEmpty(sNpcQName))
                        continue;
                    DrawTalk(iMasgPlace);
                    path = "";
                    if (PicFile.ContainsKey(sNpcQName))
                    {
                        path = PicFile[sNpcQName];
                    }
                    if (File.Exists(path))
                    {
                        Image img = Image.FromFile(path);
                        DrawImage(img, i, g);
                    }
                    else
                        sbMsg.AppendFormat("\r\n没有找到名称为【{0}】的人物贴图文件！", sNpcQName);

                }
                g.Dispose();

                if (chkTalk.Checked)
                {
                    TalkManagerEdit msgForm = null;
                    bool wExist = _tl.CheckFormIsOpen("TalkManager_Edit");

                    if (!wExist)
                    {
                        msgForm = new TalkManagerEdit("debug");
                        msgForm.Show();
                        msgForm.BindData(_drTalk[iTm]);
                    }
                    else
                    {
                        msgForm = (TalkManagerEdit)Application.OpenForms["TalkManager_Edit"];
                        msgForm.Show();
                        msgForm.BindData(_drTalk[iTm]);
                    }
                }
                lblMsg.Text = sbMsg.ToString();
                lblWin.Text += _tl.ExplainTalkManager(_drTalk[iTm]);
                panelTalk.Visible = true;
                iTm++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 设置对话框的位置
        /// <summary>
        /// 设置对话框的位置
        /// </summary>
        /// <param name="iMasgPlace">对话框位置</param>
        private void DrawTalk(int iMasgPlace)
        {
            iMasgPlace = iMasgPlace + 1;
            int x = 0;
            string path1 = Application.StartupPath + "\\Images\\2.png";
            Image img1 = Image.FromFile(path1);

            string path = Application.StartupPath + "\\Images\\1.png";
            Image img = Image.FromFile(path);

            switch (iMasgPlace.ToString())
            {
                case "1":
                    panelTalk.BackgroundImage = img;
                    x = 0;
                    break;
                case "2":
                    panelTalk.BackgroundImage = img;
                    x = 105;
                    break;
                case "3":
                    panelTalk.BackgroundImage = img;
                    x = 213;
                    break;
                case "4":
                    panelTalk.BackgroundImage = img;
                    x = 310;
                    break;
                case "5":
                    panelTalk.BackgroundImage = img;
                    x = 575;
                    break;
                case "6":
                    panelTalk.BackgroundImage = img1;
                    x = 406;
                    break;
                case "7":
                    panelTalk.BackgroundImage = img1;
                    x = 516;
                    break;
                case "8":
                    panelTalk.BackgroundImage = img1;
                    x = 616;
                    break;
            }
            this.panelTalk.Location = new Point(x, panelTalk.Location.Y);
        }
        #endregion

        #region 画出人物贴图
        private void DrawImage(Image img,int position, Graphics g)
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;//高质量显示图片
            int left = 0;
            switch (position)
            {
                case 1:
                    left = -75;
                    break;
                case 2:
                    left = 30;
                    break;
                case 3:
                    left = 135;
                    break;
                case 4:
                    left = 240;
                    break;
                case 5:
                    left = 501;
                    break;
                case 6:
                    left = 606;
                    break;
                case 7:
                    left = 711;
                    break;
                case 8:
                    left = 815;
                    break;
            }
            g.DrawImage(img, left, 0, 256, 256);//将图片画在游戏区
        }
        #endregion

        int _curIndex = -1;
        private void txtNext_Click(object sender, EventArgs e)//iArg3
        {
            lbltkMsg.Text = "";
            txtNext.Text = "下一步";
            SetTalk(_index);
            _index++;
        }

        private void btnWin_Click(object sender, EventArgs e)
        {
            DataRow[] drRd = null;
            if (((Button)sender).Name == "btnWin")
            {
                 drRd = DataHelper.XkfyData.Tables["RewardData"].Select("iRID='" + _drBa[0]["sReward$6"].ToString() + "'");
            }
            else
            {
                 drRd = DataHelper.XkfyData.Tables["RewardData"].Select("iRID='" + _drBa[0]["sReward$7"].ToString() + "'");
            }

            string[] sRewardData = drRd[0]["sRewardData"].ToString().Split('*');
            ExplainHelper eh = new ExplainHelper();
            string rewardData = eh.ExplainRewardData(sRewardData);
            string talkid = DataHelper.GetId(rewardData);
            
            lblWin.Text += rewardData.Replace("#","");
            if (talkid != "")
            {
                SetTalkDr(talkid);
            }
            //txtNext.Enabled = true;
            gbZd.Visible = false;
        }
       
        private void btnA_Click(object sender, EventArgs e)
        {
            string sbArg = ((Button)sender).Tag.ToString();
            if (sbArg.IndexOf("|") != -1)
            {
                sbArg = sbArg.Split('|')[0];
            }
            SetTalkDr2(sbArg);
            btnA.Visible = false;
            btnB.Visible = false;
            btnC.Visible = false;
            btnD.Visible = false;
            SetTalk(_index);
            txtNext.Text = "下一步";
            _index++;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if(_curIndex != -1)
                SetTalk(_curIndex);
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            gbCondition.Visible = false;
            txtCondition.Text = "";
            if (((Button)sender).Name == "btnYes")
            {
                SetTalkDr(_drDq[0]["iArg3"].ToString());
                //index = 0;
            }
            else
            {
                SetTalkDr(_drDq[0]["sEndAdd"].ToString());
                //index = 0;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                lbltkMsg.Text = "";
                _drTalk[_curIndex]["rowState"] = "0";
                _drTalk[_curIndex]["sManager"] = txtTalk.Text;
                lbltkMsg.Text = "修改成功";
            }
            catch (Exception ex)
            {
                lbltkMsg.Text = ex.Message;
            }
        }
    }
}
