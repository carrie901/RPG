using UnityEngine;
using UnityEngine.UI;
namespace Summer
{
    public class SpriteAtlasLoader : MonoBehaviour
    {
        public Image _img;
        public string _spriteTag;
        public string _resPath;
        public string _oldResPath;
        public bool _initComplete = false;
        void OnEnable()
        {
            _initComplete = true;
            SpriteAltasCachePool.Instance.LoadSprite(this);
        }

        void OnDisable()
        {
            ResLog.Assert(_initComplete, "初始化没有完成.GameObject Name:[{0}],Res Path:[{1}]", gameObject.name, _resPath);
            if (!_initComplete) return;
            SpriteAltasCachePool.Instance.UnLoadSprite(this);
        }

        /*public override string GetResPath()
        {
            return _resPath;
        }

        public override void SetResPath(string resPath)
        {
            _resPath = resPath;
        }*/
    }
}
