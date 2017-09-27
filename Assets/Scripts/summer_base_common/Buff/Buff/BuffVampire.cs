using Summer;
/// <summary>
/// 吸血buff
/// 这里涉及到的是Buff的触发点问题，本身目前还没有抽象出角色
/// </summary>
public class BuffVampire : Buff
{
    public BuffParamData _param = new BuffParamData();

    public override void Init(BuffCnf conf)
    {
        base.Init(conf);
        _refresh_param();
    }

    public override void OnAttach(iCharacterBaseController caster, iCharacterBaseController target)
    {
        base.OnAttach(caster, target);
        _target.RegisterHandler(E_BuffTrigger.on_attack_enemy_damage, _on_attack_enemy);
    }

    public override void OnDetach()
    {
        _target.UnRegisterHandler(E_BuffTrigger.on_attack_enemy_damage, _on_attack_enemy);
        base.OnDetach();
    }

    public void _on_attack_enemy(EventBuffSetData param)
    {
        /*CharOpertionCharInfo opertion_info = param as CharOpertionCharInfo;
        if (opertion_info == null) return;

        // 1.如果不是伤害返回
        if (!opertion_info.is_damage()) return;
        // 2.得到伤害值
        int damage = (int)opertion_info.value;
        float temp = 0;
        // 3.计算吸血效果
        BuffHelper.Calc(damage, ref temp, _param);
        // 4.治疗自己
        IntValueEventBuff data = new IntValueEventBuff()
        {
            value = (int)temp,
        };

        _target.RaiseEvent(E_BuffTrigger.on_buff_health, data);

        LogManager.Log("{0}伤害[{1}]，获得[{2}]治疗量", _target.ToString(), damage, temp);*/
    }

    public void _refresh_param()
    {
        _param.ParseParam(_conf.param1);
    }
}
