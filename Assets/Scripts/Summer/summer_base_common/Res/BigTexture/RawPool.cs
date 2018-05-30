using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-5 20:8:55
// FileName : RawPool.cs
//=============================================================================

namespace Summer
{
    // LRU算法 和缓存淘汰算法
    public class RawPool
    {
        public static RawPool Instance = new RawPool();

        // 引用计算缓存六个

        public void LoadTextureAsync(RawImage img, string name)
        {
            ResManager.instance.LoadTextureAsync(img, ResRequestFactory.CreateRequest<Texture>(name, E_GameResType.quanming), OnComplete);
        }

        public void ReaycelTexture(RawImage img, string name)
        {
            ResManager.instance.ResetDefaultTexture(img);
        }

        public void OnComplete(Texture texture)
        {

        }
    }

    public class BigTextureInfo
    {
        public string name;
        public RawImage icon;
        public int ref_count;
        public bool is_load;
    }


    #region 最近最少使用算法


    public class PoolCache<TKey, TValue>
    {
        #region 属性
        public const int DEFAULT_CAPACITY = 255;

        public int _capacity;
        public IDictionary<TKey, TValue> _dictionary;
        public LinkedList<TKey> _linked_list;

        #endregion

        #region Get

        public ICollection<TKey> Keys { get { return _dictionary.Keys; } }

        public ICollection<TValue> Values { get { return _dictionary.Values; } }

        public int Count { get { return _dictionary.Count; } }

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
                        _linked_list.RemoveLast();
                    }
                }
            }
        }


        #endregion

        public PoolCache() : this(DEFAULT_CAPACITY) { }

        public PoolCache(int capacity)
        {
            _capacity = capacity > 0 ? capacity : DEFAULT_CAPACITY;
            _dictionary = new Dictionary<TKey, TValue>();
            _linked_list = new LinkedList<TKey>();
        }

        public void Set(TKey key, TValue value)
        {
            _dictionary[key] = value;
            _linked_list.Remove(key);
            _linked_list.AddFirst(key);
            if (_linked_list.Count > _capacity)
            {
                _dictionary.Remove(_linked_list.Last.Value);
                _linked_list.RemoveLast();
            }
        }

        public bool TryGet(TKey key, out TValue value)
        {
            bool b = _dictionary.TryGetValue(key, out value);
            if (b)
            {
                _linked_list.Remove(key);
                _linked_list.AddFirst(key);
            }
            return b;
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }
    }


    #endregion
}
