using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    public class EInput : ERectItem
    {
        public string text;
        public bool CanEditor { get; set; }
        public EInput(string text, float width, float height = DEFAULT_HEIGHT) : base(width, height)
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
