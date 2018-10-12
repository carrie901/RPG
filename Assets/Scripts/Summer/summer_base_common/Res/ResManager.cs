using System;
using Summer;
using UnityEngine;
using UnityEngine.UI;

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
/// 
/// TODO 2018.10.12
///     基本的资源工具 一般需要3层
///     1. 顶层，针对不同的资源有不同的资源策略，类似缓存池、Sprite的LRU算法
///     2. 中层的Resloader 资源加载，缓存最大加载限制，同时加载限制等等，不用关心到底是Resource/AssetDataBase/AssetBundle
///     3. Resource/AssetDataBase/AssetBundle 这一层 基本上忽略本地加载/和Resources 关注下AssetBundle的缓存
/// </summary>
public class ResManager : I_ResManager
{
    public static ResManager instance = new ResManager();
    public Texture _bgLoading;                                      // 异步加载时的图片资源
    public Sprite _defaultSprite;
    public ResLoader _resLoader;

    public ResManager()
    {
        _resLoader = ResLoader.instance;
        _init();
    }

    #region 引用计数

    public RefCounter _internal_ref_increase(ResRequestInfo resRequest, GameObject obj)
    {
        RefCounter counter = obj.GetComponent<RefCounter>();
        if (counter == null)
            counter = obj.AddComponent<RefCounter>();

        counter.AddRef(resRequest.ResPath);
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

    public Texture LoadTexture(ResRequestInfo resRequest)
    {
        Texture texture = _resLoader.LoadAsset<Texture>(resRequest);
        return texture;
    }

    public Texture LoadTexture(RawImage img, ResRequestInfo resRequest)
    {
        GameObject obj = img.gameObject;
        Texture texture = _resLoader.LoadAsset<Texture>(resRequest);

        if (texture != null)
        {
            _internal_ref_decrease(obj);
            img.texture = texture;
            _internal_ref_increase(resRequest, obj);
        }
        else
        {
            img.texture = _bgLoading;
        }
        return texture;
    }

    public void LoadTextureAsync(RawImage img, ResRequestInfo resRequest, Action<Texture> callback = null)
    {
        GameObject obj = img.gameObject;


        img.texture = _bgLoading;
        Action<Texture> defaultCallback = delegate (Texture texture)
        {
            if (texture != null)
            {
                _internal_ref_decrease(obj);
                img.texture = texture;
                //img.SetNativeSize();
                _internal_ref_increase(resRequest, img.gameObject);
            }
        };
        _resLoader.LoadAssetAsync(resRequest, callback, defaultCallback);
    }

    public void ResetDefaultTexture(RawImage img)
    {
        img.texture = _bgLoading;
    }

    #endregion

    #region Sprite

    /*public Sprite LoadSprite(ResRequestInfo res_request)
    {
        Sprite sprite = _res_loader.LoadAsset<Sprite>(res_request);
        return sprite;
    }*/

    /*public Sprite LoadSprite(Image img, ResRequestInfo res_request)
    {
        if (img == null) return null;
        Sprite sprite = _res_loader.LoadAsset<Sprite>(res_request);
        if (sprite != null)
        {
            img.sprite = sprite;
        }
        return sprite;
    }*/

    /*public void LoadSpriteAsync(Image img, ResRequestInfo res_request,
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
    }*/


    #endregion

    #region Audio

    #endregion

    #region Animation

    public AnimationClip LoadAnimationClip(ResRequestInfo resRequest)
    {
        AnimationClip animationClip = _resLoader.LoadAsset<AnimationClip>(resRequest);
        return animationClip;
    }

    public void LoadAnimationClipAsync(ResRequestInfo resRequest, Action<AnimationClip> complete)
    {
        _resLoader.LoadAssetAsync(resRequest, complete);
    }

    #endregion

    #region GameObject

    public GameObject LoadPrefab(string resName, E_GameResType resType = E_GameResType.QUANMING, bool isCopy = true)
    {
        ResRequestInfo resRequest = ResRequestFactory.CreateRequest<GameObject>(resName, resType);
        GameObject prefabGameobj = _resLoader.LoadAsset<GameObject>(resRequest);
        if (prefabGameobj == null) return null;
        if (!isCopy)
        {
            return prefabGameobj;
        }
        GameObject instantisteGameobj = GameObjectHelper.Instantiate(prefabGameobj);
        _internal_ref_increase(resRequest, instantisteGameobj);
        return instantisteGameobj;
    }

    public void LoadPrefabAsync(string resName, E_GameResType resType = E_GameResType.QUANMING, Action<GameObject> complete = null)
    {
        ResRequestInfo resRequest = ResRequestFactory.CreateRequest<GameObject>(resName, resType);
        Action<GameObject> action = delegate (GameObject gameObject)
        {
            GameObject instantisteGameobj = GameObjectHelper.Instantiate(gameObject);
            _internal_ref_increase(resRequest, instantisteGameobj);
            if (complete != null)
                complete.Invoke(instantisteGameobj);
        };
        _resLoader.LoadAssetAsync(resRequest, action);
    }

    #endregion

    #region bytes

    public string LoadText(ResRequestInfo resRequest)
    {
        TextAsset textAsset = _resLoader.LoadAsset<TextAsset>(resRequest);
        return textAsset.text;
    }

    public byte[] LoadByte(string resName, E_GameResType resType)
    {
        TextAsset textAsset = _resLoader.LoadAsset<TextAsset>(ResRequestFactory.CreateRequest<TextAsset>(resName, resType));
        return textAsset.bytes;
    }

    public byte[] LoadByte(ResRequestInfo resRequest)
    {
        TextAsset textAsset = _resLoader.LoadAsset<TextAsset>(resRequest);
        return textAsset.bytes;
    }

    #endregion

    #endregion

    #region public

    #endregion

    #region private

    public void _init()
    {
        _bgLoading = Resources.Load<Texture>("default/bg_loading");
        ResLog.Assert(_bgLoading != null, "找不到默认的图片信息");

        Resources.Load<Sprite>("default/default_sprite");
    }

    #endregion



}


