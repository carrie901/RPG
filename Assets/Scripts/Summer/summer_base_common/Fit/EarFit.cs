
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
    public class EarFit : MonoBehaviour
    {
        #region

        enum EarDirection
        {
            Left,
            Right,
        }

        #endregion

        #region

        //这里可能需要
        [SerializeField]
        private float earOffset = 37f;


        [SerializeField]
        private EarDirection _dircetion = EarDirection.Left;

        #endregion

        private void Awake()
        {
#if UNITY_EDITOR
            if (FitHelper.ScreenRate < 0.47f)
            {
                SetEar();
            }
#else
            string deviceModel = SystemInfo.deviceModel;
            if (FitHelper.IsIphone(deviceModel)) return;

            SetEar();
#endif
        }

        private void SetEar()
        {
            RectTransform rectTransform = transform as RectTransform;
            if (rectTransform == null) return;

            float posX = 0f;
            if (_dircetion == EarDirection.Left)
            {
                posX = rectTransform.anchoredPosition.x + earOffset;
            }
            else if (_dircetion == EarDirection.Right)
            {
                posX = rectTransform.anchoredPosition.x - earOffset;
            }
            rectTransform.anchoredPosition = new Vector2(posX, rectTransform.anchoredPosition.y);
        }
    }
}