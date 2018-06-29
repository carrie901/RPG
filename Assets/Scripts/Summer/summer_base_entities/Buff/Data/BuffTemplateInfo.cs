
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
    #region Buff的逻辑数据

    /// <summary>
    /// Buff的逻辑数据
    /// </summary>
    public class BuffTemplateInfo : ScriptableObject
    {
        public int id;                              // 模板id

        public string desc;                         // 模板描述
        public int duration;                        // 持续时间
        public int interval_time;                   // 间隔时间
        public int max_layer;                       // 最大层级
        public List<EffectLogicInfo> _effs
            = new List<EffectLogicInfo>();          // 包含的效果
    }

    #endregion
}
