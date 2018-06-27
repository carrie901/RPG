
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

using UnityEngine;

namespace Summer
{
    public class EffectDesInfo: ScriptableObject
    {
        public string effect_type;                              // 效果类型

    }

    public class EffectAttributeInfo : EffectDesInfo
    {
        public bool is_fixed_value;                             // false=固定值,true=百分比 百分比以需要/10000 只余下两位数
        public string data_source_type;                         // 百分比的数据源

        public EffectAttributeInfo()
        {
            effect_type = Const.effect_types[0];
        }
    }

    public class EffectValueInfo : EffectDesInfo
    {
        public bool is_fixed_value;                             // false=固定值,true=百分比 百分比以需要/10000 只余下两位数
        public string data_source_type;                         // 百分比的数据源

        public EffectValueInfo()
        {
            effect_type = Const.effect_types[1];
        }
    }

    public class EffectStateInfo : EffectDesInfo
    {
        public string state_type;                               // 百分比的数据源

        public EffectStateInfo()
        {
            effect_type = Const.effect_types[2];
        }
    }

}