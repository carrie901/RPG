
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

namespace SummerEditor
{
    public class ERectHelper
    {
        public static Vector2 GetSize(List<ERect> rects)
        {
            Vector2 size = Vector2.zero;

            float min_x = 0;
            float min_y = 0;
            float max_x = 0;
            float max_y = 0;
            for (int i = 0; i < rects.Count; i++)
            {
                ERect rect = rects[i];

                float left_x = rect.Ex - rect.Ew / 2;
                float left_y = rect.Ey - rect.Eh / 2;
                float right_x = rect.Ex + rect.Ew / 2;
                float right_y = rect.Ey + rect.Eh / 2;

                if (left_x < min_x)
                    min_x = left_x;

                if (left_y < min_y)
                    min_y = left_y;

                if (right_x > max_x)
                    max_x = right_x;

                if (right_y > max_y)
                    max_y = right_y;
            }
            size.x = max_x - min_x;
            size.y = max_y - min_y;
            return size;
        }

    }

}
