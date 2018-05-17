using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using Summer;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-28 10:56:2
// FileName : EPathHelper.cs
//=============================================================================

namespace SummerEditor
{
    /// <summary>
    /// 
    /// TODO 2018.3.28
    /// 有一些重复性质的方法，等待后期整理
    /// 可以对这些方法做一些归纳整理
    /// 1.得到型 得到相关的路径之类的
    /// 2.转换型 通过路径转换成指定类型的
    /// 3.检测型 判断路径类型
    /// 4.创建型
    /// 
    /// 
    /// TODO 2018.5.14
    ///     BUG:老实搞错Asset路径和File路径 他们之间的在部分API调用的时候会出现
    /// </summary>
	public class EPathHelper
    {
        public static string asset_directory = "Assets/";

        public static string relative_asset = Path.GetFullPath(Application.dataPath);

        #region 转换型 对路径做处理

        /// <summary>
        /// 绝对路径转成相对路径
        /// path=可以是绝对路径也可以是相对路径，返回是以Assets作为开头的路径 例如Assets/Raw/...
        /// </summary>
        public static string AbsoluteToRelativePathWithAssets(string path)
        {
            string tmp_path = NormalizePath(path);
            int index = tmp_path.IndexOf(asset_directory, StringComparison.Ordinal);
            if (index >= 0)
            {
                string result = tmp_path.Substring(index);
                return result;
            }
            else
            {
                Debug.LogError("路径转成Asset失败" + path);
                return path;
            }
        }

        /// <summary>
        /// 绝对路径转成相对路径 剔除了Assets/
        /// </summary>
        public static string AbsoluteToRelativePathRemoveAssets(string path)
        {
            path = NormalizePath(path);
            int last_idx = path.LastIndexOf(asset_directory, StringComparison.Ordinal);
            if (last_idx >= 0)
            {
                int start = last_idx + asset_directory.Length;
                int length = path.Length - start;
                string result = path.Substring(start, length);
                return result;
            }
            else
            {
                Debug.LogError("路径转成Asset失败" + path);
                return path;

            }
        }

        /// <summary>
        /// 移除后缀名
        /// </summary>
        public static string RemoveSuffix(string path)
        {
            int last_idx = path.LastIndexOf(".", StringComparison.Ordinal);
            if (last_idx < 0)
                return path;
            path = path.Substring(0, last_idx);
            return path;
        }

        /// <summary>
        /// 去掉Assets/和后缀的路径
        /// </summary>
        public static string RemoveAssetsAndSuffixforPath(string file_path)
        {
            string tmp_path = NormalizePath(file_path);
            int last_idx = tmp_path.IndexOf(asset_directory, StringComparison.Ordinal);
            if (last_idx >= 0)
            {
                int start = last_idx + asset_directory.Length;
                tmp_path = tmp_path.Substring(start);
            }

            return RemoveSuffix(tmp_path);
        }

        //标准化路径 把绝对路径的符号转成相对路径的符号
        public static string NormalizePath(string path)
        {
            return path.Replace('\\', '/');
        }

        //格式化Assets路径
        public static string FormatAssetPath(string path)
        {
            int index = path.IndexOf("Assets", StringComparison.Ordinal);
            if (index != -1)
            {
                path = path.Substring(index);
            }
            return NormalizePath(path);
        }

        #endregion

        #region 得到类型 指定根目录得到其下所有的文件

        /// <summary>
        /// 得到Asset目录下的资源，剔除掉meta文件，并且格式转成Asset\格式
        /// </summary>
        public static List<string> GetAssetPathList01(string root_path, bool deep, string suffix = "*.*")
        {
            List<string> ret = new List<string>();
            ScanDirectoryFile(root_path, deep, ret, suffix);
            NoEndsWithFilter filter = new NoEndsWithFilter(".meta");
            SuffixHelper.Filter(ret, filter);
            for (int i = 0; i < ret.Count; ++i)
            {
                ret[i] = AbsoluteToRelativePathWithAssets(ret[i]);
            }

            return ret;
        }

        public static List<string> GetAssetPathList(string root_path, bool deep, string suffix = "*.*")
        {
            List<string> ret = new List<string>();
            ScanDirectoryFile(root_path, deep, ret, suffix);

            for (int i = 0; i < ret.Count; ++i)
            {
                ret[i] = FormatAssetPath(ret[i]);
            }

            return ret;
        }
        /// <summary>
        /// 扫描文件夹 返回所有的文件路径
        /// </summary>
        public static void ScanDirectoryFile(string root, bool deep, List<string> list, string suffix = "*.*")
        {
            if (string.IsNullOrEmpty(root) || !Directory.Exists(root))
            {
                Debug.LogError("scan directory file failed! " + root);
                return;
            }

            DirectoryInfo dir_info = new DirectoryInfo(root);
            FileInfo[] files = dir_info.GetFiles(suffix);
            int length = files.Length;
            for (int i = 0; i < length; ++i)
            {
                list.Add(files[i].FullName);
            }

            if (deep)
            {
                DirectoryInfo[] dirs = dir_info.GetDirectories();
                length = dirs.Length;
                for (int i = 0; i < length; ++i)
                {
                    ScanDirectoryFile(dirs[i].FullName, true, list, suffix);
                }
            }
        }

        public static void ScanDirectory(string root, List<string> list)
        {
            list.Clear();
            if (string.IsNullOrEmpty(root) || !Directory.Exists(root))
            {
                Debug.LogError("scan directory file failed! " + root);
                return;
            }

            DirectoryInfo dir_info = new DirectoryInfo(root);
            DirectoryInfo[] child_dirs = dir_info.GetDirectories();
            for (int i = 0; i < child_dirs.Length; i++)
            {
                list.Add(child_dirs[i].FullName);
            }
        }

        /// <summary>
        /// 强制要求是 / 而不是\
        /// </summary>
        public static string GetDirectory(string path)
        {
            string normailze_path = NormalizePath(path);
            int index = normailze_path.LastIndexOf("/", StringComparison.Ordinal);
            if (index >= 0)
            {
                string result = normailze_path.Substring(0, index);
                return result;
            }
            else
            {
                Debug.Log("查询目录失败:" + path);
                return path;
            }
        }

        #endregion

        #region 创建型

        //创建文件夹
        public static void CreateDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        #endregion

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
            /*if (PathEndWithExt(path, EditorConst.model_exts))
            {
                string _asset_path = FormatAssetPath(path);
                ModelImporter model_importer = AssetImporter.GetAtPath(_asset_path) as ModelImporter;
                if (model_importer != null && model_importer.importAnimation)
                {
                    return true;
                }
                return false;
            }*/
            return PathEndWithExt(path, EditorConst.animation_exts);
        }

        #endregion

        #region private 

        /// <summary>
        /// 通过路径和后缀名判断类型
        /// </summary>
        public static bool PathEndWithExt(string path, string[] ext)
        {
            int length = ext.Length;
            for (int i = 0; i < length; ++i)
            {
                if (path.EndsWith(ext[i], StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
