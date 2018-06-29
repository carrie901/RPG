
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
    public class AttributeEffect : BaseEffect
    {
        public float _cumulative_data;

        public AttributeEffect(I_Trigger entiry_trigger, EffectLogicInfo info)
            : base(entiry_trigger, info) { }

        public override void OnAttach()
        {
            _param.ParseParam(null);
        }

        public override void ExcuteEffect(EventSetData data)
        {
            // 参数
            EventSetEffectData attribute_data = data as EventSetEffectData;
            EffectLog.Assert(attribute_data == null, "属性变更 参数类型不对[{0}]", data);
            if (attribute_data == null) return;

            /*EffectAttributeInfo info = _info.eff_info as EffectAttributeInfo;
            if (info == null) return;

            // 属性和数值
            EntityAttributeProperty att_pro = attribute_data.entity.AttributeProp;

            // 1.更新的属性类型
            E_EntityAttributeType attribute_type = info.entity_attribute_type;
            AttributeIntParam attribute_param = att_pro.FindAttribute(attribute_type);
            float old_value = attribute_param.Value;

            float new_value = 0;
            for (int i = 0; i < info.values.Count; i++)
            {
                float tmp_value = 0;
                // 2.更新的数据的类型(百分比、固定数值)
                E_DataUpdateType data_update_type = info.values[i].data_update_type;
                // 3.数值
                float data_value = info.values[i].value;

                // 4.计算
                ValueHelper.Calc(attribute_param, data_update_type, (int)data_value);

                new_value += tmp_value;
            }

            _cumulative_data += new_value;
            EffectLog.Log("执行计算--->属性:[{0}],before:[{1}],after[{2}]", attribute_type, old_value, new_value);*/
        }

        public override void ReverseEffect(EventSetData data)
        {
            /*EffectLog.Log("属性变更回退-->");
            EventSetEffectData attribute_data = data as EventSetEffectData;
            EffectLog.Assert(attribute_data == null, "属性变更 参数类型不对[{0}]", data);
            if (attribute_data == null) return;

            EffectAttributeInfo info = _info.eff_info as EffectAttributeInfo;
            if (info == null) return;

            // 属性和数值
            EntityAttributeProperty att_pro = attribute_data.entity.AttributeProp;

            // 1.更新的属性类型
            E_EntityAttributeType attribute_type = info.entity_attribute_type;
            AttributeIntParam attribute_param = att_pro.FindAttribute(attribute_type);

            attribute_param.SetPlus(_cumulative_data);*/
        }

        public override void OnUpdate(float dt)
        {

        }
    }
}
