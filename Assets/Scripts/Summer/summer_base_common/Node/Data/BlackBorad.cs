
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 黑箱数据
    /// </summary>
    public class BlackBorad
    {
        protected Dictionary<string, BlackBoardItem> _items
            = new Dictionary<string, BlackBoardItem>();

        public BlackBorad() { }
        public void AddValue(string key, BlackBoardItem item)
        {
            if (_items.ContainsKey(key))
            {
                _items[key] = item;
            }
            else
            {
                _items.Add(key, item);
            }
        }

        public void SetEmpty(string key)
        {
            if (_items.ContainsKey(key))
            {
                _items.Remove(key);
            }
        }

        public BlackBoardItem GetValue(string key)
        {
            BlackBoardItem item;
            _items.TryGetValue(key, out item);
            return item;
        }
    }

    public class BlackBoardItem
    {
        public virtual void Clear()
        {

        }
    }

    public class BlockBoradInt : BlackBoardItem
    {
        public int value;
    }

    public class BlockBoradFloat : BlackBoardItem
    {
        public float value;
    }

    public class BlockBoradString : BlackBoardItem
    {
        public float value;
    }


}
