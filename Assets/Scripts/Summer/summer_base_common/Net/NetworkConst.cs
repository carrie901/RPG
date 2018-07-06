using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summer
{
    #region NetWorkConst 

    public class NetworkConst
    {
        public const string PREF_USER_ID = "USER_ID";
        public const string PREF_USER_PASSWORD = "USER_RPASSWORD";
        public const string PREF_USER_ITEM = "USER_ITEM";

        public const int DEFAULT_SCREEN_WIDTH = 1080;

        public static string ip_address = "192.168.160.167";
        public static int ip_port = 8004;

        public const int HEAD_SIZE = 4;                                     // 头长度
        public const int MSG_ID_SIZE = 4;
        public const int MSG_DATA_SIZE = 4;
        public const int MAX_BUFF_SIZE = 8192;                              // buffersize长度

        /// <summary>
        /// 连接超时时间
        /// </summary>
        public const float CONNECT_TIME_OUT = 3.0f;
        /// <summary>
        /// 回复超时时间
        /// </summary>
        public const float REQ_TIME_OUT = 5.0f;
        public const float KEEP_ALIVE_TIME_OUT = 10.0f;
    }

    #endregion

    public enum E_NetModule
    {
        /// <summary>
        /// 连接
        /// </summary>
        socket_connected = 1000,
        /// <summary>
        /// 断开连接
        /// </summary>
        socket_disconnected,
        /// <summary>
        /// 请求结束
        /// </summary>
        req_finish,
        /// <summary>
        /// 请求超时
        /// </summary>
        req_timeout,
    }

    public class NetworkMessageCode
    {
        public const Int32 KEEP_ALIVE_SYNC = 0;

        public const Int32 REQVERSION = 1;
    }
}
