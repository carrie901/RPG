using UnityEngine;

namespace SummerEditor
{
    public class ELabel : ERect
    {
        public string text;
        public ELabel(float width, string lab) : base(width, 20)
        {
            text = lab;
        }
        public ELabel(float width, float height, string lab) : base(width, height)
        {
            text = lab;
        }

        public override void _on_draw()
        {
            EView.Label(_world_pos, text);
        }
    }
}
