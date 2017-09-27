using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Summer;
using UnityEditor;

namespace SummerEditor
{
    public class EabBuildMenu
    {
        [MenuItem("Tool/AssetBundle/4.Build", false, 4)]
        public static void BuildAssetBundle()
        {
            EabBuildTool.BuildAssetBundle();
        }
    }

    //=============================================================================
    // Author : mashaomin
    // CreateTime : 2017-9-7
    // 整体步骤
    // 1.分析
    // 2.bundle策略
    // 3.设置名字
    // 4.build
    // 5.根据原依赖文件生成自身的依赖文件
    //=============================================================================
    public class EabBuildTool
    {

        public static void BuildAssetBundle()
        {


            // AB包输出路径
            string out_path = Application.streamingAssetsPath + "/AssetBundle";

            // 检查路径是否存在
            CheckDirAndCreate(out_path);

            BuildPipeline.BuildAssetBundles(out_path, 0, EditorUserBuildSettings.activeBuildTarget);

            // 刚创建的文件夹和目录能马上再Project视窗中出现
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("AssetBundle Build结束", "检查打包是否结束", "确定");
        }

        /// <summary>
        /// 判断路径是否存在,不存在则创建
        /// </summary>
        public static void CheckDirAndCreate(string dir_path)
        {
            if (!Directory.Exists(dir_path))
            {
                Directory.CreateDirectory(dir_path);
            }
        }
    }
}

