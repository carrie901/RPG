using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// AssetBundle的依赖文件
    /// </summary>
    public class AssetBundleDepManager
    {

        public static AssetBundleDepManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssetBundleDepManager();
                }
                return _instance;
            }
        }

        public static AssetBundleDepManager _instance;
        public Dictionary<string, AssetBundleDepInfo> dep_map
                = new Dictionary<string, AssetBundleDepInfo>();


        protected AssetBundleDepManager()
        {
            Init();
        }

        protected void Init()
        {
            string config_path = AssetBundleConst.GetAssetBundleRootDirectory() + AssetBundleConst.assetbundle_dep_path;
            AssetBundle ab = AssetBundle.LoadFromFile(config_path);
            Object obj = ab.LoadAllAssets()[0];
            TextAsset textasset = obj as TextAsset;
            _parse_data(textasset.text);
            ab.Unload(true);
        }


        public void _parse_data(string contexts)
        {
            string[] lines = contexts.ToStrs(StringHelper.split_huanhang);
            int length = lines.Length;

            for (int i = 0; i < length; i++)
            {
                string[] results = lines[i].ToStrs(StringHelper.split_douhao);
                if (results.Length <= 1) continue;
                AssetBundleDepInfo dep_info = new AssetBundleDepInfo(results);
                dep_map.Add(dep_info.AssetBundleName, dep_info);
            }
        }
    }
}

