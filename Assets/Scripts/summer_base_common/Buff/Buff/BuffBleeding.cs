
namespace Summer
{
    public class IntValueEventBuff : EventBuffSetData
    {
        public int value;
    }


    /// <summary>
    /// 流血Buff
    /// </summary>
    public class BuffBleeding : BuffDataProcessValue
    {
        public override void Use()
        {
            base.Use();
            _damage();
        }

        public void _damage()
        {
            float tmp_damage = 0;

            float origin = 0;
            if (_param.by_value == E_CharDataByValue.current)
                origin = _target.FindValue(E_CharValueType.hp);
            else
                origin = _target.FindAttribute(E_CharAttributeType.max_hp).Value;

            BuffHelper.Calc(origin, ref tmp_damage, _param);
            IntValueEventBuff int_value = new IntValueEventBuff();
            int_value.value = (int)tmp_damage;
            _target.RaiseEvent(E_BuffTrigger.on_buff_damage, int_value);
        }
    }
}
