using System;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;


namespace Summer
{
    /// <summary>
    /// 加载器
    /// 1.加载器不参与引用统计
    /// </summary>
    public class ResLoader //: I_Update
    {
        #region 属性

        public static ResLoader instance = new ResLoader();
        public static int _maxAsyncCount = 2;                                           // 异步最大加载的数量

        public Dictionary<string, AssetInfo> _cacheRes
            = new Dictionary<string, AssetInfo>(256);                                   // 已经加载的资源
        public List<ResLoadOpertion> _loadOpertions = new List<ResLoadOpertion>(256);   // 加载中的操作
        public List<string> _onLoadingRes = new List<string>(8);                        // 加载中的资源
        public int _currAsyncCount;                                                     // 当前异步加载的数量
        public MaxAsyncCountOpertion _maxOperationLoad = new MaxAsyncCountOpertion();   // 最大加载请求
        public const float TIME_OUT = 120;                                              // 超时时间
        public I_ResourceLoad _loader;

        #endregion

        #region 构造

        public ResLoader()
        {
            Init();
        }

        #endregion

        #region 基础的Load

        public T LoadAsset<T>(ResRequestInfo resRequest) where T : Object
        {
            // 1.优先从缓存中提取资源信息
            T t = PopAssetForCache<T>(resRequest);
            if (t != null) return t;

            // 2.通过加载器加载
            InternalLoadAsset<T>(resRequest);

            // 3.从缓存中得到资源
            t = PopAssetForCache<T>(resRequest);
            ResLog.Assert(t != null, "ResLoader LoadAsset 加载资源失败.路径:[{0}]", resRequest.ResPath);
            return t;
        }

        public void LoadAssetAsync<T>(ResRequestInfo resRequest, Action<T> callback, Action<T> defaultCallback = null) where T : Object
        {
            // 1.优先从缓存中提取资源信息
            bool result = CallbackByCache(resRequest, callback, defaultCallback);
            if (result) return;

            // 2.得到真实路径
            StartCoroutineManager.Start(InternalLoadAssetAsync(resRequest, callback, defaultCallback));
        }

        public bool UnloadRes(ResRequestInfo resRequest)
        {
            ResLog.Assert(_cacheRes.ContainsKey(resRequest.ResPath),"ResLoader UnLoadRes 失败,通过[{0}]找不到对应的AssetInfo", resRequest.ResPath);
            if (!_cacheRes.ContainsKey(resRequest.ResPath)) return false;

            AssetInfo assetInfo = _cacheRes[resRequest.ResPath];
            _cacheRes.Remove(resRequest.ResPath);

            bool result = _loader.UnloadAssetBundle(assetInfo);
            ResLog.Assert(result, "卸载失败:[{0}]", resRequest.ResPath);
            return result;
        }

        public bool UnLoadRes(Object obj)
        {
            var enumerator = _cacheRes.GetEnumerator();
            AssetInfo assetInfo = null;
            while (enumerator.MoveNext())
            {
                assetInfo = enumerator.Current.Value;
                _cacheRes.Remove(enumerator.Current.Key);
                break;
            }

            ResLog.Assert(assetInfo != null, "ResLoader UnLoadRes 失败,通过Object:[{0}]找不到对应的AssetInfo", obj.name);
            if (assetInfo == null) return false;

            bool result = _loader.UnloadAssetBundle(assetInfo);
            ResLog.Assert(result, "卸载失败:[{0}]", assetInfo.ResPath);
            return result;
        }

        public void OnUpdate(float dt)
        {
            if (_loader != null)
                _loader.OnUpdate();

            int length = _loadOpertions.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                ResLoadOpertion loadOpertion = _loadOpertions[i];
                loadOpertion.OnUpdate();
                if (!loadOpertion.IsExit()) continue;

                _onLoadingRes.Remove(loadOpertion.RequestResPath);
                _loadOpertions.RemoveAt(i);
            }
        }

        #endregion

        #region public 

        public int GetRefCount(string resPath)
        {
            if (_cacheRes.ContainsKey(resPath))
                return _cacheRes[resPath].RefCount;
            return 0;
        }

        public bool ContainsRes(string resPath)
        {
            return _cacheRes.ContainsKey(resPath);
        }

        // 可以异步加载，不超过最大加载数量
        public bool CanAsynLoad()
        {
            return _currAsyncCount < _maxAsyncCount;
        }

        public void CheckInfo()
        {
            int length = _onLoadingRes.Count;
            LogManager.Log("--------------------加载中的资源:[{0}]--------------------", length);
            for (int i = 0; i < length; i++)
            {
                ResLog.Log(_onLoadingRes[i]);
            }
            length = _loadOpertions.Count;
            LogManager.Log("--------------------加载中的请求:[{0}]--------------------", length);
            for (int i = 0; i < length; i++)
            {
                ResLog.Log(_loadOpertions[i].RequestResPath);
            }
        }

