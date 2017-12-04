using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace xkfy_mod.Config
{
    public class BattleCondition
    {
        /// <summary>
        /// 状态名称
        /// </summary>
        [XmlElement]
        public string Value { get; set; }

        /// <summary>
        /// 状态Key 唯一
        /// </summary>
        [XmlElement]
        public string Key { get; set; }

        /// <summary>
        /// 效果集合
        /// </summary>
        public List<Condition> Conditions { get; set; }

    }

    public class Condition
    {
        /// <summary>
        /// 状态类型
        /// </summary>
        [XmlElement]
        public string EffectType { get; set; }

        /// <summary>
        /// 触发方式
        /// </summary>
        [XmlElement]
        public string Accumulate { get; set; }

        /// <summary>
        /// 是否百分比
        /// </summary>
        [XmlElement]
        public string Percent { get; set; }

        /// <summary>
        /// 数值（Percent为0固定数值、Percent为1百分比、Accumulate为7对自己触发添效果ID）
        /// </summary>
        [XmlElement]
        public string Value1 { get; set; }

        /// <summary>
        /// 数值（Percent为0固定数值、Percent为1百分比、Accumulate为7对目标触发添效果ID）
        /// </summary>
        [XmlElement]
        public string Value2 { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        [XmlElement]
        public string ValueLimit { get; set; }
        
    }
}
