using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Summer;
using UnityEditor;

namespace SummerEditor
{
    /// <summary>
    /// 分析资源的相关引用
    /// </summary>
    public class AssetAnalysisE
    {
        public static Dictionary<string, EabMainVbo> _main_ab_map = new Dictionary<string, EabMainVbo>();
        public static Dictionary<string, EabDepVbo> _dep_ab_map = new Dictionary<string, EabDepVbo>();

        public static void AllAnalysisAsset()
        {
            _main_ab_map.Clear();
            _dep_ab_map.Clear();
            List<string> files_path = EPathHelper.GetAssetPathList(EAssetBundleConst.ASSET_PATH, true, "*.*");

            SuffixHelper.Filter(files_path, new NoEndsWithFilter(EAssetBundleConst.SUFFIX_META));
            int length = files_path.Count;
            for (int i = 0; i < length; i++)
            {
                //查找文件  
                EditorUtility.DisplayProgressBar("分析文件", files_path[i], (float)i / length);
                _analysis_file(files_path[i]);
            }
            EditorUtility.ClearProgressBar();
            Resources.UnloadUnusedAssets();
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

        //文件分析
        public static void _analysis_file(string file_path)
        {
            EabMainVbo main_ab;
            if (_main_ab_map.TryGetValue(file_path, out main_ab))
            {
                Debug.Log(string.Format("已经分析过这个资源了,Path:[{0}]", file_path));
                return;
            }
            main_ab = new EabMainVbo(file_path);
            _main_ab_map.Add(file_path, main_ab);
        }
    }
}
