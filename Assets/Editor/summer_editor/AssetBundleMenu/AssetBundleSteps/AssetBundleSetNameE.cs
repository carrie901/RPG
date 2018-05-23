using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class AssetBundleSetNameE
    {
        #region 主资源设置名字，进行打包

        /// <summary>
        /// 设置主资源名字
        /// </summary>
        public static void OblyMainAbName()
        {
            List<string> assets_path = EPathHelper.GetAssetsPath(EAssetBundleConst.main_res_driectory, true);
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

        #endregion

        #region  根据分析的结果进行大bundle

        public static void SetAllAssetName()
        {
            Dictionary<string, EAssetObjectInfo> all_assets = EAssetBundleAnalysis._all_assets;

            int index = 1;
            foreach (var info in all_assets)
            {
                EAssetObjectInfo asset_info = info.Value;
                index++;
                EditorUtility.DisplayProgressBar("设置主AssetBundle名字", asset_info.AssetPath, (float)(index) / all_assets.Count);
                if (asset_info.IsMainAsset)
                {
                    SetAbNameByPath(asset_info.AssetPath);
                }
                else if (asset_info.RefCount > 1)
                {
                    SetAbNameByPath(asset_info.AssetPath);
                }
            }

            EditorUtility.ClearProgressBar();
            Resources.UnloadUnusedAssets();
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();

            EditorUtility.DisplayDialog("设置名字完成", "请查看log日志", "Ok");
        }

        #endregion

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

        /// <summary>
        /// 根据文件的File Path设置Asset的BundleName BundleName=Asset/XX/
        /// </summary>
        public static void SetAbNameByPath(string file_path)
        {
            string asset_path = EPathHelper.AbsoluteToRelativePathWithAssets(file_path);
            AssetImporter importer = AssetImporter.GetAtPath(asset_path);
            if (importer != null)
            {
                // 去掉Assets/ 和文件的后缀
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
                // 剔除Assets/ 
                string remove_asset_path = EPathHelper.AbsoluteToRelativePathRemoveAssets(asset_path);
                // 得到文件的目录
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
        public static void SetAbNameByParam(string file_path, string ab_name)
        {
            string asset_path = EPathHelper.AbsoluteToRelativePathWithAssets(file_path);
            AssetImporter importer = AssetImporter.GetAtPath(asset_path);
            if (importer != null)
            {
                string assetbundle_name = EPathHelper.NormalizePath(ab_name)+ EAssetBundleConst.ASSETBUNDLE_EXTENSION;
                importer.assetBundleName = assetbundle_name ;
                importer.SaveAndReimport();
            }
            else
            {
                UnityEngine.Debug.LogError("找不到对应的路径的资源:" + file_path);
            }
        }

        #endregion

        #region 清除名字

        //[MenuItem("Assets/Tools/AssetBundle ClearName")]
        public static void ClearSelectionAssetBundleName()
        {
            foreach (var id in Selection.instanceIDs)
            {
                string asset_path = AssetDatabase.GetAssetPath(id);
                _clear_assetbundle_name(asset_path);
            }
            AssetDatabase.Refresh();
        }

        // 清除当前所有的AssetBundleName
        public static void ClearAllAssetBundleName()
        {
            string[] names = AssetDatabase.GetAllAssetBundleNames();

            int length = names.Length;
            for (int i = 0; i < length; i++)
            {
                EditorUtility.DisplayProgressBar("清除AssetBundle名字", "清除:" + names[i] + "的AssetBundle的名字", (float)(i + 1) / length);
                AssetDatabase.RemoveAssetBundleName(names[i], true);
            }
            EditorUtility.ClearProgressBar();
            AssetDatabase.RemoveUnusedAssetBundleNames();
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        #endregion

        #region private 

        /// <summary>
        /// 清除指定Asset路径下的AssetBundleName
        /// </summary>
        public static void _clear_assetbundle_name(string file_path)
        {
            string asset_path = EPathHelper.AbsoluteToRelativePathWithAssets(file_path);
            AssetImporter importer = AssetImporter.GetAtPath(asset_path);
            if (importer != null)
            {
                importer.assetBundleName = "";
                importer.SaveAndReimport();
            }
        }

        #endregion
    }
}
