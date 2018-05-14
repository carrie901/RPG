

using UnityEngine;

namespace SummerEditor
{
    public class EAssetBundleConst
    {
        public const string IGNORE_SUFFIX = ".cs";                              // 资源的忽略类型
        public const string ASSET_PATH = "Assets/Res/Prefab";                    // 资源的主目录

        public const string SUFFIX_META = ".meta";                              // 忽略资源主目录的类型

        public const string ASSETBUNDLE_EXTENSION = ".ab";                      // 后缀名

        public const float ASSETBUILD_MAX_SIZE = 1024 * 1204;                        // 多个资源打包到一个AssetBundle中，这个AssetBundle的大小

        public static string assetbundle_directory = Application.streamingAssetsPath+"/rpg";
    }
}
