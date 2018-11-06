
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

public class TestTimeDelay : MonoBehaviour
{

    #region 属性



    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {

    }

    public bool flag = false;
    public float timescale = 0.1f;
    // Update is called once per frame
    void Update()
    {
        if (!flag) return;
        flag = false;
        Time.timeScale = timescale;
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods



    #endregion
}
