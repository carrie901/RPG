
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
public class PanelLogin : BaseView
{

    #region 属性

    [UIChild("Login_Btn")]
    public Button _back_btn;
    [UIChild("OnEnterAlert_Btn")]
    public Button _alert_btn;

    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {
        _back_btn.onClick.AddListener(OnBack);
        _alert_btn.onClick.AddListener(OnAlert);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region Public



    #endregion

    #region 响应

    public void OnBack()
    {
        OpenView(E_ViewId.main);
    }

    public void OnAlert()
    {
        OpenView(E_ViewId.alert_main);
    }

    #endregion

    #region Private Methods



    #endregion
}
