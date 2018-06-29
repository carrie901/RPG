
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
using System.Collections.Generic;
namespace Summer
{


    #region Effect

    /// <summary>
    /// Effect
    /// </summary>
    [System.Serializable]
    public class EffectLogicInfo
    {
        public string des;                                          // 效果描述
        public string trigger_evt;                                  // 触发器事件
        public EffectConditionInfo condition;                       // 触发条件
        public TextNode target_select_node;                             // 过滤类型

        public E_EffectType effect_type;
        public TextNode node;
    }

    #endregion

    #region 触发条件

    /// <summary>
    /// 触发条件
    /// </summary>
    [System.Serializable]
    public class EffectConditionInfo
    {
        public string condition;                                    // 触发条件
        public string condition_data;                               // 触发条件参数
    }

    #endregion

    #region 目标过滤类型

    /// <summary>
    /// 目标过滤类型
    /// </summary>
    [System.Serializable]
    public class TargetSelectInfo
    {

        #region 属性

        public string target_select_type;                                       // 目标过滤类型
        public string target_select_data;

        #endregion

    }

    #endregion

    #region 单一效果


    //[System.Serializable]
    /*public class EffectAttributeInfo
    {
        public E_EntityAttributeType entity_attribute_type;     // 这个属性的类型
        public List<ValueInfo> values = new List<ValueInfo>(2);
        public string effect_type;
        public EffectAttributeInfo()
        {
            effect_type = Const.effect_types[0];
            values = new List<ValueInfo>();
        }
    }*/

    //[System.Serializable]
    /*public class EffectValueInfo
    {
        public string effect_type;
        public List<ValueInfo> values = new List<ValueInfo>(2);

        public EffectValueInfo()
        {
            effect_type = Const.effect_types[1];
        }
    }*/

    //[System.Serializable]
    /*public class EffectStateInfo
    {
        public string effect_type;
        public string state_type;                               // 百分比的数据源

        public EffectStateInfo()
        {
            effect_type = Const.effect_types[2];
        }
    }*/


    /// <summary>
    /// 数值
    /// </summary>
    //[System.Serializable]
    /*public class ValueInfo
    {
        public E_DataUpdateType data_update_type;               // false=固定值,true=百分比 百分比以需要/10000 只余下两位数
        //public string data_source_type;                         // 百分比的数据源
        public int value;
    }
*/
    #endregion
}

