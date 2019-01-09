
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

    /// <summary>
    /// 大图背景
    /// </summary>
    public class BgAutoFit : MonoBehaviour
    {

        #region 属性



        #endregion

        #region MONO Override

        // Use this for initialization
        void Awake()
        {
            FitHelper.OnBgAtuo(transform as RectTransform);
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region Public



        #endregion

        #region Private Methods



        #endregion
    }
}

