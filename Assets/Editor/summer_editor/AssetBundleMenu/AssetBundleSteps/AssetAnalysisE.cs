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
    /*public class AssetAnalysisE
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
    }*/
    /// <summary>
    /// 分析资源的相关引用
    /// </summary>
    public class AssetBundleAnalysisE
    {
        public static Dictionary<string, EAssetMainInfo> _main_ab_map = new Dictionary<string, EAssetMainInfo>();
        public static Dictionary<string, EAssetDepInfo> _dep_ab_map = new Dictionary<string, EAssetDepInfo>();


        public static void AllAnalysisAsset()
        {
            _main_ab_map.Clear();
            _dep_ab_map.Clear();

            //
            List<string> tmp_paths = EPathHelper.GetAssetPathList(EAssetBundleConst.ASSET_PATH, true, "*.*");
            SuffixHelper.Filter(tmp_paths, new NoEndsWithFilter(EAssetBundleConst.SUFFIX_META));

            List<string> asset_paths = EPathHelper.GetAssetPathList01("Assets/Res/", true);
            if (tmp_paths.Count == asset_paths.Count)
            {
                Debug.LogError("路径查找错误");
                return;
            }
            int length = asset_paths.Count;
            for (int i = 0; i < length; i++)
            {
                //查找文件  
                EditorUtility.DisplayProgressBar("分析主资源", asset_paths[i], (float)(i + 1) / length);
                AddMainAsset(asset_paths[i]);
            }

            int index = 1;
            foreach (var info in _main_ab_map)
            {
                EditorUtility.DisplayProgressBar("分析主资源", info.Value.asset_path, (float)(index) / _main_ab_map.Count);
                info.Value.Init();
                index++;
            }

            index = 1;
            foreach (var info in _dep_ab_map)
            {
                EditorUtility.DisplayProgressBar("分析所有资源", info.Value.asset_path, (float)(index) / _dep_ab_map.Count);
                info.Value.Init();
                index++;
            }

            foreach (var info in _main_ab_map)
            {
                if (info.Value.ref_count != 0)
                {
                    Debug.LogFormat("--->路径:[{0}],引用次数:[{1}]", info.Value.asset_path, info.Value.ref_count);
                }
            }
            foreach (var info in _dep_ab_map)
            {
                if (info.Value.ref_count == 0)
                {
                    Debug.LogFormat("===>路径:[{0}],引用次数:[{1}]", info.Value.asset_path, info.Value.ref_count);
                }else if (info.Value.ref_count >1)
                {
                    Debug.LogFormat("||||>路径:[{0}],引用次数:[{1}]", info.Value.asset_path, info.Value.ref_count);
                }
            }

                EditorUtility.ClearProgressBar();
            Resources.UnloadUnusedAssets();
        }

        //根据名字查找依赖文件
        public static EAssetInfo FindDep(string asset_path)
        {
            if (_dep_ab_map.ContainsKey(asset_path))
            {
                return _dep_ab_map[asset_path];
            }

            Debug.LogError("居然引用了主资源：" + asset_path);
            if (_main_ab_map.ContainsKey(asset_path))
            {
                return _main_ab_map[asset_path];
            }
            Debug.LogError("无论主资源还是依赖资源都找不到对应的路径：" + asset_path);
            return null;
        }

        public static void AddDepAsset(string dep_asset_path)
        {
            if (_dep_ab_map.ContainsKey(dep_asset_path)) return;

            EAssetDepInfo dep_info = new EAssetDepInfo(dep_asset_path);
            _dep_ab_map.Add(dep_info.asset_path, dep_info);
        }

        public static void AddMainAsset(string asset_path)
        {
            EAssetMainInfo main_info = new EAssetMainInfo(asset_path);
            _main_ab_map.Add(main_info.asset_path, main_info);

        }
    }
}
