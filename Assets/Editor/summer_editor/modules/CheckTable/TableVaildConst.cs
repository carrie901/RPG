using UnityEngine;
using System.Collections.Generic;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-7 20:17:42
// FileName : TableVaildConst.cs
//=============================================================================

namespace SummerEditor
{
    public class TableVaildConst
    {
        public static string table_vaild_report= Application.dataPath + "/../../three_check/table_vaild.csv";

        public static List<string> ignore_files = new List<string>() { };


        #region 解析参数

        public const int LINE_DES = 0;
        public const int LINE_PROP = 1;
        public const int LINE_KEY = 2;
        public const int LINE_VAILD = 3;                            // 有效规则间

        public const char SPLIT = ',';                              // 分割符号

        #endregion

        #region 错误信息

        public static string no_valid = "无效的规则:{0}";

        public static string length_error = "长度有问题{0}";

        public static string exist_error = "表格:[{0}],字段:[{1}],不存在[{2}]";

        #endregion
    }
}
