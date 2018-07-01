
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

namespace Summer
{


    #region Effect

    /// <summary>
    /// Effect
    /// </summary>
    [System.Serializable]
    public class EffectTemplateInfo
    {
        public const string TRIGGER_EVENT = "TriggerEvent";

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
        public TextNode effect_node;

        public string GetTriggerEvent()
        {
            if (trigger_node == null) return string.Empty;
            return trigger_node.GetAttribute(TRIGGER_EVENT);
        }
    }

    #endregion
}

