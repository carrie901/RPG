using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class AssetBundleConfig
    {

        public Dictionary<string, AssetBundleDepInfo> dep_map
                = new Dictionary<string, AssetBundleDepInfo>();

        public Dictionary<string, AssetBundlePackage> package_map
          = new Dictionary<string, AssetBundlePackage>();

        public Dictionary<string, AssetBundleRes> res_map
            = new Dictionary<string, AssetBundleRes>();


        protected void Init()
        {
            string dep_text = LoadAsset(AssetBundleConst.assetbundle_dep_path);
            List<string[]> dep_result = ParseData(dep_text);

            int length = dep_result.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleDepInfo dep = new AssetBundleDepInfo(dep_result[i]);
                dep_map.Add(dep.AssetBundleName, dep);
            }

            string package_text = LoadAsset(AssetBundleConst.assetbundle_package_path);
            List<string[]> package_result = ParseData(package_text);

            length = package_result.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundlePackage package = new AssetBundlePackage(package_result[i]);
                package_map.Add(package.PackagePath, package);
            }


            string res_text = LoadAsset(AssetBundleConst.assetbundle_package_path);
            List<string[]> res_result = ParseData(res_text);

            length = res_result.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleRes res = new AssetBundleRes(package_result[i]);
                res_map.Add(res.res_path, res);
            }
        }

        public string LoadAsset(string asset_name)
        {
            string config_path = AssetBundleConst.GetAssetBundleRootDirectory() + asset_name;
            AssetBundle ab = AssetBundle.LoadFromFile(config_path);
            Object obj = ab.LoadAllAssets()[0];
            TextAsset textasset = obj as TextAsset;
            string result = textasset.text;
            ab.Unload(true);
            return result;
        }

        public List<string[]> ParseData(string text)
        {
            List<string[]> result = new List<string[]>();
            string[] lines = text.ToStrs(StringHelper.split_huanhang);
            int length = lines.Length;

            for (int i = 0; i < length; i++)
            {
                string[] results = lines[i].ToStrs(StringHelper.split_douhao);
                if (results.Length <= 1) continue;
                result.Add(results);
            }
            return result;
        }

    }
}

