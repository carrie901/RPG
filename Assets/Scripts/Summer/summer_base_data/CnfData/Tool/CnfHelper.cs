using System.Collections.Generic;
using System.IO;

namespace Summer
{
    public class CnfHelper
    {

        // 这一块可以做一点改变 读取指定目录 分成File和Resouces目录
        public static Dictionary<string, BaseCsvInfo> LoadFileContent()
        {
            Dictionary<string, BaseCsvInfo> csvInfos = new Dictionary<string, BaseCsvInfo>();
            string[] csvsPath = Directory.GetFiles(CnfConst.csv_path);
            // 2.依次读取File信息转成Txt
            int length = csvsPath.Length;
            for (int i = 0; i < length; i++)
            {
                // 忽略文件
                string withOutExtensionName = Path.GetFileNameWithoutExtension(csvsPath[i]);
                if (CnfConst.ingore_file.Contains(withOutExtensionName) || string.IsNullOrEmpty(withOutExtensionName)) continue;

                BaseCsvInfo csvInfo = new BaseCsvInfo(csvsPath[i]);
                csvInfos.Add(csvInfo._className, csvInfo);
            }
            return csvInfos;
        }

        public static string NormalizeName(string fileName)
        {
            string[] nameList = fileName.Split('_');
            string name = string.Empty;//name_list[0].Substring(0, 1).ToUpper()+ name_list[0].Substring(1);
            int length = nameList.Length;

            for (int i = 0; i < length; i++)
            {
                name += nameList[i].Substring(0, 1).ToUpper() + nameList[i].Substring(1);
            }
            return name;
        }

        public static List<List<string>> GetContext(string originalFilePath, int first = 1)
        {
            List<List<string>> datas = new List<List<string>>();
            if(!FileHelper.IsExit(originalFilePath))return datas;
            
            string text = FileHelper.ReadAllText(originalFilePath);

            string[] lines = text.ToStrs(StringHelper._splitHuanhang);
            if (lines.Length < first)
            {
                LogManager.Error("配置文件出错,长度不够", originalFilePath);
            }
            else
            {
                int length = lines.Length;
                for (int i = first; i < length; i++)
                {
                    if (string.IsNullOrEmpty(lines[i])) continue;
                    string[] contents = lines[i].ToStrs(StringHelper._splitDouhao);

                    datas.Add(new List<string>(contents));
                }
            }
            return datas;
        }
    }
}


