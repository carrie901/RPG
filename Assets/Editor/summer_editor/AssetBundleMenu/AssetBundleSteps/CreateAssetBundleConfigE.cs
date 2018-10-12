using System;
using System.Collections.Generic;
using System.Text;
using Summer;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// 生成资源配置列表,加载资源都是从这张表格中加载
    /// </summary>
    public class CreateAssetBundleConfigE
    {
        #region 包配置表

        public static void CreateAbConfigFile()
        {
            
            // 1.得到所有的AssetBundle Name
            string[] assetBundles = AssetDatabase.GetAllAssetBundleNames();
           
            //string[] assetPaths = AssetDatabase.GetAllAssetPaths();
            CreateResFile(assetBundles);
            CreatePackageFile(assetBundles);
            CreateDepFile(assetBundles);

            SetAbName();

        }

        public static void CreateResFile(string[] assetBundles)
        {
            
            // 2.通过过滤器剔除掉不是主目录的AssetBundle
            List<string> filterFiles = SuffixHelper.Filter(assetBundles, new StartsWithFilter(EAssetBundleConst.MAIN_RES_DRIECTORY));
            filterFiles.Clear();
            filterFiles.AddRange(assetBundles);

            List<List<string>> resultMap = new List<List<string>>();
            int length = filterFiles.Count;
            for (int abIndex = 0; abIndex < length; abIndex++)
            {
                // 这个资源的AssetBundle名字
                string abName = filterFiles[abIndex];
                string[] assetBundleNames = AssetDatabase.GetAssetPathsFromAssetBundle(abName);
                for (int mainIndex = 0; mainIndex < assetBundleNames.Length; mainIndex++)
                {
                    // 主目录下的资源路径
                    string mainAbPath = assetBundleNames[mainIndex];
                    if (!mainAbPath.StartsWith(EAssetBundleConst.MAIN_RES_DRIECTORY)) continue;

                    List<string> result = new List<string>();

                    mainAbPath = EPathHelper.RemoveSuffix(mainAbPath);
                    result.Add(mainAbPath.Replace("Assets/", string.Empty));
                    result.Add(abName);
                    resultMap.Add(result);
                }
            }
            CreateConfig(resultMap, AssetBundleConst.ResConfigName);
        }

        public static void CreatePackageFile(string[] assetBundles)
        {
            List<List<string>> resultMap = new List<List<string>>();
            int length = assetBundles.Length;
            for (int i = 0; i < length; i++)
            {
                string abName = assetBundles[i];
                string[] assetBundleNames = AssetDatabase.GetAssetPathsFromAssetBundle(abName);

                List<string> result = new List<string>();
                result.Add(abName);
                for (int k = 0; k < assetBundleNames.Length; k++)
                {
                    string noSuffix = EPathHelper.RemoveSuffix(assetBundleNames[k]);
                    string tmpName = EPathHelper.GetName1(noSuffix);
                    Debug.Assert(!string.IsNullOrEmpty(tmpName), assetBundleNames[k]);

                    result.Add(noSuffix.Replace("Assets/", string.Empty));
                    result.Add(tmpName);
                }
                resultMap.Add(result);
            }

            CreateConfig(resultMap, AssetBundleConst.PackageConfigName);
        }

        public static void CreateDepFile(string[] assetBundles)
        {
            List<List<string>> resultMap = new List<List<string>>();
            int length = assetBundles.Length;
            for (int i = 0; i < length; i++)
            {
                List<string> result = new List<string>();
                string[] deps = AssetDatabase.GetAssetBundleDependencies(assetBundles[i], true);
                result.Add(assetBundles[i]);
                result.Add(deps.Length.ToString());
                for (int k = 0; k < deps.Length; k++)
                {
                    result.Add(deps[k]);
                }
                resultMap.Add(result);
            }
            CreateConfig(resultMap, AssetBundleConst.DepConfigName);
        }

        // 创建配置文件
        public static void CreateConfig(List<List<string>> resultMap, string path)
        {
            StringBuilder sb = new StringBuilder();
            resultMap.Sort(SortByName);

            int length = resultMap.Count;
            for (int i = 0; i < length; i++)
            {
                List<string> values = resultMap[i];
                for (int k = 0; k < values.Count; k++)
                {
                    if (k == values.Count - 1)
                        sb.Append(values[k]);
                    else
                        sb.Append(values[k] + ",");
                }
                sb.AppendLine();
            }
            Summer.FileHelper.WriteTxtByFile(path, sb.ToString());
        }

        public static void SetAbName()
        {
            AssetBundleSetNameE.SetAbNameByPath(AssetBundleConst.ResConfigName);
            AssetBundleSetNameE.SetAbNameByPath(AssetBundleConst.PackageConfigName);
            AssetBundleSetNameE.SetAbNameByPath(AssetBundleConst.DepConfigName);
        }

        public static int SortByName(List<string> a, List<string> b)
        {
            if (a == null || a.Count <= 1 || b == null || b.Count < 1) return 0;
            return String.Compare(a[0], b[0], StringComparison.Ordinal);
        }


        #endregion

    }
}

