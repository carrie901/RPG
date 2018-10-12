
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
using Summer;
using UnityEngine;

public class TestResLoad1 : MonoBehaviour
{

    #region 属性



    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Check1();
        Check2();
    }

    #endregion

    #region Public

    public bool flag1;
    public void Check1()
    {
        if (!flag1) return;
        flag1 = false;
        ResRequestInfo info = ResRequestFactory.CreateRequest<GameObject>("");
        ResLoader.instance.LoadAssetAsync<GameObject>(info, Check1Callback);
    }

    private void Check1Callback(GameObject go)
    {

    }

    public bool flag2;
    public void Check2()
    {
        if (!flag2) return;
        flag2 = false;
    }

    public bool flag3;
    public void Check3()
    {
        if (!flag3) return;
        flag3 = false;
    }

    public bool flag4;
    public void Check4()
    {
        if (!flag4) return;
        flag4 = false;
    }

    #endregion

    #region Private Methods



    #endregion
}
