using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace xkfy_mod
{
    public class UnionDropDown
    {
        /// <summary>
        /// 状态名称
        /// </summary>
        [XmlElement]
        public string Key { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        [XmlElement]
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<DicConfig> TwoLevel { get; set; }
    }
}
