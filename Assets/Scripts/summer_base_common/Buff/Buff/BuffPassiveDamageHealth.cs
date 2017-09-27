using Summer;
/// <summary>
/// 被动回血技能
/// </summary>
public class BuffPassiveDamageHealth : Buff
{
    private BuffParamData _param = new BuffParamData();

    public override void Init(BuffCnf conf)
    {
        base.Init(conf);
        _refresh_param();
    }

    public override void OnAttach(iCharacterBaseController caster, iCharacterBaseController target)
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
        /*CharOpertionCharInfo opertion_info = param as CharOpertionCharInfo;
        if (opertion_info == null) return;

        // 1.不是伤害返回
        if (!opertion_info.is_damage()) return;

        // 2.得到当前的伤害
        int damage = (int)opertion_info.value;

        float temp = 0;

        // 3.计算回血效果
        BuffHelper.Calc(damage, ref temp, _param);
        // 4.治疗自己
        IntValueEventBuff data = new IntValueEventBuff()
        {
            value = (int)temp,
        };

        _target.RaiseEvent(E_BuffTrigger.on_buff_health, data);

        LogManager.Log("{0}收到攻击伤害{1}，回复{2}血量", _target.ToString(), damage, temp);*/
    }

    public void _refresh_param()
    {
        _param.ParseParam(_conf.param1);
    }
}
