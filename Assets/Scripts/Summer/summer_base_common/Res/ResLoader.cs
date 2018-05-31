using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Summer.Loader
{
    public class ResLoader : I_Update
    {
        #region 属性

        public static ResLoader instance = new ResLoader();

        public Dictionary<string, AssetInfo> _map_res =
        new Dictionary<string, AssetInfo>();

        protected List<string> _on_loading_res = new List<string>();    // 加载中的资源
        public static int max_async_count = 5;                          // 异步最大加载的数量
        public int curr_async_index;                                    // 当前异步加载的数量
        public const float TIME_OUT = 120;                              // 超时时间
        public I_ResourceLoad _loader;

        #endregion

        #region 构造
        public ResLoader()
        {
            //通过修改配置文件,并且通过工具来调整
            // 4.RESOUCES 前期本地和发布用
            //_loader = ResoucesLoader.instance;

            // 2.ASSETBUNDLE 实际发布用
            _loader = AssetBundleLoader.Instance;
            ResPathManager._suffix = new AssetBundleSuffix();
            // 3.WWW 实际发布用
            //_loader = W3Loader.instance;

            // 1.LOCAL 本地加载做研发用


#if UNITY_EDITOR
            //_loader = AssetDatabaseLoader.instance;
            //ResPathManager._suffix = new AssetDatabaseSuffix();
#endif

            _init();
        }


        #endregion

        #region 基础的Load

        public T LoadAsset<T>(ResRequestInfo res_request) where T : Object
        {
            // 1.优先从缓存中提取资源信息
            AssetInfo asset_info = _pop_asset_for_cache(res_request);
            if (asset_info != null)
                return asset_info.GetAsset<T>();

            // 2.通过加载器加载
            _internal_load_asset<T>(res_request);

            // 3.从缓存中得到资源
            asset_info = _pop_asset_for_cache(res_request);
            if (asset_info != null)
                return asset_info.GetAsset<T>();

            //ResLog.Error("ResLoader结论,加载资源失败路径:[{0}]", res_request.res_path);
            return null;
        }

        public void LoadAssetAsync<T>(ResRequestInfo res_request, Action<T> callback, Action<T> default_callback = null) where T : Object
        {
            // 1.优先从缓存中提取资源信息
            bool result = _callback_asset_by_cache(res_request, callback, default_callback);
            if (result) return;

            // 2.得到真实路径
            StartCoroutineManager.Start(_internal_load_asset_async(res_request, callback, default_callback));
        }

        public bool UnloadAll()
        {
            return false;
        }

        public bool UnloadAssetBundle(string ref_name, E_GameResType res_type)
        {
            return false;
        }

        public void OnUpdate(float dt)
        {
            /*int length = _load_opertions.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                if (!_load_opertions[i].Update())
                {
                    _load_opertions.RemoveAt(i);
                }
            }
            */
            if (_loader != null)
                _loader.OnUpdate();
        }

        #endregion

        #region

        public bool ContainsRes(string assetbundle_name)
        {
            return _on_loading_res.Contains(assetbundle_name);
        }

        #endregion

        #region private 

        public void _init()
        {

        }

        #region internal Loader

        public void _internal_load_asset<T>(ResRequestInfo request_info) where T : Object
        {
            AssetInfo asset_info = _loader.LoadAsset(request_info.res_path);
            ResLog.Assert(asset_info != null, "ResLoader结论:内部加载失败,找不到对应的资源，路径:[{0}]", request_info.res_path);
            _push_asset_to_cache(asset_info);
        }

        public IEnumerator _internal_load_asset_async<T>(ResRequestInfo res_request, Action<T> callback, Action<T> default_callback = null) where T : Object
        {
            // 1.得到路径 检测是否处于加载中
            if (_on_loading_res.Contains(res_request.res_path))
            {
                // 3.等待加载 或者之类来一个ResWaitOpetion来确认
                float load_time = 0f;
                while (_on_loading_res.Contains(res_request.res_path))
                {
                    load_time += Time.deltaTime;
                    if (load_time > TIME_OUT)
                    {
                        ResLog.Error("超时加载：{0}", res_request.res_path);
                        yield break;
                    }
                    yield return null;
                }
            }
            else
            {
                curr_async_index++;
                _on_loading_res.Add(res_request.res_path);
                // 4.请求异步加载
                LoadOpertion load_opertion = _loader.LoadAssetAsync(res_request.res_path);
                // 等待加载完成
                yield return load_opertion;
                AssetInfo asset_info = new AssetInfo(load_opertion.GetAsset(), res_request);
                // 6.卸载信息
                load_opertion.UnloadRequest();
                // 7.t推送到内存中
                _push_asset_to_cache(asset_info);
                _on_loading_res.Remove(res_request.res_path);
                curr_async_index--;
            }
            bool result = _callback_asset_by_cache(res_request, callback, default_callback);
            if (!result)
                ResLog.Error("加载完成...但出现资源错误,path:[{0}]", res_request.res_path);
        }

        #endregion

        //从缓存中得到
        public AssetInfo _pop_asset_for_cache(ResRequestInfo res_request)
        {
            AssetInfo asset_info;
            if (!_map_res.TryGetValue(res_request.res_path, out asset_info))
                return null;
            asset_info.RefCount++;
            return asset_info;
        }

        //放到缓存中
        public bool _push_asset_to_cache(AssetInfo asset_info)
        {
            if (asset_info == null) return false;
            // 1.根据资源类型和名字找到对应的cache
            // 2.如果已经存在引用-1，如果不存在，填入到缓存
            if (!_map_res.ContainsKey(asset_info.ResPath))
            {
                // 2.添加到cache中去
                _map_res.Add(asset_info.ResPath, asset_info);
                //LogManager.Log("添加到缓存中[{0}]", asset_info.ToString());
            }
            else
            {
                asset_info.RefCount--;
            }
            return true;
        }

        // 异步从缓冲得到资源，并且回复
        public bool _callback_asset_by_cache<T>(ResRequestInfo res_request_info, Action<T> callback, Action<T> default_callback = null) where T : Object
        {
            // 1.优先从缓存中提取资源信息
            AssetInfo asset_info = _pop_asset_for_cache(res_request_info);
            if (asset_info == null) return false;
            T t = asset_info.GetAsset<T>();
            if (callback != null)
                callback.Invoke(t);
            if (default_callback != null)
                default_callback.Invoke(t);
            return true;
        }

        #endregion
    }
}

