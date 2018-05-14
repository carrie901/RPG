

using UnityEditor;

namespace SummerEditor
{
    public class AssetBundleMenuE
    {
        [MenuItem("Tools/AssetBundle/生成资源配置列表")]
        public static void CreateAbConfigFile()
        {
            CreateAssetBundleConfigE.CreateAbConfigFile();
        }

        [MenuItem("Tools/AssetBundle/开始分析")]
        public static void AllAnalysisAsset()
        {
            AssetAnalysisE.AllAnalysisAsset();
        }

        [MenuItem("Tools/AssetBundle/打包策略")]
        public static void BuildStrategy()
        {

        }

        [MenuItem("Tools/AssetBundle/测试 设置AssetBundle Name")]
        public static void SetAllAssetBundleName()
        {
            string[] all_ab_names = AssetDatabase.GetAllAssetBundleNames();
            UnityEngine.Debug.Log("设置名字之前all_ab_names:" + all_ab_names.Length);
            AssetBundleSetNameE.SetMainAbName();
        }

        [MenuItem("Tools/AssetBundle/清除所有AssetBundle 名字")]
        public static void ClearAssetBundleName()
        {
            AssetBundleSetNameE.ClearAllAssetBundleName();
        }

        [MenuItem("Assets/AssetBundle/设置AssetBundle 名字")]
        public static void SetAssetBundleName()
        {
            AssetBundleSetNameE.SetSelectionAssetBundleName();
        }

        [MenuItem("Tools/AssetBundle/打包资源")]
        public static void BuildAssetBuild()
        {
            AssetBundleBuildE.AllAssetBundleBuild();
        }
    }
}
