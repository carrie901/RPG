using UnityEngine;
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 主资源加载
    /// 大量的上层模块依赖于底层模块，目前没有太好的方法来进行设计
    ///     如果针对请求干净化整个类，只提供最终资源的加载
    /// TODO 异步加载的方式，如果先主后依赖，会导致这变成一个同步加载的方式
    /// </summary>
    public class AssetBundleAsyncLoadOpertion : LoadOpertion
    {
        public string _bundle_name;             // 打包成ab的名称
        public AssetBundleRequest _request;     // AssetBundle的资源加载请求
        public AssetBundle _assetbundle;
        public AssetBundlePackageInfo _package_info;
        public bool _init_complete;
        public AssetBundleAsyncLoadOpertion(AssetBundlePackageInfo package_info)
        {
            if (package_info != null)
            {
                RequestResPath = package_info.PackagePath;
                _package_info = package_info;
                _bundle_name = package_info.FullPath;
            }
        }

        public override void OnInit()
        {
            _assetbundle = AssetBundle.LoadFromFile(_bundle_name);
            _init_complete = true;
            _request = _assetbundle.LoadAllAssetsAsync();
        }

        protected override bool Update()
        {
            /*if (_request != null)
                return false;

            _assetbundle = AssetBundle.LoadFromFile(_bundle_name);
            if (_assetbundle != null)
            {
                _init_complete = true;
                _request = _assetbundle.LoadAllAssetsAsync();
                return true;
            }

            return true;*/
            return true;
        }

        public override bool IsDone()
        {
            if (!_init_complete) return false;

            if (_request == null)
            {
                ResLog.Error("Class AssetBundleAsyncLoadOpertion Error,Path:[0]", _bundle_name);
                return false;
            }

            if (_package_info != null && !_package_info.IsDone())
                return false;
            return _request.isDone;
        }

        public override Object GetAsset()
        {
            if (_request != null && _request.isDone)
                return _request.asset;
            return null;
        }

        public override void UnloadRequest()
        {
            /*if (_assetbundle != null)
                _assetbundle.Unload(false);
            else
                LogManager.Error("OabDepLoadOpertion Error,AssetBundle is null.Path:[0]", _bundle_name);*/
        }
    }
}

