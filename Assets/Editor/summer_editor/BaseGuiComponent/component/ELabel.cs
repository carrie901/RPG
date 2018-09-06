using UnityEngine;

namespace SummerEditor
{
    public class ELabel : ERectItem
    {
        public string text;

        public ELabel(string lab, float width, float height = DEFAULT_HEIGHT) : base(width, height)
        {
            text = lab;
        }

        public override void Draw()
        {

            EView.Label(_world_pos, text);
        }


    }
}
