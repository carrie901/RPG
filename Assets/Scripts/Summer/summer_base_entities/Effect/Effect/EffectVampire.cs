

namespace Summer
{
    /// <summary>
    /// 自身攻击造成目标造成的伤害
    /// 以伤害值的百分比或者固定值为自身恢复生命
    /// </summary>
    public class EffectVampire : SEffect
    {
        public EventEntityOpertionData event_data;
        public EffectValueData _value_data = new EffectValueData();
        public override void ResetInfo(System.Object data)
        {
            event_data = data as EventEntityOpertionData;
        }

        public override void _on_parse()
        {
            //_value_data.ParseParam(cnf.param1);
        }

        public override bool _on_excute()
        {
            _on_attack_enemy();
            return false;
        }

        public override void _on_reverse()
        {

        }

        //自身收到伤害的时候，回复血量
        public void _on_attack_enemy()
        {
           /* EventEntityOpertionData opertion_info = event_data as EventEntityOpertionData;
            if (opertion_info == null) return;

            // 1.如果不是伤害返回
            if (!opertion_info.is_damage()) return;

            float tmp_curr = 0;
            float origin = opertion_info.value;
            BuffHelper.Calc(origin, ref tmp_curr, _value_data._calc_type, _value_data._calc_data);
            DamageInterFace.CalculaterBuffTreatment(opertion_info._target, (int)tmp_curr);
            Log("自身攻击造成目标造成的伤害,以伤害值的百分比或者固定值为自身恢复生命--->对目标造成伤害[0],治疗自身[1]", origin, tmp_curr);*/
        }
    }

}
