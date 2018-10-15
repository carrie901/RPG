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
        public TimeInterval _timeInterval = new TimeInterval(60f);
        public static string _residentTag = "resident";                                                 // 常驻Tag            
        public PoolCache<string, SpriteInfo> _uiPoolCache
            = new PoolCache<string, SpriteInfo>();                                                      // ui
        public Dictionary<string, SpriteInfo> _residentSprite
            = new Dictionary<string, SpriteInfo>();                                                     // 常驻内存
        public PoolCache<string, SpriteBigAutoLoader> _bigSpritePoolCache
            = new PoolCache<string, SpriteBigAutoLoader>();                                             // 大图

        public ResLoader _resLoader;

        #endregion

        public SpritePool()
        {
            _resLoader = ResLoader.instance;

            _bigSpritePoolCache.OnRemoveValueEvent += OnBigSprite;
        }

        private void OnBigSprite(string key)
        {
            //ResLoader.instance.UnLoadRes();
        }

        #region 提供对外的加载方式 是否可以合并相关的接口

        public void LoadSprite(Image img, string resPath)
        {
           
            Sprite sprite = LoadSprite(resPath);
            if (sprite)
            {
                ResManager.instance._internal_ref_decrease(img.gameObject);
                img.sprite = sprite;
                ResManager.instance._internal_ref_increase(resPath, img.gameObject);
            }
        }

        #region ui

        public void LoadSprite(SpriteAutoLoader autoSprite)
        {
            Sprite sprite = LoadSprite(autoSprite._resPath);
            if (sprite != null)
            {
                ResManager.instance._internal_ref_decrease(autoSprite.gameObject);
                autoSprite.img.sprite = sprite;
                ResManager.instance._internal_ref_increase(autoSprite._resPath, autoSprite.gameObject);
            }
            else
            {
                autoSprite.img.sprite = ResManager.instance._defaultSprite;
            }
        }

        public void ReaycelSprite(SpriteAutoLoader sprite)
        {
            sprite.ReaycelSprite();
        }

        #endregion

        #region Selectable

        public void LoadSprite(SpriteSelectableAutoLoader autoSprite)
        {

        }

        public void ReaycelSprite(SpriteSelectableAutoLoader selectable)
        {
            selectable.ReaycelSprite();
        }

        #endregion

        #region 大图

        public void LoadSprite(SpriteBigAutoLoader autoSprite)
        {
            if (!autoSprite.gameObject.activeSelf) return;

            Sprite sprite = LoadSprite(autoSprite._resPath);
            if (sprite != null)
            {
                ResManager.instance._internal_ref_decrease(autoSprite.gameObject);
                autoSprite.SetSprite(sprite);
                ResManager.instance._internal_ref_increase(autoSprite._resPath, autoSprite.gameObject);
                _bigSpritePoolCache.Set(autoSprite._resPath, autoSprite);
            }
            else
            {
                autoSprite.SetSprite(ResManager.instance._defaultSprite);
            }
        }

        public void ReaycelSprite(SpriteBigAutoLoader bigSprite)
        {
            bigSprite.ReaycelSprite();
        }

        #endregion 

        #endregion


        #region override Load Sprite

        public Sprite LoadSprite(string resPath)
        {
            Sprite sprite = _resLoader.LoadAsset<Sprite>(resPath);
            return sprite;
        }

        public void LoadSpriteAsync(Image img, string resPath, Action<Sprite> complete = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void OnUpdate(float dt)
        {
            if (_timeInterval.OnUpdate())
            {
                _bigSpritePoolCache.SetDefaultCapacity();
            }
        }
    }

    public class SpriteInfo : I_PoolCacheRef
    {
        public string _tag;
        public Dictionary<string, GameObject> _spriteMap = new Dictionary<string, GameObject>();


        public int GetRefCount()
        {
            return 0;
        }
    }

}
