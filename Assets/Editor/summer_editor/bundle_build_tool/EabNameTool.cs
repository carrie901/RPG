using System;
using System.Collections.Generic;
using System.IO;
using Summer;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class EabNameMenu
    {
        [MenuItem("Tool/AssetBundle/3.资源命名(剔除1.分析和2.策略)", false, 3)]
        public static void SetAssetBundleName()
        {
            // 解析配置文件
            EdNode node = ParseConfig();
            if (node == null) return;
            EdNode assetbundle_node = node.GetNode("AssetBundle");
            if (assetbundle_node == null) return;

            List<EdNode> ab_nodes = assetbundle_node.GetNodes("AssetBundleInfo");
            List<DirectoryInfo> dir_map = new List<DirectoryInfo>();
            int length = ab_nodes.Count;

            // 得到需要打包的目录
            for (int i = 0; i < length; i++)
            {
                string path = ab_nodes[i].GetAttribute("path").ToStr();
                DirectoryInfo dir_info = new DirectoryInfo(path);
                dir_map.Add(dir_info);
            }

            // 根据目录得到对应的文件名
            List<string> files = new List<string>();
            length = dir_map.Count;
            try
            {
                for (int i = 0; i < length; i++)
                {
                    DirectoryInfo dir_info = dir_map[i];
                    FileInfo[] fileinfos = dir_info.GetFiles();
                    //遍历所有子文件查找文件  
                    for (int j = 0; j < fileinfos.Length; j++)
                    {
                        // 剔除Assets和后缀
                        string full_name = fileinfos[j].FullName;
                        string extension = Path.GetExtension(full_name);
                        if (extension == ".meta") continue;

                        files.Add(full_name);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }


            EabNameTool.SetAssetBundleName(files.ToArray());
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("资源命名结束", "资源命名", "确定");
        }

        [MenuItem("Tool/AssetBundleOther/清除所有AssetBundleName")]
        public static void ClearAssetBundleName()
        {
            EabNameTool.ClearAllAssetBundleName();
        }

        public static EdNode ParseConfig()
        {
            ResMd rm = new ResMd();
            string str_path = "Assets\\Editor\\summer_editor\\editor_config\\build_ab_config.txt";
            string content = FileHelper.ReadTxtByFile(str_path);
            rm.ParseText(content);
            return rm._root_node;
        }
    }

    //=============================================================================
    // Author : mashaomin
    // CreateTime : 2017-9-7
    // 设置Asset的AssetBundle Name
    // 又是一个半成品的工具哦，等待后期修改，确认方案
    //=============================================================================
    public class EabNameTool
    {
        const string EXTENSION = ".ab";

        public static void SetAssetBundleName(string[] full_names)
        {
            for (int i = 0; i < full_names.Length; i++)
            {
                string name = full_names[i];
                float progress = (i + 1f) / full_names.Length;
                SetAssetBundleName(name);
                EditorUtility.DisplayProgressBar("正在生成AssetBundleName", "Change: " + name, progress);
            }

            EditorUtility.ClearProgressBar();
        }
        public static void SetAssetBundleName(string full_name)
        {
            full_name = EditorCommon.AbsoluteToRelativePathRemoveAssets(full_name);
            AssetImporter importer = AssetImporter.GetAtPath(full_name);
            if (importer != null)
            {
                string str = EditorCommon.NormalizeAssetBundle(full_name);
                importer.assetBundleName = str + EXTENSION;
                importer.SaveAndReimport();
            }
        }
        public static void SetAssetBundleName(string full_name, string asset_bundle_name)
        {
            full_name = EditorCommon.AbsoluteToRelativePathRemoveAssets(full_name);
            AssetImporter importer = AssetImporter.GetAtPath(full_name);
            if (importer != null)
            {
                string str = EditorCommon.NormalizeAssetBundle(asset_bundle_name);
                importer.assetBundleName = str;
                importer.SaveAndReimport();
            }
        }
        public static void SetSelectionAssetBundleName()
        {
            foreach (var id in Selection.instanceIDs)
            {
                string str = AssetDatabase.GetAssetPath(id);
                SetAssetBundleName(str);
            }
            AssetDatabase.Refresh();
        }
        public static void ClearAssetBundleName(string full_name)
        {
            full_name = EditorCommon.AbsoluteToRelativePathRemoveAssets(full_name);
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
                AssetDatabase.RemoveAssetBundleName(names[i], true);
            }

            AssetDatabase.RemoveUnusedAssetBundleNames();
        }

    }
}

