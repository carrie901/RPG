
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

namespace Summer
{
    public class BuffLog
    {
        [System.Diagnostics.Conditional("LOG")]
        public static void Log(string message)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Log(message);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Log(string message, params object[] args)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Log(message, args);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Error(string message)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Error(message);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Error(string message, params object[] args)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Error(message, args);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Assert(bool condition, string message)
        {
            if (!LogManager._openLoadRes)
            LogManager.Assert(condition, message);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Assert(bool condition, string message, params object[] args)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Assert(condition, message, args);
        }
    }
}