using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Summer;

namespace SummerEditor
{
    public enum E_ItemResType
    {
        img,
        text,
        raw_image
    }
    public abstract class ItemRes
    {
        public GameObject target_gameobject;           // 所属的GameObject
        public bool raycast_target;         // 碰撞
        public Material _material;
        public ButtonSound sound;
        public string ItemName { get { return _item_name; } }
        protected string _item_name;
        protected E_ItemResType _type;
        public virtual void SetTarget(GameObject go)
        {
            target_gameobject = go;
        }

        public virtual void Refresh()
        {

        }


        public virtual void PrintRaycastInfo()
        {
            if (raycast_target)
            {
                if (sound == null)
                    Debug.LogFormat("{0}: 有碰撞,但没有ButtonSound,不合法", target_gameobject.name);
                else
                    Debug.LogFormat("{0}: ButtonSound: View:[{1}], Key[{2}]", target_gameobject.name, sound.view, sound.key);
            }
        }

    }

    public class ItemImageRes : ItemRes
    {
        public Image img;
        private Sprite sprite;
        public override void SetTarget(GameObject go)
        {
            base.SetTarget(go);
            img = go.GetComponent<Image>();
            sprite = img.sprite;
            _type = E_ItemResType.img;
            _item_name = sprite.name;
        }
    }

    public class ItemRawImageRes : ItemRes
    {
        public RawImage raw_img;
        public override void SetTarget(GameObject go)
        {
            base.SetTarget(go);
            raw_img = go.GetComponent<RawImage>();
            _type = E_ItemResType.raw_image;
            _item_name = raw_img.name;
        }
    }

    public class ItemTextRes : ItemRes
    {
        public Text text;
        public Font _font;
        public LanguageLoc _loc;
        public override void SetTarget(GameObject go)
        {
            base.SetTarget(go);
            text = go.GetComponent<Text>();
            _font = text.font;
            _type = E_ItemResType.text;
            _item_name = _font.name;
            _loc = go.GetComponent<LanguageLoc>();
        }

        public override void Refresh()
        {
            text = target_gameobject.GetComponent<Text>();
            _font = text.font;
            _item_name = _font.name;
            raycast_target = text.raycastTarget;
            if (raycast_target)
                sound = target_gameobject.GetComponent<ButtonSound>();
        }

        public void SetLanguage(bool value)
        {
            if (_loc != null)
                _loc.SetLanguage(value);
        }

        public void SaveLanguageLoc()
        {

        }
    }
}

