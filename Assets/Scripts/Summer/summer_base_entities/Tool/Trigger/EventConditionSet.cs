
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
    /// 单一化
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class EventConditionSet<TKey>
    {
        public EventSet<TKey, EventSetData> _event_set;

        public Dictionary<TKey, I_Condition> _condition_set;
        public EventConditionSet(int dic_size = 8)
        {
            _event_set = new EventSet<TKey, EventSetData>(dic_size);
            _condition_set = new Dictionary<TKey, I_Condition>(dic_size);
        }

        public EventConditionSet(IEqualityComparer<TKey> comparer, int dic_size = 8)
        {
            _event_set = new EventSet<TKey, EventSetData>(comparer, dic_size);
            _condition_set = new Dictionary<TKey, I_Condition>(dic_size);
        }

        public bool RegisterHandler(TKey key, EventSet<TKey, EventSetData>.EventHandler handler, I_Condition condition = null)
        {
            if (condition != null)
            {
                _condition_set.Add(key, condition);
            }
            return _event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(TKey key, EventSet<TKey, EventSetData>.EventHandler handler, I_Condition condition = null)
        {
            if (_condition_set.ContainsKey(key))
            {
                _condition_set.Remove(key);
            }
            return _event_set.UnRegisterHandler(key, handler);
        }

        public bool RaiseEvent(TKey key, EventSetData param)
        {
            I_Condition condition;
            _condition_set.TryGetValue(key, out condition);
            if (condition != null && !condition.IsTrue(param)) return false;
            return _event_set.RaiseEvent(key, param);
        }

    }
}