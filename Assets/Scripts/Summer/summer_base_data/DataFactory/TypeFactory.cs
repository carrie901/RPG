using System;
using System.Collections;
using System.Collections.Generic;

namespace Summer
{
    public class TypeFactory<T> where T : class, new()
    {

        /*public Dictionary<Type, T> _data = new Dictionary<Type, T>();
        public Dictionary<Type, T> _data_in = new Dictionary<Type, T>();
        public int default_count = 1;
        public T Pop<T>() where T : EventSetData, new()
        {
            Type type = typeof(T);
            if (!_data.ContainsKey(type))
            {
                List<T> lists = new List<T>(8);
                _data.Add(type, lists);
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

        public void Push(EventSetData t) //where T : EventSetData
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
        }*/
    }
}
