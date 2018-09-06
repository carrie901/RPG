using UnityEngine;
using System.Collections.Generic;

namespace SummerEditor
{
    public delegate void OnToolBarSelect(EToolBar bar);

    public class EToolBar : ERectItem
    {
        public string[] _texts;
        public int _text_count = 0;
        public int _select_index = -1;
        public int _last_select = -1;

        public int SelectIndex
        {
            get { return _select_index; }
            set
            {
                _select_index = value;
                _callback();
            }
        }

        public event OnToolBarSelect on_select;

        public EToolBar(string[] texts, float width, float height=DEFAULT_HEIGHT) : base(width, height)
        {
            _texts = texts;
            _text_count = _texts.Length;
        }

        public EToolBar(float width, float height, string[] texts) : base(width, height)
        {
            _texts = texts;
            _text_count = _texts.Length;
        }

        public override void Draw()
        {
            _select_index = EView.Toolbar(_world_pos, _select_index, _texts);
            _callback();
        }

        public void _callback()
        {
            if (_last_select != _select_index)
            {
                if (on_select != null)
                    on_select(this);
                _last_select = _select_index;
            }
        }
    }
}

