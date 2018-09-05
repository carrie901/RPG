
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 黑箱数据
    /// </summary>
    public class BlackBoard
    {
        private Dictionary<string, BlackboardItem> _items
            = new Dictionary<string, BlackboardItem>();

        class BlackboardItem
        {
            private System.Object _value;
            public void SetValue(System.Object v)
            {
                _value = v;
            }
            public T GetValue<T>()
            {
                return (T)_value;
            }
        }

        public BlackBoard() { }
        public void SetValue(string key, System.Object v)
        {
            BlackboardItem item;
            if (_items.ContainsKey(key) == false)
            {
                item = new BlackboardItem();
                _items.Add(key, item);
            }
            else
            {
                item = _items[key];
            }
            item.SetValue(v);
        }

        public void SetValue(string key, int v)
        {
            BbInt bb_int = new BbInt();
            SetValue(key, bb_int);
        }

        public void SetValue(string key, float v)
        {
            BbFloat bb_f = new BbFloat();
            bb_f.SetValue(v);
            SetValue(key, bb_f);
        }

        public void SetValue(string key, string v)
        {
            BbString bb_s = new BbString();
            bb_s.SetValue(v);
            SetValue(key, bb_s);
        }

        public void SetValue(string key, Vector3 v)
        {
            BbV3 bb_v3 = new BbV3();
            bb_v3.SetValue(v);
            SetValue(key, bb_v3);
        }

        public void SetEmpty(string key)
        {
            if (_items.ContainsKey(key))
            {
                _items.Remove(key);
            }
        }

        public T GetValue<T>(string key) where T : class, new()
        {
            if (!_items.ContainsKey(key))
            {
                SetValue(key, new T());
            }
            return _items[key].GetValue<T>();
        }

        public void Clear()
        {
            foreach (var info in _items)
            {

            }

            _items.Clear();
        }
    }
}
