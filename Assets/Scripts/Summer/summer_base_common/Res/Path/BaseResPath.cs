
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

namespace Summer
{
    public class BaseResPath
    {

        /// <summary>
        /// 资源的名字
        /// </summary>
        public string ResName { get { return _resName; } set { SetResName(value); } }

        public string _resName;
        /// <summary>
        /// 资源实际的路径 存放在res_bundle下的路径（非AB路径）
        /// </summary>
        public string ResPath { get { return _resName; } }

        public string _resPath;

        protected virtual void SetResName(string resName)
        {
            _resName = resName;
            SetResPath();
        }

        protected virtual void SetResPath()
        {

        }


    }

}

