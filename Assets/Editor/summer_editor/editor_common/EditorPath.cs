using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public static class EditorPath
    {

        #region 通过路径判断目标文件类型（图片/脚本/材质球/shader等等相关）
        //图片
        public static bool IsTexture(string path)
        {
            return PathEndWithExt(path, EditorConst.texture_exts);
        }
        //材质
        public static bool IsMaterial(string path)
        {
            return PathEndWithExt(path, EditorConst.material_exts);
        }
        //模型
        public static bool IsModel(string path)
        {
            return PathEndWithExt(path, EditorConst.model_exts);
        }
        //.meta文件
        public static bool IsMeta(string path)
        {
            return PathEndWithExt(path, EditorConst.meta_exts);
        }
        //shader
        public static bool IsShader(string path)
        {
            return PathEndWithExt(path, EditorConst.shader_exts);
        }
        //脚本
        public static bool IsScript(string path)
        {
            return PathEndWithExt(path, EditorConst.script_exts);
        }
        //动作
        public static bool IsAnimation(string path)
        {
            if (PathEndWithExt(path, EditorConst.model_exts))
            {
                string assetPath = FormatAssetPath(path);
                ModelImporter modelImporter = AssetImporter.GetAtPath(assetPath) as ModelImporter;
                if (modelImporter != null && modelImporter.importAnimation)
                {
                    return true;
                }
                return false;
            }
            return PathEndWithExt(path, EditorConst.animation_exts);
        }
        #endregion

        public static List<string> GetAssetPathList(string root_path)
        {
            List<string> ret = new List<string>();
            ScanDirectoryFile(root_path, true, ret);

            for (int i = 0; i < ret.Count; ++i)
            {
                ret[i] = FormatAssetPath(ret[i]);
            }

            return ret;
        }
        //扫描文件夹 返回所有的文件路径
        public static void ScanDirectoryFile(string root, bool deep, List<string> list)
        {
            if (string.IsNullOrEmpty(root) || !Directory.Exists(root))
            {
                Debug.LogError("scan directory file failed! " + root);
                return;
            }

            DirectoryInfo dir_info = new DirectoryInfo(root);
            FileInfo[] files = dir_info.GetFiles("*.*");
            int length = files.Length;
            for (int i = 0; i < length; ++i)
            {
                list.Add(files[i].FullName);
            }

            if (deep)
            {
                DirectoryInfo[] dirs = dir_info.GetDirectories("*.*");
                length = dirs.Length;
                for (int i = 0; i < length; ++i)
                {
                    ScanDirectoryFile(dirs[i].FullName, deep, list);
                }
            }
        }
        //格式化Assets路径
        public static string FormatAssetPath(string path)
        {
            int index = path.IndexOf("Assets", StringComparison.Ordinal);
            if (index != -1)
            {
                path = path.Substring(index);
            }
            return NormalizePathSplash(path);
        }
        //标准化路径
        public static string NormalizePathSplash(string path)
        {
            return path.Replace('\\', '/');
        }
        //通过路径和后缀名判断类型
        public static bool PathEndWithExt(string path, string[] ext)
        {
            for (int i = 0; i < ext.Length; ++i)
            {
                if (path.EndsWith(ext[i], System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}


