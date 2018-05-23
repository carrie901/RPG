using UnityEngine;
using System.Collections.Generic;
using Summer;
using UnityEditor;

namespace SummerEditor
{
    public class EAssetBundleAnalysis
    {
        public static Dictionary<string, EAssetObjectInfo> _all_assets = new Dictionary<string, EAssetObjectInfo>();

        public static void AllAnalysisAsset()
        {
            _all_assets.Clear();
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
                EditorUtility.DisplayProgressBar("分析主资源", asset_paths[i], (float)(i + 1) / length);
                AddAsset(asset_paths[i], true);

                string[] deps = AssetDatabase.GetDependencies(asset_paths[i], true);
                for (int j = 0; j < deps.Length; j++)
                {
                    if (asset_paths[i] == deps[j]) continue;
                    AddAsset(deps[j], false);
                }
            }

            foreach (var info in _all_assets)
            {
                info.Value.AnalyzeAsset();
            }
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("分析资源", "分析结束，请开始设置AssetBundle Name", "Ok");
            Resources.UnloadUnusedAssets();
        }

        public static void AddAsset(string asset_path, bool is_main_asset)
        {

            if (asset_path.EndsWith(EAssetBundleConst.IGNORE_BUILD_ASSET_SUFFIX)) return;

            if (_all_assets.ContainsKey(asset_path))
            {
                Debug.Assert(_all_assets[asset_path].IsMainAsset == is_main_asset, "主资源和依赖资源重复" + asset_path);
                return;
            }

            EAssetObjectInfo info = new EAssetObjectInfo(asset_path, is_main_asset);
            _all_assets.Add(info.AssetPath, info);
        }

        public static EAssetObjectInfo FindAssetObject(string asset_path)
        {
            if (_all_assets.ContainsKey(asset_path))
            {
                return _all_assets[asset_path];
            }
            return null;
        }
    }
}
