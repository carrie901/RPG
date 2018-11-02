
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

using Object = System.Object;
namespace Summer
{

    /// <summary>
    /// Author : Ma ShaoMin
    /// CreateTime : 2017-7-25 11:57:58
    /// FileName : StringEventSystem.cs
    /// 事件管理器以String作为Key值
    /// </summary>
    public class StringEventSystem : TSingleton<GameEventSystem>
    {
        public EventSet<string, Object> _eventSet;

        #region public 
        protected override void OnInit()
        {
            _eventSet = new EventSet<string, Object>(256);
        }

        #region Register/UnRegister/RaiseEvent

        public bool RegisterHandler(string key, EventSet<string, Object>.EventHandler handler)
        {
            return _eventSet.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(string key, EventSet<string, Object>.EventHandler handler)
        {
            return _eventSet.UnRegisterHandler(key, handler);
        }

        public bool RaiseEvent(string key, Object param = null, bool bDelay = false)
        {
            return _eventSet.RaiseEvent(key, param, bDelay);
        }

        #endregion

        #endregion
    }
}
