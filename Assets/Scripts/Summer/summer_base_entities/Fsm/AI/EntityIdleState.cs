
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
            PlayAnimationEventData parm = EventDataFactory.Pop<PlayAnimationEventData>();
            parm.animation_name = "idle";
            entity.RaiseEvent(E_EntityInTrigger.play_animation, parm);

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
