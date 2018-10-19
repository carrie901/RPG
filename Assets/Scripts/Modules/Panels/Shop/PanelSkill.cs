
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

using UnityEngine.UI;

namespace Summer
{
    public class PanelSkill : BaseView
    {

        #region 属性

        [UIChild("BackBtn")]
        public Button _backBtn;

        #endregion

        #region MONO Override

        void Awake()
        {
            _backBtn.onClick.AddListener(OnClickBtn);
        }

        void Start()
        {

        }

        #endregion

        #region Public

        public void OnClickBtn()
        {
            CloseView();
        }

        #endregion

        #region Private Methods



        #endregion
    }
}