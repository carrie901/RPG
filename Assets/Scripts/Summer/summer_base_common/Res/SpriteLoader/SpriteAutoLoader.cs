using UnityEngine;
using UnityEngine.UI;
namespace Summer
{
    public class SpriteAutoLoader : MonoBehaviour
    {
        public Image img;
        public string _spriteTag;
        public string _resPath;
        public bool _isCommon = false;

        public bool _initComplete = false;

        void OnEnable()
        {
            _initComplete = true;
            SpritePool.Instance.LoadSprite(this);
        }

        void OnDisable()
        {
            ResLog.Assert(_initComplete, "初始化没有完成.GameObject Name:[{0}],Res Path:[{1}]", gameObject.name, _resPath);
            if (!_initComplete) return;
            SpritePool.Instance.ReaycelSprite(this);
        }

        public void ReaycelSprite()
        {
            img.sprite = null;
        }
    }
}
