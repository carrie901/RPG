using UnityEngine;
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 主资源加载
    /// </summary>
    public class OabMainLoadOpertion : OloadOpertion
    {
        public string _bundle_name;
        public string _asset_name;
        public AssetBundleRequest _request;
        public MainBundleInfo _info;
        public AssetBundle _assetbundle;
        public bool _init_complete;
        public OabMainLoadOpertion(string bundle_name, string asset_name, MainBundleInfo info)
        {
            _bundle_name = bundle_name;
            _asset_name = asset_name;
            _info = info;
        }

        public override bool Update()
        {
            LogManager.Log("------------Update----------------");
            if (_request != null)
                return false;

            _assetbundle = AssetBundle.LoadFromFile(_bundle_name);
            if (_assetbundle != null)
            {
                _init_complete = true;
                _request = _assetbundle.LoadAssetAsync(_asset_name);
                return true;
            }

            return true;
        }

        public override bool IsDone()
        {
            //LogManager.Log("------------IsDone------------" + _info.IsDone());
            if (!_init_complete) return false;

            if (_request == null)
            {
                LogManager.Error("Class OabMainLoadOpertion Error,Path:[0]", _bundle_name);
                return false;
            }
            if (!_info.IsDone())
                return false;
            return _request.isDone;
        }

        public override Object GetAsset()
        {
            if (_request != null && _request.isDone)
                return _request.asset;
            return null;
        }

        public override void UnloadAssetBundle()
        {
            if (_assetbundle != null)
                _assetbundle.Unload(false);
            else
                LogManager.Error("OabDepLoadOpertion Error,AssetBundle is null.Path:[0]", _bundle_name);
        }
    }
}

