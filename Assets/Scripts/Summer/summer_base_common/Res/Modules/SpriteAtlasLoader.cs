using UnityEngine;
using UnityEngine.UI;
namespace Summer
{
    public class SpriteAtlasLoader : MonoBehaviour,I_PoolCacheRef
    {
        public Image _img;
        public string _spriteTag;
        public string _resPath;
        //public bool _isCommon = false;

        public bool _initComplete = false;

        void OnEnable()
        {
            _initComplete = true;
            //SpritePool.Instance.LoadSprite(this);
        }

        void OnDisable()
        {
            ResLog.Assert(_initComplete, "初始化没有完成.GameObject Name:[{0}],Res Path:[{1}]", gameObject.name, _resPath);
            if (!_initComplete) return;
            //SpritePool.Instance.ReaycelSprite(this);
        }

        public void ReaycelSprite()
        {
            _img.sprite = null;
        }

        public int GetRefCount()
        {
            throw new System.NotImplementedException();
        }
    }
}
