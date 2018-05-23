﻿

using UnityEngine;

namespace SummerEditor
{
    public class EAssetBundleConst
    {
        public static string main_driectory = "Assets/res_bundle/";                                     // 扫描的目录
        public static string config_name = "Assets/res_bundle/ResConfig.bytes";                         // 配置文件名称
        public const string ASSET_PATH = "Assets/res_bundle/Prefab";                    // 资源的主目录

        public const string SUFFIX_META = ".meta";                              // 忽略资源主目录的类型

        public const string ASSETBUNDLE_EXTENSION = ".ab";                      // 后缀名

        public const float ASSETBUILD_MAX_SIZE = 1024 * 1204;                        // 多个资源打包到一个AssetBundle中，这个AssetBundle的大小

        public static string assetbundle_directory = Application.streamingAssetsPath + "/rpg";
        public static string assetbundle_mainfest_file_name = "rpg";
        public const string IGNORE_BUILD_ASSET_SUFFIX = ".cs";                                                      // 打包忽略的资源类型

        public static string ManifestPath
        {
            get { return assetbundle_directory + "/" + assetbundle_mainfest_file_name; }
        }


        #region SerializedObject 获取属性

        /// <summary>
        /// 获取GUID值
        /// </summary>
        public const string LOCAL_ID_DENTFIER_IN_FILE = "m_LocalIdentfierInFile";

        #endregion

    }
}
