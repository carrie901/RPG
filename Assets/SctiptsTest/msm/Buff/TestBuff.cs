
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

public class TestBuff : MonoBehaviour
{

    #region 属性

    public BaseEntityController base_controller;

    #endregion

    #region MONO Override

    private void Awake()
    {
        base_controller = gameObject.GetComponent<BaseEntityController>();
    }

    // Use this for initialization
    void Start()
    {

    }

    public bool flag = false;
    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            flag = false;
            base_controller._baseEntity.AddBuff();
        }
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods



    #endregion
}
