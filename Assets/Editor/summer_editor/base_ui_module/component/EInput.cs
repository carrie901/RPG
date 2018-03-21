using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    public class EInput : ERectItem
    {
        public string text;
        public bool CanEditor { get; set; }
        public EInput(float width, float height, string text) : base(width, height)
        {
            this.text = text;
            CanEditor = true;
        }

        public override void Draw()
        {
            if (CanEditor)
            {
                text = EView.Input(_world_pos, text);
            }
            else
            {
                EView.Input(_world_pos, text);
            }

        }
    }
}
