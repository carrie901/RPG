using UnityEngine;
using System.Collections;

namespace Summer
{
    /// <summary>
    /// 依赖资源的异步加载
    /// </summary>
    public class OabDepLoadOpertion : OloadOpertion
    {
        public string _bundle_name;
        public string _asset_name;
        protected AssetBundleRequest _request;
        public AssetBundle assetbundle;
        public bool _init_complete;
        public OabDepLoadOpertion(string bundle_name, string asset_name)
        {
            _bundle_name = bundle_name;
            _asset_name = asset_name;
        }

        public override bool Update()
        {
            if (_request != null)
                return false;

            assetbundle = AssetBundle.LoadFromFile(_bundle_name);
            if (assetbundle != null)
            {
                _init_complete = true;
                //_request = assetbundle.LoadAssetAsync(_asset_name);
                
                return true;
            }
            return false;
        }

        public override bool IsDone()
        {
            if (!_init_complete) return true;
            if (_request == null)
            {
                LogManager.Error("OabDepLoadOpertion Error,Path:[0]", _bundle_name);
                return false;
            }

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
            if (assetbundle != null)
                assetbundle.Unload(false);
            else
                LogManager.Error("OabDepLoadOpertion Error,AssetBundle is null.Path:[0]", _bundle_name);
        }
    }
}


