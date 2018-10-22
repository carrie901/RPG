using UnityEngine;

namespace SummerEditor
{
    public class EAssetBundleConst
    {
        public const string MAIN_RES_DRIECTORY = "Assets/res_bundle/";                              // 主资源扫描的目录
        public const string UI_MAIN_DIRECTORY = "Assets/UIResources/UITexture/";                    // 主资源扫描的目录
        public const string SUFFIX_META = ".meta";                                                  // 忽略资源主目录的类型

        public const string ASSETBUNDLE_EXTENSION = ".ab";                                          // 后缀名

        public const float ASSETBUILD_MAX_SIZE = 1024 * 1204;                                       // 多个资源打包到一个AssetBundle中，这个AssetBundle的大小

        public static string AssetbundleDirectory = Application.streamingAssetsPath + "/rpg";
        public const string ASSETBUNDLE_MAINFEST_FILE_NAME = "rpg";
        public const string IGNORE_BUILD_ASSET_SUFFIX = ".cs";                                      // 打包忽略的资源类型

        #region 打包

        public const string RES_SPRITE_DRIECTORY = "Assets/UIResources/UITexture";

        public const string SHADER_BUNDLE_NAME = "common_shader";                                   // 所有shader打包到一个ab中
    
        #endregion

        public static string ManifestPath
        {
            get { return AssetbundleDirectory + "/" + ASSETBUNDLE_MAINFEST_FILE_NAME; }
        }


        #region SerializedObject 获取属性

        /// <summary>
        /// 获取GUID值
        /// </summary>
        public const string LOCAL_ID_DENTFIER_IN_FILE = "m_LocalIdentfierInFile";

        #endregion

    }
}
