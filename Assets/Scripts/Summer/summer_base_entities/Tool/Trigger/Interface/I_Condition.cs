
namespace Summer
{
    public interface I_Trigger
    {
        //注册回调点
        bool RegisterHandler(string key, EventSet<string, EventSetData>.EventHandler handler);

        //卸载回调点
        bool UnRegisterHandler(string key, EventSet<string, EventSetData>.EventHandler handler);

        //触发回调点
        void RaiseEvent(string key, EventSetData data);
    }


    public interface I_Buff
    {
        //注册回调点
        bool RegisterHandler(E_Buff_Event key, EventSet<E_Buff_Event, EventSetData>.EventHandler handler,I_Condition condition);

        //卸载回调点
        bool UnRegisterHandler(E_Buff_Event key, EventSet<E_Buff_Event, EventSetData>.EventHandler handler, I_Condition condition);

        //触发回调点
        void RaiseEvent(E_Buff_Event key, EventSetData data);
    }


    /// <summary>
    /// 条件
    /// </summary>
    public interface I_Condition
    {
        bool IsTrue(EventSetData data);
    }
}
