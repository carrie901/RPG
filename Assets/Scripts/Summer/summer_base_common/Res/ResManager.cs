using System;
using System.Collections;
using System.Collections.Generic;
using Summer;
using Summer.Loader;
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
public class ResManager : I_ResLoad
{
    public static ResManager instance = new ResManager();
    public Texture _bg_loading;                                     // 异步加载时的图片资源
    public ResLoader _res_loader;



    public ResManager()
    {
        _res_loader = ResLoader.instance;
        _init();
    }

    #region 引用计数

    public void RefIncrease(string res_path)
    {

        /* // 1.找到资源
         AssetInfo asset_info = null;
         // 2.引用+1
         asset_info.RefCount++;*/
    }

    public void RefDecrease(string res_path)
    {
        /* // 1.找到资源
         AssetInfo asset_info = null;
         // 2.引用-1
         asset_info.RefCount--;
         if (asset_info.RefCount < 0)
             LogManager.Error("");*/
    }

    public RefCounter _internal_ref_increase(ResRequestInfo res_request, GameObject obj)
    {
        RefCounter counter = obj.GetComponent<RefCounter>();
        if (counter == null)
            counter = obj.AddComponent<RefCounter>();

        counter.AddRef(res_request.res_path);
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



    #region Load类型 Texture Audio Animation GameObject 

    #region Texture图片加载

    public Texture LoadTexture(ResRequestInfo res_request)
    {
        Texture texture = _res_loader.LoadAsset<Texture>(res_request);
        return texture;
    }

    public Texture LoadTexture(RawImage img, ResRequestInfo res_request)
    {
        GameObject obj = img.gameObject;


        img.texture = _bg_loading;
        Texture texture = _res_loader.LoadAsset<Texture>(res_request);

        if (texture != null)
        {
            _internal_ref_decrease(obj);
            img.texture = texture;
            _internal_ref_increase(res_request, obj);

        }
        return texture;
    }

    public void LoadTextureAsync(RawImage img, ResRequestInfo res_request, Action<Texture> callback = null)
    {
        GameObject obj = img.gameObject;


        img.texture = _bg_loading;
        Action<Texture> default_callback = delegate (Texture texture)
        {
            if (texture != null)
            {
                _internal_ref_decrease(obj);
                img.texture = texture;
                //img.SetNativeSize();
                _internal_ref_increase(res_request, img.gameObject);
            }
        };
        _res_loader.LoadAssetAsync(res_request, callback, default_callback);
    }

    public void ResetDefaultTexture(RawImage img)
    {
        img.texture = _bg_loading;
    }

    #endregion

    #region Audio

    /*public AudioClip LoadAudio(AudioSource audio_source, string name, E_GameResType res_type = E_GameResType.quanming)
    {
        if (audio_source == null) return null;
        AudioClip audio = LoadAsset<AudioClip>(name, res_type);
        if (audio != null)
        {
            audio_source.clip = audio;
        }
        return audio;
    }*/

    /*public void LoadAudioAsync(AudioSource audio_source, string name, E_GameResType res_type, Action<AudioClip> complete)
    {
        if (audio_source == null) return;
        Action<AudioClip> action = delegate (AudioClip audio_clip)
        {
            audio_source.clip = audio_clip;
            if (complete != null)
                complete.Invoke(audio_clip);
        };
        LoadAssetAsync(name, res_type, action);
    }*/


    #endregion

    #region Animation

    public AnimationClip LoadAnimationClip(ResRequestInfo res_request)
    {
        AnimationClip animation_clip = _res_loader.LoadAsset<AnimationClip>(res_request);
        return animation_clip;
    }

    public void LoadAnimationClipAsync(ResRequestInfo res_request, Action<AnimationClip> complete)
    {
         _res_loader.LoadAssetAsync<AnimationClip>(res_request, complete);
    }

    #endregion

    #region GameObject

    public GameObject LoadPrefab(ResRequestInfo res_request, bool is_copy=true)
    {
        GameObject prefab_gameobj = _res_loader.LoadAsset<GameObject>(res_request);
        if (!is_copy)
        {
            return prefab_gameobj;
        }
        GameObject instantiste_gameobj = GameObjectHelper.Instantiate(prefab_gameobj);
        return instantiste_gameobj;
    }

    public void LoadPrefabAsync(ResRequestInfo res_request, Action<GameObject> complete = null)
    {
        Action<GameObject> action = delegate (GameObject game_object)
        {
            GameObject instantiste_gameobj = GameObjectHelper.Instantiate(game_object);
            if (complete != null)
                complete.Invoke(instantiste_gameobj);
        };
        _res_loader.LoadAssetAsync(res_request, action);
    }

    #endregion

    #region bytes

    public string LoadText(ResRequestInfo res_request)
    {
        TextAsset text_asset = _res_loader.LoadAsset<TextAsset>(res_request);
        return text_asset.text;
    }

    public byte[] LoadByte(string res_name, E_GameResType res_type)
    {
        TextAsset text_asset = _res_loader.LoadAsset<TextAsset>(ResRequestFactory.CreateRequest<TextAsset>(res_name, res_type));
        return text_asset.bytes;
    }

    public byte[] LoadByte(ResRequestInfo res_request)
    {
        TextAsset text_asset = _res_loader.LoadAsset<TextAsset>(res_request);
        return text_asset.bytes;
    }

    #endregion

    #region Sprite

    public Sprite LoadSprite(ResRequestInfo res_request)
    {
        Sprite sprite = _res_loader.LoadAsset<Sprite>(res_request);
        return sprite;
    }

    public Sprite LoadSprite(Image img, ResRequestInfo res_request)
    {
        if (img == null) return null;
        Sprite sprite = _res_loader.LoadAsset<Sprite>(res_request);
        if (sprite != null)
        {
            img.sprite = sprite;
        }
        return sprite;
    }

    public void LoadSpriteAsync(Image img, ResRequestInfo res_request,
        Action<Sprite> complete = null)
    {
        if (img == null) return;
        Action<Sprite> action = delegate (Sprite sprite)
        {
            img.sprite = sprite;
            if (complete != null)
                complete.Invoke(sprite);
        };
        _res_loader.LoadAssetAsync(res_request, action);
    }


    #endregion

    #endregion

    #region public

    #endregion

    #region private

    public void _init()
    {
        _bg_loading = Resources.Load<Texture>("default/bg_loading");
        ResLog.Assert(_bg_loading != null, "找不到默认的图片信息");
    }

    #endregion



}


