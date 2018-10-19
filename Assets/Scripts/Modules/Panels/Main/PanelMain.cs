
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
    public class PanelMain : BaseView
    {

        #region 属性

        [UIChild("TaskBtn")]
        public Button _taskBtn;
        [UIChild("BattleBtn")]
        public Button _battleBtn;
        [UIChild("ShopBtn")]
        public Button _shopBtn;
        [UIChild("RolesBtn")]
        public Button _rolesBtn;
        [UIChild("BagBtn")]
        public Button _bagItem;

        #endregion

        #region MONO Override

        // Use this for initialization
        void Start()
        {
            _taskBtn.onClick.AddListener(OnClickTask);
            _battleBtn.onClick.AddListener(OnClickBattle);
            _shopBtn.onClick.AddListener(OnClickShop);
            _rolesBtn.onClick.AddListener(OnClickRole);
            _bagItem.onClick.AddListener(OnClickBag);
        }

        #endregion

        #region Public

        public void OnClickTask()
        {
            OpenView(E_ViewId.TASK);
        }

        public void OnClickBattle() { OpenView(E_ViewId.SELECT_LEVEL); }
        public void OnClickShop() { OpenView(E_ViewId.SHOP); }
        public void OnClickRole() { OpenView(E_ViewId.ROLES); }
        public void OnClickBag() { OpenView(E_ViewId.BAG); }


        #endregion

        #region Private Methods



        #endregion
    }
}