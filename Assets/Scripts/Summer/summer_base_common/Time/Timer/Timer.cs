using System;

namespace Summer
{
    public class Timer
    {
        #region OnTimerHandler

        public delegate void OnTimerHandler(Timer timer);
        private event OnTimerHandler TimeEvent;

        #endregion

        #region AddTimer

        public static Timer AddTimer(float interval, OnTimerHandler handler)
        {
            return AddTimer(interval, 1, handler);
        }

        public static Timer AddTimer(float interval, int repeatCount, OnTimerHandler handler)
        {
            Timer t = null;

            if (repeatCount == 1)
                t = new Timer(interval, handler);
            else
                t = new TimerMulti(interval, repeatCount, handler);
            TimerManager.Instance.AddTimer(t);
            return t;
        }

        #endregion

        public int SeqId { get; private set; }                                  
        public float ElapsedTime { get; protected set; }                    //流逝的时间                                      
        public float Interval { get; private set; }                         //间隔时间
        public float _scale;                                                //时间缩放
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
            TimeEvent += handler;
            Interval = interval;
            _scale = 1;
            ElapsedTime = 0;
            SeqId = TimerSeq.Get();
        }
        // 添加时间事件
        public bool AddHandler(OnTimerHandler handler)
        {
            //1. 异常情况，handler已经被加入
            if (TimeEvent != null && _handler_exist(handler, TimeEvent))
                return false;

            //2.正常情况,加入handler
            TimeEvent += handler;
            return true;
        }
        // 更新时间
        public void OnUpdate(float dt)
        {
            float realInterval = dt * _scale;
            ElapsedTime += realInterval;
        }
        // 检测超时与否 true=超时
        public bool IsTimeout()
        {
            return ElapsedTime >= Interval;
        }
        // 强制超时
        public void ForceTimeout()
        {
            if (!IsTimeout())
                ElapsedTime = Interval;
        }

        public virtual void OnTimeout()
        {
            if (TimeEvent != null)
                TimeEvent(this);
        }
        // 取消定时器
        public void Cancel()
        {
            TimerManager.Instance.CancelTimer(this);
        }

        private bool _handler_exist(Delegate handler, Delegate handlerSet)
        {
            if (handlerSet == null)
                return false;

            Delegate[] dels = handlerSet.GetInvocationList();
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
}



