
namespace Summer
{
    public class PoolBaseSimpleObject : PoolBase
    {
        public PoolBaseSimpleObject(I_ObjectFactory factory, int maxCount = 0)
            : base(factory, maxCount) { }
    }
}

