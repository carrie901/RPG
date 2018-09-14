using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    /// <summary>
    /// 当前这个职责是有一点混乱的
    /// 本身应该注重的事AssetBundle方面的加载
    /// 同事他有涉及到了一定程度上的cache方面的 职责上的问题
    /// 
    /// 问题
    /// 1. 目前没有处理如果加载的资源又是主资源，又是依赖资源这种情况
    ///     1.1引用的问题，主引用和依赖引用
    ///     1.2处于别人的依赖包中/处于别人加载的包中
    /// 2. 无法处理同步和异步同事加载一个资源，而且也没有预警措施
    /// 
    /// 区分res_path路径，assetbundle_path,assetbundle_name
    /// 分别是原始的加载路径（res/../a.prefab），对应的AB包路径(StreamingAssets/../a.ab)，ab的名字（a）
    /// </summary>
    public class AssetBundleLoader : I_ResourceLoad
    {
        #region param

        #region 静态

        public static AssetBundleLoader _instance;
        public static AssetBundleLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssetBundleLoader();
                    Init();
                }
                return _instance;
            }
        }
        private static int _maxLoadingCount = 5;                                                    // 一次性最大加载个数
        public static Dictionary<string, AssetBundleDepInfo> _depMap                                // 依赖表信息
              = new Dictionary<string, AssetBundleDepInfo>();
        public static Dictionary<string, AssetBundleResInfo> _resMap                                // 资源-->Ab包 通过资源名得到对应的Ab包
           = new Dictionary<string, AssetBundleResInfo>();
        public static Dictionary<string, AssetBundlePackageInfo> _packageMap                        // Ab包信息
          = new Dictionary<string, AssetBundlePackageInfo>();

        #endregion

        protected List<LoadOpertion> _onLoadingRequest = new List<LoadOpertion>();                  // 正在加载的请求           
        protected Queue<LoadOpertion> _waitToLoadRequest = new Queue<LoadOpertion>();               // 等待加载的请求   
        protected List<string> _onLoadingAbPackage = new List<string>();                            // 加载中的资源包
        protected Dictionary<string, int> _onWaitAbPackage = new Dictionary<string, int>();         // 加载中的资源包


        #region 构造

        #endregion

        #endregion

        #region I_ResourceLoad

        // TODO Bug没有好的防御机制，在加载失败的情况下，不会导致整个程序死掉
        public AssetInfo LoadAsset(string resPath)
        {
            // 1.资源对应的包信息
            AssetBundleResInfo resInfo = GetAssetBundleRes(resPath);
            if (resInfo == null) return null;

            // 2.得到AssetBundle包
            AssetBundlePackageInfo mainPackageInfo = GetPackageInfo(resInfo._packagePath);
            // 3.加载依赖信息
            AssetBundleDepInfo depsInfo = GetDepInfo(resInfo._packagePath);
            foreach (var depInfo in depsInfo._childRef)
            {
                AssetBundlePackageInfo depPackageInfo = GetPackageInfo(depInfo.Key);

                if (!_need_load(depPackageInfo))
                {
                    depPackageInfo.RefParent(mainPackageInfo.PackagePath);
                    continue;
                }
                _internal_syncload_package(depPackageInfo, mainPackageInfo.PackagePath);
            }

            _internal_syncload_package(mainPackageInfo, string.Empty);

            // 3.包中的资源
            AssetInfo assetInfo = mainPackageInfo.GetAsset(resInfo._resPath);
            return assetInfo;
        }

        public LoadOpertion LoadAssetAsync(string resPath)
        {
            // 1.根据资源的路径找到 AB包
            AssetBundleResInfo resInfo = GetAssetBundleRes(resPath);
            if (resInfo == null) return null;
            AssetBundlePackageInfo mainPackageInfo = GetPackageInfo(resInfo._packagePath);

            // 2.通过AB包得到相关的依赖信息
            AssetBundleDepInfo depInfo = GetDepInfo(resInfo._packagePath);
            foreach (var info in depInfo._childRef)
            {
                AssetBundlePackageInfo depPackageInfo = GetPackageInfo(info.Key);

                if (!_need_load(depPackageInfo))
                {
                    depPackageInfo.RefParent(mainPackageInfo.PackagePath);
                    continue;
                }

                AssetBundleAsyncLoadOpertion depPackageOpertion =
                    new AssetBundleAsyncLoadOpertion(depPackageInfo, string.Empty, resInfo._packagePath);
                _waitToLoadRequest.Enqueue(depPackageOpertion);

            }

            AssetBundleAsyncLoadOpertion mainOpertion = new AssetBundleAsyncLoadOpertion(mainPackageInfo, resPath, string.Empty);
            return mainOpertion;
        }

        public bool UnloadAssetBundle(AssetInfo assetInfo)
        {
            return _un_load_assetbundle(assetInfo);
        }

        public void OnUpdate()
        {
            // 1.更新请求
            int length = _onLoadingRequest.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                LoadOpertion opertion = _onLoadingRequest[i];
                opertion.OnUpdate();
                if (opertion.IsExit())
                {
                    opertion.UnloadRequest();
                    _onLoadingRequest.RemoveAt(i);
                    opertion = null;
                }
            }

            // 检测新的内容
            for (int i = length; i < _maxLoadingCount; i++)
            {
                if (_waitToLoadRequest.Count <= 0) continue;
                LoadOpertion loadOpertion = _waitToLoadRequest.Dequeue();
                _onLoadingRequest.Add(loadOpertion);
            }
        }

        #endregion

        #region public 

        // AssetBundle是否处于加载状态
        public bool ContainsLoadAssetBundles(string abPackagePath)
        {
            return _onLoadingAbPackage.Contains(abPackagePath);
        }

        #endregion

        #region private

        public void _internal_syncload_package(AssetBundlePackageInfo packageInfo, string parentPath)
        {
            AssetBundle assetbundle = AssetBundle.LoadFromFile(packageInfo.FullPath);
            bool result = assetbundle != null;
            ResLog.Assert(result, "同步加载AssetBundlePack失败，路径不存在:[{0}]", packageInfo.PackagePath);

            if (!result) return;
            Object[] objs = assetbundle.LoadAllAssets();
            packageInfo.RefParent(parentPath);
            packageInfo.InitAssetBundle(assetbundle, objs);
        }

        // TODO BUG:有一定的bug的情况出现，就是如果同步加载和异步加载同时出现
        public bool _need_load(AssetBundlePackageInfo packageInfo)
        {
            if (packageInfo == null) return false;
            // 已经完成
            if (packageInfo.IsComplete) return false;

            // 在等待中
            if (_onWaitAbPackage.ContainsKey(packageInfo.PackagePath)) return false;
            // 已经在加载中
            if (_onLoadingAbPackage.Contains(packageInfo.PackagePath)) return false;
            return true;
        }

        public bool _un_load_assetbundle(AssetInfo assetInfo, bool includeMain = true)
        {
            // 1.资源对应的包信息
            AssetBundleResInfo resInfo = GetAssetBundleRes(assetInfo.ResPath);
            if (resInfo == null) return false;

            // 2.得到AssetBundle包
            AssetBundlePackageInfo mainPackageInfo = GetPackageInfo(resInfo._packagePath);

            // 3.加载依赖信息
            AssetBundleDepInfo depsInfo = GetDepInfo(resInfo._packagePath);
            foreach (var depInfo in depsInfo._childRef)
            {
                AssetBundlePackageInfo packageInfo = GetPackageInfo(depInfo.Key);
                packageInfo.UnRef(resInfo._packagePath);
                packageInfo.UnLoad();
            }
            if (includeMain)
            {
                //TODO: Bug 
                mainPackageInfo.UnRef(resInfo._packagePath);
                mainPackageInfo.UnLoad();
            }

            return true;
        }

        #endregion

        #region static

        public static AssetBundleResInfo GetAssetBundleRes(string resPath)
        {
            AssetBundleResInfo resInfo;
            _resMap.TryGetValue(resPath, out resInfo);
            ResLog.Assert((resInfo != null), "资源[{0}]找不到对应的包", resPath);
            return resInfo;
        }

        public static AssetBundleDepInfo GetDepInfo(string assetbundlePackagePath)
        {
            if (_depMap.ContainsKey(assetbundlePackagePath))
                return _depMap[assetbundlePackagePath];

            ResLog.Error("dep_map不可能出现的情况，尼玛居然出现了[{0}]______", assetbundlePackagePath);
            return null;
        }

        public static AssetBundlePackageInfo GetPackageInfo(string assetbundlePackagePath)
        {
            if (_packageMap.ContainsKey(assetbundlePackagePath))
                return _packageMap[assetbundlePackagePath];
            ResLog.Error("找不到对应的主包:[{0}]不存在", assetbundlePackagePath);
            return null;
        }

        public static void Init()
        {
            _depMap.Clear();
            _packageMap.Clear();
            _resMap.Clear();

            List<string[]> depResult = AssetBundleConfig.GetAbInfo(AssetBundleConst.AssetbundleDepPath);
            int length = depResult.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleDepInfo dep = new AssetBundleDepInfo(depResult[i]);
                _depMap.Add(dep.AssetBundleName, dep);
            }

            List<string[]> packageResult = AssetBundleConfig.GetAbInfo(AssetBundleConst.AssetbundlePackagePath);
            length = packageResult.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundlePackageInfo packageInfo = new AssetBundlePackageInfo(packageResult[i]);
                _packageMap.Add(packageInfo.PackagePath, packageInfo);
            }

            List<string[]> resResult = AssetBundleConfig.GetAbInfo(AssetBundleConst.AssetbundleResPath);
            length = resResult.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleResInfo res = new AssetBundleResInfo(resResult[i]);
                _resMap.Add(res._resPath, res);
            }
        }

        #endregion

    }
}
