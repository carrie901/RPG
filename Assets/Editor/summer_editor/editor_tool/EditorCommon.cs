using System;
using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    public class EditorCommon
    {
        public static string asset_directory = "Assets";
        //绝对路径转换为Unity/Assets相对路径
        public static string AbsoluteToRelativePathRemoveAssets(string path)
        {
            path = path.Replace('\\', '/');
            int last_idx = path.LastIndexOf(asset_directory, StringComparison.Ordinal);
            if (last_idx < 0)
                return path;

            int start = last_idx + asset_directory.Length;
            int length = path.Length - start;
            return asset_directory + path.Substring(start, length);
        }

        public static string AbsoluteToRelativePath(string path)
        {
            path = path.Replace('\\', '/');
            int last_idx = path.LastIndexOf(asset_directory, StringComparison.Ordinal);
            if (last_idx < 0)
                return path;

            int start = last_idx + asset_directory.Length + 1;
            int length = path.Length - start;
            return path.Substring(start, length);
        }

        public static string RemoveSuffix(string path)
        {
            int last_idx = path.LastIndexOf(".", StringComparison.Ordinal);
            if (last_idx < 0)
                return path;
            path = path.Substring(0, last_idx);
            return path;
        }

        const string ASSETS = "Assets";
        //规范化名字，去掉Assets/和后缀
        public static string NormalizeAssetBundle(string full_name)
        {
            int last_idx = full_name.IndexOf(ASSETS, StringComparison.Ordinal);
            if (last_idx >= 0)
            {
                int start = last_idx + ASSETS.Length + 1;
                full_name = full_name.Substring(start);
            }

            return RemoveSuffix(full_name);
        }
    }
}

