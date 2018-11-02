using System;

namespace Summer
{
    /// <summary>
    /// 日期相关的工具类
    /// </summary>
    public class DateTimeHelper
    {
        private readonly static DateTime _d0 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        private readonly static DateTime _d1 = new DateTime(1970, 1, 1);
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

    }
}


