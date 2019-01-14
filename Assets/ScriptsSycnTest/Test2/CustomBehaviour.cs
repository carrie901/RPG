
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

public class CustomBehaviour : MonoBehaviour {


    #region 属性

    bool m_isDestroy = false;

    public bool IsDestroy
    {
        get { return m_isDestroy; }
    }

    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {
        
    }
     
    // Update is called once per frame
    void Update()
    {
     
    }

    #endregion

    #region Public

    public virtual void OnDestroy() { }


    #endregion

    #region Private Methods



    #endregion
}
