
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

using Summer;
using UnityEngine;
using UnityEngine.UI;

namespace Summer
{
    public class PanelLogin : BaseView
    {

        #region 属性

        [UIChild("Login_Btn")]
        public Button _loginBtn;

        #endregion

        #region MONO Override

        // Use this for initialization
        private void Start()
        {
            _loginBtn.onClick.AddListener(OnClickLogin);
        }

        #endregion

        #region Public

        public override void OnInit()
        {

        }

        #endregion

        #region 响应

        public void OnClickLogin()
        {
            OpenView(E_ViewId.MAIN);
        }



        #endregion

        #region Private Methods



        #endregion
    }
}