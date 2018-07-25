
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
using Summer;
public class PanelMain : BaseView
{

    #region 属性

    [UIChild("Alert_Btn")]
    public Button _alert_btn;
    [UIChild("Back_Btn")]
    public Button _back_btn;

    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {
        _alert_btn.onClick.AddListener(OnAlert);
        _back_btn.onClick.AddListener(OnBack);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region Public

    public void OnAlert()
    {
        OpenView(E_ViewId.alert);
    }

    public void OnBack()
    {
        CloseView();
    }


    #endregion

    #region Private Methods



    #endregion
}
