
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

using UnityEngine;
using Object = System.Object;
namespace Summer
{
    /// <summary>
    /// 和NetworkEventSystem做了区分
    /// NetworkEventSystem是Socket Net 内部的事件分发机制
    /// NetMessageSystem是网络消息的分发机制
    /// </summary>
    public class NetMessageSystem : MonoBehaviour
    {

        #region 属性

        private static NetMessageSystem _instance;
        public static NetMessageSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NetMessageSystem();
                }
                return _instance;
            }
        }

        #endregion

        private NetMessageSystem() { }

        public EventSet<int, System.Object> _event_set = new EventSet<int, System.Object>();

        public bool RegisterHandler(int key, EventSet<int, Object>.EventHandler handler)
        {
            return _event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(int key, EventSet<int, Object>.EventHandler handler)
        {
            return _event_set.UnRegisterHandler(key, handler);
        }

        public void RaiseEvent(int key, Object obj_info)
        {
            if (_event_set == null) return;
            _event_set.RaiseEvent(key, obj_info);
        }

    }
}