        #region 引用

        public void RefIncrease(string resPath)
        {
            if (!_cacheRes.ContainsKey(resPath)) return;
            // 1.找到资源
            AssetInfo assetInfo = _cacheRes[resPath];
            // 2.引用+1
            assetInfo.RefCount++;
        }

        public void RefDecrease(string resPath)
        {
            if (!_cacheRes.ContainsKey(resPath)) return;
            // 1.找到资源
            AssetInfo assetInfo = _cacheRes[resPath];
            // 2.引用-1
            assetInfo.RefCount--;
            ResLog.Assert(assetInfo.RefCount >= 0, "引用计数错误:[{0}]，Ref Count:[{1}]", resPath, assetInfo.RefCount);
        }

        #endregion

        #endregion

        #region private 

        private void Init()
        {
            //通过修改配置文件,并且通过工具来调整
            // 4.RESOUCES 前期本地和发布用
            //_loader = ResoucesLoader.instance;

            // 2.ASSETBUNDLE 实际发布用
            //_loader = AssetBundleLoader.Instance;
            //ResPathManager._suffix = new AssetBundleSuffix();
            // 3.WWW 实际发布用
            //_loader = W3Loader.instance;

            // 1.LOCAL 本地加载做研发用
#if UNITY_EDITOR
            _loader = AssetDatabaseLoader.instance;
            ResPathManager._suffix = new AssetDatabaseSuffix();
#endif
        }

        #region internal Loader

        private void InternalLoadAsset<T>(ResRequestInfo requestInfo) where T : Object
        {
            AssetInfo assetInfo = _loader.LoadAsset<T>(requestInfo.ResPath);
            ResLog.Assert(assetInfo != null, "ResLoader结论:内部加载失败,找不到对应的资源，路径:[{0}]", requestInfo.ResPath);
            PushAssetToCache(assetInfo);
        }

        private IEnumerator InternalLoadAssetAsync<T>(ResRequestInfo resRequest, Action<T> callback, Action<T> defaultCallback = null) where T : Object
        {
            // 1.得到路径 检测是否处于加载中
            if (_onLoadingRes.Contains(resRequest.ResPath))
            {
                // 3.等待加载 或者之类来一个ResWaitOpetion来确认
                // TODO Factory
                ResWaitLoadOpertion resWaitLoad = new ResWaitLoadOpertion(resRequest.ResPath, TIME_OUT);
                yield return resWaitLoad;
                resWaitLoad.OutResult();
                resWaitLoad = null;
            }
            else
            {
                if (!CanAsynLoad())
                {
                    yield return _maxOperationLoad;
                }
                _currAsyncCount++;
                _onLoadingRes.Add(resRequest.ResPath);
                // 4.请求异步加载
                ResLoadOpertion loadOpertion = _loader.LoadAssetAsync<T>(resRequest.ResPath);
                _loadOpertions.Add(loadOpertion);
                // 等待加载完成
                yield return loadOpertion;
                AssetInfo assetInfo = loadOpertion.GetAsset();
                // 6.卸载请求信息
                loadOpertion.UnloadRequest();
                // 7.t推送到内存中
                PushAssetToCache(assetInfo);

                _currAsyncCount--;
            }
            bool result = CallbackByCache(resRequest, callback, defaultCallback);
            if (!result)
                ResLog.Error("加载完成...但出现资源错误,path:[{0}]", resRequest.ResPath);
        }

        #endregion

        //从缓存中得到
        private T PopAssetForCache<T>(ResRequestInfo resRequest) where T : UnityEngine.Object
        {
            AssetInfo assetInfo;
            _cacheRes.TryGetValue(resRequest.ResPath, out assetInfo);
            if (assetInfo != null)
                return assetInfo.GetAsset<T>();
            return null;
        }

        //放到缓存中
        private bool PushAssetToCache(AssetInfo assetInfo)
        {
            bool result = (assetInfo == null);
            ResLog.Assert(!result, "ResLoader PushAssetToCache 推送到缓存中的AssetInfo 是空的");

            if (result || _cacheRes.ContainsKey(assetInfo.ResPath))
                return false;
            _cacheRes.Add(assetInfo.ResPath, assetInfo);
            return true;
        }

        // 异步从缓冲得到资源，并且回复
        public bool CallbackByCache<T>(ResRequestInfo resRequest, Action<T> callback, Action<T> defaultCallback = null) where T : Object
        {
            // 1.优先从缓存中提取资源信息
            T t = PopAssetForCache<T>(resRequest);
            if (t == null) return false;

            if (callback != null)
                callback.Invoke(t);
            if (defaultCallback != null)
                defaultCallback.Invoke(t);
            return true;
        }

        #endregion
    }
}

