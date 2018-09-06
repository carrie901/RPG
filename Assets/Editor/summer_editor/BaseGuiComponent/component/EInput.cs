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

    public class EIntInput : ERectItem
    {
        public int _value;
        public bool CanEditor { get; set; }
        public EIntInput(int default_value, float width, float height = DEFAULT_HEIGHT) : base(width, height)
        {
            _value = default_value;
            CanEditor = true;
        }

        public override void Draw()
        {
            if (CanEditor)
            {
                _value = EView.IntInput(_world_pos, _value);
            }
            else
            {
                EView.IntInput(_world_pos, _value);
            }
        }

        public int GetValue() { return _value; }
    }



}
