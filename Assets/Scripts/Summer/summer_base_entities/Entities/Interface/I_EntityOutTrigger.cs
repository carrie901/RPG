namespace Summer
{
    /// <summary>
    /// 挂载在角色身上的buff,以回调机制触发他们的效果
    /// 1.通过回调点触发，会导致Buff实际的逻辑在最终在角色身上,改变数据的入口只有一个
    /// 2.如果在Buff中调用处理一些比如掉血，乃至其他改变状态等等，发生需求变更的时候入口太多导致需要多处修改
    /// 3.完全把Buff从角色身上剥离处理，提炼角色以一个接口的性质注入到角色中去
    /// </summary>
    public interface I_EntityOutTrigger
    {
        //注册回调点
        bool RegisterHandler(E_EntityOutTrigger key, EventSet<E_EntityOutTrigger, EventSetData>.EventHandler handler);

        //卸载回调点
        bool UnRegisterHandler(E_EntityOutTrigger key, EventSet<E_EntityOutTrigger, EventSetData>.EventHandler handler);

        //触发回调点
        void RaiseEvent(E_EntityOutTrigger key, EventSetData param);


        // 这个有严重违反依赖导致原则，会整体的破坏了接口
        //BaseEntity GetEntity();
    }
}

