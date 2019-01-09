
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
using MsgPb;
using UnityEngine;


namespace Summer
{

    public class FitHelper
    {

        #region 属性

        public static float DevelopHeigh = 720f;                                        // 开发屏幕的长
        public static float DevelopWidth = 1280f;                                       // 开发屏幕的宽
        public static float DevelopRate = DevelopHeigh / DevelopWidth;                  // 开发高宽比 默认16：9

        public static float ScreenRate = (float)Screen.height / (float)Screen.width;    // 当前屏幕高宽比
        public static float CameraRectHeightRate = DevelopHeigh / ((DevelopWidth / Screen.width) * Screen.height);          // 世界摄像机rect高的比例
        public static float CameraRectWidthRate = DevelopWidth / ((DevelopHeigh / Screen.height) * Screen.width);           // 世界摄像机rect宽的比例

        public static List<string> _iphonesDevice = new List<string>()
        {
            "iPhone10,3","iPhone10,6","iPhone11,8","iPhone11,2",
            "iPhone11,6"
        };
        #endregion

        public static void OnBgAtuo(RectTransform rectTrans)
        {
            rectTrans.anchorMax = Vector2.one / 2f;
            rectTrans.anchorMin = Vector2.one / 2f;

            rectTrans.pivot = Vector2.one / 2f;

            float oldWidth = rectTrans.rect.width;
            float oldHeight = rectTrans.rect.height;

            if (DevelopRate <= ScreenRate)
            {
                rectTrans.sizeDelta = new Vector2(oldWidth / CameraRectHeightRate, oldHeight / CameraRectHeightRate);
            }
            else
            {
                rectTrans.sizeDelta = new Vector2(oldWidth / CameraRectWidthRate, oldHeight / CameraRectWidthRate);
            }
        }

        public static bool IsIphone(string deviceModel)
        {
            if (_iphonesDevice.Contains(deviceModel)) return true;
            return false;
        }

    }
}