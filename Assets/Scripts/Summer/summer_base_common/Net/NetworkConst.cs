using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summer
{
    public class NetworkConst
    {
        public const string PREF_USER_ID = "USER_ID";
        public const string PREF_USER_PASSWORD = "USER_RPASSWORD";
        public const string PREF_USER_ITEM = "USER_ITEM";

        public const int DEFAULT_SCREEN_WIDTH = 1080;

        public static string ip_address = "104.224.165.21";
        public static int ip_port = 8081;
    }

    #region 主要事件 General 1000 - 2000

    public enum E_NetModuleMessage : int
    {
        socket_connected = 1000,
        socket_disconnected,
        try_login,
        req_finish,
        req_timeout,
    }

    public class NetworkMessageCode
    {
        public const int KEEP_ALIVE_SYNC = 1;
    }


    #endregion
}
