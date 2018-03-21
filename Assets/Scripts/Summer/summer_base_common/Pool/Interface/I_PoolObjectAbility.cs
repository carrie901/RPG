namespace Summer
{
    /// <summary>
    /// 对象和池耦合的问题
    /// 1.对象需要知道自己在池之中，是否需要知道自己在哪一个池之中、或者说持有这个池子的引用
    /// </summary>
    public interface I_PoolObjectAbility
    {
        /// <summary>
        /// new分配 初始化
        /// </summary>
        void OnInit();

        /// <summary>
        /// 销毁
        /// </summary>
        void OnRecycled();

        /// <summary>
        /// 从池子中拿一个资源出来
        /// </summary>
        void OnPop();

        /// <summary>
        /// 把资源放到池子中
        /// </summary>
        void OnPush();

        /// <summary>
        /// 是否使用中
        /// </summary>
        bool IsUse { get; set; }

        /// <summary>
        /// 资源名
        /// </summary>
        string ObjectName { get; }
    }
}

