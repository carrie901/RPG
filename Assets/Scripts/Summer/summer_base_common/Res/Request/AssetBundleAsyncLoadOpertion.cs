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
    public class AssetBundleAsyncLoadOpertion : ResLoadOpertion
    {
        private readonly string _bundlePath;                        // 打包成ab的名称
        public AssetBundleRequest _request;                         // AssetBundle的资源加载请求
        public AssetBundle _assetbundle;
        public AssetBundlePackageCnf _packageCnf;
        public AssetBundleInfo _packageInfo;

        // 0=开始，1=ab头文件异步完成，开始加载内容，2=头文件完成，异步也完成
        //public int ab_state;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageCnf">加载的AssetBundle包信息</param>
        public AssetBundleAsyncLoadOpertion(AssetBundlePackageCnf packageCnf)
        {
            if (packageCnf != null)
            {
                RequestResPath = packageCnf.PackagePath;
                _packageCnf = packageCnf;
                _bundlePath = packageCnf.FullPath;
            }
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
            _assetbundle = AssetBundle.LoadFromFile(_bundlePath);
            _request = _assetbundle.LoadAllAssetsAsync();
        }

        protected override bool Update()
        {
            if (_request == null) return false;
            return _request.isDone;
        }

        protected override void Complete()
        {
            _packageInfo = AssetBundleLoader.Instance.InitAssetBundleInfo(_assetbundle, _packageCnf);
        }

        public override I_ObjectInfo GetAsset<T>(string resPath)
        {
            return _packageInfo;
        }

        #endregion

        #endregion

    }
}

