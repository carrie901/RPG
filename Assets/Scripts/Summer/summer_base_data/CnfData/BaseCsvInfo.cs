using System.Collections.Generic;
using System.IO;
namespace Summer
{
    /// <summary>
    /// 本地读取的数据类，数据和属性对应
    /// </summary>
    public class BaseCsvInfo
    {
        public string _originalFileName;                // 原始文件名hero_info
        public string _originalFilePath;                // 原始文件地址/three_config/tables/hero_info.csv

        public string _className;                       // 生成的class类名
        public string _classPath;
        public List<string> _propDes = new List<string>();
        public List<string> _propType = new List<string>();
        public List<string> _propName = new List<string>();
        public List<string> _propRule = new List<string>();
        public List<List<string>> _datas = new List<List<string>>();
        public BaseCsvInfo(string filePath)
        {
            _originalFilePath = filePath;
            _originalFileName = Path.GetFileNameWithoutExtension(_originalFilePath);
            _className = CnfHelper.NormalizeName(_originalFileName) + "Cnf";
            _classPath = CnfConst.cnf_path + _className + ".cs";
            string text = FileHelper.ReadAllText(_originalFilePath);

            string[] lines = text.ToStrs(StringHelper._splitHuanhang);

            if (lines.Length < 4)
            {
                LogManager.Error("配置文件出错", _originalFilePath);
                return;
            }
            string[] dess = lines[0].ToStrs(StringHelper._splitDouhao);
            string[] types = lines[1].ToStrs(StringHelper._splitDouhao);
            string[] names = lines[2].ToStrs(StringHelper._splitDouhao);
            string[] rules = lines[3].ToStrs(StringHelper._splitDouhao);

            if (dess.Length != types.Length || dess.Length != names.Length)
            {
                LogManager.Error("配置文件出错", _originalFilePath);
                return;
            }
            _propDes.AddRange(dess);
            _propType.AddRange(types);
            _propName.AddRange(names);
            _propRule.AddRange(rules);

            int length = lines.Length;
            for (int i = 4; i < length; i++)
            {
                if (string.IsNullOrEmpty(lines[i])) continue;
                string[] contents = lines[i].ToStrs(StringHelper._splitDouhao);

                _datas.Add(new List<string>(contents));
            }
        }
    }
}

