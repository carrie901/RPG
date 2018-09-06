using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    public class EBox : ERectItem
    {
        public string text;
        public EBox(string lab, float width, float height = DEFAULT_HEIGHT) : base(width, height)
        {
            text = lab;
        }

        public override void Draw()
        {
            EView.Box(_world_pos, text);
        }
    }

}