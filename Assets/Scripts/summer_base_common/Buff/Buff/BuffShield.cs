using Summer;

/// <summary>
/// 护盾buff
/// </summary>
public class BuffShield : Buff
{
    BuffParamData _param = new BuffParamData();

    public float _real_data = 0;
    public override void Init(BuffCnf conf)
    {
        base.Init(conf);
        _refresh_param();
    }

    public override void OnAttach(iCharacterBaseController caster, iCharacterBaseController target)
    {
        base.OnAttach(caster, target);
        _target.RegisterHandler(E_BuffTrigger.on_def_damage_before, _on_def_damage_before);
    }

    public override void OnDetach()
    {
        _target.UnRegisterHandler(E_BuffTrigger.on_def_damage_before, _on_def_damage_before);
        base.OnDetach();
    }

    public void _on_def_damage_before(EventBuffSetData param)
    {
       /* CharOpertionCharInfo opertion_info = param as CharOpertionCharInfo;
        if (opertion_info == null) return;

        // 1.不是伤害返回
        if (!opertion_info.is_damage()) return;

        // 2.得到当前的伤害
        int damage = opertion_info.value;   //实际伤害
        int left_damage = damage;           //剩余伤害
        int offset = 0;                     //抵消的伤害
        bool result;
        // 3.计算护盾减免值

        if (_real_data >= damage)
        {
            _real_data = _real_data - damage;
            left_damage = 0;
            offset = damage;
            result = false;
        }
        else
        {
            left_damage = left_damage - (int)_real_data;
            _real_data = 0;
            offset = (int)_real_data;
            result = true;
        }


        // 4.护盾减免,实际伤害
        opertion_info.reset_damge(left_damage);

        LogManager.Log("{0}收到攻击伤害[{1}]，护盾抵消[{2}]剩余伤害[{3}],护盾生命[{4}]",
            opertion_info._target.ToString(),
            damage,
            offset,
            left_damage,
            _real_data);



        if (result)
        {
            _force_expire = true;
        }*/

    }

    public void _refresh_param()
    {
        _param.ParseParam(_conf.param1);

        //护盾的值
        BuffHelper.Calc(0, ref _real_data, _param);
    }
}
