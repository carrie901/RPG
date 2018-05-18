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
    public class AssetBundleAnalysisE
    {
        public static Dictionary<string, EAssetMainInfo> _main_ab_map = new Dictionary<string, EAssetMainInfo>();
        public static Dictionary<string, EAssetDepInfo> _dep_ab_map = new Dictionary<string, EAssetDepInfo>();


        public static void AllAnalysisAsset()
        {
            _main_ab_map.Clear();
            _dep_ab_map.Clear();

            List<string> tmp_paths = EPathHelper.GetAssetPathList(EAssetBundleConst.ASSET_PATH, true, "*.*");
            SuffixHelper.Filter(tmp_paths, new NoEndsWithFilter(EAssetBundleConst.SUFFIX_META));

            List<string> asset_paths = EPathHelper.GetAssetPathList01(EAssetBundleConst.main_driectory, true);
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

            // 分析主资源
            int index = 1;
            foreach (var info in _main_ab_map)
            {
                EditorUtility.DisplayProgressBar("分析主资源", info.Value.AssetPath, (float)(index) / _main_ab_map.Count);
                info.Value.CheckFirstDep();
                index++;
            }

            // 分析依赖资源
            index = 1;
            foreach (var info in _dep_ab_map)
            {
                EditorUtility.DisplayProgressBar("分析所有资源", info.Value.AssetPath, (float)(index) / _dep_ab_map.Count);
                info.Value.CheckFirstDep();
                index++;
            }

            // 打印
            foreach (var info in _main_ab_map)
            {
                if (info.Value.RefCount != 0)
                {
                    //Debug.LogFormat("--->路径:[{0}],引用次数:[{1}]", info.Value.AssetPath, info.Value.RefCount);
                }
            }
            foreach (var info in _dep_ab_map)
            {
                /*if (info.Value._ref_count == 0)
                {
                    Debug.LogFormat("===>路径:[{0}],引用次数:[{1}]", info.Value._asset_path, info.Value._ref_count);
                }
                */
                if (info.Value.RefCount > 1)
                {
                    EAssetInfo asset_info = info.Value;
                    //Debug.LogFormat("||||>路径:[{0}],引用次数:[{1}]", info.Value.AssetPath, info.Value.RefCount);

                    foreach (var parent_info in asset_info._parent_dep_map)
                    {
                        //Debug.Log(parent_info.Value.AssetPath);
                    }
                }
                else if (info.Value.RefCount == 0)
                {
                    Debug.LogErrorFormat("||||>路径:[{0}],引用次数:[{1}]", info.Value.AssetPath, info.Value.RefCount);
                }
            }

            ExcelAbManager.WriteAnalysis();
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


            if (_main_ab_map.ContainsKey(asset_path))
            {
                Debug.LogError("居然引用了主资源：" + asset_path + ",主资源不能分作两个角色");
                return null;
            }
            Debug.LogError("无论主资源还是依赖资源都找不到对应的路径：" + asset_path);
            return null;
        }

        public static void AddDepAsset(string dep_asset_path)
        {
            if (dep_asset_path.EndsWith(EAssetBundleConst.IGNORE_BUILD_ASSET_SUFFIX)) return;
            if (_dep_ab_map.ContainsKey(dep_asset_path)) return;
            if (dep_asset_path.Contains(EAssetBundleConst.main_driectory))
            {
                Debug.Log("主资源的目录文件夹" + dep_asset_path);
                return;
            }
            EAssetDepInfo dep_info = new EAssetDepInfo(dep_asset_path);
            _dep_ab_map.Add(dep_info.AssetPath, dep_info);
        }

        public static void AddMainAsset(string asset_path)
        {
            if (_main_ab_map.ContainsKey(asset_path))
            {
                Debug.LogError("重复性质的主依赖资源：" + asset_path);
                return;
            }
            EAssetMainInfo main_info = new EAssetMainInfo(asset_path);
            _main_ab_map.Add(main_info.AssetPath, main_info);

        }
    }
}
