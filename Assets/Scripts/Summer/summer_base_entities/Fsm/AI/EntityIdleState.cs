
namespace Summer
{
    public class EntityIdleState : EntityState
    {

        /*public BaseEntity _entity;
        public EntityIdleState(BaseEntity entity)
        {
            _entity = entity;
        }*/
        public EntityIdleState()
        {
            _state_id = E_StateId.idle;
        }

        #region override fsm

        public override void DoBeforeEntering()
        {
            entity.CanMovement = true;
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
