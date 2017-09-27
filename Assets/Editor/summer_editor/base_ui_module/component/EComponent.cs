using UnityEngine;
using System.Collections.Generic;

namespace SummerEditor
{

    public enum E_Anchor
    {
        none,
        left,
        right,
        up,
        down,
        left_up,
        left_down,
        right_up,
        right_down,
        center,
    }

    public class EComponent : ERect
    {
        public bool show_bg = true;
        protected List<ERect> _childs = new List<ERect>();
        protected Color bg_color = new Color32(128, 128, 128, 128);

        public EComponent(float width, float height) : base(width, height)
        {
        }
        public override void _on_draw()
        {

            // 1.背景
            if (show_bg)
                EView.DrawTexture(_world_pos, EStyle.GetColorTexture(bg_color));


            // TODO GUILayout.BeginArea(_position); //导致奔溃掉
            // 所以启用了另外一套方式
            int length = _childs.Count;
            for (int i = 0; i < length; i++)
            {
                _childs[i].OnDraw(_world_pos.x, _world_pos.y);
            }

        }

        //TODO 目前这块的添加是完全混轮的，后期需要重点设计
        //以左上角为锚点，添加到组件中
        public virtual void AddComponent(ERect rect, float pos_x, float pos_y)
        {
            pos_x = pos_x + rect.Ew / 2;
            pos_y = pos_y + rect.Eh / 2;
            rect.ResetPosition(pos_x, pos_y);
            _childs.Add(rect);
        }

        //添加
        public virtual void AddComponent(ERect rect)
        {
            AddComponent(rect, E_Anchor.left_up);
        }
        //添加 带锚点
        public virtual void AddComponent(ERect rect, E_Anchor anchor)
        {
            float pos_x = rect.Ex;
            float pos_y = rect.Ey;
            if (anchor == E_Anchor.left || anchor == E_Anchor.left_down || anchor == E_Anchor.left_up)
            {
                //pos_x =  pos_x;
            }
            else if (anchor == E_Anchor.right || anchor == E_Anchor.right_down || anchor == E_Anchor.right_up)
            {
                pos_x = _size.x - pos_x;
            }
            else if (anchor == E_Anchor.center)
            {
                pos_x = _size.x / 2 + pos_x;
            }

            if (anchor == E_Anchor.center)
            {
                pos_y = _size.y / 2 + pos_y;
            }
            else if (anchor == E_Anchor.up || anchor == E_Anchor.left_up || anchor == E_Anchor.right_up)
            {

            }
            else if (anchor == E_Anchor.down || anchor == E_Anchor.right_down || anchor == E_Anchor.right_up)
            {
                pos_y = _size.y - pos_y;
            }

            rect.ResetPosition(pos_x, pos_y);
            _childs.Add(rect);
        }
        //设置背景
        public void SetBg(int color_r, int color_g, int color_b, int color_a)
        {
            bg_color = new Color(color_r, color_g, color_b, color_a);
        }
    }


}

