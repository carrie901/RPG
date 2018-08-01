using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

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
        public Dictionary<string, string> _res_path_map = new Dictionary<string, string>();     // 资源的实际路径 Key=资源路径，value=资源的名称
        public Dictionary<string, string> _res_names = new Dictionary<string, string>();        // 资源的名称 key的资源的名称，Value=资源的路径
        public List<AssetInfo> _asset_map = new List<AssetInfo>();

        #region 引用信息

        public int RefCount { get; private set; }
        public List<string> _ref_list = new List<string>(4);

        #endregion

        #endregion

        #region 构造

        public AssetBundlePackageInfo(string[] infos)
        {
            _package_path = infos[0];
            IsComplete = false;
            // TODO 可以优化
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

        #region public 

        public void InitAssetBundle(AssetBundle ab, Object[] objs)
        {
            if (ab == null) return;
            ResLog.Assert(!IsComplete, "[{0}]已经初始化过了", PackagePath);
            _assetbundle = ab;
            IsComplete = true;

            _asset_map.Clear();
            int length = objs.Length;
            for (int i = 0; i < length; i++)
            {
                if (_res_names.ContainsKey(objs[i].name))
                {
                    AssetInfo info = new AssetInfo(objs[i], _res_names[objs[i].name]);
                    _asset_map.Add(info);
                }
                else
                {
                    Debug.LogErrorFormat("遗留问题，我也忘记了：[{0}]", objs[i].name);
                    //_fbx.Add(objs[i]);
                }
            }
        }

        public AssetInfo GetAsset(string asset_name)
        {
            if (_assetbundle == null) return null;
            AssetInfo re_asset_info = null;

            int length = _asset_map.Count;
            for (int i = 0; i < length; i++)
            {
                AssetInfo asset_info = _asset_map[i];
                if (asset_info.ResPath != asset_name) continue;
                re_asset_info = _asset_map[i];
            }
            ResLog.Assert((re_asset_info != null), "从资源主包[{0}]中找不到对应的AssetInfo:[{1}]的资源", _package_path, asset_name);
            return re_asset_info;
        }

        public bool HasAssetBundle(string res_path)
        {
            if (_res_path_map.ContainsKey(res_path))
                return true;
            return false;
        }

        public void UnLoad()
        {
            ResLog.Log("[{0}]引用--,Ref:[{1}]", PackagePath, RefCount);
            if (RefCount > 0) return;

            ResLog.Assert(RefCount == 0, "ab package ref error:[{0}]", RefCount);

            IsComplete = false;
            LogManager.Assert(_assetbundle != null, "不能为空[{0}]", _package_path);
            if (_assetbundle == null) return;

            int length = _asset_map.Count;
            for (int i = 0; i < length; i++)
            {
                _asset_map[i].UnLoad();
            }
            _asset_map.Clear();
            _assetbundle.Unload(true);
            _assetbundle = null;
        }

        public bool IsDone()
        {
            return true;
        }

        #region

        public void RefParent(string parent_path)
        {
            if (string.IsNullOrEmpty(parent_path)) return;
            if (_ref_list.Contains(parent_path)) return;
            //bool result = _ref_list.Contains(parent_path);
            //LogManager.Assert(!result, "已经包含了相关资源,[{0}],爸爸是:[{1}]", PackagePath, parent_path);
            //if (result) return;
            _ref_list.Add(parent_path);
            RefCount++;
        }

        public void UnRef(string parent_path)
        {
            if (_ref_list.Contains(parent_path))
            {
                _ref_list.Remove(parent_path);
                RefCount--;
            }
        }

        #endregion

        #endregion

        #region

        public bool _loading_complete()
        {
            if (!IsComplete || _assetbundle == null || _asset_map.Count == 0) return false;
            return true;

        }

        #endregion
    }
}

