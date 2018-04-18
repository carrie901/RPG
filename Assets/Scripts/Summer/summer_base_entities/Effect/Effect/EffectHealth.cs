
namespace Summer
{
    /// <summary>
    /// 治疗效果
    /// </summary>
    public class EffectHealth : SEffect
    {
        public EffectDamageHealthData _health_type = new EffectDamageHealthData();
        public int value;
        public override void _on_parse()
        {
            /*_health_type = (E_EffectDamgeAndhealth)cnf.sub_type;
            value = int.Parse(cnf.param1);*/
            _health_type.ParseParam(_cnf);
        }

        public override bool _on_excute()
        {
            /*//float health_value = EffectHelper.FindDamgeByType(_owner, _health_type, value);
            float health_value = _health_type._calc_data;
            float old_hp = _owner.Hp;

            DamageInterFace.CalculaterBuffTreatment(_owner, (int)health_value);
            Log("Effect Excute-->治疗效果,对目标[{0}]造成[{1}]治疗,原始血量:[{2}],现在血量[{3}]",
                _owner.CharacterTemplateID, health_value, old_hp, _owner.Hp);*/

            return false;

        }

        public override void _on_reverse()
        {
        }
    }
}

