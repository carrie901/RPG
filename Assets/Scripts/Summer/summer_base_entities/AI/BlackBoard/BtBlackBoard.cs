using System.Collections.Generic;

namespace Summer.AI
{

    public class BtBlackBoard
    {

        class BtBlackboardItem
        {
            private object _value;
            public void SetValue(object v)
            {
                _value = v;
            }
            public T GetValue<T>()
            {
                return (T)_value;
            }
        }

        private Dictionary<string, BtBlackboardItem> _items;

        public BtBlackBoard()
        {
            _items = new Dictionary<string, BtBlackboardItem>();
        }
        public void SetValue(string key, object v)
        {
            BtBlackboardItem item;
            if (_items.ContainsKey(key) == false)
            {
                item = new BtBlackboardItem();
                _items.Add(key, item);
            }
            else
            {
                item = _items[key];
            }
            item.SetValue(v);
        }
        public T GetValue<T>(string key, T default_value)
        {
            if (_items.ContainsKey(key) == false)
            {
                return default_value;
            }
            return _items[key].GetValue<T>();
        }
    }
}