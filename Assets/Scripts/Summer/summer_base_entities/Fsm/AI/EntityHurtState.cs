

namespace Summer
{
    public class EntityHurtState : EntityState
    {

        /*public BaseEntity _entity;
        public EntityHurtState(BaseEntity entity)
        {
            _entity = entity;
        }*/
        public EntityHurtState()
        {
            _state_id = E_StateId.hurt;
        }
        #region override fsm

        public override void DoBeforeEntering()
        {

        }

        public override void DoBeforeLeaving()
        {

        }

        public override void OnUpdate()
        {

        }

        #endregion
    }
}
