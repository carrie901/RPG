namespace Summer
{
    /// <summary>
    /// 默认池子
    /// </summary>
    public class PoolBaseDefault : PoolBase
    {

        public PoolBaseDefault(I_ObjectFactory factory, int maxCount = 0) : base(factory, maxCount)
        {
        }
    }
}
