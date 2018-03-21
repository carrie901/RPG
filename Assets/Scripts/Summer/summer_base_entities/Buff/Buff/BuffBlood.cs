
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

            float origin;
            BuffParamValueData value_param = _param as BuffParamValueData;
            if (value_param == null) return;

            if (value_param.by_value == E_CharDataByValue.current)
                origin = _target.FindValue(E_CharValueType.hp);
            else
                origin = _target.FindAttribute(E_CharAttributeType.max_hp).Value;

            BuffHelper.Calc(origin, ref tmp_blood, value_param);
            IntValueEventBuff int_value = new IntValueEventBuff();
            int_value.value = (int)tmp_blood;
            _target.RaiseEvent(E_BuffTrigger.on_buff_health, int_value);
        }
    }
}
