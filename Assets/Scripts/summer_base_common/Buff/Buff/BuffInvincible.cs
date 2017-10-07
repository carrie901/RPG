using Summer;

namespace Summer
{
    public class InvincibleEventBuffData : EventBuffSetData
    {
        public bool is_invincible;
    }

    /// <summary>
    /// 无敌
    /// </summary>
    public class BuffInvincible : Buff
    {
        InvincibleEventBuffData data;
        public override void OnAttach(BaseEntities caster, BaseEntities target)
        {
            base.OnAttach(caster, target);
            if (data == null)
                data = BuffDataFactory.Push<InvincibleEventBuffData>();
            data.is_invincible = false;
            _target.RaiseEvent(E_BuffTrigger.on_trigger_invincible, data);
            LogManager.Log("开启超人模式");
        }
        public override void OnDetach()
        {
            data.is_invincible = true;
            _target.RaiseEvent(E_BuffTrigger.on_trigger_invincible, data);
            base.OnDetach();
            BuffDataFactory.Pop(data);
            data = null;
            LogManager.Log("取消超人模式");
        }
    }


}


