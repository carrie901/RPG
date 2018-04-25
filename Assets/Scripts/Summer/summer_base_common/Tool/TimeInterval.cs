using UnityEngine;
using System.Collections;

namespace Summer
{
    /// <summary>
    /// 更新平率
    /// </summary>
    public class TimeInterval
    {
        public float LeftTime { get { return _interval - _time; } }
        private float _interval;
        private float _time;

        private bool start;

        public TimeInterval(float interval)
        {
            _interval = interval;
            _time = 0;
            start = true;
        }

        // 重置
        public void Reset()
        {
            _time = 0;
        }

        public void SetEnd()
        {
            _time = _interval;
        }

        // 重置
        public void Reset(float interval)
        {
            _interval = interval;
            _time = 0;
            start = true;
        }

        public bool OnUpdate()
        {
            if (!start) return false;
            _time += Time.deltaTime;
            if (_time > _interval)
            {
                _time = 0;
                return true;
            }
            return false;
        }

        // 暂停
        public void Pause()
        {
            start = false;
        }

        // 运行
        public void Play()
        {
            start = true;
        }
    }

    /// <summary>
    /// TODO
    /// 这个方法是有一定风险错误的，不合适通过TimerHelper.RealtimeSinceStartup()来进行时间的间隔
    /// </summary>
    public class TimeDt
    {
        public float dt;
        public float _last_time;

        public float OnUpdate()
        {
            float cur_time = TimeManager.RealtimeSinceStartup;
            dt = cur_time - _last_time;
            OnReset();
            return dt;
        }

        public void OnReset()
        {
            _last_time = TimeManager.RealtimeSinceStartup;
        }

    }
}

