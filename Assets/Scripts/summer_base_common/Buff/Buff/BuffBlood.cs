using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 回血Buff
    /// </summary>
    public class BuffBlood : BuffDataProcessValue
    {
        public override void Use()
        {
            base.Use();
            _blood();
        }

        public void _blood()
        {
            float tmp_blood = 0;

            float origin = 0;
            if (_param.by_value == E_CharDataByValue.current)
                origin = _target.FindValue(E_CharValueType.hp);
            else
                origin = _target.FindAttribute(E_CharAttributeType.max_hp).Value;

            BuffHelper.Calc(origin, ref tmp_blood, _param);
            IntValueEventBuff int_value = new IntValueEventBuff();
            int_value.value = (int)tmp_blood;
            _target.RaiseEvent(E_BuffTrigger.on_buff_health, int_value);
        }
    }
}
