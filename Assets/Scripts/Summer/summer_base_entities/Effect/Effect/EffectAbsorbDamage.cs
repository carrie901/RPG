using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 抵消伤害
    /// </summary>
    public class EffectAbsorbDamage : SEffect
    {
        public EffectValueData _param = new EffectValueData();
        public int left_absorb_damge;          // 剩余可以抵消的伤害

        public EventEntityOpertionData event_data;

        public override void ResetInfo(System.Object data)
        {
            event_data = data as EventEntityOpertionData;
        }

        public override void _on_parse()
        {
           /* //_region = (E_CharDataValueRegion)info.sub_type;
            _param.ParseParam(_cnf);

            // 1.找到要更新的属性
            PropertyIntParam property_value = _owner.property.FindProperty(E_CharAttributeRegion.max_hp);
            // 最大血量
            int max_hp = property_value.Value;

            float temp_absorb_damge = 0;
            // 3.自身最大血量 
            BuffHelper.Calc(max_hp, ref temp_absorb_damge, _param._calc_type, _param._calc_data);
            left_absorb_damge = (int)temp_absorb_damge;*/
        }

        public override bool _on_excute()
        {
            if (event_data == null)
            {
                Error("没有收到相关战斗双方信息的信息");
                return false;
            }

            if (left_absorb_damge == 0)
            {
                Error("抵消伤害已经结束，没有移除Effect");
                return false;
            }

            // 原始伤害
            int before = Mathf.Abs(event_data.value);
            // 抵消伤害
            int after = left_absorb_damge - before;

            // 完全抵消伤害
            if (after > 0)
            {
                left_absorb_damge = after;
                //TODO 原始伤害不明确 未知正负
                event_data.value = 0;
            }
            else
            {
                left_absorb_damge = 0;
                event_data.value = before - after;
            }


            //Log("Effect id:[{0}] excute---> absorb damage left:[{1}],before damage:[{2}],after damage[{3}]", _cnf.id, left_absorb_damge, before, event_data.value);
            event_data = null;
            if (left_absorb_damge <= 0)
                return true;
            else
                return false;
        }

        public override void _on_reverse()
        {
        }
    }
}

