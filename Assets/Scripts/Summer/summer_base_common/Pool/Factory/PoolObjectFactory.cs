
namespace Summer
{
    public abstract class PoolObjectFactory : I_ObjectFactory
    {

        protected string _factoryName;

        protected PoolObjectFactory(string factoryName)
        {
            _factoryName = factoryName;
        }

        public abstract I_PoolObjectAbility Create();

        public abstract void ExtraOpertion(I_PoolObjectAbility ability);

        public string FactoryName { get { return _factoryName; } }
    }
}

