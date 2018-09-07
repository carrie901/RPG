using UnityEngine;

namespace SummerEditor
{
    public class EButton : ERectItem
    {
        public string _text;
        public GUIStyle _guiStyle;
        public bool _result;

        public EButton(string lab, float width, float height = DEFAULT_HEIGHT) : base(width, height)
        {
            _text = lab;
        }

        public delegate void OnButtonClick(EButton button);

        public event OnButtonClick OnClick;
        public override void Draw()
        {
            _result = EView.Button(_world_pos, _text, _guiStyle);
            if (_result && OnClick != null)
            {
                OnClick(this);
            }
        }


    }

}
