namespace Summer
{
    /// <summary>
    /// 被动伤害效果
    /// 在收到伤害的时候，反伤给对方
    /// </summary>
    public class EffectPassiveDamage : EffectValue
    {

        /*public override void OnAttach(BaseEntity target)
        {
            base.OnAttach(target);
            if (_owner == null) return;
            _owner.RegisterHandler(E_AbilityTrigger.on_be_attack_damage, _on_def_damage);
        }

        public override void OnDetach()
        {
            base.OnDetach();
            _owner.UnRegisterHandler(E_AbilityTrigger.on_be_attack_damage, _on_def_damage);
        }*/

        //自身收到伤害，反弹一部分伤害给施法者
        /*public void _on_def_damage(EventSetData param)
        {
            CharOpertionCharInfo damage_info = param as CharOpertionCharInfo;
            if (damage_info == null) return;

            float tmp_curr = 0;
            float origin = damage_info.value;
            BuffHelper.Calc(origin, ref tmp_curr, _param._calc_type, _param._calc_data);
            DamageInterFace.CalculaterBuffDamage(damage_info._caster, (int)tmp_curr);
            Log("自身收到伤害,反弹一部分伤害给施法者--->收到伤害[0],反弹给对方伤害[1]", _region, origin, tmp_curr);
        }*/
    }
}

