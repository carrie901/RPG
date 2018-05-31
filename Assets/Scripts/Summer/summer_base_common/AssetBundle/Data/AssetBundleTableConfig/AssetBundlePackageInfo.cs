using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// AssetBundle包信息
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
        //public List<string> _res_path_map = new List<string>();                                 // 资源路径
        public Dictionary<string, string> _res_path_map = new Dictionary<string, string>();
        public Dictionary<string, string> _res_names = new Dictionary<string, string>();
        public Dictionary<string, AssetInfo> _asset_map = new Dictionary<string, AssetInfo>();

        public List<Object> _fbx = new List<Object>();
        //public bool IsMain { get; private set; }

        #endregion

        #region 构造

        public AssetBundlePackageInfo(string[] infos)
        {
            _package_path = infos[0];
            IsComplete = false;
            FullPath = AssetBundleConst.GetAssetBundleRootDirectory() + _package_path;
            for (int i = 1; i < infos.Length; i = i + 2)
            {
                bool result = _res_path_map.ContainsKey(infos[i]);
                LogManager.Assert(!result, "初始化AssetBundle的依赖信息失败，[{0}]", infos[i]);
                if (result) continue;

                _res_path_map.Add(infos[i], infos[i + 1]);
                _res_names.Add(infos[i + 1], infos[i]);
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
                string obj_name = objs[i].name;
                if (_res_names.ContainsKey(obj_name))
                {
                    AssetInfo info = new AssetInfo(objs[i], _res_names[obj_name]);
                    _asset_map.Add(info.ResPath, info);
                }
                else
                {
                    _fbx.Add(objs[i]);
                }
            }

            //ab.Unload(false);
        }

        public AssetInfo GetAsset(string asset_name)
        {
            if (_assetbundle == null) return null;
            AssetInfo asset_info;
            _asset_map.TryGetValue(asset_name, out asset_info);

            ResLog.Assert((asset_info != null), "从资源主包[{0}]中找不到对应的AssetInfo:[{1}]的资源", _package_path, asset_name);
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

