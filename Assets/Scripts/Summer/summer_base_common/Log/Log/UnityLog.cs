
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

namespace Summer
{
    /// <summary>
    /// Unity的Debug工具
    /// </summary>
    public class UnityLog : I_Log
    {

        protected static UnityLog _instance;
        public static UnityLog Instance
        {
            get { return _instance ?? (_instance = new UnityLog()); }
        }


        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void Log(string message, params object[] args)
        {
            Debug.LogFormat(message, args);
        }

        public void Waring(string message)
        {
            Debug.LogWarning(message);
        }

        public void Warning(string message, params object[] args)
        {
            Debug.LogWarningFormat(message, args);
        }

        public void Error(string message)
        {
            Debug.LogError(message);
        }

        public void Error(string message, params object[] args)
        {
            Debug.LogErrorFormat(message, args);
        }

        public void Assert(bool condition, string message)
        {
            Debug.Assert(condition, message);
        }

        public void Assert(bool condition, string message, params object[] args)
        {
            Debug.AssertFormat(condition, message, args);
        }

        public void Quit()
        {

        }
    }
}
