using System;

namespace Summer
{
    public class DefaultDataObjectFactory : PoolObjectFactory
    {
        protected Type _type;

        protected DefaultDataObjectFactory(string name) : base(name)
        {

        }
        public DefaultDataObjectFactory(Type t) : base(t.ToString())
        {
            _type = t;
        }

        public override I_PoolObjectAbility Create()
        {
            I_PoolObjectAbility po = Activator.CreateInstance(_type) as I_PoolObjectAbility;
            return po;
        }

        public override void ExtraOpertion(I_PoolObjectAbility ability)
        {

        }
    }

}
