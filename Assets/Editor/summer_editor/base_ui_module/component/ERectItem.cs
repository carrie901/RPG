using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    public abstract class ERectItem : ERect
    {
        private bool _enable = true;
        protected bool _show_bg;
        protected Color _bg_color = new Color32(128, 128, 128, 255);
        public bool Enabel
        {
            get { return _enable; }
            set { _enable = value; }
        }

        public ERectItem(float width, float height) : base(width, height)
        {
        }

        public void SetBg(bool show)
        {
            _show_bg = show;
        }
        public void SetColor(Color color)
        {
            _bg_color = color;
        }

        public override void _on_draw()
        {
            if (_enable)
            {
                if (_show_bg)
                    EView.DrawTexture(_world_pos, EStyle.GetColorTexture(_bg_color));
                Draw();
            }
        }

        public abstract void Draw();

    }

}
