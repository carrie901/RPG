using UnityEngine;
using UnityEngine.UI;
namespace Summer
{
    public class SpriteAutoLoader : MonoBehaviour
    {
        public Image img;
        public string sprite_tag;
        public string res_path;
        public bool is_common = false;

        public bool _init_complete = false;

        void OnEnable()
        {
            _init_complete = true;
            SpritePool.Instance.LoadSprite(this);
        }

        void OnDisable()
        {
            ResLog.Assert(_init_complete, "初始化没有完成.GameObject Name:[{0}],Res Path:[{1}]", gameObject.name, res_path);
            if (!_init_complete) return;
            SpritePool.Instance.ReaycelSprite(this);
        }

        public void ReaycelSprite()
        {
            img.sprite = null;
        }
    }
}
