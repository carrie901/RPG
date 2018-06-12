using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Summer
{
    /// <summary>
    /// 记录的是当前的图片
    /// </summary>
    public class SpriteBigAutoLoader : MonoBehaviour, I_PoolCacheRef
    {
        #region 属性

        public Image img;
        public string sprite_tag;
        public string res_path;

        public bool is_complete = false;

        public RefCounter _ref_count;

        #endregion

        #region MONO Override

        void OnEnable()
        {
            is_complete = true;
            SpritePool.Instance.LoadSprite(this);
        }

        void OnDisable()
        {
            if (!is_complete) return;
            SpritePool.Instance.ReaycelSprite(this);
            is_complete = false;
        }



        public bool flag = false;
        private void Update()
        {
            if (!flag) return;
            flag = false;

            is_complete = true;
            SpritePool.Instance.LoadSprite(this);
        }

        #endregion

        #region public 

        public void ReaycelSprite()
        {
            RefCounter rc = FindRefCount();
            if (rc != null)
                rc.RemoveRef();
            img.sprite = null;
        }

        public void SetSprite(Sprite sprite)
        {
            img.sprite = sprite;
        }

        public int GetRefCount()
        {
            int ref_count = ResLoader.instance.GetRefCount(res_path);
            return ref_count;
        }

        #endregion

        #region private

        public RefCounter FindRefCount()
        {
            if (_ref_count == null)
            {
                _ref_count = gameObject.GetComponent<RefCounter>();
            }
            return _ref_count;
        }

        #endregion
    }
}
