using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace SummerEditor
{
    /// <summary>
    /// 值存在于某一个表格中 提供表格和字段
    /// </summary>
    public class ExistTextVaild : I_TableVaild
    {
        public string table_name;                               // 某一个表格
        public string row_name;                                 // 某一行内容
        public string CheckVaild(List<string> infos)
        {
            StringBuilder sb = new StringBuilder();
            int length = infos.Count;
            for (int i = 0; i < length; i++)
            {
                bool value = TableManager.IsExist(table_name, row_name, infos[i]);
                if (!value)
                {
                    sb.AppendLine(string.Format(TableVaildConst.exist_error, table_name, row_name, infos[i]));
                    Debug.LogErrorFormat("表格:[{0}],字段:[{1}],不存在[{2}]", table_name, row_name, infos[i]);
                }
                    
            }

            return sb.ToString();
        }

        public void SetVaildData(string[] datas)
        {
            table_name = datas[1];
            row_name = datas[2];
        }
    }
}
