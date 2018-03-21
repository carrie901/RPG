using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    public class EBox : ERectItem
    {
        public string text;
        public EBox(float width, float height, string lab) : base(width, height)
        {
            text = lab;
        }

        public override void Draw()
        {
            EView.Box(_world_pos, text);
        }
    }

}