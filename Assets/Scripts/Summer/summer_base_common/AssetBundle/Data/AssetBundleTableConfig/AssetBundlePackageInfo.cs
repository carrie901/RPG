using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// AssetBundle包
    /// </summary>
    public class AssetBundlePackageInfo
    {
        #region 属性

        public string FullPath { get; private set; }                                            // 实际加载路径
        public string PackagePath { get { return _package_path; } }                             // 相对路径
        public string HashCode { get { return _hash_code; } }
        public bool IsComplete { get; private set; }                                            // 是否加载完成

        public string _package_path;                                                            // 包的路径
        public string _hash_code;                                                               // 哈希code值
        public AssetBundle _assetbundle;
        public Dictionary<string, int> _res_path_map = new Dictionary<string, int>();           // 资源路径
        public Dictionary<string, AssetInfo> _asset_map = new Dictionary<string, AssetInfo>();
        //public bool IsMain { get; private set; }

        #endregion

        #region 构造

        public AssetBundlePackageInfo(string[] infos)
        {
            _package_path = infos[0];
            IsComplete = false;
            FullPath = AssetBundleConst.GetAssetBundleRootDirectory() + _package_path;
            for (int i = 1; i < infos.Length; i++)
            {
                bool result = _res_path_map.ContainsKey(infos[i]);
                LogManager.Assert(!result, "初始化AssetBundle的依赖信息失败，[{0}]", infos[i]);
                if (result) continue;
                _res_path_map.Add(infos[i], 0);
            }
        }

        #endregion

        public void InitAssetBundle(AssetBundle ab, Object[] objs)
        {
            if (ab == null) return;
            ResLog.Assert(!IsComplete, "[{0}]已经初始化过了", PackagePath);
            _assetbundle = ab;
            IsComplete = true;

            _asset_map.Clear();
            for (int i = 0; i < objs.Length; i++)
            {
                AssetInfo info = new AssetInfo(objs[i]);
                _asset_map.Add(info.ResPath, info);
            }

            //ab.Unload(false);
        }

        public AssetInfo GetAsset(string asset_name)
        {
            if (_assetbundle == null) return null;
            AssetInfo asset_info;
            _asset_map.TryGetValue(asset_name, out asset_info);
            return asset_info;
        }

        public bool HasAssetBundle(string res_path)
        {
            if (_res_path_map.ContainsKey(res_path))
                return true;
            return false;
        }

        public void UnLoad()
        {
            IsComplete = false;
            LogManager.Assert(_assetbundle != null, "不能为空[{0}]", _package_path);
            if (_assetbundle == null) return;
            _assetbundle.Unload(true);
        }

        public bool IsDone()
        {
            return true;
        }
    }
}

