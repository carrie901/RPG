using UnityEngine;
using UnityEngine.UI;

namespace Summer
{
    /// <summary>
    /// 记录的是当前的图片
    /// </summary>
    public class SpriteBigLoader : MonoBehaviour, I_PoolCacheRef
    {
        #region 属性

        public Image _img;
        public string _resPath;

        public bool _isComplete = false;

        public RefCounter _refCount;

        #endregion

        #region MONO Override

        void OnEnable()
        {
            _isComplete = true;
            //SpritePool.Instance.LoadSprite(this);
        }

        void OnDisable()
        {
            if (!_isComplete) return;
            //SpritePool.Instance.ReaycelSprite(this);
            _isComplete = false;
        }



        public bool _flag = false;
        private void Update()
        {
            if (!_flag) return;
            _flag = false;

            _isComplete = true;
            //SpritePool.Instance.LoadSprite(this);
        }

        #endregion

        #region public 

        public void ReaycelSprite()
        {
            RefCounter rc = FindRefCount();
            if (rc != null)
                rc.RemoveRef();
            _img.sprite = null;
        }

        public void SetSprite(Sprite sprite)
        {
            _img.sprite = sprite;
        }

        public int GetRefCount()
        {
            int refCount = ResLoader.Instance.GetRefCount(_resPath);
            return refCount;
        }

        #endregion

        #region private

        public RefCounter FindRefCount()
        {
            if (_refCount == null)
            {
                _refCount = gameObject.GetComponent<RefCounter>();
            }
            return _refCount;
        }

        #endregion
    }
}
