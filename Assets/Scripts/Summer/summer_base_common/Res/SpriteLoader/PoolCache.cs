using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 最近最少使用
    /// </summary>
    public class PoolCache<TKey, TValue> where TValue : I_PoolCacheRef
    {
        #region 属性
        public const int DEFAULT_CAPACITY = 6;

        public int orgin_capacity;
        public int _capacity;
        public IDictionary<TKey, TValue> _dictionary;
        public LinkedList<TKey> _linked_list;

        public delegate void OnRemoveValue<Tkey>(Tkey key);

        public event OnRemoveValue<TKey> OnRemoveValueEvent;

        #endregion

        #region Get

        //public ICollection<TKey> Keys { get { return _dictionary.Keys; } }

        //public ICollection<TValue> Values { get { return _dictionary.Values; } }

        //public int Count { get { return _dictionary.Count; } }

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value > 0 && _capacity != value)
                {
                    _capacity = value;
                    while (_linked_list.Count > _capacity)
                    {
                        RemoveLast();
                    }
                }
            }
        }

        #endregion

        #region 构造

        public PoolCache() : this(DEFAULT_CAPACITY) { }

        public PoolCache(int capacity)
        {
            _capacity = capacity > 0 ? capacity : DEFAULT_CAPACITY;
            _dictionary = new Dictionary<TKey, TValue>();
            _linked_list = new LinkedList<TKey>();
            orgin_capacity = _capacity;
        }

        #endregion

        #region public

        public virtual void Set(TKey key, TValue value)
        {
            _dictionary[key] = value;
            _linked_list.Remove(key);
            _linked_list.AddFirst(key);
            if (_linked_list.Count > _capacity)
            {
                Capacity = Capacity + 4;
            }
        }

        /*public bool TryGet(TKey Key, out TValue value)
        {
            bool b = _dictionary.TryGetValue(Key, out value);
            if (b)
            {
                _linked_list.Remove(Key);
                _linked_list.AddFirst(Key);
            }
            return b;
        }*/

        public void SetDefaultCapacity()
        {
            RemoveNoRefCount();
        }

        #endregion

        #region private 
        // 移除最后一个
        public void RemoveLast()
        {
            RemoveAt(_linked_list.Last.Value);
        }

        // 移除指定的一个
        public void RemoveAt(TKey key)
        {
            _dictionary.Remove(key);
            _linked_list.Remove(key);
            if (OnRemoveValueEvent != null)
                OnRemoveValueEvent(key);
        }

        // 从末尾开始移除，1.引用为0并且内部数量没有超标的情况下
        public void RemoveNoRefCount()
        {
            int need_remove_count = _dictionary.Count - orgin_capacity;
            if (need_remove_count <= 0) return;

            List<TKey> need_removes = new List<TKey>();

            LinkedListNode<TKey> last_privous = _linked_list.Last;
            do
            {
                if (last_privous == null) break;

                TKey t_key = last_privous.Value;
                if (_dictionary.ContainsKey(t_key) && _dictionary[t_key].GetRefCount() <= 0)
                {
                    need_removes.Add(t_key);
                    need_remove_count--;
                    if (need_remove_count <= 0)
                        break;
                }
                last_privous = last_privous.Previous;
            } while (last_privous != null);

            for (int i = 0; i < need_removes.Count; i++)
            {
                RemoveAt(need_removes[i]);
            }

            Capacity = orgin_capacity;
        }

        #endregion
    }

    public class PoolPanelCache<TKey, TValue> : PoolCache<TKey, TValue> where TValue : I_PoolCacheRef
    {
        public Dictionary<TKey, int> _ignore_key = new Dictionary<TKey, int>();

        public PoolPanelCache(int size) : base(size) { }


        public override void Set(TKey key, TValue value)
        {
            if (_ignore_key.ContainsKey(key)) return;
            base.Set(key, value);
        }

        public void AddIgnoreKey(TKey key)
        {
            _ignore_key.Add(key, 1);
        }
    }


    public interface I_PoolCacheRef
    {
        int GetRefCount();
    }
}
