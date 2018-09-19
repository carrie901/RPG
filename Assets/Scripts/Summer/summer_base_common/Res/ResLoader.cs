using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        public Dictionary<string, AssetInfo> MapRes { get { return _mapRes; } }
        protected readonly Dictionary<string, AssetInfo> _mapRes =
            new Dictionary<string, AssetInfo>();

        protected List<LoadOpertion> _loadOpertions
            = new List<LoadOpertion>();                                         // 加载中的操作

        private readonly List<string> _onLoadingRes = new List<string>();      // 加载中的资源
        public static int _maxAsyncCount = 5;                                   // 异步最大加载的数量
        private int _currAsyncIndex;                                            // 当前异步加载的数量
        public const float TIME_OUT = 120;                                      // 超时时间
        private readonly I_ResourceLoad _loader;

        #endregion

        #region 构造

        public ResLoader()
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
            _init();
        }

        #endregion

        #region 基础的Load

        public T LoadAsset<T>(ResRequestInfo resRequest) where T : Object
        {
            // 1.优先从缓存中提取资源信息
            T t = _pop_asset_for_cache<T>(resRequest);
            if (t != null) return t;

            // 2.通过加载器加载
            _internal_load_asset(resRequest);

            // 3.从缓存中得到资源
            t = _pop_asset_for_cache<T>(resRequest);
            ResLog.Assert(t != null, "ResLoader结论,加载资源失败路径:[{0}]", resRequest.ResPath);
            return t;
        }

        public void LoadAssetAsync<T>(ResRequestInfo resRequest, Action<T> callback, Action<T> defaultCallback = null) where T : Object
        {
            // 1.优先从缓存中提取资源信息
            bool result = _callback_asset_by_cache(resRequest, callback, defaultCallback);
            if (result) return;

            // 2.得到真实路径
            StartCoroutineManager.Start(_internal_load_asset_async(resRequest, callback, defaultCallback));
        }

        public bool UnloadRes(ResRequestInfo resRequest)
        {
            if (!_mapRes.ContainsKey(resRequest.ResPath)) return false;

            AssetInfo assetInfo = _mapRes[resRequest.ResPath];
            _mapRes.Remove(resRequest.ResPath);

            bool result = _loader.UnloadAssetBundle(assetInfo);
            ResLog.Assert(result, "卸载失败:[{0}]", resRequest.ResPath);
            return result;
        }

        public void OnUpdate(float dt)
        {
            if (_loader != null)
                _loader.OnUpdate();

            int length = _loadOpertions.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                _loadOpertions[i].OnUpdate();
                if (_loadOpertions[i].IsExit())
                {
                    _loadOpertions.RemoveAt(i);
                    _onLoadingRes.Remove(_loadOpertions[i].RequestResPath);
                    _loadOpertions[i] = null;
                }
            }
        }

        #endregion

        #region 引用

        public void RefIncrease(string resPath)
        {
            if (!_mapRes.ContainsKey(resPath)) return;
            // 1.找到资源
            AssetInfo assetInfo = _mapRes[resPath];
            // 2.引用+1
            assetInfo.RefCount++;
        }

        public void RefDecrease(string resPath)
        {
            if (!_mapRes.ContainsKey(resPath)) return;
            // 1.找到资源
            AssetInfo assetInfo = _mapRes[resPath];
            // 2.引用-1
            assetInfo.RefCount--;
            ResLog.Assert(assetInfo.RefCount >= 0, "引用计数错误:[{0}]，Ref Count:[{1}]", resPath, assetInfo.RefCount);
        }

        #endregion

        #region public 

        public int GetRefCount(string assetbundleName)
        {
            if (_mapRes.ContainsKey(assetbundleName))
                return _mapRes[assetbundleName].RefCount;
            return 0;
        }

        public bool ContainsRes(string resPath)
        {
            return _mapRes.ContainsKey(resPath);
        }

        #endregion

        #region private 

        public void _init() { }

        #region internal Loader

        public void _internal_load_asset(ResRequestInfo requestInfo)
        {
            AssetInfo assetInfo = _loader.LoadAsset(requestInfo.ResPath);
            ResLog.Assert(assetInfo != null, "ResLoader结论:内部加载失败,找不到对应的资源，路径:[{0}]", requestInfo.ResPath);
            _push_asset_to_cache(assetInfo);
        }

        public IEnumerator _internal_load_asset_async<T>(ResRequestInfo resRequest, Action<T> callback, Action<T> defaultCallback = null) where T : Object
        {
            // 1.得到路径 检测是否处于加载中
            if (_onLoadingRes.Contains(resRequest.ResPath))
            {
                // 3.等待加载 或者之类来一个ResWaitOpetion来确认
                float loadTime = 0f;
                while (_onLoadingRes.Contains(resRequest.ResPath))
                {
                    loadTime += Time.deltaTime;
                    if (loadTime > TIME_OUT)
                    {
                        ResLog.Error("超时加载：{0}", resRequest.ResPath);
                        yield break;
                    }
                    yield return null;
                }
            }
            else
            {
                _currAsyncIndex++;
                _onLoadingRes.Add(resRequest.ResPath);
                // 4.请求异步加载
                LoadOpertion loadOpertion = _loader.LoadAssetAsync(resRequest.ResPath);
                _loadOpertions.Add(loadOpertion);
                // 等待加载完成
                yield return loadOpertion;

                AssetInfo assetInfo = loadOpertion.GetAsset();
                // 6.卸载信息
                loadOpertion.UnloadRequest();
                // 7.t推送到内存中
                _push_asset_to_cache(assetInfo);

                _currAsyncIndex--;
            }
            bool result = _callback_asset_by_cache(resRequest, callback, defaultCallback);
            if (!result)
                ResLog.Error("加载完成...但出现资源错误,path:[{0}]", resRequest.ResPath);
        }

        #endregion

        //从缓存中得到
        public T _pop_asset_for_cache<T>(ResRequestInfo resRequest) where T : UnityEngine.Object
        {
            AssetInfo assetInfo;
            _mapRes.TryGetValue(resRequest.ResPath, out assetInfo);
            if (assetInfo != null)
                return assetInfo.GetAsset<T>();
            return null;
        }

        //放到缓存中
        public bool _push_asset_to_cache(AssetInfo assetInfo)
        {
            if (assetInfo == null || _mapRes.ContainsKey(assetInfo.ResPath)) return false;
            _mapRes.Add(assetInfo.ResPath, assetInfo);
            return true;
        }

        // 异步从缓冲得到资源，并且回复
        public bool _callback_asset_by_cache<T>(ResRequestInfo resRequest, Action<T> callback, Action<T> defaultCallback = null) where T : Object
        {
            // 1.优先从缓存中提取资源信息
            T t = _pop_asset_for_cache<T>(resRequest);
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

