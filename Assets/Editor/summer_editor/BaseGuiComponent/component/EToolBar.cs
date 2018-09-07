using UnityEngine;
using System.Collections.Generic;

namespace SummerEditor
{
    public delegate void OnToolBarSelect(EToolBar bar);

    public class EToolBar : ERectItem
    {
        public string[] _texts;
        public int _textCount = 0;
        public int _selectIndex = -1;
        public int _lastSelect = -1;

        public int SelectIndex
        {
            get { return _selectIndex; }
            set
            {
                _selectIndex = value;
                _callback();
            }
        }

        public event OnToolBarSelect OnSelect;

        public EToolBar(string[] texts, float width, float height=DEFAULT_HEIGHT) : base(width, height)
        {
            _texts = texts;
            _textCount = _texts.Length;
        }

        public EToolBar(float width, float height, string[] texts) : base(width, height)
        {
            _texts = texts;
            _textCount = _texts.Length;
        }

        public override void Draw()
        {
            _selectIndex = EView.Toolbar(_world_pos, _selectIndex, _texts);
            _callback();
        }

        public void _callback()
        {
            if (_lastSelect != _selectIndex)
            {
                if (OnSelect != null)
                    OnSelect(this);
                _lastSelect = _selectIndex;
            }
        }
    }
}

