using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Summer
{
    /// <summary>
    ///  Sprite缓存池 
    /// 想做一个区分
    /// 1.针对UI这块的加载
    ///     1.1UI图集的加载
    ///     1.2UI中大图的加载
    /// 2.针对List中的Image的变更，Sprite的变更
    /// </summary>
    public class SpritePool : I_SpriteLoad, I_Update
    {
        #region 属性

        public static SpritePool Instance = new SpritePool();
        public TimeInterval time_interval = new TimeInterval(60f);
        public static string resident_tag = "resident";                                                 // 常驻Tag            
        public PoolCache<string, SpriteInfo> _ui_pool_cache
            = new PoolCache<string, SpriteInfo>();                                                      // ui
        public Dictionary<string, SpriteInfo> _resident_sprite
            = new Dictionary<string, SpriteInfo>();                                                     // 常驻内存
        public PoolCache<string, SpriteBigAutoLoader> _big_sprite_pool_cache
            = new PoolCache<string, SpriteBigAutoLoader>();                                             // 大图

        public ResLoader _res_loader;

        #endregion

        public SpritePool()
        {
            _res_loader = ResLoader.instance;

            _big_sprite_pool_cache.OnRemoveValueEvent += OnBigSprite;
        }

        private void OnBigSprite(string key)
        {
            ResRequestInfo request = ResRequestFactory.CreateRequest<Sprite>(key);
            ResLoader.instance.UnloadRes(request);
        }

        #region 提供对外的加载方式 是否可以合并相关的接口

        public void LoadSprite(Image img, string res_path)
        {
            ResRequestInfo request = ResRequestFactory.CreateRequest<Sprite>(res_path);
            Sprite sprite = LoadSprite(request);
            if (sprite)
            {
                ResManager.instance._internal_ref_decrease(img.gameObject);
                img.sprite = sprite;
                ResManager.instance._internal_ref_increase(request, img.gameObject);
            }
        }

        #region ui

        public void LoadSprite(SpriteAutoLoader auto_sprite)
        {
            ResRequestInfo request = ResRequestFactory.CreateRequest<Sprite>(auto_sprite.res_path);

            Sprite sprite = LoadSprite(request);
            if (sprite != null)
            {
                ResManager.instance._internal_ref_decrease(auto_sprite.gameObject);
                auto_sprite.img.sprite = sprite;
                ResManager.instance._internal_ref_increase(request, auto_sprite.gameObject);
            }
            else
            {
                auto_sprite.img.sprite = ResManager.instance._default_sprite;
            }
        }

        public void ReaycelSprite(SpriteAutoLoader sprite)
        {
            sprite.ReaycelSprite();
        }

        #endregion

        #region Selectable

        public void LoadSprite(SpriteSelectableAutoLoader auto_sprite)
        {

        }

        public void ReaycelSprite(SpriteSelectableAutoLoader selectable)
        {
            selectable.ReaycelSprite();
        }

        #endregion

        #region 大图

        public void LoadSprite(SpriteBigAutoLoader auto_sprite)
        {
            if (!auto_sprite.gameObject.activeSelf) return;

            ResRequestInfo request = ResRequestFactory.CreateRequest<Sprite>(auto_sprite.res_path);
            Sprite sprite = LoadSprite(request);
            if (sprite != null)
            {
                ResManager.instance._internal_ref_decrease(auto_sprite.gameObject);
                auto_sprite.SetSprite(sprite);
                ResManager.instance._internal_ref_increase(request, auto_sprite.gameObject);
                _big_sprite_pool_cache.Set(auto_sprite.res_path, auto_sprite);
            }
            else
            {
                auto_sprite.SetSprite(ResManager.instance._default_sprite);
            }
        }

        public void ReaycelSprite(SpriteBigAutoLoader big_sprite)
        {
            big_sprite.ReaycelSprite();
        }

        #endregion 

        #endregion

        public Sprite LoadSprite(string sprite_path)
        {
            ResRequestInfo request = ResRequestFactory.CreateRequest<Sprite>(sprite_path);
            Sprite sprite = LoadSprite(request);
            return sprite;
        }

        #region override Load Sprite

        public Sprite LoadSprite(ResRequestInfo res_request)
        {
            Sprite sprite = _res_loader.LoadAsset<Sprite>(res_request);
            return sprite;
        }

        public void LoadSpriteAsync(Image img, ResRequestInfo res_request, Action<Sprite> complete = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void OnUpdate(float dt)
        {
            if (time_interval.OnUpdate())
            {
                _big_sprite_pool_cache.SetDefaultCapacity();
            }
        }
    }

    public class SpriteInfo : I_PoolCacheRef
    {
        public string tag;
        public Dictionary<string, GameObject> _sprite_map = new Dictionary<string, GameObject>();


        public int GetRefCount()
        {
            return 0;
        }
    }

}
