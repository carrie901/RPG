using UnityEngine;
using Object = UnityEngine.Object;

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
        public string _bundle_path;             // 打包成ab的名称
        public AssetBundleRequest _request;     // AssetBundle的资源加载请求
        public AssetBundle _assetbundle;
        public AssetBundlePackageInfo _package_info;
        public string _res_path;
        public string _parent_path;

        public AssetBundleAsyncLoadOpertion(AssetBundlePackageInfo package_info, string res_path, string parent_path)
        {
            if (package_info != null)
            {
                _res_path = res_path;
                RequestResPath = package_info.PackagePath;
                _package_info = package_info;
                _bundle_path = package_info.FullPath;
            }
            _parent_path = parent_path;
        }

        #region public 


        public override void UnloadRequest()
        {
            base.UnloadRequest();
            _request = null;
            _assetbundle = null;
            _package_info = null;
        }


        #region 生命周期

        protected override void Init()
        {
            _assetbundle = AssetBundle.LoadFromFile(_bundle_path);
            _request = _assetbundle.LoadAllAssetsAsync();
        }

        protected override bool Update()
        {
            if (_package_info == null) return false;
            if (!_package_info.IsDone()) return false;
            return _request.isDone;
        }

        protected override void Complete()
        {
            if (_asset_info == null)
            {
                Object[] objs = _request.allAssets;
                _package_info.RefParent(_parent_path);
                _package_info.InitAssetBundle(_assetbundle, objs);
                _asset_info = _package_info.GetAsset(_res_path);
            }
        }

        #endregion

        #endregion

    }
}

