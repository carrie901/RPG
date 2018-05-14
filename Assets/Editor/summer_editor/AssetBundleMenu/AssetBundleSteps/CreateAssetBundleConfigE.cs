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
        public static string main_driectory = "Assets/Res/";                         // 扫描的目录
        public static string config_name = "Assets/Res/ResConfig.bytes";                      // 配置文件名称

        
        public static void CreateAbConfigFile()
        {
            // 1.得到所有的AssetBundle Name
            string[] asset_bundles = AssetDatabase.GetAllAssetBundleNames();
            // 2.通过过滤器剔除掉不是主目录的AssetBundle
            List<string> filter_files = SuffixHelper.Filter(asset_bundles, new StartsWithFilter(main_driectory));
            filter_files.Clear();
            filter_files.AddRange(asset_bundles);
            List<string[]> result_map = new List<string[]>();
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
                    if (!main_ab_path.StartsWith(main_driectory)) continue;

                    string[] result = new string[2];
                    result[0] = main_ab_path.Replace(main_driectory, string.Empty);
                    result[1] = ab_name;
                    result_map.Add(result);
                }
            }

            CreateConfig(result_map);
        }

        // 创建配置文件
        public static void CreateConfig(List<string[]> result_map)
        {
            StringBuilder sb = new StringBuilder();
            result_map.Sort(SortByName);

            int length = result_map.Count;
            for (int i = 0; i < length; i++)
            {
                string[] values = result_map[i];
                sb.AppendLine(values[0] + "," + values[1]);
            }
            Summer.FileHelper.WriteTxtByFile(config_name, sb.ToString());
        }

        public static int SortByName(string[] a, string[] b)
        {
            if (a == null || a.Length != 2 || b == null || b.Length != 2) return 0;
            return String.Compare(a[0], b[0], StringComparison.Ordinal);
        }


    }
}

