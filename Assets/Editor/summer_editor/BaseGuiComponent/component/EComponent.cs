using System;
using UnityEngine;
using System.Collections.Generic;

namespace SummerEditor
{
    [Flags]
    public enum E_Anchor
    {
        None = 0,
        Left = 0x01,
        Right = 0x02,
        UP = 0x04,
        DOWN = 0x08,
        //center = 0x16,
        DOWN_CENTER = 0x32
    }

    public class EComponent : ERect
    {
        #region 属性

        protected bool show_bg = false;
        protected List<ERect> _childs = new List<ERect>();
        protected Color bg_color = new Color32(128, 128, 128, 255);
        public bool show_box = true;
        public GUIStyle _box_style;
        #endregion

        #region Override

        public EComponent(float width, float height) : base(width, height)
        {
        }

        public override void _on_draw()
        {

            // 1.背景
            if (show_bg)
                EView.DrawTexture(_world_pos, EStyle.GetColorTexture(bg_color));

            if (show_box)
            {
                if (_box_style != null)
                    GUI.Box(_world_pos, "", _box_style);
                else
                {
                    GUI.Box(_world_pos, "");
                }
            }

            // TODO GUILayout.BeginArea(_position); //导致奔溃掉
            // 所以启用了另外一套方式
            int length = _childs.Count;
            for (int i = 0; i < length; i++)
            {
                _childs[i].OnDraw(_world_pos.x, _world_pos.y);
            }

        }

        #endregion

        #region AddComponent 

        //TODO 目前这块的添加是完全混轮的，后期需要重点设计
        //以左上角为锚点，添加到组件中
        public virtual void AddComponent(ERect rect, float pos_x, float pos_y)
        {
            pos_x = pos_x + rect.Ew / 2;
            pos_y = pos_y + rect.Eh / 2;
            rect.ResetPosition(pos_x, pos_y);
            _internal_add_chile(rect);
        }

        //添加
        public virtual void AddComponent(ERect rect)
        {
            E_Anchor a = E_Anchor.Left | E_Anchor.UP;
            AddComponent(rect, a);
        }
        //添加 带锚点
        public virtual void AddComponent(ERect rect, E_Anchor anchor)
        {
            // TODO bug 没有fixed
            float pos_x = rect.Ex;
            float pos_y = rect.Ey;
            E_Anchor e = (anchor & E_Anchor.Left);
            if ((anchor & E_Anchor.Left) == E_Anchor.Left)
            {
                pos_x = rect.Ew / 2;
            }
            else if ((anchor & E_Anchor.Right) == E_Anchor.Right)
            {
                pos_x = _size.x - pos_x - rect.Ew / 2;
            }
            /*else if ((anchor & E_Anchor.center) == E_Anchor.center)
            {
                pos_x = _size.x / 2 + pos_x;
            }*/

            /* if ((anchor & E_Anchor.center) == E_Anchor.center)
             {
                 pos_y = _size.y / 2 + pos_y;
             }
             else */
            if ((anchor & E_Anchor.UP) == E_Anchor.UP)
            {
                pos_y = pos_y + rect.Eh / 2;
            }
            else if ((anchor & E_Anchor.DOWN) == E_Anchor.DOWN)
            {
                pos_y = _size.y + pos_y - rect.Eh / 2;
            }

            if ((anchor & E_Anchor.DOWN_CENTER) == E_Anchor.DOWN_CENTER)
            {
                pos_x = (_size.x / 2) - (rect.Ew / 2);
                pos_y = _size.y + pos_y - rect.Eh / 2;
            }

            rect.ResetPosition(pos_x, pos_y);
            _internal_add_chile(rect);
        }

        // rect 在rect_a的右边
        public virtual ERect AddComponentRight(ERect rect, ERect rect_a, float r_width = 5)
        {

            float pos_x = rect_a.Ex + rect_a.Ew / 2 + r_width + rect.Ew / 2;
            //float pos_y = rect_a.Ey + (rect_a.Eh - rect.Eh) / 2;
            float pos_y = rect_a.Ey - rect_a.Eh / 2 + rect.Eh / 2;
            rect.ResetPosition(pos_x, pos_y);
            _internal_add_chile(rect);
            return rect;
        }

        public ERect AddComponentDown(ERect rect, ERect rect_a, float r_heigth = 5)
        {
            float pos_x = rect_a.Ex - rect_a.Ew / 2 + rect.Ew / 2;
            float pos_y = rect_a.Ey + rect_a.Eh / 2 + r_heigth + rect.Eh / 2;
            rect.ResetPosition(pos_x, pos_y);
            _internal_add_chile(rect);
            return rect;
        }

        #endregion

        #region SetColor

        //设置背景
        public void SetBg(byte color_r, byte color_g, byte color_b)
        {
            bg_color = new Color32(color_r, color_g, color_b, 255);
        }

        public void SetBg(bool show)
        {
            show_bg = show;
        }
        public void SetColor(Color color)
        {
            bg_color = color;
        }

        public void SetBgStyle(GUIStyle style)
        {
            _box_style = style;
        }

        #endregion

        public List<ERect> GetChilds()
        {
            return _childs;
        }

        #region privat

        public void _internal_add_chile(ERect item)
        {
            item.parent = this;
            _childs.Add(item);
        }

        #endregion
    }


}

