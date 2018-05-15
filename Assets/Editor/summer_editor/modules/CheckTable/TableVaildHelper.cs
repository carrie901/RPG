using System;
using System.Collections.Generic;
using System.IO;
using Summer;


namespace SummerEditor
{
    // 整体转移到C# winform 生成exe数据
    public class TableVaildHelper
    {
        /// <summary>
        /// 读取指定文件的信息转成TableClass
        /// </summary>
        public static TableClass ReadFile(string file_path,string file_name)
        {
            TableClass table_class = new TableClass();
            table_class.class_name = file_name;

            // 1.读取文本
            string[] lines = File.ReadAllLines(file_path);
            int length = lines.Length;

            if (length <= TableVaildConst.LINE_VAILD) return null;

            // 2.解析头部文件
            string[] dess = lines[TableVaildConst.LINE_DES].Split(',');
            string[] props = lines[TableVaildConst.LINE_PROP].Split(',');
            string[] keys = lines[TableVaildConst.LINE_KEY].Split(',');
            string[] vailds = lines[TableVaildConst.LINE_VAILD].Split(',');

            // 3.确定目前有多少个数据
            List<TableProp> tables = new List<TableProp>();
            for (int i = 0; i < dess.Length; i++)
            {
                TableProp prop = new TableProp();
                prop.key = keys[i];
                prop.des = dess[i];
                prop.prop = props[i];
                prop.vaild = vailds[i];

                tables.Add(prop);
            }

            // 4.解析从第五行开始的文本内容
            for (int i = TableVaildConst.LINE_VAILD + 1; i < lines.Length; i++)
            {
                // 这一行的内容分解 属性和数据联合起来
                string[] contents = lines[i].Split(TableVaildConst.SPLIT);

                for (int j = 0; j < contents.Length; j++)
                    tables[j].AddData(contents[j]);
            }

            table_class.AddData(tables);
            return table_class;
        }
    }
}
