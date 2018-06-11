

using System.IO;
using UnityEditor;

namespace SummerEditor
{
    public class AssetBundleBuildE
    {
        public static void AllAssetBundleBuild()
        {
            if (!Directory.Exists(EAssetBundleConst.assetbundle_directory))
            {
                Directory.CreateDirectory(EAssetBundleConst.assetbundle_directory);
            }

            BuildPipeline.BuildAssetBundles(EAssetBundleConst.assetbundle_directory, FindBuildAssetBundleOptions(), FindBuildTarget());
            EditorUtility.DisplayDialog("资源打包", "打包完毕", "OK");

        }

        public static BuildTarget FindBuildTarget()
        {
            return BuildTarget.Android;
        }

        public static BuildAssetBundleOptions FindBuildAssetBundleOptions()
        {
            return  BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.ForceRebuildAssetBundle;//ForceRebuildAssetBundle 
        }
    }
}
