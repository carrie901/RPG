namespace Summer
{
    /// <summary>
    /// 内部触发人物做某些事情 这个地方其实是有点奇怪的，如果说是触发做某些事情，其实本身必须持有相关性质的东西，才可以指定
    /// 如果本身已经持有了相关引用。那么为什么还需要通过事件机制来进行触发，不如直接进行调用呢
    /// 消除耦合的必要性质在哪里
    ///     相较于触发事件来驱动，直接调用相关的内容来说，后者耦合性更高一些，同时更加的不稳定(所谓的不稳定是指明确性，
    ///     只要持有这个接口，那么我就可以做接口提供的事情，但如果没有接口那么相对的我要持有的引用就特别多了。相当于对这些引用
    ///     做了一层包装)
    /// 
    /// </summary>
    public interface I_EntityInTrigger
    {
        //注册回调点
        bool RegisterHandler(E_EntityInTrigger key, EventSet<E_EntityInTrigger, EventSetData>.EventHandler handler);

        //卸载回调点
        bool UnRegisterHandler(E_EntityInTrigger key, EventSet<E_EntityInTrigger, EventSetData>.EventHandler handler);

        //触发回调点
        void RaiseEvent(E_EntityInTrigger key, EventSetData param);

        #region Test

        E_StateId GetState();


        #endregion
    }
}

