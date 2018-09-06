using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    public class ETextArea : ERectItem
    {
        public string _text = string.Empty;
        public ETextArea(float width, float height) : base(width, height)
        {
        }

        public override void Draw()
        {
            _text = EView.TextArea(_world_pos, _text);
        }

        public void SetInfo(string text)
        {
            _text = text;
        }
    }

}
