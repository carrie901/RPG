
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
    /// 日期相关工具类
    /// </summary>
    public class DataHelper
    {
        #region 属性

        public const int ONE_MINUTE = 60;                                           // 一分钟（秒为单位）
        public const int ONE_HOUR = 60 * ONE_MINUTE;                                // 一小时
        public const int ONE_DAY = 24 * ONE_HOUR;                                   // 一天
        public const int ONE_MONTH = ONE_DAY * 30;                                  // 一个月
        public const int ONE_YEAR = ONE_DAY * 365;                                  // 一年

        public static DateTime _d0 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        public static DateTime _d1 = new DateTime(1970, 1, 1);

        #endregion

        #region Public

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string CurrentStamp()
        {
            TimeSpan span = DateTime.UtcNow - _d0;
            return Convert.ToInt64(span.TotalSeconds).ToString();
        }

        /// <summary>
        /// 转换为Unix时间
        /// </summary>
        public static int Convert2UnixTime(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(_d1);
            return (int)(time - startTime).TotalSeconds;
        }


        /// <summary>
        /// 转换为Unix时间
        /// </summary>
        public static DateTime Stamp2DateTime(DateTime stamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(_d1);
            long lTime = long.Parse(stamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        #endregion

        #region Private Methods



        #endregion
    }
}
