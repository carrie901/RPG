using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// TODO 整体布局是混乱的,他们之间的继承关系混乱而且并行
    /// TODO 目前先优先速度来
    /// </summary>
    public class EScrollView : EComponent
    {
        public float height_interval = 10;      //高度间隔
        public float offset_x = 10;
        protected float _view_height = 10;      // 初始的offset_y
        private float _cur_height = 0;
        protected Vector2 scroll_position;
        protected Rect _view;

        public EScrollView(float width, float height) : base(width, height)
        {

        }

        public void SetHeightInterval(float height_i)
        {
            height_interval = height_i;
        }

        public override void _on_draw()
        {
            // 1.背景
            if (show_bg)
                EView.DrawTexture(_world_pos, EStyle.GetColorTexture(bg_color));

            if (show_box)
            {
                GUI.Box(_world_pos, "");
            }

            _view = new Rect(_world_pos.x, _world_pos.y, _world_pos.width, _view_height);

            scroll_position = EView.BeginScrollView(_world_pos, scroll_position, _view);
            int length = _childs.Count;
            for (int i = 0; i < length; i++)
            {
                _childs[i].OnDraw(_world_pos.x /*+ _pos.x - _size.x / 2*/, _world_pos.y /*+ _pos.y - _size.y / 2*/);
            }
            EView.EndScrollView();
        }

        public override void AddComponent(ERect rect)
        {
            Debug.LogError("垂直布局，不支持这种添加方式");
        }

        public override void AddComponent(ERect rect, E_Anchor anchor)
        {
            Debug.LogError("垂直布局，不支持这种添加方式");
        }

        public bool _change_color = false;
        public virtual void AddItem(ERect rect)
        {
            float x = rect.Ew / 2 + offset_x;
            _cur_height = (_view_height + rect.Eh / 2);
            rect.ResetPosition(x, _cur_height);
            _internal_add_chile(rect);
            _view_height = (_cur_height + rect.Eh / 2 + height_interval);
        }

        bool flag = false;
        public virtual void AddItem1(EComponent rect)
        {
            rect.SetBg(false);
            if (flag)
            {
                rect.SetBgStyle(EStyle.BoxStyle1);
            }
            else
            {
                rect.SetBgStyle(EStyle.BoxStyle2);
            }
               
            flag = !flag;
            AddItem(rect);
        }

        public void Clear()
        {
            _childs.Clear();
            _view_height = height_interval;
            _cur_height = 0;

        }
    }

}
