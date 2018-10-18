using UnityEngine;
using UnityEngine.UI;
namespace Summer
{
    public class SpriteAtlasLoader : RefCounter, I_PoolCacheRef
    {
        public Image _img;
        public string _spriteTag;
        public string _resPath;
        public bool _initComplete = false;

        public override void Load()
        {
            _initComplete = true;
            SpriteAltasCachePool.Instance.LoadSprite(this);
        }

        public override void UnLoad()
        {
            ResLog.Assert(_initComplete, "初始化没有完成.GameObject Name:[{0}],Res Path:[{1}]", gameObject.name, _resPath);
            if (!_initComplete) return;
            SpriteAltasCachePool.Instance.UnLoadSprite(this);
        }

        public int GetRefCount()
        {
            throw new System.NotImplementedException();
        }
    }
}
