using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    public delegate void ToggleBarChange(EToggleBar togglebar);

    public class EToggleBar : ERectItem
    {
        public bool select;
        public bool _last_select;
        public string text;

        public event ToggleBarChange on_change;

        public EToggleBar( string lab, float width,float height=DEFAULT_HEIGHT) : base(width, height)
        {
            text = lab;
        }

        public override void Draw()
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
