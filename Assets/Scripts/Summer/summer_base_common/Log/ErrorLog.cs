
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
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 提交网络,错误日志
    /// </summary>
    public class ErrorLog
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Start()
        {
            if (!LogManager.ErrorLog) return;
            Application.logMessageReceived += UnityLogCallback;
        }

        private static void UnityLogCallback(string condition, string stackTrace, LogType type)
        {
            if (type != LogType.Error) return;
            Error(condition);
            Error(stackTrace);
        }

        public static void Error(string message)
        {

        }


    }
}

