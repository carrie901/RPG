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
            string[] asset_bundles = AssetDatabase.GetAllAssetBundleNames();
            string[] asset_paths = AssetDatabase.GetAllAssetPaths();
            CreateResFile(asset_bundles);
            CreatePackageFile(asset_bundles);
            CreateDepFile(asset_bundles);
        }

        public static void CreateResFile(string[] asset_bundles)
        {
            // 2.通过过滤器剔除掉不是主目录的AssetBundle
            List<string> filter_files = SuffixHelper.Filter(asset_bundles, new StartsWithFilter(EAssetBundleConst.main_res_driectory));
            filter_files.Clear();
            filter_files.AddRange(asset_bundles);

            List<List<string>> result_map = new List<List<string>>();
            int length = filter_files.Count;
            for (int ab_index = 0; ab_index < length; ab_index++)
            {
                // 这个资源的AssetBundle名字
                string ab_name = filter_files[ab_index];
                string[] asset_bundle_names = AssetDatabase.GetAssetPathsFromAssetBundle(ab_name);
                for (int main_index = 0; main_index < asset_bundle_names.Length; main_index++)
                {
                    // 主目录下的资源路径
                    string main_ab_path = asset_bundle_names[main_index];
                    if (!main_ab_path.StartsWith(EAssetBundleConst.main_res_driectory)) continue;

                    List<string> result = new List<string>();

                    main_ab_path = EPathHelper.RemoveSuffix(main_ab_path);
                    result.Add(main_ab_path.Replace("Assets/", string.Empty));
                    result.Add(ab_name);
                    result_map.Add(result);
                }
            }
            CreateConfig(result_map, AssetBundleConst.res_config_name);
        }

        public static void CreatePackageFile(string[] asset_bundles)
        {
            List<List<string>> result_map = new List<List<string>>();
            int length = asset_bundles.Length;
            for (int i = 0; i < length; i++)
            {
                string ab_name = asset_bundles[i];
                string[] asset_bundle_names = AssetDatabase.GetAssetPathsFromAssetBundle(ab_name);

                List<string> result = new List<string>();
                result.Add(ab_name);
                for (int k = 0; k < asset_bundle_names.Length; k++)
                {
                    string no_suffix = EPathHelper.RemoveSuffix(asset_bundle_names[k]);
                    string tmp_name = EPathHelper.GetName1(no_suffix);
                    Debug.Assert(!string.IsNullOrEmpty(tmp_name), asset_bundle_names[k]);

                    result.Add(no_suffix.Replace("Assets/", string.Empty));
                    result.Add(tmp_name);
                }
                result_map.Add(result);
            }

            CreateConfig(result_map, AssetBundleConst.package_config_name);
        }

        public static void CreateDepFile(string[] asset_bundles)
        {
            List<List<string>> result_map = new List<List<string>>();
            int length = asset_bundles.Length;
            for (int i = 0; i < length; i++)
            {
                List<string> result = new List<string>();
                string[] deps = AssetDatabase.GetAssetBundleDependencies(asset_bundles[i], true);
                result.Add(asset_bundles[i]);
                result.Add(deps.Length.ToString());
                for (int k = 0; k < deps.Length; k++)
                {
                    result.Add(deps[k]);
                }
                result_map.Add(result);
            }
            CreateConfig(result_map, AssetBundleConst.dep_config_name);
        }

        // 创建配置文件
        public static void CreateConfig(List<List<string>> result_map, string path)
        {
            StringBuilder sb = new StringBuilder();
            result_map.Sort(SortByName);

            int length = result_map.Count;
            for (int i = 0; i < length; i++)
            {
                List<string> values = result_map[i];
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

        public static int SortByName(List<string> a, List<string> b)
        {
            if (a == null || a.Count <= 1 || b == null || b.Count < 1) return 0;
            return String.Compare(a[0], b[0], StringComparison.Ordinal);
        }


        #endregion

    }
}

