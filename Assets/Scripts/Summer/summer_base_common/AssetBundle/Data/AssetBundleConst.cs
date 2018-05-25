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
        public static string assetbundle_dep_name = "dep_config";
        public static string assetbundle_dep_path = assetbundle_dep_name + ".ab";
        public static string dep_config_name = "Assets/res_bundle/DepConfig.bytes";                     // 配置文件名称


        public static string assetbundle_package_name = "packageconfig";
        public static string assetbundle_package_path = assetbundle_package_name + ".ab";
        public static string package_config_name = "Assets/res_bundle/PackageConfig.bytes";                     // 配置文件名称


        public static string assetbundle_res_name = "resconfig";
        public static string assetbundle_res_path = assetbundle_res_name + ".ab";
        public static string res_config_name = "Assets/res_bundle/ResConfig.bytes";                     // 配置文件名称












        public static string GetAssetBundleRootDirectory()
        {
            return Application.streamingAssetsPath + "/rpg/";
        }
    }

}
