using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    public delegate void ToggleBarChange(EToggleBar togglebar);

    public class EToggleBar : ERect
    {
        public bool select;
        public bool _last_select;
        public string text;

        public event ToggleBarChange on_change;

        public EToggleBar(float width, string lab) : base(width, 20)
        {
            text = lab;
        }
        public EToggleBar(float width, float height, string lab) : base(width, height)
        {
            text = lab;
        }

        public override void _on_draw()
        {
            select = EView.Toggle(_world_pos, select, text);
            if (_last_select == select)
            {

            }
            else
            {
                _last_select = select;
                if (on_change != null)
                    on_change(this);
            }
        }
    }

}
