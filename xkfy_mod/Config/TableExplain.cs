using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace xkfy_mod
{
    public class TableExplain
    {
        /// <summary>
        /// 工具列名
        /// </summary>
        [XmlElement]
        public string ToolsColumn { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [XmlElement]
        public string Column { get; set; }

        /// <summary>
        /// 显示的文字
        /// </summary>
        [XmlElement]
        public string Text { get; set; }

        /// <summary>
        /// 移动到Label显示的详细注释
        /// </summary>
        [XmlElement]
        public string Explain { get; set; }

        /// <summary>
        /// 是否可以查询
        /// </summary>
        [XmlElement]
        public string IsSelect { get; set; }
    }
}
