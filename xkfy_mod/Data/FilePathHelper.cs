using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xkfy_mod.Data
{
    public class FilePathHelper
    {
        /// <summary>
        /// 原始Mod文件存放文件夹名称
        /// </summary>
        public static string ModsFolder = "原始文件";

        /// <summary>
        /// 
        /// </summary>
        public static string FullModsFolder = Path.Combine(Application.StartupPath, ModsFolder);

        /// <summary>
        /// 修改中Mod文件存放文件夹名称
        /// </summary>
        public static string ModsModifyFolder = "修改后的文件";

        /// <summary>
        /// 默认修改文件存放地址
        /// </summary>
        public static string DefaultSelectFilePath = Path.Combine(Application.StartupPath, ModsModifyFolder);

        /// <summary>
        /// 游戏MOD生效路径
        /// </summary>
        public static string GameModsFolder = Path.Combine(Application.StartupPath, ModsModifyFolder, "Config");
    }
}
