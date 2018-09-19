
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
            public int _index;
            public string _type;
        }

        public List<EffectObject> _effList;


        public string GetType(int index)
        {
            if (_effList == null) return string.Empty;
            int length = _effList.Count;
            for (int i = 0; i < length; i++)
            {
                if (_effList[i]._index == index)
                {
                    return _effList[i]._type;
                }
            }
            Debug.Log("找不到对应的类型");
            return string.Empty;
        }
    }


}


