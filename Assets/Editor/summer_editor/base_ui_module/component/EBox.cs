using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    public class EBox : ERect
    {
        public string text;
        public EBox(float width, float height, string lab) : base(width, height)
        {
            text = lab;
        }

        public override void _on_draw()
        {
            EView.Box(_world_pos, text);
        }
    }

}