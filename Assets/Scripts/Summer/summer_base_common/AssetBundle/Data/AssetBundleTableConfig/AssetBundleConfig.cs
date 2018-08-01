using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// AB的配置表解析
    /// </summary>
    public class AssetBundleConfig
    {
        /// <summary>
        /// Key = assetbundle_name,value=info
        /// key要在StreamingAssets目录下
        /// Key = ab
        /// </summary>
        public static List<string[]> GetAbInfo(string asset_name)
        {
            string text = LoadAsset(asset_name);
            List<string[]> result = StringHelper.ParseData(text);
            return result;
        }

        public static string LoadAsset(string asset_name)
        {
            string config_path = AssetBundleConst.GetAbResDirectory() + asset_name;
            AssetBundle ab = AssetBundle.LoadFromFile(config_path);
            Object obj = ab.LoadAllAssets()[0];
            TextAsset textasset = obj as TextAsset;
            string result = textasset.text;
            ab.Unload(true);
            ab = null;
            return result;
        }
    }
}

