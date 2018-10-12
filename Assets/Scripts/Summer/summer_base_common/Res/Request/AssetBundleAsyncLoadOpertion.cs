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
    public class AssetBundleAsyncLoadOpertion<T> : ResLoadOpertion where T : Object
    {
        public string _bundlePath;                              // 打包成ab的名称
        public AssetBundleRequest _request;                     // AssetBundle的资源加载请求
        public AssetBundle _assetbundle;
        public AssetBundlePackageCnf _packageCnf;
        public string _resPath;
        public string _parentPath;

        // 0=开始，1=ab头文件异步完成，开始加载内容，2=头文件完成，异步也完成
        //public int ab_state;

        public AssetBundleAsyncLoadOpertion(AssetBundlePackageCnf packageCnf, string resPath, string parentPath)
        {
            if (packageCnf != null)
            {
                _resPath = resPath;
                RequestResPath = packageCnf.PackagePath;
                _packageCnf = packageCnf;
                _bundlePath = packageCnf.FullPath;
            }
            _parentPath = parentPath;
        }

        #region public 

        public override void UnloadRequest()
        {
            base.UnloadRequest();
            _request = null;
            _packageCnf = null;
        }


        #region 生命周期

        protected override void Init()
        {
            //AssetBundleCreateRequest ab_create_request = AssetBundle.LoadFromFileAsync(_bundle_path);

            _assetbundle = AssetBundle.LoadFromFile(_bundlePath);
            _request = _assetbundle.LoadAllAssetsAsync();
        }

        protected override bool Update()
        {
            if (_packageCnf == null) return false;
            if (!_packageCnf.IsDone()) return false;
            return _request.isDone;
        }

        protected override void Complete()
        {
            if (_assetInfo == null)
            {
                Object[] objs = _request.allAssets;
                _packageCnf.RefParent(_parentPath);
                _packageCnf.InitAssetBundle(_assetbundle, objs);
                _assetInfo = _packageCnf.GetAsset(_resPath);
            }
        }

        #endregion

        #endregion

    }
}

