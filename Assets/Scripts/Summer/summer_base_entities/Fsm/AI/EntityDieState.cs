namespace Summer
{
    public class EntityDieState : EntityState
    {

        /*public BaseEntity _entity;
        public EntityDieState(BaseEntity entity)
        {
            _entity = entity;
        }*/

        public EntityDieState()
        {
            _state_id = E_StateId.die;
        }

        #region override fsm

        public override void DoBeforeEntering()
        {
            PlayAnimationEventData data = EventDataFactory.Pop<PlayAnimationEventData>();
            data.animation_name = "die";
            entity.RaiseEvent(E_EntityInTrigger.play_animation, data);
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