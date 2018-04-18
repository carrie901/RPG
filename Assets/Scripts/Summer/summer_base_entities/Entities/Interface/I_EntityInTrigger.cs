namespace Summer
{
    /// <summary>
    /// 内部触发人物做某些事情 这个地方其实是有点奇怪的，如果说是触发做某些事情，其实本身必须持有相关性质的东西，才可以指定
    /// 如果本身已经持有了相关引用。那么为什么还需要通过事件机制来进行触发，不如直接进行调用呢
    /// 消除偶尔的必要性质在哪里
    /// </summary>
    public interface I_EntityInTrigger
    {
        //注册回调点
        bool RegisterHandler(E_EntityInTrigger key, EventSet<E_EntityInTrigger, EventSetData>.EventHandler handler);

        //卸载回调点
        bool UnRegisterHandler(E_EntityInTrigger key, EventSet<E_EntityInTrigger, EventSetData>.EventHandler handler);

        //触发回调点
        void RaiseEvent(E_EntityInTrigger key, EventSetData param);


        // 这个有严重违反依赖导致原则，会整体的破坏了接口
        BaseEntity GetEntity();
    }
}

