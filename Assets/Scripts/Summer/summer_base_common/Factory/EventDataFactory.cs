using System;
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 
    /// </summary>
    public class EventSetData
    {
        protected bool in_use;

        public virtual void OnInit() { }

        public virtual void Pop()
        {
            in_use = false;
            Reset();
        }

        public virtual void Push()
        {
            in_use = false;
            Reset();
        }

        public virtual void Reset() { }

        public virtual string ToDes()
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 把所有的Event的Factory合并到一起
    /// </summary>
    public class EventDataFactory
    {
        public static Dictionary<Type, List<EventSetData>> _data = new Dictionary<Type, List<EventSetData>>();
        public static int default_count = 1;
        public static T Pop<T>() where T : EventSetData, new()
        {
            Type type = typeof(T);
            if (!_data.ContainsKey(type))
            {
                _data.Add(type, new List<EventSetData>(8));
            }

            if (_data[type].Count == 0)
            {
                for (int i = 0; i < default_count; i++)
                {
                    T t = new T();
                    _data[type].Add(t);
                }
            }

            T info = _data[type][0] as T;
            if (info != null)
            {
                _data[type].Remove(info);
            }
            return info;
        }

        public static void Push(EventSetData t) //where T : EventSetData
        {
            if (t == null) return;
            Type type = t.GetType();
            if (!_data.ContainsKey(type))
            {
                UnityEngine.Debug.LogFormat("Event Set Data .Push Error,type:[{0}]", type.ToString());
                return;
            }
            _data[type].Add(t);
            t.Reset();
        }
    }

}








