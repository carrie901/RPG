using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class AssetBundlePackage
    {
        #region 属性

        public string PackagePath { get { return _package_path; } }
        public string HashCode { get { return _hash_code; } }


        public string _package_path;                                                            // 包的路径
        public Dictionary<string, int> res_path_map = new Dictionary<string, int>();            // 资源路径
        public string _hash_code;                                                               // 哈希code值
        public bool IsComplete { get; private set; }                                            // 是否加载完成
        public AssetBundle _assetbundle;

        public Dictionary<string, AssetInfo> _asset_map = new Dictionary<string, AssetInfo>();

        #endregion

        public AssetBundlePackage(string[] infos)
        {
            _package_path = infos[0];
            IsComplete = false;

            for (int i = 1; i < infos.Length; i++)
            {
                bool result = res_path_map.ContainsKey(infos[i]);
                LogManager.Assert(result, "初始化AssetBundle的依赖信息失败，[{0}]", infos[i]);
                if (result) continue;
                res_path_map.Add(infos[i], 0);
            }
        }


        #region 包体的初始化信息

        public void AddResPath(string res_path)
        {
            res_path_map.Add(res_path, 0);
        }



        #endregion

        public bool HasAssetBundle(string res_path)
        {
            if (res_path_map.ContainsKey(res_path))
                return true;
            return false;
        }

        public void LoadAssetBundle(AssetBundle ab)
        {
            if (ab == null) return;
            _assetbundle = ab;
            IsComplete = true;

            Object[] objs = ab.LoadAllAssets();
            string[] AllAssetNames = ab.AllAssetNames();
            bool flag = ab.Contains("a");
            string[] assets = ab.GetAllScenePaths();
        }

        public void LoadAssetBundle(AssetBundle ab, Object[] objs)
        {
            if (ab == null) return;
            _assetbundle = ab;
            IsComplete = true;
        }


        public void UnLoad(bool unload_all_loaded_objects)
        {
            IsComplete = false;
            LogManager.Assert(_assetbundle != null, "不能为空[{0}]", _package_path);
        }
    }
}

