using System;
using System.Collections;
using System.Collections.Generic;
using Summer;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

/// <summary>
/// TODO 需要提供一个异步请求的管道，同一事件请求的数量是有限制的
/// </summary>
public class ResManager : I_TextureLoad, I_AudioLoad, I_PrefabLoad
{
    public static ResManager instance = new ResManager();
    public Texture _bg_loading;
    public Dictionary<E_GameResType, Dictionary<string, AssetInfo>> _map_res =
        new Dictionary<E_GameResType, Dictionary<string, AssetInfo>>();

    public List<OloadOpertion> _load_opertions = new List<OloadOpertion>(16);

    public static int max_async_count = 10;
    public int curr_async_index = 0;

    protected List<string> on_loading_res //加载中的资源
            = new List<string>();

    public I_ResourceLoad _loader;


    public ResManager()
    {
        //通过修改配置文件,并且通过工具来调整
        // 4.RESOUCES 前期本地和发布用
        //_loader = AssetDatabaseLoader.instance;

        // 2.ASSETBUNDLE 实际发布用
        //_loader = AssetBundleLoader.instance;

        // 3.WWW 实际发布用
        //_loader = W3Loader.instance;

        // 1.LOCAL 本地加载做研发用
        _loader = ResoucesLoader.instance;
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

    #endregion

    #region Load

    public T LoadAsset<T>(string name, E_GameResType res_type) where T : Object
    {
        // 1.优先从缓存中提取资源信息
        AssetInfo asset_info = _pop_asset_for_cache(name, res_type);
        if (asset_info != null)
        {
            return asset_info.GetAsset<T>();
        }

        // 2.通过加载器加载
        _internal_load_asset(name, res_type);
        // 3.从缓存中得到资源
        asset_info = _pop_asset_for_cache(name, res_type);
        if (asset_info != null)
            return asset_info.GetAsset<T>();

        LogManager.Error("找不到对应的资源，名字:[{0}],资源类型[{1}]", name, res_type);
        return null;
    }

    public void LoadAssetAsync<T>(string name, E_GameResType res_type, Action<T> callback) where T : Object
    {
        // 1.优先从缓存中提取资源信息
        bool result = _callback_asset_by_cache(name, res_type, callback);
        if (result)
            return;

        // 2.得到真实路径
        StartCoroutineManager.Start(_internal_load_asset_async(name, res_type, callback));
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
        int length = _load_opertions.Count - 1;
        for (int i = length; i >= 0; i--)
        {
            if (!_load_opertions[i].Update())
            {
                _load_opertions.RemoveAt(i);
            }
        }

        if (_loader != null)
            _loader.Update();
    }

    #endregion

    #region Texture图片加载

    public Texture LoadTexture(RawImage img, string name, E_GameResType res_type)
    {
        img.texture = _bg_loading;
        Texture texture = LoadAsset<Texture>(name, res_type);
        if (texture != null)
        {
            img.texture = texture;
        }
        return texture;
    }

    public void LoadTextureAsync(RawImage img, string name, E_GameResType res_type, Action<Texture> callback)
    {
        img.texture = _bg_loading;
        Action<Texture> action = delegate (Texture texture)
        {
            if (texture != null)
            {
                img.texture = texture;
                img.SetNativeSize();
            }
            if (callback != null)
                callback.Invoke(texture);
        };
        LoadAssetAsync(name, res_type, action);
    }

    #endregion

    #region Audio

    public AudioClip LoadAudio(AudioSource audio_source, string name, E_GameResType res_type)
    {
        AudioClip audio = LoadAsset<AudioClip>(name, res_type);
        if (audio != null)
        {
            audio_source.clip = audio;
        }
        return audio;
    }

    public void LoadAudioAsync(AudioSource audio_source, string name, E_GameResType res_type, Action<AudioClip> complete)
    {

        Action<AudioClip> action = delegate (AudioClip audio_clip)
        {
            audio_source.clip = audio_clip;
            if (complete != null)
                complete.Invoke(audio_clip);
        };
        LoadAssetAsync(name, res_type, action);
    }

    #endregion

    #region animation

    #endregion

    #region GameObject

    public GameObject LoadPrefab(string name, E_GameResType res_type)
    {
        GameObject game_object = LoadAsset<GameObject>(name, res_type);
        return game_object;
    }

    public void LoadPrefabAsync(string name, E_GameResType res_type, Action<GameObject> complete)
    {
        Action<GameObject> action = delegate (GameObject game_object)
        {
            if (complete != null)
                complete.Invoke(game_object);
        };
        LoadAssetAsync(name, res_type, action);
    }

    #endregion

    #region byte[]

    // 独立加载的信息
    public byte[] LoadByte(string name, E_GameResType res_type)
    {
        TextAsset text_asset = LoadAsset<TextAsset>(name, res_type);
        return text_asset.bytes;
    }

    public void LoadByteAsync(string name, E_GameResType res_type, Action<TextAsset> callback)
    {
        LoadAssetAsync(name, res_type, callback);
    }

    #endregion

    #region public
    public bool ContainsRes(string assetbundle_name)
    {
        return on_loading_res.Contains(assetbundle_name);
    }
    #endregion

    #region internal Loader

    public void _internal_load_asset(string name, E_GameResType res_type)
    {
        // 1.确定路径
        string real_path = ResPathManager.find_path(res_type, name);
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
            LogManager.Error("找不到对应的资源，路径:[{0}]", real_path);
        }
    }

    public IEnumerator _internal_load_asset_async<T>(string name, E_GameResType res_type, Action<T> callback) where T : Object
    {
        yield return null;
        // 1.得到路径
        string assetbundle_name = ResPathManager.find_path(res_type, name);
        curr_async_index++;
        // 2.检测是否处于加载中
        if (on_loading_res.Contains(assetbundle_name))
        {
            // 3.等待加载
            OabLoadWaitOpertion wait_opertion = new OabLoadWaitOpertion(assetbundle_name, 120f);
            _load_opertions.Add(wait_opertion);
            yield return wait_opertion;

        }
        else
        {
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
        }
        bool result = _callback_asset_by_cache(name, res_type, callback);
        if (!result)
            LogManager.Error("加载中...资源错误,path:[{0}]", name);
    }

    #endregion

    #region private
    public void _init()
    {
        _bg_loading = Resources.Load<Texture>("default/bg_loading");
        if (_bg_loading == null)
            LogManager.Error("找不到默认的图片信息");
    }

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
        E_GameResType res_type = asset_info.AssetType;
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

    public bool _callback_asset_by_cache<T>(string name, E_GameResType res_type, Action<T> callback) where T : Object
    {
        AssetInfo asset_info = _pop_asset_for_cache(name, res_type);
        if (asset_info != null)
        {
            T t = asset_info.GetAsset<T>();
            callback.Invoke(t);
            return true;
        }
        return false;
    }

    #endregion

}
