using UnityEngine;
using System.Collections.Generic;
namespace Summer
{
    public class CnfConst
    {
        #region 资源路径
        /// <summary>
        /// 二进制数据路径
        /// </summary>
        public static string data_byte_path = Application.dataPath + "" + "/Res/TextAsset/" + DATA_BYTE_NAME;

        public const string DATA_BYTE_NAME = "csv.bytes";

        /// <summary>
        /// csv路径 也就是资源目标路径
        /// </summary>
        public static string csv_path = Application.dataPath + "/../Data/Tables/";

        #endregion

        /// <summary>
        /// cs代码路径 cnf CnfByte
        /// </summary>
        public static string cnf_path = Application.dataPath + "/Scripts/Summer/summer_base_data/CnfData/Cnf/";

        /// <summary>
        /// 忽略文件
        /// </summary>
        public static List<string> ingore_file = new List<string>() { "text" };

    }

}
