
namespace Summer
{
    public abstract class PoolObjectFactory : I_ObjectFactory
    {

        protected string _factory_name;

        protected PoolObjectFactory(string factory_name)
        {
            _factory_name = factory_name;
        }

        public abstract I_PoolObjectAbility Create();

        public abstract void ExtraOpertion(I_PoolObjectAbility ability);

        public string FactoryName { get { return _factory_name; } }
    }
}

