using UnityEngine;

namespace SummerEditor
{
    public class ELabel : ERectItem
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

        public override void Draw()
        {
            EView.Label(_world_pos, text);
        }
    }
}
