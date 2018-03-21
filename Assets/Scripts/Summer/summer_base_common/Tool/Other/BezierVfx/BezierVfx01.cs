using UnityEngine;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2018-2-27 14:57:50
// FileName : BezierVfx01.cs
//=============================================================================

namespace Summer
{
    public class BezierVfx01 : MonoBehaviour
    {
        public float SurvivalTime { get { return _survival_time; } }
        protected Vector3 start_position;
        protected Vector3 end_position;
        protected Vector3 anchor_point;

        protected float _duration;
        protected float _survival_time = 0.5f;//结束之后保留的时间  
        protected float _time;
        protected bool _begin = false;
        protected Transform _trans;

        public Transform CachedTransform { get { if (_trans == null) _trans = transform; return _trans; } }

        void Awake()
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

            if (_time > _duration + 0.5f)
            {
                _begin = false;
            }
        }

        public void Begin(float duration)
        {
            _begin = true;
            _time = 0;
            _duration = duration;
        }

        public void SetDuration(float duration)
        {
            _duration = duration;
        }

        public void SetCurv(Transform s_go, Transform e_go, Transform anchor_go)
        {
            start_position = s_go.position;
            end_position = e_go.position;
            anchor_point = anchor_go.position;

            Vector3 pos =  start_position;
            CachedTransform.position = pos;
        }
    }
}
