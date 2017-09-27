using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    public class EButton : ERect
    {
        public string text;
        public GUIStyle _gui_style;
        public bool _result = false;

        public EButton(float width, string lab) : base(width, 20)
        {
            text = lab;
        }

        public EButton(float width, float height, string lab) : base(width, height)
        {
            text = lab;
        }


        public delegate void OnButtonClick(EButton button);

        public event OnButtonClick on_click;
        public override void _on_draw()
        {
            _result = EView.Button(_world_pos, text, _gui_style);
            if (_result && on_click != null)
            {
                on_click(this);
            }
        }


    }

}
