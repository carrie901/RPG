namespace Summer
{
    /// <summary>
    /// 对目标造成多少多少伤害
    /// </summary>
    public class EffectDamage : SEffect
    {
        public EffectDamageHealthData _damge_type = new EffectDamageHealthData();
        public int value;
        public override void _on_parse()
        {
            _damge_type.ParseParam(_cnf);
        }

        public override bool _on_excute()
        {
           /* // float damge = -EffectHelper.FindDamgeByType(_owner, _damge_type, value);
            float damage = _damge_type._calc_data;
            float old_hp = _owner.Hp;
            //Log("伤害类型[{0}],伤害参数[{1}],对目标[{2}]造成[{3}]伤害", _damge_type, value, _owner.CharacterTemplateID, damge);

            DamageInterFace.CalculaterBuffDamage(_owner, (int)damage, _buff);
            Log("Effect Excute-->伤害效果,对目标[{0}]造成[{1}]伤害,原始血量:[{2}],现在血量[{3}]",
                _owner.CharacterTemplateID, damage, old_hp, _owner.Hp);*/
            return false;
        }

        public override void _on_reverse()
        {


        }
    }

}

