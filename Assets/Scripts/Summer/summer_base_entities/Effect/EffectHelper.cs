using UnityEngine;
using System.Collections;

namespace Summer
{
    public class EffectHelper
    {
        public const float BASE_VALUE = 100f;

        /// <summary>
        /// 根据伤害/治疗的类型 来找数据
        /// </summary>
        /// <param name="target"> 目标</param>
        /// <param name="type">固定值</param>
        /// <param name="origin">百分比</param>
        /// <returns></returns>
        public static float FindDamgeByType(BaseEntity target, E_EffectDamgeAndhealth type, float origin)
        {

            /*if (type == E_EffectDamgeAndhealth.fixation)
                return origin;
            float value = 0f;
            if (target == null)
            {
                SEffect.Error("找不到对应的伤害类型的效果-->目标为空");
                return value;
            }

            if (type == E_EffectDamgeAndhealth.atk)
            {
                value = target.property.FindProperty(E_CharAttributeRegion.atk).Value;
            }
            else if (type == E_EffectDamgeAndhealth.curr_hp)
            {
                value = target.property.GetHp();
            }
            else if (type == E_EffectDamgeAndhealth.max_hp)
            {
                value = target.property.FindProperty(E_CharAttributeRegion.max_hp).Value;
            }
            else
            {
                SEffect.Error("找不到对应的伤害类型的效果");
            }
            value = (value * origin) / BASE_VALUE;
            return value;*/
            return 0;
        }
    }
}

