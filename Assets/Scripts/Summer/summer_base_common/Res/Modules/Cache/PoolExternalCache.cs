
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 简单的记录最近使用记录
    /// </summary>
    public class PoolExternalCache<TKey, TValue> where TValue : I_PoolCacheRef
    {

        #region 属性
        public const int DEFAULT_CAPACITY = 6;

        public int _orginCapacity;
        public int _capacity;
        public IDictionary<TKey, TValue> _dictionary;
        public LinkedList<TKey> _linkedList;

        public delegate void OnRemoveValue<Tkey>(Tkey key);
        public event OnRemoveValue<TKey> OnRemoveValueEvent;
        public Dictionary<TKey, int> _ignoreKey = new Dictionary<TKey, int>();

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value > 0 && _capacity != value)
                {
                    _capacity = value;
                    LogManager.Assert(_linkedList.Count <= _capacity, "PoolExternalCache Capacity 容量缩小了");
                    /*while (_linkedList.Count > _capacity)
                    {
                        RemoveLast();
                    }*/
                }
            }
        }

        #endregion

        #region 构造
        public PoolExternalCache() : this(DEFAULT_CAPACITY) { }
        public PoolExternalCache(int capacity)
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
            if (_ignoreKey.ContainsKey(key)) return;
            _dictionary[key] = value;
            _linkedList.Remove(key);
            _linkedList.AddFirst(key);
            if (_linkedList.Count > _capacity)
            {
                Capacity = Capacity * 2;
            }
        }

        // 主动监测
        public void TakeCheck()
        {
            RemoveNoRefCount();
        }
        // 忽略的Key
        public void AddIgnoreKey(TKey key)
        {
            _ignoreKey.Add(key, 1);
        }

        #endregion

        #region private 

        // 移除指定的一个
        private void RemoveByKey(TKey key)
        {
            _dictionary.Remove(key);
            _linkedList.Remove(key);
            if (OnRemoveValueEvent != null)
                OnRemoveValueEvent(key);
        }

        protected List<TKey> _needRemoves = new List<TKey>();
        // 从末尾开始移除，1.引用为0并且内部数量没有超标的情况下
        private void RemoveNoRefCount()
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
                RemoveByKey(_needRemoves[i]);
            }

            Capacity = _orginCapacity;
            _needRemoves.Clear();
        }

        #endregion
    }

}

