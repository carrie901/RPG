
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
using UnityEngine.UI;
namespace Summer
{

    public class PanelLoading : BaseView
    {

        #region 属性

        private float _loadingTime = 5f;
        [UIChild("Slider")]
        public Slider _slider;

        private float _leftTime;
        #endregion

        #region MONO Override

        // Use this for initialization
        void Awake()
        {
            _slider.value = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (_leftTime > _loadingTime) return;
            _leftTime += Time.deltaTime;
            _slider.value = _leftTime / _loadingTime;
            if (_leftTime >= _loadingTime)
            {
                OpenView(E_ViewId.LOGIN);
            }
        }

        #endregion

        #region Public



        #endregion

        #region Private Methods



        #endregion
    }
}

