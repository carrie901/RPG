using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 更新平率
    /// </summary>
    public class TimeInterval
    {
        public float LeftTime { get { return _interval - _time; } }         // 剩余时间
        private float _interval;                                            // 间隔
        private float _time;                                                // 流逝的时间
        private bool _start;                                                // 是否开始

        public TimeInterval(float interval)
        {
            _interval = interval;
            _time = 0;
            _start = true;
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
            _start = true;
        }

        public bool OnUpdate()
        {
            if (!_start) return false;
            _time += Time.deltaTime;
            if (_time > _interval)
            {
                _time = 0;
                _start = false;
                return true;
            }
            return false;
        }

        // 暂停
        public void Pause()
        {
            _start = false;
        }

        // 运行
        public void Play()
        {
            _start = true;
        }
    }

    /// <summary>
    /// TODO
    /// 这个方法是有一定风险错误的，不合适通过TimerHelper.RealtimeSinceStartup()来进行时间的间隔
    /// </summary>
    public class TimeDt
    {
        public float _dt;
        public float _lastTime;

        public float OnUpdate()
        {
            float curTime = TimeModule.RealtimeSinceStartup;
            _dt = curTime - _lastTime;
            OnReset();
            return _dt;
        }

        public void OnReset()
        {
            _lastTime = TimeModule.RealtimeSinceStartup;
        }
    }
}

