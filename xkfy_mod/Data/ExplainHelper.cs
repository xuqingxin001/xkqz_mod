using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xkfy_mod.Data
{
    public class ExplainHelper
    {
        public static string ExplainRewardData(string[] sRewardData)
        {
            StringBuilder sbExplain = new StringBuilder();

            foreach (string rw in sRewardData)
            {
                string[] reward = rw.Replace(")", "").Replace("(", "").Split(',');
                if (reward.Length < 5)
                {
                    sbExplain.Append("本行数据有误,请检查!\r\n");
                    continue;
                }

                string type = reward[0];
                string row2 = reward[1];
                string value = reward[2];
                string stringId = reward[3];
                string row5 = reward[4];

                string tempStr = string.Empty;
                switch (type)
                {
                    case "2":
                        sbExplain.AppendFormat("{0}学会{1}",DataHelper.GetNpcName(row2),DataHelper.GetRoutineName(value));
                        break;
                    case "3":
                        sbExplain.AppendFormat("{0}学会{1}", DataHelper.GetNpcName(row2), DataHelper.GetNeiGongName(value));
                        break;
                    case "4":
                        sbExplain.AppendFormat("获得{0}好感{1}点", DataHelper.GetNpcName(row2), value);
                        break;
                    case "5":
                        sbExplain.AppendFormat(DataHelper.GetStringTab(stringId), DataHelper.GetItemName(row2), value);
                        break;
                    case "6":
                    case "7":
                        sbExplain.AppendFormat(DataHelper.GetStringTab(stringId), DataHelper.GetNpcName(row2));
                        break;
                    case "8":
                        sbExplain.AppendFormat("金钱 {0}", value);
                        break;
                    case "9":
                    case "10":
                        sbExplain.AppendFormat(DataHelper.GetStringTab(stringId), value);
                        break;
                    case "12":
                        sbExplain.AppendFormat("触发对话 {0}", row2);
                        break;
                    case "13":
                        sbExplain.AppendFormat("触发结局 {0}", row5);
                        break;
                    case "15":
                        sbExplain.AppendFormat(DataHelper.GetStringTab("210045"), DataHelper.GetNpcName(row2), DataHelper.GetTalentName(value));
                        break;
                    case "18":
                        sbExplain.AppendFormat("{0}获得修炼经验 {1}点", DataHelper.GetNpcName(row2), value);
                        break;
                    case "19":
                        sbExplain.AppendFormat("完成事件 {0}", DataHelper.GetQuestDataManager(row2));
                        break;
                    case "21":
                        sbExplain.AppendFormat("失去 {0}", DataHelper.GetItemName(row2));
                        break;
                    default:
                        sbExplain.AppendFormat("弱鸡作者无法理解 {0} 是什么意思", rw);
                        break;

                }
                sbExplain.Append("\r\n");
            }
            return sbExplain.ToString();
        }

        public static string GetCharacterData(DataGridViewRow dr)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("携带道具:");
            string itemData = dr.Cells["Item$25"].Value.ToString();
            ArrayOne(itemData.Split('*'), sb, "ItemData");

            sb.AppendFormat("\r\n\r\n招式:");
            string routineId = dr.Cells["RoutineID$26"].Value.ToString();
            ArrayOne(routineId.Split('*'), sb, "RoutineData");

            sb.AppendFormat("\r\n\r\n内功:");
            string neigong = dr.Cells["NeigongID$27"].Value.ToString();
            ArrayTwo(neigong, sb, "NeiGong");

            sb.AppendFormat("\r\n\r\n天赋:");
            string talent = dr.Cells["Talent$28"].Value.ToString();
            ArrayTwo(talent, sb, "Talent");
            return sb.ToString();
        }

        private static void ArrayOne(string[] arrs,StringBuilder sb, string source)
        {
            for (int i = 0; i < arrs.Length; i++)
            {
                ArrayTwo(arrs[i], sb, source);
            }
        }

        private static void ArrayTwo(string arrs,StringBuilder sb,string source)
        {
            string[] item = arrs.Replace("(", "").Replace(")", "").Split(',');
            if (item.Length == 2)
            {
                string text = DataHelper.GetText(source, item[0]);
                switch (source)
                {
                    case "ItemData":
                        sb.AppendFormat("{0}*{1} ", text, item[1]);
                        break;
                    case "RoutineData":
                    case "NeiGong":
                        sb.AppendFormat("{0}{1}重 ", text, item[1]);
                        break;
                    case "Talent":
                        sb.AppendFormat("{0} {1}", text, DataHelper.GetText(source, item[1]));
                        break;

                }
            }
        }

        public static string ExplainNeiGong(DataGridViewRow dr)
        {
            StringBuilder sbExplain = new StringBuilder();

            sbExplain.Append(DataHelper.GetNeiGong(dr.Cells["ConditionEffectID1$8"].Value.ToString()));
            sbExplain.Append("\r\n" + DataHelper.GetNeiGong(dr.Cells["ConditionEffectID2$10"].Value.ToString()));
            sbExplain.Append("\r\n" + DataHelper.GetNeiGong(dr.Cells["ConditionEffectID3$12"].Value.ToString()));
            sbExplain.Append("\r\n" + DataHelper.GetNeiGong(dr.Cells["ConditionEffectID3$14"].Value.ToString()));

            return sbExplain.ToString();
        }
    }
}
