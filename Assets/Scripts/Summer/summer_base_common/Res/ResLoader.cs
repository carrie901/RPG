using System;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;


namespace Summer
{
    /// <summary>
    /// 加载器
    /// 1.加载器不参与引用统计
    /// 
    /// 
    /// TODO 2018.10.17
    ///     1.I_ResourceLoad 提供卸载机制，是强行卸载的，还是有引用卸载的机制
    ///     2. 比如强卸载 。直接卸载掉这个资源的相关的所有内容，比如这个包中包含了三个资源。那么强卸载就会直接卸载掉三个资源
    ///     3. 引用卸载，等这个资源的 所有内容多卸载了。那么就直接卸载掉
    ///         例子: 卸载资源的A的引用，然后就对卸载 
    /// 
    /// </summary>
    public class ResLoader //: I_Update
    {
        #region 属性

        public static ResLoader Instance = new ResLoader();
        public static int _maxAsyncCount = 2;                                           // 异步最大加载的数量

        public Dictionary<string, I_ObjectInfo> _cacheRes
            = new Dictionary<string, I_ObjectInfo>(256);                                   // 已经加载的资源

        public List<LoadOpertion> _otherOperation = new List<LoadOpertion>(256);        // 其他非加载操作

        public List<ResLoadOpertion> _loadOpertions = new List<ResLoadOpertion>(256);   // 加载中的操作
        public List<string> _onLoadingRes = new List<string>(8);                        // 加载中的资源 

        public MaxAsyncCountOpertion _maxOperationLoad = new MaxAsyncCountOpertion();   // 最大加载请求
        public const float TIME_OUT = 120;                                              // 超时时间
        public int _currAsyncCount;                                                     // 当前异步加载的数量
        public I_ResourceLoad _loader;

        #endregion

        #region 构造

        public ResLoader()
        {
            Init();
        }

        #endregion

        #region 基础的Load

        public T LoadAsset<T>(string resPath) where T : Object
        {
            // 1.优先从缓存中提取资源信息
            T t = PopAssetForCache<T>(resPath);
            if (t != null) return t;

            // 2.通过加载器加载
            InternalLoadAsset<T>(resPath);

            // 3.从缓存中得到资源
            t = PopAssetForCache<T>(resPath);
            ResLog.Assert(t != null, "ResLoader LoadAsset 加载资源失败.路径:[{0}]", resPath);
            return t;
        }

        public void LoadAssetAsync<T>(string resPath, Action<T> callback, Action<T> defaultCallback = null) where T : Object
        {
            // 1.优先从缓存中提取资源信息
            bool result = CallbackByCache(resPath, callback, defaultCallback);
            if (result) return;

            // 2.得到真实路径
            StartCoroutineManager.Start(InternalLoadAssetAsync(resPath, callback, defaultCallback));
        }

        public bool UnLoadRes(string resPath)
        {
            string key = _loader.GetResPath(resPath);
            ResLog.Assert(_cacheRes.ContainsKey(key), "ResLoader UnLoadRes Fail.通过[{0}]找不到对应的I_ObjectInfo", resPath);
            if (!_cacheRes.ContainsKey(key)) return false;

            I_ObjectInfo objectInfo = _cacheRes[key];
            objectInfo.UnLoad();
            bool result = _loader.UnloadAsset(objectInfo);
            if (result)
                _cacheRes.Remove(objectInfo.Path);
            ResLog.Assert(result, "卸载失败:[{0}]", objectInfo.Path);
            return result;
        }

        public bool UnLoadRes(Object obj)
        {
            I_ObjectInfo objectInfo = GetAssetInfo(obj);
            if (objectInfo == null) return false;

            objectInfo.UnLoad();
            bool result = _loader.UnloadAsset(objectInfo);
            if (result)
                _cacheRes.Remove(objectInfo.Path);
            ResLog.Assert(result, "卸载失败:[{0}]", objectInfo.Path);
            return result;
        }

        public bool UnLoadRef(Object obj)
        {
            if (obj == null) return false;
            I_ObjectInfo objectInfo = GetAssetInfo(obj);
            if (objectInfo == null) return false;
            objectInfo.UnLoad();
            return true;
        }

        public void OnUpdate(float dt)
        {
            if (_loader != null)
                _loader.OnUpdate();
            int length = _otherOperation.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                LoadOpertion loadOpertion = _otherOperation[i];
                loadOpertion.OnUpdate();
                if (loadOpertion.IsDone())
                {
                    _otherOperation.RemoveAt(i);
                }
            }

            length = _loadOpertions.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                ResLoadOpertion loadOpertion = _loadOpertions[i];
                loadOpertion.OnUpdate();
                if (!loadOpertion.IsExit()) continue;

