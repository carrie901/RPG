﻿using System;
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
        public static string _assetDirectory = "Assets/";

        public static string _relativeAsset = Path.GetFullPath(Application.dataPath);

        #region 转换型 对路径做处理

        /// <summary>
        /// 绝对路径转成相对路径
        /// path=可以是绝对路径也可以是相对路径，返回是以Assets作为开头的路径 例如Assets/Raw/...
        /// </summary>
        public static string AbsoluteToRelativePathWithAssets(string path)
        {
            string tmpPath = NormalizePath(path);
            int index = tmpPath.IndexOf(_assetDirectory, StringComparison.Ordinal);
            if (index >= 0)
            {
                string result = tmpPath.Substring(index);
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
            int lastIdx = path.LastIndexOf(_assetDirectory, StringComparison.Ordinal);
            if (lastIdx >= 0)
            {
                int start = lastIdx + _assetDirectory.Length;
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
            int lastIdx = path.LastIndexOf(".", StringComparison.Ordinal);
            if (lastIdx < 0)
                return path;
            path = path.Substring(0, lastIdx);
            return path;
        }

        /// <summary>
        /// 去掉Assets/和后缀的路径
        /// </summary>
        public static string RemoveAssetsAndSuffixforPath(string filePath)
        {
            string tmpPath = NormalizePath(filePath);
            int lastIdx = tmpPath.IndexOf(_assetDirectory, StringComparison.Ordinal);
            if (lastIdx >= 0)
            {
                int start = lastIdx + _assetDirectory.Length;
                tmpPath = tmpPath.Substring(start);
            }

            return RemoveSuffix(tmpPath);
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
        public static List<string> GetAssetsPath(string rootPath, bool deep, string suffix = "*.*")
        {
            List<string> ret = GetFilesPath(rootPath, deep, suffix);
            for (int i = 0; i < ret.Count; ++i)
            {
                ret[i] = AbsoluteToRelativePathWithAssets(ret[i]);
            }

            return ret;
        }

        public static List<string> GetFilesPath(string rootPath, bool deep, string suffix = "*.*")
        {
            List<string> ret = new List<string>();
            ScanDirectoryFile(rootPath, deep, ret, suffix);
            NoEndsWithFilter filter = new NoEndsWithFilter(".meta");
            SuffixHelper.Filter(ret, filter);
            return ret;
        }

        /// <summary>
        /// 扫描文件夹 返回所有的文件路径
        /// </summary>
        protected static void ScanDirectoryFile(string root, bool deep, List<string> list, string suffix = "*.*")
        {
            if (string.IsNullOrEmpty(root) || !Directory.Exists(root))
            {
                Debug.LogError("scan directory file failed! " + root);
                return;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(root);
            FileInfo[] files = dirInfo.GetFiles(suffix);
            int length = files.Length;
            for (int i = 0; i < length; ++i)
            {
                list.Add(files[i].FullName);
            }

            if (deep)
            {
                DirectoryInfo[] dirs = dirInfo.GetDirectories();
                length = dirs.Length;
                for (int i = 0; i < length; ++i)
                {
                    ScanDirectoryFile(dirs[i].FullName, true, list, suffix);
                }
            }
        }

        /// <summary>
        /// 强制要求是 / 而不是\
        /// </summary>
        public static string GetDirectory(string path)
        {
            string normailzePath = NormalizePath(path);
            int index = normailzePath.LastIndexOf("/", StringComparison.Ordinal);
            Debug.Assert(index >= 0, "GetDirectory Error");
            string result = normailzePath.Substring(0, index);
            return result;
        }

        /// <summary>
        /// 得到最近的目录
        /// </summary>
        public static string GetDirectoryLast(string path)
        {
            string normailzePath = NormalizePath(path);
            string[] names = normailzePath.Split('/');
            Debug.Assert(names.Length >= 2, "GetDirectoryLast Error");
            return names[names.Length - 2];
        }

        public static string GetName(string filePath)
        {
            filePath = NormalizePath(filePath);
            int index = filePath.LastIndexOf('/');
            if (index < 0)
                return filePath;
            else
                return filePath.Substring(index);
        }

        public static string GetName1(string filePath)
        {
            filePath = NormalizePath(filePath);
            int index = filePath.LastIndexOf('/');
            if (index < 0)
                return filePath;
            else
                return filePath.Substring(index + 1);
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
            return PathEndWithExt(path, AssetFormatConst._textureExts);
        }
        //材质
        public static bool IsMaterial(string path)
        {
            return PathEndWithExt(path, AssetFormatConst._materialExts);
        }
        //模型
        public static bool IsModel(string path)
        {
            return PathEndWithExt(path, AssetFormatConst._modelExts);
        }
        //.meta文件
        public static bool IsMeta(string path)
        {
            return PathEndWithExt(path, AssetFormatConst._metaExts);
        }
        //shader
        public static bool IsShader(string path)
        {
            return PathEndWithExt(path, AssetFormatConst._shaderExts);
        }
        //脚本
        public static bool IsScript(string path)
        {
            return PathEndWithExt(path, AssetFormatConst._scriptExts);
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
            return PathEndWithExt(path, AssetFormatConst._animationExts);
        }

        #endregion

        public static long GetFileSize(string path)
        {
            FileInfo info = new FileInfo(path);
            if (info.Exists)
                return info.Length;
            return 0;
        }

        public static bool IsExitDirectory(string path)
        {
            if (Directory.Exists(path)) return true;
            Debug.LogErrorFormat("目录不存在:[0]", path);
            return false;
        }

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
