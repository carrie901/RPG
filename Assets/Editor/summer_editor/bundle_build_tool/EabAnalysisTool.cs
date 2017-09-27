using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;

namespace SummerEditor
{
    public class EabAnalysisMenu
    {
        [MenuItem("Tool/资源检测/开始分析")]
        public static void AnalysisDirectory()
        {
            EabAnalysisTool.AnalysisDirectory();
        }

        [MenuItem("Tool/资源检测/导出结果")]
        public static void ExportResult()
        {
            EabAnalysisTool.ExportResult();
        }
    }

    public class EabAnalysisTool
    {
        public static Dictionary<string, EabMainVbo> _main_ab_map = new Dictionary<string, EabMainVbo>();
        public static Dictionary<string, EabDepVbo> _dep_ab_map = new Dictionary<string, EabDepVbo>();

        public static void AnalysisDirectory()
        {
            _main_ab_map.Clear();
            _dep_ab_map.Clear();
            string path = "E:\\work_three\\trunk\\Threecountry\\Assets\\Resources\\GameObjectRes\\PrefabObject\\Character\\NPC";
            DirectoryInfo dir_info = new DirectoryInfo(path);

            //遍历所有文件夹
            foreach (DirectoryInfo d in dir_info.GetDirectories())
            {
                string str = d.ToString();
            }

            //遍历所有子文件
            foreach (FileInfo f in dir_info.GetFiles()) //查找文件  
            {
                _analysis_file(f);
            }
        }

        public static void ExportResult()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<root>");
            foreach (var info in _main_ab_map)
            {
                string str = info.Value.GetString("");
                sb.Append(str);
                sb.AppendLine(string.Empty);
            }
            sb.AppendLine("</root>");


            string path = Application.dataPath;
            int index = path.LastIndexOf('/');
            path = path.Substring(0, index);
            path = path + "/AssetBundleAnalysis.txt";

            StreamWriter sw = new StreamWriter(path, true);
            sw.Write(sb);
            sw.Flush();
            sw.Close();
            sw = null;
        }
        //根据名字查找依赖文件
        public static EabDepVbo FindDep(string path)
        {
            if (_dep_ab_map.ContainsKey(path))
            {
                return _dep_ab_map[path];
            }

            EabDepVbo dep = new EabDepVbo(path);
            _dep_ab_map.Add(dep.asset_name, dep);
            return dep;
        }
        //目录分析
        public static void _analysis_directory(DirectoryInfo dir_info)
        {

        }
        //文件分析
        public static void _analysis_file(FileInfo file_info)
        {
            string path = file_info.FullName;
            string extension = Path.GetExtension(path);
            if (extension == ".meta") return;
            Debug.Log("解析文件路径:" + path);
            EabMainVbo main_ab;
            path = EditorCommon.AbsoluteToRelativePathRemoveAssets(path);
            if (_main_ab_map.TryGetValue(path, out main_ab))
            {
                Debug.Log(string.Format("已经分析过这个资源了,Path:[{0}]", path));
                return;
            }
            main_ab = new EabMainVbo(path);
            _main_ab_map.Add(path, main_ab);
        }

    }
}
