using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Summer
{
    public class SpriteSelectableAutoLoader : MonoBehaviour
    {
        public Selectable _selectable;
        private SpriteState new_sprite = new SpriteState();
        public string disabled_sprite_path;
        public string disabled_sprite_tag;

        public string highlighted_sprite_path;
        public string highlighted_sprite_tag;

        public string pressed_sprite_path;
        public string pressed_sprite_tag;

        void OnEnable()
        {
            if (!string.IsNullOrEmpty(disabled_sprite_path))
            {
                Sprite sprite = SpritePool.Instance.LoadSprite(disabled_sprite_path);
                new_sprite.disabledSprite = sprite;
            }

            if (!string.IsNullOrEmpty(highlighted_sprite_path))
            {
                Sprite sprite = SpritePool.Instance.LoadSprite(highlighted_sprite_path);
                new_sprite.highlightedSprite = sprite;
            }

            if (!string.IsNullOrEmpty(pressed_sprite_path))
            {
                Sprite sprite = SpritePool.Instance.LoadSprite(pressed_sprite_path);
                new_sprite.highlightedSprite = sprite;
            }
            _selectable.spriteState = new_sprite;
            
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
