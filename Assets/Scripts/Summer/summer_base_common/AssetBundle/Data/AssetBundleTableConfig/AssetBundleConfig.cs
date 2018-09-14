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
        public static List<string[]> GetAbInfo(string assetName)
        {
            string text = LoadAsset(assetName);
            List<string[]> result = StringHelper.ParseData(text);
            return result;
        }

        public static string LoadAsset(string assetName)
        {
            string configPath = AssetBundleConst.GetAbResDirectory() + assetName;
            AssetBundle ab = AssetBundle.LoadFromFile(configPath);
            Object obj = ab.LoadAllAssets()[0];
            TextAsset textasset = obj as TextAsset;
            string result = textasset.text;
            ab.Unload(true);
            ab = null;
            return result;
        }
    }
}

