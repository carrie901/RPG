using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class AssetBundleConfig
    {

        /// <summary>
        /// key = assetbundle_name,value=info
        /// key要在StreamingAssets目录下
        /// key = ab
        /// </summary>
        public static Dictionary<string, AssetBundleDepInfo> dep_map
                = new Dictionary<string, AssetBundleDepInfo>();

        public static Dictionary<string, AssetBundlePackageInfo> package_map
          = new Dictionary<string, AssetBundlePackageInfo>();

        public static Dictionary<string, AssetBundleRes> res_map
            = new Dictionary<string, AssetBundleRes>();


        public static AssetBundleDepInfo GetDepInfo(string assetbundle_package_path)
        {
            if (dep_map.ContainsKey(assetbundle_package_path))
                return dep_map[assetbundle_package_path];

            ResLog.Error("不可能出现的情况，尼玛居然出现了[{0}]______", assetbundle_package_path);
            return null;
        }

        public static  void Init()
        {
            dep_map.Clear();
            package_map.Clear();
            res_map.Clear();
            List<string[]> dep_result = GetAbInfo(AssetBundleConst.assetbundle_dep_path);

            int length = dep_result.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleDepInfo dep = new AssetBundleDepInfo(dep_result[i]);
                dep_map.Add(dep.AssetBundleName, dep);
            }

            List<string[]> package_result = GetAbInfo(AssetBundleConst.assetbundle_package_path);

            length = package_result.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundlePackageInfo package_info = new AssetBundlePackageInfo(package_result[i]);
                package_map.Add(package_info.PackagePath, package_info);
            }

            List<string[]> res_result = GetAbInfo(AssetBundleConst.assetbundle_res_path);

            length = res_result.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleRes res = new AssetBundleRes(res_result[i]);
                res_map.Add(res.res_path, res);
            }
        }

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

