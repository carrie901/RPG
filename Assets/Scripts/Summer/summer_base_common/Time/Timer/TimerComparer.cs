
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
    /// Timer的比较器
    /// </summary>
    public class TimerComparer : IComparer<Timer>
    {
        public int _equal = 0;
        public int _smaller = -1;
        public int _larger = 1;
        public int Compare(Timer x, Timer y)
        {
            if (x == null || y == null) return _smaller;
            float targetTimeX = x.Interval - x.ElapsedTime;
            float targetTimeY = y.Interval - y.ElapsedTime;
            // 1. timeout时间不同的情况下，
            // 判断timeout时间，timeout时间先的排在前面
            if (Mathf.Abs(targetTimeX - targetTimeY) > float.Epsilon)
            {
                return targetTimeX < targetTimeY ? _smaller : _larger;
            }

            // 2. timeout时间相同的情况下，判断seq，seq小的排在前面
            return x.SeqId < y.SeqId ? _smaller : _larger;
        }
    }
}