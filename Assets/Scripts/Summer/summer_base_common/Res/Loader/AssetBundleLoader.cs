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

        public static AssetBundleLoader Instance = new AssetBundleLoader();


        protected List<ResLoadOpertion> _onLoadingRequest = new List<ResLoadOpertion>(8);           // 正在加载的请求            
        protected List<string> _onLoadingAbPackage = new List<string>(8);                           // 加载中的资源包

        protected List<ResLoadOpertion> _waitToLoadRequest = new List<ResLoadOpertion>(16);         // 等待加载的请求   
        protected List<string> _onWaitAbPackage = new List<string>();                               // 等待加载的资源包

        private readonly Dictionary<string, AssetBundlePackageInfo> _packageMap
            = new Dictionary<string, AssetBundlePackageInfo>(256);                                  // 已经加载的资源
        private readonly AssetBundleManifestInfo _mainInfo = AssetBundleManifestInfo.Instance;
        private static int _maxLoadingCount = 5;                                                    // 一次性最大加载个数
        private AssetBundleLoader() { InitInfo(); }

        #endregion

        #region I_ResourceLoad

        // TODO Bug没有好的防御机制，在加载失败的情况下，不会导致整个程序死掉
        public AssetInfo LoadAsset<T>(string resPath) where T : Object
        {
            // 1.根据资源路径，得到对应的AssetBundlePackage包的路径
            ResToAssetBundleCnf mainPackageCnf = _mainInfo.GetResToAb(resPath);
            if (mainPackageCnf == null) return null;

            // 2.根据主包路径，得到起主包以及相关依赖包的信息
            string mainPackagePath = mainPackageCnf.PackagePath;
            AssetBundleDepCnf depsCnf = _mainInfo.GetDepsInfo(mainPackagePath);
            if (depsCnf == null) return null;

            // 3.依次加载依赖包
            foreach (var depInfo in depsCnf._childRef)
            {
                InternalSyncloadPackage(depInfo.Key);
            }

            // 4.加载主包
            AssetBundlePackageInfo mainPackage = InternalSyncloadPackage(mainPackagePath);
            ResLog.Assert(mainPackage != null, "AssetBundleLoader LoadAsset 加载主包失败:[{0}]", resPath);
            if (mainPackage == null) return null;
            AssetInfo info = mainPackage.GetAsset<T>(mainPackageCnf.ResName);
            return info;
        }

        public ResLoadOpertion LoadAssetAsync<T>(string resPath) where T : Object
        {
            // 1.根据资源路径，得到对应的AssetBundlePackage包的路径
            ResToAssetBundleCnf mainPackageCnf = _mainInfo.GetResToAb(resPath);
            if (mainPackageCnf == null) return null;

            // 2.根据主包路径，得到起主包以及相关依赖包的信息
            string mainPackagePath = mainPackageCnf.PackagePath;
            AssetBundleDepCnf depsCnf = _mainInfo.GetDepsInfo(mainPackagePath);
            if (depsCnf == null) return null;

            // 3.依次加载依赖包
            foreach (var depInfo in depsCnf._childRef)
            {
                InternalAsyncLoadPackage<Object>(depInfo.Key);
            }

            // 4.加载主包
            ResLoadOpertion resLoadOperation = InternalAsyncLoadPackage<Object>(depsCnf.AssetBundleName);
            return resLoadOperation;
        }

        public bool UnloadAssetBundle(AssetInfo assetInfo)
        {
            // 1.资源对应的包信息
            ResToAssetBundleCnf info = _mainInfo.GetResToAb(assetInfo.ResPath);
            if (info == null) return false;

            // 2.得到AssetBundlePackage包
            AssetBundlePackageCnf mainPackageCnf = _mainInfo.GetPackageCnf(info.PackagePath);
            string mainPackagePath = mainPackageCnf.PackagePath;

            // 3.卸载主包
            bool result = InternalUnLoad(mainPackagePath);
            if (!result) return false;

            // 4.卸载依赖包
            AssetBundleDepCnf depCnf = _mainInfo.GetDepsInfo(mainPackagePath);
            if (depCnf == null) return true;
            foreach (var depInfo in depCnf._childRef)
            {
                InternalUnLoad(depInfo.Key);
            }
            return true;
        }

        public void OnUpdate()
        {
            // 1.更新请求
            int length = _onLoadingRequest.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                ResLoadOpertion opertion = _onLoadingRequest[i];
                opertion.OnUpdate();
                if (opertion.IsExit())
                {
                    opertion.UnloadRequest();
                    _onLoadingRequest.RemoveAt(i);
                    _onLoadingAbPackage.RemoveAt(i);
                    opertion = null;
                }
            }

            // 检测新的内容
            int removeCount = 0;
            length = _waitToLoadRequest.Count;
            for (int i = 0; i < length; i++)
            {
                if (_onLoadingRequest.Count >= _maxLoadingCount) break;
                ResLoadOpertion loadOpertion = _waitToLoadRequest[i];
                removeCount++;
                _onLoadingRequest.Add(loadOpertion);
                _onLoadingAbPackage.Add(loadOpertion.RequestResPath);
            }
            if (removeCount > 0)
            {
                _waitToLoadRequest.RemoveRange(0, removeCount);
                _onWaitAbPackage.RemoveRange(0, removeCount);
            }
        }

        public void CheckInfo()
        {
            InternalCheckInfo();
        }

        #endregion

        #region public

        public AssetBundlePackageInfo InitAssetBundleInfo(AssetBundle assetbundle, AssetBundlePackageCnf packageCnf)
        {
            Object[] objs = assetbundle.LoadAllAssets();
            AssetBundlePackageInfo packageInfo = AbPackageFactory.Create(packageCnf);

            packageInfo.InitAssetBundle(assetbundle, objs);
            _packageMap.Add(packageInfo.PackagePath, packageInfo);
            return packageInfo;
        }

        #endregion

        #region private

        private void InitInfo()
        {
            _packageMap.Clear();
            _mainInfo.InitInfo();
        }

        private AssetBundlePackageInfo InternalSyncloadPackage(string packagePath)
        {
            // 1.已经在缓存中,引用+1
            AssetBundlePackageInfo packageInfo = null;
            _packageMap.TryGetValue(packagePath, out packageInfo);
            if (packageInfo != null)
            {
                return packageInfo;
            }

            // 2.如果已经在加载中,妈蛋老子只能等你了呗
            if (_onLoadingAbPackage.Contains(packagePath))
            {
                ResLog.Assert(!string.IsNullOrEmpty(packagePath), "主资源:[{0}]，同步和异步同时加载，报错", packagePath);
                return null;
            }
            // 3.如果在等待资源中直接剔除请求
            int index = _onWaitAbPackage.IndexOf(packagePath);
            if (index > 0)
            {
                _onWaitAbPackage.RemoveAt(index);
                _waitToLoadRequest.RemoveAt(index);
            }

            // 4.加载AssetBundle资源
            AssetBundlePackageCnf packageCnf = _mainInfo.GetPackageCnf(packagePath);
            AssetBundle assetbundle = AssetBundle.LoadFromFile(packageCnf.FullPath);
            ResLog.Assert(assetbundle != null, "同步加载AssetBundlePack失败，路径不存在:[{0}]", packageCnf.FullPath);
            if (assetbundle == null) return null;

            // 5.初始化AssetBundle
            packageInfo = InitAssetBundleInfo(assetbundle, packageCnf);

            return packageInfo;
        }

        private ResLoadOpertion InternalAsyncLoadPackage<T>(string packagePath) where T : Object
        {
            // 1.已经在缓存中,引用+1
            AssetBundlePackageInfo packageInfo;
            _packageMap.TryGetValue(packagePath, out packageInfo);
            if (packageInfo != null)
            {
                AssetBundleCompleteLoadOperation operation = new AssetBundleCompleteLoadOperation(packageInfo);
                operation.OnUpdate();
                return operation;
            }

            // 2.如果已经在加载中,返回已经在加载中的结果
            int index = _onLoadingAbPackage.IndexOf(packagePath);
            if (index >= 0)
            {
                ResLoadOpertion loadOperation = _onLoadingRequest[index];
                return loadOperation;
            }

            // 3.如果在等待中,返回等待中加载中的结果
            index = _onWaitAbPackage.IndexOf(packagePath);
            if (index >= 0)
            {
                ResLoadOpertion loadOperation = _waitToLoadRequest[index];
                return loadOperation;
            }

            // 4得到Ab的包消息,并且添加到等待列表中
            AssetBundlePackageCnf packageCnf = _mainInfo.GetPackageCnf(packagePath);
            AssetBundleAsyncLoadOpertion packageOpertion =
                new AssetBundleAsyncLoadOpertion(packageCnf);
            _waitToLoadRequest.Add(packageOpertion);
            _onWaitAbPackage.Add(packagePath);
            return packageOpertion;
        }

        private bool InternalUnLoad(string packagePath)
        {
            // 1.得到AB包的配置信息
            AssetBundlePackageCnf mainPackageCnf = _mainInfo.GetPackageCnf(packagePath);
            string mainPackagePath = mainPackageCnf.PackagePath;

            // 2.从缓存查找得到AssetBundle
            AssetBundlePackageInfo packageInfo;
            _packageMap.TryGetValue(mainPackagePath, out packageInfo);
            ResLog.Assert(packageInfo != null, "AssetBundleLoader UnLoadAssetBundle 缓存中找不到对应的资源:[{0}]", mainPackagePath);
            if (packageInfo == null) return false;

            // 3.这个AssetBundle的爸爸们还在，所以不能卸载
            List<string> parentPaths = _mainInfo.GetParentPaths(mainPackageCnf.PackagePath);
            if (parentPaths != null)
            {
                int length = parentPaths.Count;
                for (int i = 0; i < length; i++)
                {
                    if (_packageMap.ContainsKey(parentPaths[i]))
                        return false;
                }
            }
            // 4.爸爸们已经不在了，看下内部的引用是否已经完全为0了
            bool result = packageInfo.CheckRef();
            if (result)
            {
                packageInfo.UnLoad();
                _packageMap.Remove(mainPackagePath);
            }

            return result;
        }

        public void InternalCheckInfo()
        {
            LogManager.Assert(_onLoadingRequest.Count == _onLoadingAbPackage.Count, "AssetBundleLoader OnLoading Request List！=Name List");

            LogManager.Log("--------------------资源数量:[{0}]--------------------", _packageMap.Count);
            int length = _onLoadingRequest.Count;
            LogManager.Log("--------------------加载中的请求:[{0}]--------------------", length);
            for (int i = 0; i < length; i++)
            {
                ResLog.Log(_onLoadingRequest[i].RequestResPath);
            }

            LogManager.Assert(_waitToLoadRequest.Count == _onWaitAbPackage.Count, "OnWait Request List！=Name List");
            length = _waitToLoadRequest.Count;
            LogManager.Log("--------------------等待中的请求:[{0}]--------------------", length);
            for (int i = 0; i < length; i++)
            {
                ResLog.Log(_waitToLoadRequest[i].RequestResPath);
            }

        }

        #endregion

    }
}
