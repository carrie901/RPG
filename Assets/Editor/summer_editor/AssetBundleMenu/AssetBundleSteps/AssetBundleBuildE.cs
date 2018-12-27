

using System.IO;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class AssetBundleBuildE
    {
        public static void AllAssetBundleBuild()
        {
            if (!Directory.Exists(EAssetBundleConst.AssetbundleDirectory))
            {
                Directory.CreateDirectory(EAssetBundleConst.AssetbundleDirectory);
            }
            
            BuildPipeline.BuildAssetBundles(EAssetBundleConst.AssetbundleDirectory, FindBuildAssetBundleOptions(), FindBuildTarget());

            EditorUtility.DisplayDialog("资源打包", "打包完毕", "OK");

        }

        public static BuildTarget FindBuildTarget()
        {
            return BuildTarget.Android;
        }

        public static BuildAssetBundleOptions FindBuildAssetBundleOptions()
        {
            return BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.ForceRebuildAssetBundle ;//ForceRebuildAssetBundle 
        }
    }
}
