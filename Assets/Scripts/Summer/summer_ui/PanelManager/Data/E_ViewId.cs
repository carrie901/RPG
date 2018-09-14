﻿
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

namespace Summer
{
    /// <summary>
    /// 对应的UI枚举需要对应的信息配置
    /// </summary>
    public enum E_ViewId
    {
        INVAILD = 0,
        LOGIN = 1,                      // 登陆界面
        MAIN = 2,                       // 主界面
        ALERT = 3,
        ALERT_MAIN = 4,
        MAX,
    }

    public class PanelComparer : IEqualityComparer<E_ViewId>
    {
        public static PanelComparer Instance = new PanelComparer();
        private PanelComparer() { }
        public bool Equals(E_ViewId x, E_ViewId y)
        {
            return x == y;
        }

        public int GetHashCode(E_ViewId obj)
        {
            return (int)obj;
        }
    }
}

