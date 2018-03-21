
namespace Summer
{
    public class PoolBaseSimpleObject : PoolBase
    {
        public PoolBaseSimpleObject(I_ObjectFactory factory, int max_count = 0)
            : base(factory, max_count) { }
    }
}

