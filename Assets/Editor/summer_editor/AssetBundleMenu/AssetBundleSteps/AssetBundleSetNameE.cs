using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class AssetBundleSetNameE
    {
        /// <summary>
        /// 设置主资源名字
        /// </summary>
        public static void SetMainAbName()
        {
            /*Dictionary<string, EabMainVbo> main_ab = AssetAnalysisE._main_ab_map;
            int index = 1;
            foreach (var variable in main_ab)
            {
                EditorUtility.DisplayProgressBar("设置AssetBundle名字", variable.Value.asset_path, (float)index / main_ab.Count);
                SetAbNameByPath(variable.Value.asset_path);
            }*/

            List<string> assets_path = EPathHelper.GetAssetPathList01("Assets/Res/", true);
            int length = assets_path.Count;
            for (int i = 0; i < length; i++)
            {
                EditorUtility.DisplayProgressBar("设置AssetBundle名字", assets_path[i], (float)(i + 1) / length);
                SetAbNameByPath(assets_path[i]);
            }
            EditorUtility.ClearProgressBar();
            Resources.UnloadUnusedAssets();
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        #region 设置Asset的Bundle Name

        /// <summary>
        /// 设置选中的Asset的BundleName
        /// </summary>
        public static void SetSelectionAssetBundleName()
        {
            foreach (var id in Selection.instanceIDs)
            {
                string str = AssetDatabase.GetAssetPath(id);
                SetAbNameByPath(str);
            }
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        public static void SetAbNameByPath(string file_path)
        {
            string asset_path = EPathHelper.AbsoluteToRelativePathWithAssets(file_path);
            AssetImporter importer = AssetImporter.GetAtPath(asset_path);
            if (importer != null)
            {
                string str = EPathHelper.RemoveAssetsAndSuffixforPath(file_path);
                importer.assetBundleName = str + EAssetBundleConst.ASSETBUNDLE_EXTENSION;
                importer.SaveAndReimport();
            }
            else
            {
                UnityEngine.Debug.LogError("找不到对应的路径的资源:" + file_path);
            }
        }

        /// <summary>
        /// 根据Asset的上层目录设置Asset的名字
        /// </summary>
        public static void SetAbNameByDirectory(string asset_path)
        {
            AssetImporter importer = AssetImporter.GetAtPath(asset_path);
            if (importer != null)
            {
                string remove_asset_path = EPathHelper.AbsoluteToRelativePathRemoveAssets(asset_path);
                string str = EPathHelper.GetDirectory(remove_asset_path);
                importer.assetBundleName = str + EAssetBundleConst.ASSETBUNDLE_EXTENSION;
                importer.SaveAndReimport();
            }
            else
            {
                UnityEngine.Debug.LogError("找不到对应的路径的资源:" + asset_path);
            }
        }

        /// <summary>
        /// 根据外部名字设置Asset的名字
        /// </summary>
        /// <param name="asset_path"></param>
        /// <param name="ab_name"></param>
        public static void SetAbNameByParam(string asset_path, string ab_name)
        {
            AssetImporter importer = AssetImporter.GetAtPath(asset_path);
            if (importer != null)
            {
                string assetbundle_name = EPathHelper.NormalizePath(ab_name);
                importer.assetBundleName = assetbundle_name + EAssetBundleConst.ASSETBUNDLE_EXTENSION;
                importer.SaveAndReimport();
            }
            else
            {
                UnityEngine.Debug.LogError("找不到对应的路径的资源:" + asset_path);
            }
        }

        #endregion

        #region 清除名字

        public static void ClearAssetBundleName(string full_name)
        {
            full_name = EPathHelper.AbsoluteToRelativePathRemoveAssets(full_name);
            AssetImporter importer = AssetImporter.GetAtPath(full_name);
            if (importer != null)
            {
                importer.assetBundleName = "";
                importer.SaveAndReimport();
            }
        }
        public static void ClearSelectionAssetBundleName()
        {
            foreach (var id in Selection.instanceIDs)
            {
                string str = AssetDatabase.GetAssetPath(id);
                ClearAssetBundleName(str);
            }
            AssetDatabase.Refresh();
        }
        public static void ClearAllAssetBundleName()
        {
            string[] names = AssetDatabase.GetAllAssetBundleNames();

            int length = names.Length;
            for (int i = 0; i < length; i++)
            {
                EditorUtility.DisplayProgressBar("设置AssetBundle名字", names[i], (float)(i + 1) / length);
                AssetDatabase.RemoveAssetBundleName(names[i], true);
            }
            EditorUtility.ClearProgressBar();
            AssetDatabase.RemoveUnusedAssetBundleNames();
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            EditorUtility.DisplayDialog("清除AssetBundle", "清除完成，请查看", "OK");
        }

        #endregion
    }
}
