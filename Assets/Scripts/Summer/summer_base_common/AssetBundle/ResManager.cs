using System;
using System.Collections;
using System.Collections.Generic;
using Summer;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

/// <summary>
/// TODO
///     1.后缀名的解决方案
///     2.加载的资源和加载的路径是两回事
/// Bug 由于提供了全路径，所以导致他加载的时候所在的cache是会出问题的， 这就需要对资源做严格的区分
/// Bug 按照资源的类型严格区分，同时屏蔽掉quanming路径这个类型
/// 可能是概念性bug，之前的思路出现了错误想法，高内聚，低耦合思路
/// 本身资源加载的作用就是资源加载器
/// 提供基础的作用如下
/// 1.加载资源
/// 2.控制同时加载次数
/// 3.控制同一时间多次假期
/// 等相关问题
/// 
/// 那么针对资源具体的卸载。就需要根据不同类型资源不同的卸载策略了。所以quanming这个不一定是bug问题
/// </summary>
public class ResManager : I_TextureLoad, I_AudioLoad, I_PrefabLoad
{
    public static ResManager instance = new ResManager();
    public Texture _bg_loading;                                     // 异步加载时的图片资源
    public Dictionary<E_GameResType, Dictionary<string, AssetInfo>> _map_res =
        new Dictionary<E_GameResType, Dictionary<string, AssetInfo>>();

    protected List<string> _on_loading_res = new List<string>();    // 加载中的资源
    public static int max_async_count = 5;                          // 异步最大加载的数量
    public int curr_async_index;                                    // 当前异步加载的数量

    public I_ResourceLoad _loader;
    public AResourceSuffix _res_suffix;

    public ResManager()
    {
        //通过修改配置文件,并且通过工具来调整
        // 4.RESOUCES 前期本地和发布用
        //_loader = ResoucesLoader.instance;

        // 2.ASSETBUNDLE 实际发布用
        //_loader = AssetBundleLoader.instance;

        // 3.WWW 实际发布用
        //_loader = W3Loader.instance;

        // 1.LOCAL 本地加载做研发用
        _loader = AssetDatabaseLoader.instance;
        _res_suffix = new AssetDatabaseSuffix();

        _init();
    }

    #region 引用计数

    public void RefIncrease(string ref_name, E_GameResType res_type)
    {

        /* // 1.找到资源
         AssetInfo asset_info = null;
         // 2.引用+1
         asset_info.RefCount++;*/
    }

    public void RefDecrease(string ref_name, E_GameResType res_type)
    {
        /* // 1.找到资源
         AssetInfo asset_info = null;
         // 2.引用-1
         asset_info.RefCount--;
         if (asset_info.RefCount < 0)
             LogManager.Error("");*/
    }

    public RefCounter _internal_ref_increase(string name, E_GameResType res_type, GameObject obj)
    {
        RefCounter counter = obj.GetComponent<RefCounter>();
        if (counter == null)
            counter = obj.AddComponent<RefCounter>();

        counter.AddRef(name, res_type);
        return counter;
    }

    public RefCounter _internal_ref_decrease(GameObject obj)
    {
        RefCounter counter = obj.GetComponent<RefCounter>();
        if (counter != null)
            counter.RemoveRef();
        return counter;
    }

    #endregion

    #region 基础的Load

    public T LoadAsset<T>(string name, E_GameResType res_type) where T : Object
    {
        // 1.优先从缓存中提取资源信息
        AssetInfo asset_info = _pop_asset_for_cache(name, res_type);
        if (asset_info != null)
        {
            return asset_info.GetAsset<T>();
        }

        // 2.通过加载器加载
        _internal_load_asset<T>(name, res_type);
        // 3.从缓存中得到资源
        asset_info = _pop_asset_for_cache(name, res_type);
        if (asset_info != null)
            return asset_info.GetAsset<T>();

        ResLog.Error("找不到对应的资源，名字:[{0}],资源类型[{1}]", name, res_type);
        return null;
    }

    public void LoadAssetAsync<T>(string name, E_GameResType res_type, Action<T> callback, Action<T> default_callback = null) where T : Object
    {
        // 1.优先从缓存中提取资源信息
        bool result = _callback_asset_by_cache(name, res_type, callback, default_callback);
        if (result)
            return;

        // 2.得到真实路径
        StartCoroutineManager.Start(_internal_load_asset_async(name, res_type, callback, default_callback));
    }

    public bool UnloadAll(E_GameResType res_type)
    {
        return false;
    }

    public bool UnloadAssetBundle(string ref_name, E_GameResType res_type)
    {
        return false;
    }

