
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
using UnityEngine;

namespace Summer
{
    public class EffectScriptableObject : ScriptableObject
    {
        [System.Serializable]
        public class EffectObject
        {
            public int index;
            public string type;
        }

        public List<EffectObject> _eff_list;


        public string GetType(int index)
        {
            if (_eff_list == null) return string.Empty;
            for (int i = 0; i < _eff_list.Count; i++)
            {
                if (_eff_list[i].index == index)
                {
                    return _eff_list[i].type;
                }
            }
            Debug.Log("找不到对应的类型");
            return string.Empty;
        }
    }


}


