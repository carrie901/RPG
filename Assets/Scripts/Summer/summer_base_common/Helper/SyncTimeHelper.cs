
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
using System.Collections;
using System;

namespace Summer
{
    /// <summary>
    /// 时间同步相关工具
    /// </summary>
    public class SyncTimeHelper
    {

        #region 属性
        public static DateTime UtcNow { get { return DateTime.UtcNow.AddSeconds(DeltaTime); } }
        public static DateTime Now { get { return DateTime.Now.AddSeconds(DeltaTime); } }

        public static DateTime _d0 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
        public static DateTime _d1 = new DateTime(1970, 1, 1);
        private static double DeltaTime = 0;
        private static double ServerTime = 0;                                                           // 服务器基准时间
        private static double ValidStartGameTime = 0;                                                   // 游戏开始的有效时间
        #endregion

        #region Public

        /// <summary>
        /// 同步服务器时间
        /// </summary>
        /// <param name="time"></param>
        public static void Sync(long time)
        {
            ValidStartGameTime = Time.realtimeSinceStartup;
            DateTime dt = _d0.AddSeconds(time);
            DeltaTime = (dt - DateTime.UtcNow).TotalSeconds;
            ServerTime = time;
        }

        public static TimeSpan GetLeftTime(long validTime)
        {
            TimeSpan ts = (_d1.AddSeconds((double)validTime)).Subtract(UtcNow);
            return ts;
        }

        public static long GetTimeStamp(bool bflag = true)
        {
            TimeSpan ts = DateTime.UtcNow - _d0;
            long ret;
            if (bflag)
                ret = Convert.ToInt64(ts.TotalSeconds);
            else
                ret = Convert.ToInt64(ts.TotalMilliseconds);
            return ret;
        }

        #endregion

        #region Private Methods



        #endregion

    }
}
