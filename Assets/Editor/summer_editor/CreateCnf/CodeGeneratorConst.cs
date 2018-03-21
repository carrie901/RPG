using UnityEngine;
using System.Collections.Generic;

//=============================================================================
// Author : mashao
// CreateTime : 2018-2-2 20:26:30
// FileName : CodeGeneratorConst.cs
//=============================================================================

namespace Summer
{
    public class CodeGeneratorConst
    {
        #region

        /// <summary>
        /// 生成的cs管理类的文件地址
        /// </summary>
        public static string config_cs_path = Application.dataPath + "/Scripts/game/common/csv/cnf/ConfigManager.cs";
        /// <summary>
        /// csv路径 也就是资源目标路径
        /// </summary>
        public static string csv_path = Application.dataPath + "/../../three_config/tables/";
        /// <summary>
        /// cs代码路径 cnf CnfByte
        /// </summary>
        public static string cnf_path = Application.dataPath + "/Scripts/game/common/csv/cnf/";
        /// <summary>
        /// 忽略文件
        /// </summary>
        public static List<string> ingore_file = new List<string>() { "text" };
        /// <summary>
        /// 默认的空间名
        /// </summary>
        public const string NAMESPACE = "namespace Summer";

        /// <summary>
        /// 二进制数据路径
        /// </summary>
        public static string data_byte_path = Application.dataPath + "" + "/Resources/csv.bytes";

        public static string data_byte_name = "csv";

        #endregion

        public static string config_manager = "ConfigManager";
    }
}