                _onLoadingRes.RemoveAt(i);
                _loadOpertions.RemoveAt(i);
            }
        }

        #endregion

        #region public 

        public int GetRefCount(string resPath)
        {
            string key = _loader.GetResPath(resPath);
            if (_cacheRes.ContainsKey(key))
                return _cacheRes[key].RefCount;
            return 0;
        }

        public bool ContainsRes(string resPath)
        {
            string key = _loader.GetResPath(resPath);
            return _cacheRes.ContainsKey(key);
        }

        // 可以异步加载，不超过最大加载数量
        public bool CanAsynLoad()
        {
            return _currAsyncCount < _maxAsyncCount;
        }

        public void CheckInfo()
        {
            int length = _otherOperation.Count;
            LogManager.Log("--------------------资源数量:[{0}]--------------------", _cacheRes.Count);
            LogManager.Log("--------------------其他请求:[{0}]--------------------", length);

            length = _onLoadingRes.Count;
            LogManager.Log("--------------------加载中的名字:[{0}]--------------------", length);
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
            _loader.CheckInfo();
        }

        #endregion

        #region private 

        private void Init()
        {
            //通过修改配置文件,并且通过工具来调整
            // 4.RESOUCES 前期本地和发布用
            //_loader = ResoucesLoader.instance;

            // 2.ASSETBUNDLE 实际发布用
            _loader = AssetBundleLoader.Instance;

            // 3.WWW 实际发布用
            //_loader = W3Loader.instance;

            // 1.LOCAL 本地加载做研发用
#if UNITY_EDITOR
            //_loader = AssetDatabaseLoader.instance;
#endif
        }

        #region internal Loader

        private void InternalLoadAsset<T>(string resPath) where T : Object
        {
            I_ObjectInfo objectInfo = _loader.LoadAsset<T>(resPath);
            ResLog.Assert(objectInfo != null, "ResLoader结论:内部加载失败,找不到对应的资源，路径:[{0}]", resPath);
            PushAssetToCache(objectInfo);
        }

        private IEnumerator InternalLoadAssetAsync<T>(string resPath, Action<T> callback, Action<T> defaultCallback = null) where T : Object
        {
            // 1.得到路径 检测是否处于加载中
            if (_onLoadingRes.Contains(resPath))
            {
                // 3.等待加载 或者之类来一个ResWaitOpetion来确认
                // TODO Factory
                ResWaitLoadOpertion resWaitLoad = new ResWaitLoadOpertion(resPath, TIME_OUT);
                _otherOperation.Add(resWaitLoad);
                yield return resWaitLoad;
                if (!ContainsRes(resPath))
                {
                    ResLog.Error("ResLoader InternalLoadAssetAsync 超时请求:[{0}]", resPath);
                    yield break;
                }
            }
            else
            {
                if (!CanAsynLoad())
                    yield return _maxOperationLoad;

                _currAsyncCount++;
                // 4.请求异步加载
                ResLoadOpertion loadOpertion = _loader.LoadAssetAsync<T>(resPath);
                ResLog.Assert(loadOpertion != null, "ResLoader InternalLoadAssetAsync 异步请求为空.加载路径:[{0}]", resPath);
                if (loadOpertion == null) yield break;
                // TODO 请求的地址是和加载的路径是不一样的东西
                _onLoadingRes.Add(resPath);
                _loadOpertions.Add(loadOpertion);
                // 等待加载完成
                if (!loadOpertion.IsDone())
                {
                    yield return loadOpertion;
                }
                I_ObjectInfo objectInfo = loadOpertion.GetAsset<T>(resPath);
                // 6.卸载请求信息
                loadOpertion.UnloadRequest();
                // 7.t推送到内存中
                PushAssetToCache(objectInfo);
                _currAsyncCount--;
            }
            bool result = CallbackByCache(resPath, callback, defaultCallback);
            if (!result)
                ResLog.Error("加载完成...但出现资源错误,path:[{0}]", resPath);
            yield return null;
        }

        #endregion

        //从缓存中得到
        private T PopAssetForCache<T>(string resPath) where T : Object
        {
            string key = _loader.GetResPath(resPath);
            I_ObjectInfo objectInfo;
            _cacheRes.TryGetValue(key, out objectInfo);
            if (objectInfo != null)
                return objectInfo.GetAsset<T>(resPath);
            return null;
        }

        //放到缓存中
        private void PushAssetToCache(I_ObjectInfo objectInfo)
        {
            ResLog.Assert(objectInfo != null, "ResLoader PushAssetToCache 推送到缓存中的AssetInfo 是空的");
            if (objectInfo == null || _cacheRes.ContainsKey(objectInfo.Path)) return;
            _cacheRes.Add(objectInfo.Path, objectInfo);
        }

        // 异步从缓冲得到资源，并且回复
        private bool CallbackByCache<T>(string resPath, Action<T> callback, Action<T> defaultCallback = null) where T : Object
        {
            // 1.优先从缓存中提取资源信息
            T t = PopAssetForCache<T>(resPath);
            if (t == null) return false;

            if (callback != null)
                callback.Invoke(t);
            if (defaultCallback != null)
                defaultCallback.Invoke(t);
            return true;
        }

        private I_ObjectInfo GetAssetInfo(Object obj)
        {
            var enumerator = _cacheRes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                I_ObjectInfo tmpObjectInfo = enumerator.Current.Value;
                if (tmpObjectInfo.CheckObject(obj))
                {
                    return tmpObjectInfo;
                }
            }

            ResLog.Assert(false, "ResLoader UnLoadRes 失败,通过Object:[{0}]找不到对应的AssetInfo", obj.name);
            return null;
        }

        #endregion
    }
}

