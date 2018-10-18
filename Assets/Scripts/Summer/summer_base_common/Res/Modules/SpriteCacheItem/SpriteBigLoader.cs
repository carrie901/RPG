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

        #endregion

        #region MONO Override

        void OnEnable()
        {
            _isComplete = true;
            SpriteBigCachePool.Instance.LoadSprite(this);
        }

        void OnDisable()
        {
            if (!_isComplete) return;
            SpriteBigCachePool.Instance.UnLoadSprite(this);
            _isComplete = false;
        }

        #endregion

        #region public 

        public int GetRefCount()
        {
            int refCount = ResLoader.Instance.GetRefCount(_resPath);
            return refCount;
        }

        #endregion
    }
}
