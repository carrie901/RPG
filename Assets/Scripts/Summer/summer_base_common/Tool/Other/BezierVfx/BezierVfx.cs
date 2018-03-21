using UnityEngine;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2018-2-27 14:47:20
// FileName : BezierVFX.cs
//=============================================================================

namespace Summer
{
    public class BezierVfx : MonoBehaviour
    {
        public Vector3 start_position;
        public Vector3 end_position;
        public Vector3 anchor_point;
        protected float _duration;
        protected float _survival_time = 0.5f;//结束之后保留的时间  
        protected float _time;
        protected bool _begin = false;
        protected Transform _trans;

        public Transform CachedTransform { get { if (_trans == null) _trans = transform; return _trans; } }

        void Start()
        {
            _time = 0;
            _begin = false;
        }

        void Update()
        {
            if (!_begin)
            {
                return;
            }
            _time += Time.deltaTime;

            float t = _time / _duration;
            t = Mathf.Clamp01(t);
            t = t * t;//曲线  

            Vector3 pos = (1 - t) * (1 - t) * start_position + 2 * t * (1 - t) * anchor_point + t * t * end_position;

            CachedTransform.position = pos;

            if (_time > _duration + _survival_time)
            {
                Destroy(gameObject);
                _begin = false;
            }
        }

        public void Begin(float duration)
        {
            _begin = true;
            _duration = duration;
        }

        public void SetDuration(float duration)
        {
            _duration = duration;
        }

        public void ResetPositon()
        {

        }
    }
}
