
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

namespace Summer
{
    #region Effect

    /// <summary>
    /// Effect
    /// </summary>
    [System.Serializable]
    public class EffectTemplateInfo
    {
        #region Node 参数

        #region 触发器 相关

        public const string TRIGGER_NODE_NAME = "TriggerNodeName";
        public const string TRIGGER_EVENT = "TriggerEvent";
        public const string TRIGGER_CONITION_EVNET = "TriggerConditionEvent";
        public const string TRIGGER_CONITION_PARAM = "";

        #endregion

        #region 属性

        public const string ATTRIBUTETYPE = "AttributeType";                // 属性类型
        public const string GROUP = "Group";                                // 数组
        public const string DATAUPDATETYPE = "DataUpdateType";              // 数值更新类型
        public const string VALUE = "Value";                                // 数值

        #endregion

        #endregion

        public E_EffectType effect_type;

        /// <summary>
        /// <Root>
        ///     TriggerEvent=触发事件
        ///     <Condition>
        ///         ConditonEvent=触发条件
        ///         <ConditionParam>
        ///             param1=
        ///         </ConditionParam>
        ///     </Condition>
        /// </Root>           
        /// </summary>
        public TextNode trigger_node;                               // 触发条件
        public TextNode target_select_node;                         // 过滤类型
        /// <summary>
        /// <Root>
        ///     AttributeType=属性类型
        ///     <Group>
        ///         DataUpdateType=plus
        ///         Value=1
        ///     </Group>
        ///     <Group>
        ///         DataUpdateType=multiply_plus
        ///         Value=1
        ///     </Group>
        /// </Root>
        /// </summary>
        public TextNode effect_node;

        #region 触发类型

        private E_Buff_Event _buff_evt;
        public E_Buff_Event GetBuffEvt()
        {
            if (_buff_evt == E_Buff_Event.none)
                _buff_evt = (E_Buff_Event)Enum.Parse(typeof(E_Buff_Event), GetTriggerString());
            return _buff_evt;
        }

        public string GetTriggerString()
        {
            if (trigger_node == null) return string.Empty;
            return trigger_node.GetAttribute(TRIGGER_EVENT);
        }


        #endregion

        #region effect_node 属性

        #endregion
    }

    #endregion
}

