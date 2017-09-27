using Summer;
using System;
public class Timer
{
    #region OnTimerHandler

    public delegate void OnTimerHandler(Timer timer);
    private event OnTimerHandler _time_event;

    #endregion

    #region AddTimer

    public static Timer AddTimer(float interval, OnTimerHandler handler)
    {
        return AddTimer(interval, 1, handler);
    }

    public static Timer AddTimer(float interval, int repeat_count, OnTimerHandler handler)
    {
        Timer t = null;

        if (repeat_count == 1)
            t = new Timer(interval, handler);
        else
            t = new Timer_Multi(interval, repeat_count, handler);
        TimerManager.Instance.AddTimer(t);
        return t;
    }

    #endregion

    public int SeqId { get; set; }

    public float _elapsed_time;                                     //流逝的时间
    public float ElapsedTime { get { return _elapsed_time; } }

    public float _interval;                                         //间隔时间
    public float Interval { get { return _interval; } }

    public float _scale;                                            //时间缩放
    public float Scale
    {
        get { return _scale; }
        set
        {
            LogManager.Assert(value > 0, "Scale.set: scale must > 0");
            _scale = value;
        }
    }

    public Timer(float interval, OnTimerHandler handler)
    {
        _time_event += handler;
        _interval = interval;
        _scale = 1;
        _elapsed_time = 0;
    }

    public bool AddHandler(OnTimerHandler handler)
    {
        //1. 异常情况，handler已经被加入
        if (_time_event != null && _handler_exist(handler, _time_event))
            return false;

        //2.正常情况,加入handler
        _time_event += handler;
        return true;
    }

    public void OnUpdate(float dt)
    {
        float real_interval = dt * _scale;
        _elapsed_time += real_interval;
    }

    public bool IsTimeout()
    {
        return _elapsed_time >= _interval;
    }

    public void ForceTimeout()
    {
        if (!IsTimeout())
            _elapsed_time = _interval;
    }

    public virtual void OnTimeout()
    {
        /*try
        {
           
        }
        catch (Exception e)
        {
            LogManager.Error(e.Message);
        }*/
        if (_time_event != null)
            _time_event(this);
    }

    public void Cancel()
    {
        TimerManager.Instance.CancelTimer(this);
    }

    public bool _handler_exist(Delegate handler, Delegate handler_set)
    {
        if (handler_set == null)
            return false;

        Delegate[] dels = handler_set.GetInvocationList();
        int length = dels.Length;
        for (int i = 0; i < length; i++)
        {
            if (handler == dels[i])
            {
                LogManager.Log("Timer.AddHandler : duplicate handler {0}", handler);
                return true;
            }
        }
        return false;
    }

}



public class Timer_Multi : Timer
{
    private int _count;
    private int _repeat_count;
    //count 如果小于等于0，相当于无限次
    public Timer_Multi(float interval, int repeat_count, OnTimerHandler handler)
        : base(interval, handler)
    {
        _repeat_count = repeat_count;
        _count = 0;
    }

    public override void OnTimeout()
    {
        base.OnTimeout();
        _count++;
        //TODO 会有偶一定的问题 关于超时时间
        if (_repeat_count <= 0 || _count < _repeat_count)
        {
            _elapsed_time = 0;
            TimerManager.Instance.AddTimer(this);
        }

    }
}