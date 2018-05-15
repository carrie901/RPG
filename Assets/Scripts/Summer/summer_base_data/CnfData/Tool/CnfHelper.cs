using System.Collections.Generic;
using System.IO;

namespace Summer
{
    public class CnfHelper
    {

        // 这一块可以做一点改变 读取指定目录 分成File和Resouces目录
        public static Dictionary<string, BaseCsvInfo> LoadFileContent()
        {
            Dictionary<string, BaseCsvInfo> csv_infos = new Dictionary<string, BaseCsvInfo>();
            string[] csvs_path = Directory.GetFiles(CnfConst.csv_path);
            // 2.依次读取File信息转成Txt
            int length = csvs_path.Length;
            for (int i = 0; i < length; i++)
            {
                // 忽略文件
                string with_out_extension_name = Path.GetFileNameWithoutExtension(csvs_path[i]);
                if (CnfConst.ingore_file.Contains(with_out_extension_name) || string.IsNullOrEmpty(with_out_extension_name)) continue;

                BaseCsvInfo csv_info = new BaseCsvInfo(csvs_path[i]);
                csv_infos.Add(csv_info.class_name, csv_info);
            }
            return csv_infos;
        }



        public static string NormalizeName(string file_name)
        {
            string[] name_list = file_name.Split('_');
            string name = string.Empty;//name_list[0].Substring(0, 1).ToUpper()+ name_list[0].Substring(1);
            int length = name_list.Length;

            for (int i = 0; i < length; i++)
            {
                name += name_list[i].Substring(0, 1).ToUpper() + name_list[i].Substring(1);
            }
            return name;
        }

        public static List<List<string>> GetContext(string original_file_path)
        {
            List<List<string>> datas = new List<List<string>>();
            string text = FileHelper.ReadAllText(original_file_path);
            string[] lines = text.ToStrs(StringHelper.split_huanhang);
            if (lines.Length < 4)
            {
                LogManager.Error("配置文件出错,长度不够", original_file_path);
            }
            else
            {
                int length = lines.Length;
                for (int i = 4; i < length; i++)
                {
                    if (string.IsNullOrEmpty(lines[i])) continue;
                    string[] contents = lines[i].ToStrs(StringHelper.split_douhao);

                    datas.Add(new List<string>(contents));
                }
            }

            return datas;
        }
    }
}


