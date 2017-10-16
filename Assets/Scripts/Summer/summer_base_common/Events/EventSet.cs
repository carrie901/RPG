using System.Collections.Generic;

namespace Summer
{

    //=============================================================================
    /// Author : Ma ShaoMin
    /// CreateTime : 2017-7-25 11:57:58
    /// FileName : EventSet.cs
    //=============================================================================
    public class EventSet<TKey, TValue>
    {
        #region DelayEvent

        private struct DelayEvent
        {
            public TKey key;
            public TValue param;
        };

        #endregion

        #region Param

        public delegate void EventHandler(TValue param);

        private Queue<DelayEvent> _delay_quene = new Queue<DelayEvent>();

        private readonly Dictionary<TKey, EventHandler> _events = new Dictionary<TKey, EventHandler>();

        #endregion

        #region Register/UnRegister/Raise

        public bool RegisterHandler(TKey key, EventHandler handler)
        {
            if (_events.ContainsKey(key))
            {
                System.Delegate[] dels = _events[key].GetInvocationList();
                int length = dels.Length;
                for (int i = 0; i < length; i++)
                {
                    if (dels[i] == (System.Delegate)handler)
                        return true;
                }
                _events[key] += handler;
            }
            else
            {
                _events.Add(key, handler);
            }

            return true;

        }

        public bool UnRegisterHandler(TKey key, EventHandler handler)
        {
            if (_events.ContainsKey(key))
            {
                _events[key] -= handler;

                if (_events[key] == null)
                    _events.Remove(key);

                return true;
            }
            else
                return false;
        }

        public bool RaiseEvent(TKey key, TValue param, bool b_delay = false)
        {
            if (b_delay)
            {
                DelayEvent de = new DelayEvent
                {
                    key = key,
                    param = param
                };

                if (_delay_quene == null)
                    _delay_quene = new Queue<DelayEvent>();

                _delay_quene.Enqueue(de);

                return true;
            }
            return _internal_real_raiser_event(key, param);
        }

        public void Clear()
        {
            _delay_quene.Clear();
            _events.Clear();
        }

        #endregion

        #region Delay Event
        //只处理当前帧的所有消息，如果在处理过程中，添加了新的消息，不处理
        //如果需要处理所有的消息，可以循环调用直到返回值为0
        public int ProcessDelayEvents()
        {
            int n_ret = 0;

            //只处理当前帧的所有消息
            int n_count = _delay_quene.Count;
            while (n_count > 0 && _delay_quene != null && _events != null && _delay_quene.Count > 0)
            {
                DelayEvent de = _delay_quene.Dequeue();
                _internal_real_raiser_event(de.key, de.param);
                n_ret++;
                n_count--;
            }

            return n_ret;
        }

        public int ProcessAllDelayEvents()
        {
            int n_ret = 0;
            int n_count = 0;
            do
            {
                n_count = ProcessDelayEvents();
                n_ret += n_count;
            }
            while (n_count > 0);

            return n_ret;
        }
        #endregion
        private bool _internal_real_raiser_event(TKey key, TValue param)
        {
            EventHandler event_hander;
            _events.TryGetValue(key, out event_hander);
            if (event_hander != null)
            {
                _events[key].Invoke(param);
                return true;
            }
            return false;
        }
    }
}
