
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Summer;
using UnityEngine;

public class TestMainAttribute : MonoBehaviour
{

    #region 属性



    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {
        System.Attribute[] attrs = System.Attribute.GetCustomAttributes(typeof(Rectangle));
        //Debug.Log("attrs:" + attrs.Length);


        PropertyInfo[] propertys = typeof(Rectangle).GetProperties();//返回FirstClass的所有公共属性  
        //Debug.Log("propertys:" + propertys.Length);

        /*Type t = typeof(Rectangle);
        var something = t.GetCustomAttributes(true);
        foreach (DeBugInfo each in something)
        {
            Debug.LogFormat("Name:{0}", each.ToDes());
        }*/
        //Debug.Log("---------");
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region Public



    #endregion

    #region Private Methods



    #endregion
}
