
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
    /// 事件触发器+前置条件
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class EventConditionSet<TKey>
    {
        public EventSet<TKey, EventSetData> _eventSet;
        public Dictionary<TKey, I_Condition> _conditionSet;

        public EventConditionSet(IEqualityComparer<TKey> comparer, int dicSize = 8)
        {
            _eventSet = new EventSet<TKey, EventSetData>(comparer, dicSize);
            _conditionSet = new Dictionary<TKey, I_Condition>(dicSize);
        }

        public bool RegisterHandler(TKey key, EventSet<TKey, EventSetData>.EventHandler handler, I_Condition condition = null)
        {
            if (condition != null)
            {
                _conditionSet.Add(key, condition);
            }
            return _eventSet.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(TKey key, EventSet<TKey, EventSetData>.EventHandler handler, I_Condition condition = null)
        {
            if (_conditionSet.ContainsKey(key))
            {
                _conditionSet.Remove(key);
            }
            return _eventSet.UnRegisterHandler(key, handler);
        }

        public bool RaiseEvent(TKey key, EventSetData param)
        {
            I_Condition condition;
            _conditionSet.TryGetValue(key, out condition);
            if (condition != null && !condition.IsTrue(param)) return false;
            return _eventSet.RaiseEvent(key, param);
        }
    }
}