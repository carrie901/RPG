using Summer;

public class InvincibleEventBuffData : EventBuffSetData
{
    public bool is_invincible;
}

/// <summary>
/// 无敌
/// </summary>
public class BuffInvincible : Buff
{
    InvincibleEventBuffData data = new InvincibleEventBuffData();
    public override void OnAttach(iCharacterBaseController caster, iCharacterBaseController target)
    {
        base.OnAttach(caster, target);

        data.is_invincible = false;
        _target.RaiseEvent(E_BuffTrigger.on_trigger_invincible, data);
        LogManager.Log("开启超人模式");
    }
    public override void OnDetach()
    {
        data.is_invincible = true;
        _target.RaiseEvent(E_BuffTrigger.on_trigger_invincible, data);
        base.OnDetach();
        LogManager.Log("取消超人模式");
    }
}
