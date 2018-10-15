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

        public int _orginCapacity;
        public int _capacity;
        public IDictionary<TKey, TValue> _dictionary;
        public LinkedList<TKey> _linkedList;

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
                    while (_linkedList.Count > _capacity)
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
            _linkedList = new LinkedList<TKey>();
            _orginCapacity = _capacity;
        }

        #endregion

        #region public

        public virtual void Set(TKey key, TValue value)
        {
            _dictionary[key] = value;
            _linkedList.Remove(key);
            _linkedList.AddFirst(key);
            RemoveNoRefCount();
            if (_linkedList.Count > _capacity)
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
            RemoveAt(_linkedList.Last.Value);
        }

        // 移除指定的一个
        public void RemoveAt(TKey key)
        {
            _dictionary.Remove(key);
            _linkedList.Remove(key);
            if (OnRemoveValueEvent != null)
                OnRemoveValueEvent(key);
        }

        protected  List<TKey> _needRemoves = new List<TKey>();
        // 从末尾开始移除，1.引用为0并且内部数量没有超标的情况下
        public void RemoveNoRefCount()
        {
            int needRemoveCount = _dictionary.Count - _orginCapacity;
            if (needRemoveCount <= 0) return;

            _needRemoves.Clear();

            LinkedListNode<TKey> lastPrivous = _linkedList.Last;
            do
            {
                if (lastPrivous == null) break;

                TKey tKey = lastPrivous.Value;
                if (_dictionary.ContainsKey(tKey) && _dictionary[tKey].GetRefCount() <= 0)
                {
                    _needRemoves.Add(tKey);
                    needRemoveCount--;
                    if (needRemoveCount <= 0)
                        break;
                }
                lastPrivous = lastPrivous.Previous;
            } while (lastPrivous != null);

            for (int i = _needRemoves.Count - 1; i >= 0; i--)
            {
                RemoveAt(_needRemoves[i]);
            }

            Capacity = _orginCapacity;
        }

        #endregion
    }

    public class PoolPanelCache<TKey, TValue> : PoolCache<TKey, TValue> where TValue : I_PoolCacheRef
    {
        public Dictionary<TKey, int> _ignoreKey = new Dictionary<TKey, int>();

        public PoolPanelCache(int size) : base(size) { }


        public override void Set(TKey key, TValue value)
        {
            if (_ignoreKey.ContainsKey(key)) return;
            base.Set(key, value);
        }

        public void AddIgnoreKey(TKey key)
        {
            _ignoreKey.Add(key, 1);
        }
    }


    public interface I_PoolCacheRef
    {
        int GetRefCount();
    }
}
