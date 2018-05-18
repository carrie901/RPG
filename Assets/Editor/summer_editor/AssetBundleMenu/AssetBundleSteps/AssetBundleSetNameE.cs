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
        public static void TestSetMainAbName()
        {
            List<string> assets_path = EPathHelper.GetAssetPathList01(EAssetBundleConst.main_driectory, true);
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

        public static void SetAllAssetName()
        {
            Dictionary<string, EAssetDepInfo> dep_map = AssetBundleAnalysisE._dep_ab_map;
            Dictionary<string, EAssetMainInfo> main_map = AssetBundleAnalysisE._main_ab_map;
            if (dep_map.Count == 0 || main_map.Count == 0)
            {
                EditorUtility.DisplayDialog("请先执行AssetBundle分析", "设置名字失败", "Ok");
                return;
            }

            /*for (int i = 0; i < length; i++)
            {
                EditorUtility.DisplayProgressBar("设置AssetBundle名字", assets_path[i], (float)(i + 1) / length);
                SetAbNameByPath(assets_path[i]);
            }*/

            int index = 1;
            foreach (var info in main_map)
            {
                if (info.Value.RefCount == 0)
                {
                    string path = info.Value.AssetPath;
                    EditorUtility.DisplayProgressBar("设置主AssetBundle名字", path, (float)(index) / main_map.Count);
                    SetAbNameByPath(path);
                    index++;
                }
                else
                {
                    Debug.LogError("path:" + info.Value.AssetPath + "Ref:" + info.Value.RefCount + " 主资源打包失败");
                }
            }

            index = 1;
            foreach (var info in dep_map)
            {
                if (info.Value.RefCount > 1)
                {
                    string path = info.Value.AssetPath;
                    EditorUtility.DisplayProgressBar("设置依赖资源名字", path, (float)(index) / dep_map.Count);
                    SetAbNameByPath(path);
                    index++;
                }
            }


            EditorUtility.ClearProgressBar();
            Resources.UnloadUnusedAssets();
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();

            EditorUtility.DisplayDialog("设置名字完成", "请查看log日志", "Ok");
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

        /// <summary>
        /// 根据文件的File Path设置Asset的BundleName BundleName=Asset/XX/
        /// </summary>
        /// <param name="file_path"></param>
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
