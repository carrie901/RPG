
namespace Summer
{
    public interface I_EntityTrigger
    {
        //注册回调点
        bool RegisterHandler(E_TRIGGER_EVT key, EventSet<E_TRIGGER_EVT, BlackBorad>.EventHandler handler);

        //卸载回调点
        bool UnRegisterHandler(E_TRIGGER_EVT key, EventSet<E_TRIGGER_EVT, BlackBorad>.EventHandler handler);

        //触发回调点
        void RaiseEvent(E_TRIGGER_EVT key, BlackBorad param);
    }

    /// <summary>
    /// 条件
    /// </summary>
    public interface I_TriggerCondition
    {
        bool IsTrue(BlackBorad blackboard);
    }

    /// <summary>
    /// 动作
    /// </summary>
    public interface I_ActionCondition
    {
        void Excute(BlackBorad blackboard);
    }
}
