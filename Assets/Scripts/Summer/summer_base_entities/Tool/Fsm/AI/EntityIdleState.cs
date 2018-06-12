
namespace Summer
{
    public class EntityIdleState : FsmState
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
            EntityEventFactory.PlayAnimation(entity, "Idle");
        }

        public override void DoBeforeLeaving()
        {

        }

        public override void OnUpdate(float dt)
        {

        }

        #endregion
    }
}
