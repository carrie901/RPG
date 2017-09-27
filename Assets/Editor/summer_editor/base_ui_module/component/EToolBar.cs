using UnityEngine;
using System.Collections.Generic;

namespace SummerEditor
{
    public delegate void OnToolBarSelect(EToolBar bar);

    public class EToolBar : ERect
    {
        public string[] _texts;
        public int _text_count = 0;
        public int _select_index = -1;
        public int _last_select = -1;

        public event OnToolBarSelect on_select;

        public EToolBar(float width, string[] texts) : base(width, 20)
        {
            _texts = texts;
            _text_count = _texts.Length;
        }

        public EToolBar(float width, float height, string[] texts) : base(width, height)
        {
            _texts = texts;
            _text_count = _texts.Length;
        }

        public override void _on_draw()
        {
            _select_index = EView.Toolbar(_world_pos, _select_index, _texts);
            if (_last_select != _select_index)
            {
                if (on_select != null)
                    on_select(this);
                _last_select = _select_index;
            }
        }
    }
}

