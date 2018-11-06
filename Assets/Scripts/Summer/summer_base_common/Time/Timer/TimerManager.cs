using System.Collections.Generic;

namespace Summer
{
    public class TimerManager : TSingleton<TimerManager>, I_Update
    {
        #region 属性

        protected float _currTime;                                      // 当前时间
        protected bool _pause;                                          // 暂停
        public bool _needClear;
        protected Dictionary<Timer, float> _allTimers
            = new Dictionary<Timer, float>();

        public TimerComparer _comparer = new TimerComparer();           //比较器
        public float CurrentTime() { return _currTime; }                   //Query
        readonly List<Timer> _timeoutTimers = new List<Timer>(16);      //超时的队列

        #endregion

        #region 上层操作

        public void Pause() { _pause = true; }
        public void Resume() { _pause = false; }
        public void Reset() { _pause = false; _currTime = 0; }
        public void ClearTimer() { _needClear = true; }

        #endregion

        #region 单个操作

        public bool AddTimer(Timer timer)
        {
            if (_allTimers.ContainsKey(timer)) return false;
            float time = timer.Interval + CurrentTime();
            _allTimers[timer] = time;
            return true;
        }

        public void CancelTimer(Timer t)
        {
            if (_allTimers.ContainsKey(t))
                _allTimers.Remove(t);
        }
        
        #endregion

        #region Update

        public void OnUpdate(float dt)
        {
            if (_pause) return;
            _currTime += dt;

            // 清除检查
            CheckClear();

            // 更新所有timers
            Dictionary<Timer, float>.Enumerator etor = _allTimers.GetEnumerator();
            while (etor.MoveNext())
            {
                Timer timer = etor.Current.Key;
                timer.OnUpdate(dt);
                if (timer.IsTimeout())
                {
                    _timeoutTimers.Add(timer);
                }
            }

            if (_timeoutTimers.Count == 0) return;
            // timeout的顺序按照TimerComparer来
            _timeoutTimers.Sort(_comparer);
            // 移除所有的timeout timers
            for (int i = 0; i < _timeoutTimers.Count; i++)
            {
                if (!_allTimers.ContainsKey(_timeoutTimers[i]))//防止在循环中，某timer被cancel了
                {
                    continue;
                }

                _allTimers.Remove(_timeoutTimers[i]);
                _timeoutTimers[i].OnTimeout();
            }

            _timeoutTimers.Clear();
        }

        #endregion

        #region private

        private void CheckClear()
        {
            if (!_needClear) return;
            _allTimers.Clear();
            _needClear = false;
        }

        #endregion
    }
}