    public void Update()
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
            _loader.Update();
    }

    #endregion

    #region Load类型 Texture Audio Animation GameObject 

    #region Texture图片加载

    public Texture LoadTexture(RawImage img, string name, E_GameResType res_type)
    {
        GameObject obj = img.gameObject;
        _internal_ref_decrease(obj);

        img.texture = _bg_loading;
        Texture texture = LoadAsset<Texture>(name, res_type);

        if (texture != null)
        {
            img.texture = texture;

            _internal_ref_increase(name, res_type, obj);

        }
        return texture;
    }

    public void LoadTextureAsync(RawImage img, string name, E_GameResType res_type, Action<Texture> callback = null)
    {
        ResLog.Log("name:{0},res_type:{1}", name, res_type);
        GameObject obj = img.gameObject;
        _internal_ref_decrease(obj);

        img.texture = _bg_loading;
        Action<Texture> default_callback = delegate (Texture texture)
        {
            if (texture != null)
            {
                img.texture = texture;
                //img.SetNativeSize();
                _internal_ref_increase(name, res_type, img.gameObject);
            }
        };
        LoadAssetAsync(name, res_type, callback, default_callback);
    }

    public void ResetDefaultTexture(RawImage img)
    {
        img.texture = _bg_loading;
    }

    #endregion

    #region Audio

    public AudioClip LoadAudio(AudioSource audio_source, string name, E_GameResType res_type)
    {
        if (audio_source == null) return null;
        AudioClip audio = LoadAsset<AudioClip>(name, res_type);
        if (audio != null)
        {
            audio_source.clip = audio;
        }
        return audio;
    }


    public void LoadAudioAsync(AudioSource audio_source, string name, E_GameResType res_type, Action<AudioClip> complete)
    {
        if (audio_source == null) return;
        Action<AudioClip> action = delegate (AudioClip audio_clip)
        {
            audio_source.clip = audio_clip;
            if (complete != null)
                complete.Invoke(audio_clip);
        };
        LoadAssetAsync(name, res_type, action);
    }


    #endregion

    #region Animation

    #endregion

    #region GameObject
    public GameObject LoadPrefab(string name, E_GameResType res_type)
    {
        GameObject prefab_gameobj = LoadAsset<GameObject>(name, res_type);
        GameObject instantiste_gameobj = GameObjectHelper.Instantiate(prefab_gameobj);
        return instantiste_gameobj;
    }

    public void LoadPrefabAsync(string name, E_GameResType res_type, Action<GameObject> complete)
    {
        Action<GameObject> action = delegate (GameObject game_object)
        {
            GameObject instantiste_gameobj = GameObjectHelper.Instantiate(game_object);
            if (complete != null)
                complete.Invoke(instantiste_gameobj);
        };
        LoadAssetAsync(name, res_type, action);
    }

    #endregion

    #region bytes

    public string LoadText(string name, E_GameResType res_type = E_GameResType.quanming)
    {
        TextAsset text_asset = LoadAsset<TextAsset>(name, res_type);
        return text_asset.text;
    }

    public byte[] LoadByte(string name, E_GameResType res_type = E_GameResType.quanming)
    {
        TextAsset text_asset = LoadAsset<TextAsset>(name, res_type);
        return text_asset.bytes;
    }

    #endregion

    #endregion

    #region public

    public bool ContainsRes(string assetbundle_name)
    {
        return _on_loading_res.Contains(assetbundle_name);
    }

    #endregion

    #region private

    public void _init()
    {
        _bg_loading = Resources.Load<Texture>("default/bg_loading");
        if (_bg_loading == null)
            ResLog.Error("找不到默认的图片信息");
    }

    #region internal Loader

    public void _internal_load_asset<T>(string name, E_GameResType res_type) where T : Object
    {
        // 1.确定路径
        string real_path = ResPathManager.FindPath<T>(res_type, name, _res_suffix);
        float time = Time.realtimeSinceStartup;
        // 2.加载资源
        Object obj = _loader.LoadAsset(real_path);
        if (obj != null)
        {
            // 3.添加到缓存
            AssetInfo asset_info = new AssetInfo(obj, name, res_type);
            asset_info.load_time = Time.realtimeSinceStartup - time;
            asset_info.async = false;
            _push_asset_to_cache(asset_info);
        }
        else
        {
            ResLog.Error("找不到对应的资源，路径:[{0}]", real_path);
        }
    }

    public IEnumerator _internal_load_asset_async<T>(string name, E_GameResType res_type, Action<T> callback, Action<T> default_callback = null) where T : Object
    {
        // 1.得到路径
        string assetbundle_name = ResPathManager.FindPath<T>(res_type, name, _res_suffix);
        // 2.检测是否处于加载中
        if (_on_loading_res.Contains(assetbundle_name))
        {
            // 3.等待加载
            float time_out = 60f;
            float load_time = 0f;
            while (_on_loading_res.Contains(assetbundle_name))
            {
                load_time += Time.deltaTime;
                if (load_time > time_out)
                {
                    ResLog.Error("超时加载：{0}", assetbundle_name);
                    break;
                }
                yield return null;
            }
        }
        else
        {
            //while (curr_async_index > max_async_count)
            //    yield return null;

            curr_async_index++;
            _on_loading_res.Add(assetbundle_name);
            // 4.请求异步加载
            float time = Time.realtimeSinceStartup;
            OloadOpertion load_opertion = _loader.LoadAssetAsync(assetbundle_name);
            // 等待加载完成
            yield return load_opertion;
            AssetInfo asset_info = new AssetInfo(load_opertion.GetAsset(), name, res_type);
            // 5.记录测试信息
            asset_info.load_time = Time.realtimeSinceStartup - time;
            asset_info.async = true;
            // 6.卸载信息
            load_opertion.UnloadAssetBundle();
            // 7.t推送到内存中
            _push_asset_to_cache(asset_info);
            _on_loading_res.Remove(assetbundle_name);
            curr_async_index--;
        }
        bool result = _callback_asset_by_cache(name, res_type, callback, default_callback);
        if (!result)
            ResLog.Error("加载中...资源错误,path:[{0}]", name);
    }

    #endregion

    //从缓存中得到
    public AssetInfo _pop_asset_for_cache(string name, E_GameResType res_type)
    {
        // 1.确认是否缓存
        Dictionary<string, AssetInfo> map_assets;
        AssetInfo asset_info;
        if (!_map_res.TryGetValue(res_type, out map_assets))
        {
            return null;
        }
        // 2.已经缓存，立马返回，缓存+1,
        map_assets.TryGetValue(name, out asset_info);
        if (asset_info != null)
        {
            asset_info.RefCount++;
        }
        return asset_info;
    }

    //放到缓存中
    public bool _push_asset_to_cache(AssetInfo asset_info)
    {
        E_GameResType res_type = asset_info.GameResType;
        string asset_name = asset_info.Name;
        Dictionary<string, AssetInfo> map_assets;
        // 1.根据资源类型和名字找到对应的cache
        if (!_map_res.TryGetValue(res_type, out map_assets))
        {
            map_assets = new Dictionary<string, AssetInfo>();
            _map_res.Add(res_type, map_assets);
        }

        // 2.如果已经存在引用-1，如果不存在，填入到缓存
        if (!map_assets.ContainsKey(asset_name))
        {
            // 2.添加到cache中去
            map_assets.Add(asset_name, asset_info);
            //LogManager.Log("添加到缓存中[{0}]", asset_info.ToString());
        }
        else
        {
            asset_info.RefCount--;
        }
        return true;
    }

    public bool _callback_asset_by_cache<T>(string name, E_GameResType res_type, Action<T> callback, Action<T> default_callback = null) where T : Object
    {
        AssetInfo asset_info = _pop_asset_for_cache(name, res_type);
        if (asset_info != null/*&& asset_info.HasAsset()*/)
        {
            T t = asset_info.GetAsset<T>();
            if (callback != null)
                callback.Invoke(t);
            if (default_callback != null)
                default_callback.Invoke(t);
            return true;
        }
        return false;
    }

    #endregion

}


public class ResLog
{
    #region Log
    public static void Log(string message)
    {
        if (!LogManager.open_load_res) return;
        LogManager.Log(message);
    }

    public static void Log(string message, params object[] args)
    {
        if (!LogManager.open_load_res) return;
        LogManager.Log(message, args);
    }

    public static void Error(string message)
    {
        if (!LogManager.open_load_res) return;
        LogManager.Error(message);
    }

    public static void Error(string message, params object[] args)
    {
        if (!LogManager.open_load_res) return;
        LogManager.Error(message, args);
    }

    public static void Assert(bool condition, string message)
    {
        if (!LogManager.open_load_res) return;
        LogManager.Assert(condition, message);
    }

    public static void Assert(bool condition, string message, params object[] args)
    {
        if (!LogManager.open_load_res) return;
        LogManager.Assert(condition, message, args);
    }

    #endregion
}