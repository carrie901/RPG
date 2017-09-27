using UnityEngine;

namespace SummerEditor
{
    public abstract class ERect : I_Draw
    {
        protected Vector2 _size = new Vector2(0, 0);            //大小
        protected Vector2 _pos = new Vector2(0, 0);             //相对上层坐标
        protected Rect _world_pos = new Rect(0, 0, 50, 30);         //世界坐标
        public string des = "";
        public float Ex { get { return _pos.x; } }
        public float Ey { get { return _pos.y; } }
        public float Ew { get { return _size.x; } }
        public float Eh { get { return _size.y; } }
        public ERect(float width, float height)
        {
            _size.x = width;
            _size.y = height;
        }

        //依靠在item的右边
        public void SetPositionRight(ERect item, float width = 10)
        {
            _pos.x = item.Ex + item.Ew / 2 + _size.x / 2 + width;
            _pos.y = item.Ey;
        }
        public void SetPositionLeft(ERect item, float width = 10)
        {
            _pos.x = item.Ex - item.Ew / 2 - _size.x / 2 - width;
            _pos.y = item.Ey;
        }
        public void SetPositionDown(ERect item, float height = 10)
        {
            _pos.x = item.Ex;
            _pos.y = item.Ey + item.Eh / 2 + _size.y / 2 + height;
        }
        public void ResetPosition(float pos_x, float pos_y)
        {
            _pos.x = pos_x;
            _pos.y = pos_y;
        }

        public Vector2 Position { get { return _pos; } }
        public Vector2 Size { get { return _size; } }
        public void OnDraw(float parent_x, float parent_y)
        {
            _world_pos = new Rect(parent_x + _pos.x - _size.x / 2, parent_y + _pos.y - _size.y / 2, _size.x, _size.y);
            _on_draw();
        }

        public abstract void _on_draw();

    }

}
