
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 黑箱数据
    /// </summary>
    public class BlackBorad
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

        public BlackBorad() { }
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
