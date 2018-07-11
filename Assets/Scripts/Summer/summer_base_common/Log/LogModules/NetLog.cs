
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
    public class NetLog
    {
        public static void Log(string message)
        {
            if (!LogManager.open_net) return;
            LogManager.Log(message);
        }

        public static void Log(string message, params object[] args)
        {
            if (!LogManager.open_net) return;
            LogManager.Log(message, args);
        }

        public static void Error(string message)
        {
            //if (!LogManager.open_net) return;
            LogManager.Error(message);
        }

        public static void Error(string message, params object[] args)
        {
            //if (!LogManager.open_net) return;
            LogManager.Error(message, args);
        }

        public static void Assert(bool condition, string message)
        {
            //if (!LogManager.open_net) return;
            LogManager.Assert(condition, message);
        }

        public static void Assert(bool condition, string message, params object[] args)
        {
            //if (!LogManager.open_net) return;
            LogManager.Assert(condition, message, args);
        }
    }
}