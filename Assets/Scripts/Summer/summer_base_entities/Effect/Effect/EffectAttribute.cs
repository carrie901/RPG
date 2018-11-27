
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
        public float _cumulativeData;
        public EffectAttributeParam _attParam = new EffectAttributeParam();                 // 每一个效果的参数

        public override void OnAttach()
        {
            base.OnAttach();
            _attParam.ParseParam(_info.effect_node);
        }

        public override void OnDetach()
        {
            base.OnDetach();
            _attParam.Clear();
            _attParam = null;
        }

        public override void ExcuteEffect(EventSetData data)
        {
            // 参数
            EventSetEffectData attributeData = data as EventSetEffectData;
            EffectLog.Assert(attributeData != null, "属性变更 参数类型不对[{0}]", data);
            if (attributeData == null) return;

            // 属性和数值
            EntityAttributeProperty attPro = attributeData._entity.AttributeProp;

            // 1.更新的属性类型
            E_EntityAttributeType attributeType = _attParam.entity_attribute_type;
            AttributeIntParam attributeParam = attPro.FindAttribute(attributeType);
            float oldValue = attributeParam.Value;

            float newValue = 0;
            int length = _attParam.values.Count;
            for (int i = 0; i < length; i++)
            {
                float tmpValue = attributeParam.Value;
                // 2.更新的数据的类型(百分比、固定数值)
                E_DataUpdateType dataUpdateType = _attParam.values[i].data_type;
                // 3.数值
                float dataValue = _attParam.values[i].value;

                // 4.计算
                ValueHelper.Calc(attributeParam, dataUpdateType, (int)dataValue);

                newValue += (attributeParam.Value - tmpValue);
            }

            _cumulativeData += newValue;
            EffectLog.Log("执行计算--->属性:[{0}],before:[{1}],after:[{2}],change:[{3}]", attributeType, oldValue, attributeParam.Value, newValue);
        }

        public override void ReverseEffect(EventSetData data)
        {
            EventSetEffectData attributeData = data as EventSetEffectData;
            EffectLog.Assert(attributeData != null, "属性变更 参数类型不对[{0}]", data);
            if (attributeData == null) return;

            // 属性和数值
            EntityAttributeProperty attPro = attributeData._entity.AttributeProp;

            // 1.更新的属性类型
            E_EntityAttributeType attributeType = _attParam.entity_attribute_type;
            AttributeIntParam attributeParam = attPro.FindAttribute(attributeType);

            float oldValue = attributeParam.Value;
            attributeParam.SetPlus(_cumulativeData);

            float changeValue = attributeParam.Value - oldValue;
            EffectLog.Log("属性变更回退--->属性:[{0}],before:[{1}],after:[{2}],change:[{3}]", attributeType, oldValue, attributeParam.Value, changeValue);
        }
    }
}
