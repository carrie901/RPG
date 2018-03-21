using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    public abstract class ERectItem : ERect
    {
        private bool _enable = true;

        public bool Enabel
        {
            get { return _enable; }
            set { _enable = value; }
        }

        public ERectItem(float width, float height) : base(width, height)
        {
        }

        public override void _on_draw()
        {
            if (_enable)
            {
                Draw();
            }
        }

        public abstract void Draw();

    }

}
