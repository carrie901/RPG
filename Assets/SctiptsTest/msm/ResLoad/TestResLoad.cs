
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

public class TestResLoad : MonoBehaviour
{
    #region MONO Override

    // Use this for initialization
    void Start()
    {
        _flag0 = true;
        Check0();
    }

    // Update is called once per frame
    void Update()
    {
        Check0();
        Check1();

        Check10();
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods

    public bool _flag0 = false;
    public void Check0()
    {
        if (!_flag0) return;
        _flag0 = false;
        Resources.UnloadUnusedAssets();
    }

    public bool _flag1 = false;
    public void Check1()
    {
        if (!_flag1) return;
        _flag1 = false;
        for (int i = 0; i < 10; i++)
        {
            ResRequestInfo info1 = ResRequestFactory.CreateRequest<Texture2D>("Assets/res_bundle/63" + i + ".png");
            ResLoader.instance.LoadAssetAsync<Texture2D>(info1, null);
        }
    }

    public bool _flag10 = false;
    public void Check10()
    {
        if (!_flag10) return;
        _flag10 = false;
        ResLoader.instance.CheckInfo();
    }

    #endregion
}
