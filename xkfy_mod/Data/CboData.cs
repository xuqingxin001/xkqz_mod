using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xkfy_mod.Data
{
    /***
    *绑定配置下拉框专用类
    *tangzhi 2016年7月2日 06:42:16
    **/

    public static class CboData
    {
        public static string AppPath = Application.StartupPath + "/工具配置文件/下拉框配置文件/";

        /// <summary>
        /// BattleCondition，招式效果配置
        /// </summary>
        public static string BattleConditionPath = AppPath + "BattleConditionType.xml";

        /// <summary>
        /// BattleCondition，生效方式配置,和内功公用
        /// </summary>
        public static string AccumulatePath = AppPath + "Accumulate.xml";

        /// <summary>
        /// BattleNeigong，招式效果
        /// </summary>
        public static string BattleNeigongPath = AppPath + "内功招式效果.xml";

        /// <summary>
        /// NeigongEdit，内功修炼属性
        /// </summary>
        public static string NeiGongPath = AppPath + "内功属性.xml";

        /// <summary>
        /// BattleCondition_Edit,预设状态
        /// </summary>
        public static string PresetConditionPath = AppPath + "预设状态.xml";

        /// <summary>
        /// ItemDataEdit,预设状态
        /// </summary>
        public static string Recover = AppPath + "物品类型.xml";

        /// <summary>
        /// ItemDataEdit,招式类型
        /// </summary>
        public static string WearAmspath = AppPath + "招式类型.xml";

        /// <summary>
        /// RewardDataEdit,奖励类型
        /// </summary>
        public static string RewardData = AppPath + "奖励类型.xml";

        /// <summary>
        /// 武器招式类型
        /// </summary>
        /// <returns></returns>
        public static void BindiWearAmsType(ComboBox cb)
        {
            List<DicConfig> list = DataHelper.ReadXmlToList<DicConfig>(WearAmspath);
            DataHelper.BinderComboBox(cb, list);
        }

        /// <summary>
        /// 通用数据读取绑定
        /// </summary>
        /// <returns></returns>
        public static void BindiComboBox(ComboBox cb,string path)
        {
            List<DicConfig> list = DataHelper.ReadXmlToList<DicConfig>(path);
            DataHelper.BinderComboBox(cb, list);
        }

    }
}
