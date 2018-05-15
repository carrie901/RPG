using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace SummerEditor
{
    /// <summary>
    /// 表格数据
    /// </summary>
    public class TableClass
    {
        public string class_name;                               // 表格名称
        public Dictionary<string, TableProp> table_map
            = new Dictionary<string, TableProp>();              // Key = 属性名称，value=这一列所有的数据

        // 添加数据
        public void AddData(List<TableProp> tables)
        {
            int length = tables.Count;
            for (int i = 0; i < length; i++)
            {
                table_map.Add(tables[i].key, tables[i]);
            }
        }

        // 检测表格数据
        public string CheckAllVaild()
        {
            Debug.Log("检测表格数据:" + class_name);
            StringBuilder sb = new StringBuilder();
            foreach (var info in table_map)
            {
                string error_message = info.Value.CheckVaild();
                if (string.IsNullOrEmpty(error_message)) continue;
                sb.AppendLine(error_message);
            }
            return sb.ToString();
        }

        // 某一个字段是否存在某一个数据
        public bool IsExist(string prop_name, string text)
        {
            if (!table_map.ContainsKey(prop_name))
            {
                Debug.Log("不存在这字段：" + prop_name);
                return false;
            }
            return table_map[prop_name].IsExist(text);
        }



    }

}
