using UnityEngine;
using System.Collections.Generic;
using Summer;
using UnityEditor;

namespace SummerEditor
{
    /// <summary>
    /// 分析资源的相关依赖
    /// </summary>
    public class EAssetBundleAnalysis
    {
        public static Dictionary<string, EAssetObjectInfo> _allAssets = new Dictionary<string, EAssetObjectInfo>();

        public static void AllAnalysisAsset()
        {
            _allAssets.Clear();
            List<string> assetPaths = EPathHelper.GetAssetsPath(EAssetBundleConst.MAIN_RES_DRIECTORY, true);
            int length = assetPaths.Count;
            for (int i = 0; i < length; i++)
            {
                EditorUtility.DisplayProgressBar("分析主资源", assetPaths[i], (float)(i + 1) / length);
                AddAsset(assetPaths[i], true);

                string[] deps = AssetDatabase.GetDependencies(assetPaths[i], true);
                for (int j = 0; j < deps.Length; j++)
                {
                    if (assetPaths[i] == deps[j]) continue;
                    AddAsset(deps[j], false);
                }
            }

            foreach (var info in _allAssets)
            {
                info.Value.AnalyzeAsset();
            }
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("分析资源", "分析结束，请开始设置AssetBundle Name", "Ok");
            Resources.UnloadUnusedAssets();
        }

        public static void AddAsset(string assetPath, bool isMainAsset)
        {

            if (assetPath.EndsWith(EAssetBundleConst.IGNORE_BUILD_ASSET_SUFFIX)) return;

            if (_allAssets.ContainsKey(assetPath))
            {
                Debug.Assert(_allAssets[assetPath].IsMainAsset == isMainAsset, "主资源和依赖资源重复" + assetPath);
                return;
            }

            EAssetObjectInfo info = new EAssetObjectInfo(assetPath, isMainAsset);
            _allAssets.Add(info.AssetPath, info);
        }

        public static EAssetObjectInfo FindAssetObject(string assetPath)
        {
            if (_allAssets.ContainsKey(assetPath))
            {
                return _allAssets[assetPath];
            }
            return null;
        }
    }
}
