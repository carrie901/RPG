
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

public class TestShader : MonoBehaviour
{

    #region 属性

    public float index;
    public Image _img;
    public Material _mat;
    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {
        _img = GetComponent<Image>();
        _mat = _img.material;
    }

    // Update is called once per frame
    void Update()
    {
        Update1();
    }

    #endregion

    #region Public

    public bool flag = false;

    public void Update1()
    {
        if (!flag) return;
        flag = false;
        _mat.SetFloat("_Index", index);
    }

    #endregion

    #region Private Methods



    #endregion
}
