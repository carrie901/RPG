namespace Summer
{

    public interface I_PoolBase
    {
        /// <summary>
        /// 分配
        /// </summary>
        /// <returns></returns>
        I_PoolObjectAbility Pop();

        bool Push(I_PoolObjectAbility obj);

        void ReaycelAll();
    }
}
