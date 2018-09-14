using UnityEngine;
using UnityEngine.UI;

namespace Summer
{
    public class SpriteSelectableAutoLoader : MonoBehaviour
    {
        public Selectable _selectable;
        private SpriteState _newSprite = new SpriteState();
        public string _disabledSpritePath;
        public string _disabledSpriteTag;

        public string _highlightedSpritePath;
        public string _highlightedSpriteTag;

        public string _pressedSpritePath;
        public string _pressedSpriteTag;

        void OnEnable()
        {
            if (!string.IsNullOrEmpty(_disabledSpritePath))
            {
                Sprite sprite = SpritePool.Instance.LoadSprite(_disabledSpritePath);
                _newSprite.disabledSprite = sprite;
            }

            if (!string.IsNullOrEmpty(_highlightedSpritePath))
            {
                Sprite sprite = SpritePool.Instance.LoadSprite(_highlightedSpritePath);
                _newSprite.highlightedSprite = sprite;
            }

            if (!string.IsNullOrEmpty(_pressedSpritePath))
            {
                Sprite sprite = SpritePool.Instance.LoadSprite(_pressedSpritePath);
                _newSprite.highlightedSprite = sprite;
            }
            _selectable.spriteState = _newSprite;
            
        }

        void OnDisable()
        {
            SpritePool.Instance.ReaycelSprite(this);
        }

        public void ReaycelSprite()
        {
            //_selectable.spriteState = new SpriteState();
            //_selectable.spriteState = new SpriteState();
            //_selectable.spriteState.disabledSprite = null;
            //_selectable.spriteState.disabledSprite = default_sprite;
            LogManager.Error("SpriteSelectableAutoLoader: 搞不定呀搞不定");
        }
    }
}
