using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    /// <summary>
    /// TODO 整体布局是混乱的,他们之间的继承关系混乱而且并行
    /// TODO 目前先优先速度来
    /// </summary>
     /*public class EVecticalScrollView : EScrollView
    {
       public float height_interval = 15;      //高度间隔
        private float _cur_height = 0;
        public EVecticalScrollView(float width, float height) : base(width, height)
        {
        }

        public override void AddComponent(ERect rect)
        {
            Debug.LogError("垂直布局，不支持这种添加方式");
        }

        public override void AddComponent(ERect rect, E_Anchor anchor)
        {
            Debug.LogError("垂直布局，不支持这种添加方式");
        }

        public virtual void AddItem(ERect rect)
        {
            float x = _size.x / 2;
            _cur_height += _cur_height + rect.Eh / 2 + height_interval;
            rect.ResetPosition(x, _cur_height);
            _childs.Add(rect);
        }
    }*/
}


