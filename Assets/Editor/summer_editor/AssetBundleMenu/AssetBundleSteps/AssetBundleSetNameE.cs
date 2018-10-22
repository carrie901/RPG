using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class AssetBundleSetNameE
    {

        public static void BuildAssetBundle()
        {

        }

        #region 主资源设置名字，进行打包

        /// <summary>
        /// 设置主资源名字
        /// </summary>
        public static void OblyMainAbName()
        {
            List<string> assetsPath = EPathHelper.GetAssetsPath(EAssetBundleConst.MAIN_RES_DRIECTORY, true);
            List<string> assetsPath1 = EPathHelper.GetAssetsPath(EAssetBundleConst.UI_MAIN_DIRECTORY, true);
            assetsPath.AddRange(assetsPath1);
            int length = assetsPath.Count;
            for (int i = 0; i < length; i++)
            {
                EditorUtility.DisplayProgressBar("设置AssetBundle名字", assetsPath[i], (float)(i + 1) / length);
                SetAbNameByPath(assetsPath[i]);
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
            Dictionary<string, EAssetObjectInfo> allAssets = EAssetBundleAnalysis._allAssets;
            BuildAssetStrateyManager.Init();
            BuildAssetStrateyManager.SetAssetBundleName();
            int index = 1;
            foreach (var info in allAssets)
            {
                EAssetObjectInfo assetInfo = info.Value;
                index++;
                EditorUtility.DisplayProgressBar("设置主AssetBundle名字", assetInfo.AssetPath, (float)(index) / allAssets.Count);

                if (BuildAssetStrateyManager.IsBundleStratey(assetInfo)) continue;


                if (assetInfo.IsMainAsset)
                {
                    SetAbNameByPath(assetInfo.AssetPath);
                }
                else if (assetInfo.RefCount > 1)
                {
                    SetAbNameByPath(assetInfo.AssetPath);
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

        public static void SetSelectionAssetBundleName1()
        {
            foreach (var id in Selection.instanceIDs)
            {
                string str = AssetDatabase.GetAssetPath(id);
                SetAbNameByDirectory(str);
            }
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }


        /// <summary>
        /// 根据文件的File Path设置Asset的BundleName BundleName=Asset/XX/
        /// </summary>
        public static void SetAbNameByPath(string filePath)
        {
            string assetPath = EPathHelper.AbsoluteToRelativePathWithAssets(filePath);
            AssetImporter importer = AssetImporter.GetAtPath(assetPath);
            if (importer != null)
            {
                // 去掉Assets/ 和文件的后缀
                string str = EPathHelper.RemoveAssetsAndSuffixforPath(filePath);
                Debug.AssertFormat(string.IsNullOrEmpty(importer.assetBundleName), "设置AssetBundle之前她已经有名字了:[{0}]", filePath);
                importer.assetBundleName = str + EAssetBundleConst.ASSETBUNDLE_EXTENSION;
                importer.SaveAndReimport();
            }
            else
            {
                UnityEngine.Debug.LogError("找不到对应的路径的资源:" + filePath);
            }
        }

        /// <summary>
        /// 根据Asset的上层目录设置Asset的名字
        /// </summary>
        public static void SetAbNameByDirectory(string assetPath)
        {
            AssetImporter importer = AssetImporter.GetAtPath(assetPath);
            if (importer != null)
            {
                // 剔除Assets/ 
                string removeAssetPath = EPathHelper.AbsoluteToRelativePathRemoveAssets(assetPath);
                // 得到文件的目录
                string str = EPathHelper.GetDirectory(removeAssetPath);
                importer.assetBundleName = str + EAssetBundleConst.ASSETBUNDLE_EXTENSION;
                importer.SaveAndReimport();
            }
            else
            {
                Debug.LogError("找不到对应的路径的资源:" + assetPath);
            }
        }

        /// <summary>
        /// 根据外部名字设置Asset的名字
        /// </summary>
        public static void SetAbNameByParam(string filePath, string abName)
        {
            string assetPath = EPathHelper.AbsoluteToRelativePathWithAssets(filePath);
            AssetImporter importer = AssetImporter.GetAtPath(assetPath);
            if (importer != null)
            {
                string assetbundleName = EPathHelper.NormalizePath(abName) + EAssetBundleConst.ASSETBUNDLE_EXTENSION;
                importer.assetBundleName = assetbundleName;
                importer.SaveAndReimport();
            }
            else
            {
                Debug.LogError("找不到对应的路径的资源:" + filePath);
            }
        }

        #endregion

        #region 清除名字

        //[MenuItem("Assets/Tools/AssetBundle ClearName")]
        public static void ClearSelectionAssetBundleName()
        {
            foreach (var id in Selection.instanceIDs)
            {
                string assetPath = AssetDatabase.GetAssetPath(id);
                _clear_assetbundle_name(assetPath);
            }
            AssetDatabase.Refresh();
        }

        // 清除当前所有的AssetBundleName
        public static void ClearAllAssetBundleName()
        {
            AssetDatabase.RemoveUnusedAssetBundleNames();
            string[] names = AssetDatabase.GetAllAssetBundleNames();

            int length = names.Length;
            for (int i = 0; i < length; i++)
            {
                EditorUtility.DisplayProgressBar("清除AssetBundle名字", "清除:" + names[i] + "的AssetBundle的名字", (float)(i + 1) / length);
                AssetDatabase.RemoveAssetBundleName(names[i], true);
            }
            EditorUtility.ClearProgressBar();

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        #endregion

        #region private 

        /// <summary>
        /// 清除指定Asset路径下的AssetBundleName
        /// </summary>
        public static void _clear_assetbundle_name(string filePath)
        {
            string assetPath = EPathHelper.AbsoluteToRelativePathWithAssets(filePath);
            AssetImporter importer = AssetImporter.GetAtPath(assetPath);
            if (importer != null)
            {
                importer.assetBundleName = "";
                importer.SaveAndReimport();
            }
        }

        #endregion
    }
}
