
namespace Summer
{
    /// <summary>
    /// 被动回血 
    /// 自身收到伤害的时候，回复血量
    /// </summary>
    public class EffectPassiveDamageHealth : EffectValue
    {

        /*public override void OnAttach(BaseEntity target)
        {
            base.OnAttach(target);
            _owner.RegisterHandler(E_AbilityTrigger.on_be_attack_damage, _on_be_attack_damage);
        }

        public override void OnDetach()
        {
            base.OnDetach();
            _owner.UnRegisterHandler(E_AbilityTrigger.on_be_attack_damage, _on_be_attack_damage);
        }*/

        //自身收到伤害的时候，回复血量
        public void _on_be_attack_damage(EventSetData param)
        {
            /*CharOpertionCharInfo damage_info = param as CharOpertionCharInfo;
            if (damage_info == null) return;

            float tmp_curr = 0;
            float origin = damage_info.value;
            BuffHelper.Calc(origin, ref tmp_curr, _param._calc_type, _param._calc_data);
            DamageInterFace.CalculaterBuffTreatment(damage_info._caster, (int)tmp_curr);
            Log("自身收到伤害,回复一部分治疗给自身--->收到伤害[0],治疗给自身[1]", _region, origin, tmp_curr);*/
        }
    }

}
