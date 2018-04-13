using System;

namespace Summer
{
    public class NormalObjectFactory : PoolObjectFactory
    {
        protected Func<I_PoolObjectAbility> m_factory_method;
        public NormalObjectFactory(Func<I_PoolObjectAbility> factory_method, string name) : base(name)
        {
            _factory_name = name;
            m_factory_method = factory_method;
        }



        public override I_PoolObjectAbility Create()
        {
            return m_factory_method.InvokeGracefully();
        }

        public override void ExtraOpertion(I_PoolObjectAbility ability)
        {

        }
    }
}

