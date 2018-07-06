
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
    /// <summary>
    /// 修改属性
    /// </summary>
    public class EffectAttribute : BaseEffect
    {
        public float _cumulative_data;
        public EffectAttributeParam _att_param = new EffectAttributeParam();                 // 每一个效果的参数

        public override void OnAttach()
        {
            base.OnAttach();
            _att_param.ParseParam(_info.effect_node);
        }

        public override void OnDetach()
        {
            base.OnDetach();
            _att_param.Clear();
            _att_param = null;
        }

        public override void ExcuteEffect(EventSetData data)
        {
            // 参数
            EventSetEffectData attribute_data = data as EventSetEffectData;
            EffectLog.Assert(attribute_data != null, "属性变更 参数类型不对[{0}]", data);
            if (attribute_data == null) return;

            // 属性和数值
            EntityAttributeProperty att_pro = attribute_data.entity.AttributeProp;

            // 1.更新的属性类型
            E_EntityAttributeType attribute_type = _att_param.entity_attribute_type;
            AttributeIntParam attribute_param = att_pro.FindAttribute(attribute_type);
            float old_value = attribute_param.Value;

            float new_value = 0;
            int length = _att_param.values.Count;
            for (int i = 0; i < length; i++)
            {
                float tmp_value = attribute_param.Value;
                // 2.更新的数据的类型(百分比、固定数值)
                E_DataUpdateType data_update_type = _att_param.values[i].data_type;
                // 3.数值
                float data_value = _att_param.values[i].value;

                // 4.计算
                ValueHelper.Calc(attribute_param, data_update_type, (int)data_value);

                new_value += (attribute_param.Value - tmp_value);
            }

            _cumulative_data += new_value;
            EffectLog.Log("执行计算--->属性:[{0}],before:[{1}],after:[{2}],change:[{3}]", attribute_type, old_value, attribute_param.Value, new_value);
        }

        public override void ReverseEffect(EventSetData data)
        {
            EventSetEffectData attribute_data = data as EventSetEffectData;
            EffectLog.Assert(attribute_data != null, "属性变更 参数类型不对[{0}]", data);
            if (attribute_data == null) return;

            // 属性和数值
            EntityAttributeProperty att_pro = attribute_data.entity.AttributeProp;

            // 1.更新的属性类型
            E_EntityAttributeType attribute_type = _att_param.entity_attribute_type;
            AttributeIntParam attribute_param = att_pro.FindAttribute(attribute_type);

            float old_value = attribute_param.Value;
            attribute_param.SetPlus(_cumulative_data);

            float change_value = attribute_param.Value - old_value;
            EffectLog.Log("属性变更回退--->属性:[{0}],before:[{1}],after:[{2}],change:[{3}]", attribute_type, old_value, attribute_param.Value, change_value);
        }
    }
}
