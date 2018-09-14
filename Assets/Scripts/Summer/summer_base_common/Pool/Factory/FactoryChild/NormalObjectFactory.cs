using System;

namespace Summer
{
    public class NormalObjectFactory : PoolObjectFactory
    {
        protected Func<I_PoolObjectAbility> _mFactoryMethod;
        public NormalObjectFactory(Func<I_PoolObjectAbility> factoryMethod, string name) : base(name)
        {
            _factoryName = name;
            _mFactoryMethod = factoryMethod;
        }



        public override I_PoolObjectAbility Create()
        {
            return _mFactoryMethod.InvokeGracefully();
        }

        public override void ExtraOpertion(I_PoolObjectAbility ability)
        {

        }
    }
}

