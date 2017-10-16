using Summer;

/// <summary>
/// 受到伤害的时候以一定的比例或者固定伤害反弹对手
/// </summary>
public class BuffPassiveDamage : Buff
{
    public override void Init(BuffCnf conf)
    {
        base.Init(conf);
        _param = new BuffParamData();
        _refresh_param();
    }

    public override void OnAttach(BaseEntities caster, BaseEntities target)
    {
        base.OnAttach(caster, target);
        _target.RegisterHandler(E_BuffTrigger.on_def_damage, _on_def_damage);
    }

    public override void OnDetach()
    {
        _target.UnRegisterHandler(E_BuffTrigger.on_def_damage, _on_def_damage);
        base.OnDetach();
    }

    public void _on_def_damage(EventBuffSetData param)
    {
        /*CharOpertionCharInfo damage_info = param as CharOpertionCharInfo;
        if (damage_info == null) return;
        
        // 1.计算实际的伤害值
        float temp = 0;
        BuffHelper.Calc(damage_info.value, ref temp, _param);

        //2.生成参数
        IntValueEventBuff data = new IntValueEventBuff();
        data.value = (int) temp;

        //2.触发回调
        damage_info._caster.RaiseEvent(E_BuffTrigger.on_buff_damage, data);
   
        LogManager.Log("{0}收到攻击伤害{1}，反弹对手{2}使其流血{3}", damage_info._target.ToString(), damage_info.value, damage_info._caster.ToString(), temp);
        */
    }

    public void _refresh_param()
    {
        _param.ParseParam(vbo.info.param1);
    }
}
