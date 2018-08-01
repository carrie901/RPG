using System.Collections;
using System.Collections.Generic;
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

        public static string assetbundle_dep_name = "depconfig";
        public static string assetbundle_dep_path = assetbundle_dep_name + ".ab";
        public static string dep_config_name = "Assets/res_bundle/depconfig.bytes";                     // 配置文件名称

        #endregion

        #region AB 包表格

        public static string assetbundle_package_name = "packageconfig";
        public static string assetbundle_package_path = assetbundle_package_name + ".ab";
        public static string package_config_name = "Assets/res_bundle/packageconfig.bytes";                     // 配置文件名称

        #endregion

        #region 主资源对应到AB包的表格 主资源是有可能被打包到一个资源包中的

        public static string assetbundle_res_name = "resconfig";
        public static string assetbundle_res_path = assetbundle_res_name + ".ab";
        public static string res_config_name = "Assets/res_bundle/resconfig.bytes";                     // 配置文件名称

        #endregion

        #endregion

        public static string assetbundle_main_directory = Application.streamingAssetsPath + "/rpg/";    // 打AssetBundle资源到指定的目录
        public static string res_directory = "res_bundle";                                              // 存放主资源的目录
        public static string _ab_res_directory = assetbundle_main_directory + res_directory + "/";




        /// <summary>
        /// Ab资源的主目录，区别于依赖目录，这个目录下的所有资源都是主资源
        /// </summary>
        /// <returns></returns>
        public static string GetAbResDirectory()
        {
            return _ab_res_directory;
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
