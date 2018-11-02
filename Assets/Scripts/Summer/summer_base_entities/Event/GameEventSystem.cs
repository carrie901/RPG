
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
//                 			 ���� ����             

using System.Collections.Generic;
using Object = System.Object;
namespace Summer
{
    //=============================================================================
    /// Author : Ma ShaoMin
    /// CreateTime : 2017-7-25 11:57:58
    /// FileName : GameEventSystem.cs
    //=============================================================================
    public class GameEventSystem : TSingleton<GameEventSystem>
    {

        #region DelayEvent

        /*private struct DelayEvent
        {
            public E_GLOBAL_EVT _key;
            public Object _param;
        };*/

        #endregion

        #region param

        /*private List<DelayEvent> _eventQuene = new List<DelayEvent>();*/
        public EventSet<E_GLOBAL_EVT, Object> _eventSet; //= new EventSet<E_GLOBAL_EVT, Object>();

        public GameEventSystem()
        {
            GlobalEvtComparer comparer = new GlobalEvtComparer();
            _eventSet = new EventSet<E_GLOBAL_EVT, Object>(comparer, 256);
        }

        #endregion

        #region Register/UnRegister/RaiseEvent

        public bool RegisterHandler(E_GLOBAL_EVT key, EventSet<E_GLOBAL_EVT, Object>.EventHandler handler)
        {
            return _eventSet.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_GLOBAL_EVT key, EventSet<E_GLOBAL_EVT, Object>.EventHandler handler)
        {
            return _eventSet.UnRegisterHandler(key, handler);
        }

        public bool RaiseEvent(E_GLOBAL_EVT key, Object param = null, bool bDelay = false)
        {
            return _eventSet.RaiseEvent(key, param, bDelay);
        }

        #endregion

        #region Delay GetProcess �ӳ�ִ��/�Ƿ��б�Ҫ����Щ����/Ԥ���ṩ�����Ĳ���

        /*public int ProcessDelayEvents()
        {
            return _event_set.ProcessDelayEvents();
        }*/

        /*public int ProcessAllDelayEvents()
        {
            return _event_set.ProcessAllDelayEvents();
        }*/

        #endregion

        #region Push/Peek

        /*public void PushEvent(E_GLOBAL_EVT key, Object param)
        {
            if (_event_quene == null)
            {
                _event_quene = new List<DelayEvent>();
            }

            DelayEvent de = new DelayEvent
            {
                key = key,
                param = param
            };
            _event_quene.Add(de);
        }*/

        //��ص�call back
        /*public bool PeekEvent(E_GLOBAL_EVT key)
        {
            if (_event_quene == null)
                return false;

            for (int i = 0; i < _event_quene.Count; i++)
            {
                if (key == _event_quene[i].key)
                {
                    DelayEvent de = _event_quene[i];
                    _event_quene.RemoveAt(i);
                    RaiseEvent(de.key, de.param, false);
                    return true;
                }
            }
            return false;
        }*/

        #endregion
    }

    public class GlobalEvtComparer : IEqualityComparer<E_GLOBAL_EVT>
    {
        public bool Equals(E_GLOBAL_EVT x, E_GLOBAL_EVT y)
        {
            return x == y;
        }

        public int GetHashCode(E_GLOBAL_EVT obj)
        {
            return (int)obj;
        }
    }
}

