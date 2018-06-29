
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

    /// <summary>
    /// 条件
    /// </summary>
    public interface I_TriggerCondition
    {
        bool IsTrue(EventSetData data);
    }
}
