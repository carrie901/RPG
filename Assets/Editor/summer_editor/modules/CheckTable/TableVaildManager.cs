using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Summer;


//=============================================================================
// Author : mashao
// CreateTime : 2017-12-15 11:43:21
// FileName : TableVaildManager.cs
//=============================================================================

namespace SummerEditor
{
    // 表格管理
    public class TableManager
    {
        public static Dictionary<string, TableClass> map = new Dictionary<string, TableClass>();

       
        public static void CheckVaild()
        {
            map.Clear();
            // 1.查找指定目录下的所有csv文件
            string[] files = Directory.GetFiles(CnfConst.csv_path);

            // 2.依次读取文件的相关信息
            for (int i = 0; i < files.Length; i++)
            {
                string file_name = FileHelper.GetFileNameByPath(files[i]);
                if (TableVaildConst.ignore_files.Contains(file_name)) continue;
                TableClass data = TableVaildHelper.ReadFile(files[i], file_name);
                map.Add(data.class_name, data);
            }

            StringBuilder sb = new StringBuilder();
            // 3.依次检测文件的有效性
            foreach (var info in map)
            {
                string mess = info.Value.CheckAllVaild();
                if (string.IsNullOrEmpty(mess)) continue;
                sb.AppendLine(mess);
            }

            if (File.Exists(TableVaildConst.table_vaild_report))
                File.Delete(TableVaildConst.table_vaild_report);

            File.WriteAllText(TableVaildConst.table_vaild_report, sb.ToString());
        }

        public static bool IsExist(string table_name, string prop_name, string text)
        {
            if (!map.ContainsKey(table_name))
            {
                Debug.LogError("不存在这个表格:" + table_name);
                return false;
            }
            return map[table_name].IsExist(prop_name, text);
        }
    }
}
