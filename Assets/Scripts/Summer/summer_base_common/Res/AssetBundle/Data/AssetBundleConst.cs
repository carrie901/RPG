
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// const可以变成config
    /// </summary>
    public class AssetBundleConst
    {
        #region AB打包的三个必要文件 AB的依赖表格 AB包表格 / 主资源对应到AB包

        #region AB的依赖表格

        public static string AssetbundleDepName = "depconfig";
        public static string AssetbundleDepPath = AssetbundleDepName + ".ab";
        public static string DepConfigName = "Assets/res_bundle/depconfig.bytes";                     // 配置文件名称

        #endregion

        #region AB 包表格

        public static string AssetbundlePackageName = "packageconfig";
        public static string AssetbundlePackagePath = AssetbundlePackageName + ".ab";
        public static string PackageConfigName = "Assets/res_bundle/packageconfig.bytes";                     // 配置文件名称

        #endregion

        #region 主资源对应到AB包的表格 主资源是有可能被打包到一个资源包中的

        public static string AssetbundleResName = "resconfig";
        public static string AssetbundleResPath = AssetbundleResName + ".ab";
        public static string ResConfigName = "Assets/res_bundle/resconfig.bytes";                     // 配置文件名称

        #endregion

        #endregion

        public static string AssetbundleMainDirectory = Application.streamingAssetsPath + "/rpg/";    // 打AssetBundle资源到指定的目录
        public static string ResDirectory = "res_bundle";                                              // 存放主资源的目录
        public static string AbResDirectory = AssetbundleMainDirectory + ResDirectory + "/";

        /// <summary>
        /// Ab资源的主目录，区别于依赖目录，这个目录下的所有资源都是主资源
        /// </summary>
        /// <returns></returns>
        public static string GetAbResDirectory()
        {
            return AbResDirectory;
        }

        /// <summary>
        /// AB的根目录
        /// </summary>
        /// <returns></returns>
        public static string GetAssetBundleRootDirectory()
        {
            return Application.streamingAssetsPath + "/rpg/";
        }
    }
}
