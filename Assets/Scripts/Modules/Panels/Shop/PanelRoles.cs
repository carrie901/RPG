
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
    public class PanelRoles : BaseView
    {

        #region 属性

        [UIChild("BackBtn")]
        public Button _backBtn;
        [UIChild("EquipBtn")]
        public Button _equipBtn;
        [UIChild("SkillBtn")]
        public Button _skillBtn;
        #endregion

        #region MONO Override

        void Awake()
        {
            _backBtn.onClick.AddListener(OnClickBtn);
            _equipBtn.onClick.AddListener(OnClickEquip);
            _skillBtn.onClick.AddListener(OnClickSkill);
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

        public void OnClickEquip()
        {
            OpenView(E_ViewId.EQUIP);
        }
        public void OnClickSkill()
        {
            OpenView(E_ViewId.SKILL);
        }
        #endregion

        #region Private Methods



        #endregion
    }
}