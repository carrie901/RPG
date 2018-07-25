
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

public class RvoGo : MonoBehaviour
{

    #region 属性

    protected Transform trans;


    public Transform target;

    #endregion

    #region MONO Override

    private void Awake()
    {
        trans = gameObject.transform;
    }

    // Use this for initialization
    void Start()
    {

    }

    public int detal = 2;
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= 360; i = i + detal)
        {
            Quaternion rotation = Quaternion.Euler(0f, i, 0f);
            Debug.DrawRay(trans.position, rotation * Vector3.right * 10, Color.yellow);
        }
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods



    #endregion
}
