using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
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
            List<string[]> result = ParseData(text);
            return result;
        }

        public static string LoadAsset(string asset_name)
        {
            string config_path = AssetBundleConst.GetAssetBundleRootDirectory() + "res_bundle/" + asset_name;
            AssetBundle ab = AssetBundle.LoadFromFile(config_path);
            Object obj = ab.LoadAllAssets()[0];
            TextAsset textasset = obj as TextAsset;
            string result = textasset.text;
            ab.Unload(true);
            return result;
        }

        public static List<string[]> ParseData(string text)
        {
            List<string[]> result = new List<string[]>();
            string[] lines = text.ToStrs(StringHelper.split_huanhang);
            int length = lines.Length;

            for (int i = 0; i < length; i++)
            {
                string[] results = lines[i].ToStrs(StringHelper.split_douhao);
                if (results.Length <= 1)
                {
                    continue;
                }
                result.Add(results);
            }
            return result;
        }
    }
}

