using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml.Serialization;

namespace xkfy_mod
{
    public class MyConfig
    {
        /// <summary>
        /// 主表名称
        /// </summary>
        [XmlElement]
        public string MainDtName { get; set; }

        /// <summary>
        /// 明细表名称
        /// </summary>
        [XmlElement]
        public string DetailDtName { get; set; }

        /// <summary>
        /// 文件类型,1，有主表，有明细表; 2，只有一个表; 3，但是列重复，4 不使用MOD列,使用工具列的文件
        /// </summary>
        [XmlElement]
        public string DtType { get; set; }

        /// <summary>
        /// 文件全称
        /// </summary>
        [XmlElement]
        public string TxtName { get; set; }

        /// <summary>
        /// 类型,用于加载节点
        /// </summary>
        [XmlElement]
        public string Classify { get; set; }

        /// <summary>
        /// 备注说明,TXT文件的中文名称
        /// </summary>
        [XmlElement]
        public string Notes { get; set; }

        /// <summary>
        /// DtType = 1 时有效，表的基本信息列截止到哪列
        /// </summary>
        [XmlElement]
        public string BasicCritical { get; set; }

        /// <summary>
        /// DtType = 1 时有效，表的重复信息列从哪列开始
        /// </summary>
        [XmlElement]
        public string EffectCritical { get; set; }

        /// <summary>
        /// 编辑窗口名称,反射制订的窗口用
        /// </summary>
        [XmlElement]
        public string EditFormName { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        [XmlElement]
        public string IsCache { get; set; }

        /// <summary>
        /// 是否含有表头
        /// </summary>
        [XmlElement]
        public string HaveColumn { get; set; }

        /// <summary>
        /// 是否含有帮助列
        /// </summary>
        [XmlElement]
        public string HaveHelperColumn { get; set; }

        /// <summary>
        /// 是否是DLC文件
        /// </summary>
        [XmlElement]
        public string IsDlcFile { get; set; }
    }
}
