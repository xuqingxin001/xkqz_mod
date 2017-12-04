using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace xkfy_mod
{
    public class AppConfig
    {
        /// <summary>
        /// 创建时选择的路径
        /// </summary>
        [XmlElement]
        public string CreatePath;

        /// <summary>
        /// 选择时选择的路径
        /// </summary>
        [XmlElement]
        public string SelectPath;
    }
}
