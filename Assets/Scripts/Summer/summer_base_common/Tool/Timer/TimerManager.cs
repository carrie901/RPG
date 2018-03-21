using System.Collections.Generic;
using Summer;
using UnityEngine;
public class TimerManager : TSingleton<TimerManager>
{
    #region TimerSeq
    public class TimerSeq
    {
        static int _seq;
        public static int Get() { return ++_seq; }
    }
    #endregion

    #region TimerComparer
    public class TimerComparer : IComparer<Timer>
    {
        public int _equal = 0;
        public int _smaller = -1;
        public int _larger = 1;
        public int Compare(Timer x, Timer y)
        {
            if (x == null || y == null) return _smaller;
            float target_time_x = x.Interval - x.ElapsedTime;
            float target_time_y = y.Interval - y.ElapsedTime;
            // 1. timeout时间不同的情况下，
            //   判断timeout时间，timeout时间先的排在前面
            if (Mathf.Abs(target_time_x - target_time_y) > float.Epsilon)
            {
                return target_time_x < target_time_y ? _smaller : _larger;
            }

            // 2. timeout时间相同的情况下，判断seq，seq小的排在前面
            return x.SeqId < y.SeqId ? _smaller : _larger;
        }
    }
    #endregion

    protected float _timer;                                         //
    protected bool _pause;                                          //暂停
    public bool _need_clear;
    protected Dictionary<Timer, float> _all_timers                  
        = new Dictionary<Timer, float>();

    public TimerComparer comparer = new TimerComparer();            //比较器
    public float CurrentTime() { return _timer; }                   //Query
    readonly List<Timer> _timeout_timers = new List<Timer>();       //超时的队列

    #region 上层操作
    public void Pause() { _pause = true; }
    public void Resume() { _pause = false; }
    public void Reset() { _pause = false; _timer = 0; }
    public void ClearTimer() { _need_clear = true; }
    #endregion

    #region 单个操作
    public bool AddTimer(Timer timer)
    {
        if (_all_timers.ContainsKey(timer))
        {
            return false;
        }

        timer.SeqId = TimerSeq.Get();

        float time = timer.Interval + CurrentTime();
        _all_timers[timer] = time;
        return true;
    }

    public void CancelTimer(Timer t)
    {
        if (_all_timers.ContainsKey(t))
            _all_timers.Remove(t);
    }
    #endregion

    #region Update
    public void OnUpdate(float dt)
    {
        if (_pause) return;
        _timer += dt;

        // 清除
        if (_need_clear)
        {
            _all_timers.Clear();
            _need_clear = false;
            return;
        }

        // 更新所有timers
        Dictionary<Timer, float>.Enumerator etor = _all_timers.GetEnumerator();
        while (etor.MoveNext())
        {
            Timer timer = etor.Current.Key;
            timer.OnUpdate(dt);
            if (timer.IsTimeout())
            {
                _timeout_timers.Add(timer);
            }
        }

        if (_timeout_timers.Count == 0) return;
        // timeout的顺序按照TimerComparer来
        _timeout_timers.Sort(comparer);
        // 移除所有的timeout timers
        for (int i = 0; i < _timeout_timers.Count; i++)
        {
            if (!_all_timers.ContainsKey(_timeout_timers[i]))//防止在循环中，某timer被cancel了
            {
                continue;
            }

            _all_timers.Remove(_timeout_timers[i]);
            _timeout_timers[i].OnTimeout();
        }

        _timeout_timers.Clear();
    }
    #endregion
}