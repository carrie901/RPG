using UnityEngine;

//=============================================================================
// Author : mashao
// CreateTime : 2018-2-2 20:26:30
// FileName : CodeGeneratorConstE.cs
//=============================================================================

namespace SummerEditor
{
    public class CodeGeneratorConstE
    {
        /// <summary>
        /// 生成的cs管理类的文件地址
        /// </summary>
        public static string config_cs_path = Application.dataPath + "/Scripts/Summer/summer_base_data/CnfData/ConfigManager.cs";
        /// <summary>
        /// 默认的空间名
        /// </summary>
        public const string NAMESPACE = "namespace Summer";
        public static string config_manager = "ConfigManager";
    }
}
