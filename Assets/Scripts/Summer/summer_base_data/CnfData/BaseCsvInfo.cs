using System.Collections.Generic;
using System.IO;
using Summer;

namespace Summer
{
    /// <summary>
    /// 本地读取的数据类，数据和属性对应
    /// </summary>
    public class BaseCsvInfo
    {
        public string original_file_name;               // 原始文件名hero_info
        public string original_file_path;               // 原始文件地址/three_config/tables/hero_info.csv

        public string class_name;                       // 生成的class类名
        public string class_path;
        public List<string> prop_des = new List<string>();
        public List<string> prop_type = new List<string>();
        public List<string> prop_name = new List<string>();
        public List<string> prop_rule = new List<string>();
        public List<List<string>> datas = new List<List<string>>();
        public BaseCsvInfo(string file_path)
        {
            original_file_path = file_path;
            original_file_name = Path.GetFileNameWithoutExtension(original_file_path);
            class_name = CodeGeneratorHelperE.NormalizeName(original_file_name) + "Cnf";
            class_path = CodeGeneratorConstE.cnf_path + class_name + ".cs";
            string text = FileHelper.ReadAllText(original_file_path);

            string[] lines = text.ToStrs(StringHelper.split_huanhang);

            if (lines.Length < 4)
            {
                LogManager.Error("配置文件出错", original_file_path);
                return;
            }
            string[] dess = lines[0].ToStrs(StringHelper.split_douhao);
            string[] types = lines[1].ToStrs(StringHelper.split_douhao);
            string[] names = lines[2].ToStrs(StringHelper.split_douhao);
            string[] rules = lines[3].ToStrs(StringHelper.split_douhao);

            if (dess.Length != types.Length || dess.Length != names.Length)
            {
                LogManager.Error("配置文件出错", original_file_path);
                return;
            }
            prop_des.AddRange(dess);
            prop_type.AddRange(types);
            prop_name.AddRange(names);
            prop_rule.AddRange(rules);

            int length = lines.Length;
            for (int i = 4; i < length; i++)
            {
                if (string.IsNullOrEmpty(lines[i])) continue;
                string[] contents = lines[i].ToStrs(StringHelper.split_douhao);

                datas.Add(new List<string>(contents));
            }
        }
    }
}

