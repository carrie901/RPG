
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

namespace Summer
{
    public class TweenerHelper
    {
        public static void Sample(UTweener.Method method, float factor, bool isFinished)
        {
            // Calculate the sampling value
            float val = Mathf.Clamp01(factor);

            if (method == UTweener.Method.EaseIn)
            {
                val = 1f - Mathf.Sin(0.5f * Mathf.PI * (1f - val));
            }
            else if (method == UTweener.Method.EaseOut)
            {
                val = Mathf.Sin(0.5f * Mathf.PI * val);
            }
            else if (method == UTweener.Method.EaseInOut)
            {
                const float pi2 = Mathf.PI * 2f;
                val = val - Mathf.Sin(val * pi2) / pi2;
            }
            else if (method == UTweener.Method.BounceIn)
            {
                val = BounceLogic(val);
            }
            else if (method == UTweener.Method.BounceOut)
            {
                val = 1f - BounceLogic(1f - val);
            }

            // Call the virtual update
            //OnUpdate(val, isFinished);
        }

        static float BounceLogic(float val)
        {
            if (val < 0.363636f) // 0.363636 = (1/ 2.75)
            {
                val = 7.5685f * val * val;
            }
            else if (val < 0.727272f) // 0.727272 = (2 / 2.75)
            {
                val = 7.5625f * (val -= 0.545454f) * val + 0.75f; // 0.545454f = (1.5 / 2.75) 
            }
            else if (val < 0.909090f) // 0.909090 = (2.5 / 2.75) 
            {
                val = 7.5625f * (val -= 0.818181f) * val + 0.9375f; // 0.818181 = (2.25 / 2.75) 
            }
            else
            {
                val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f; // 0.9545454 = (2.625 / 2.75) 
            }
            return val;
        }
    }
}