
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

            float minX = 0;
            float minY = 0;
            float maxX = 0;
            float maxY = 0;
            for (int i = 0; i < rects.Count; i++)
            {
                ERect rect = rects[i];

                float leftX = rect.Ex - rect.Ew / 2;
                float leftY = rect.Ey - rect.Eh / 2;
                float rightX = rect.Ex + rect.Ew / 2;
                float rightY = rect.Ey + rect.Eh / 2;

                if (leftX < minX)
                    minX = leftX;

                if (leftY < minY)
                    minY = leftY;

                if (rightX > maxX)
                    maxX = rightX;

                if (rightY > maxY)
                    maxY = rightY;
            }
            size.x = maxX - minX;
            size.y = maxY - minY;
            return size;
        }

    }

}
