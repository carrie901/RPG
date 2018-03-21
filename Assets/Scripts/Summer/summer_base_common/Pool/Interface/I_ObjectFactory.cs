namespace Summer
{
    /// <summary>
    /// 谁负责初始化重用对象 只负责创建
    /// </summary>
    public interface I_ObjectFactory
    {
        I_PoolObjectAbility Create();

        /// <summary>
        ///  Push的时候额外处理，实在处理不好这块内容放到上面地方
        /// </summary>
        /// <param name="ability"></param>
        void ExtraOpertion(I_PoolObjectAbility ability);

        string FactoryName { get; }
    }
}