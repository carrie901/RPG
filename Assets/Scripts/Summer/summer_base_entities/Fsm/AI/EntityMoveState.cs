namespace Summer
{
    public class EntityMoveState : FsmState
    {
        public EntityMoveState()
        {
            _state_id = E_StateId.move;
        }


        #region override fsm

        public override void DoBeforeEntering()
        {
            entity.CanMovement = true;
            PlayAnimationEventData parm = EventDataFactory.Pop<PlayAnimationEventData>();
            parm.animation_name = "run";
            entity.RaiseEvent(E_EntityInTrigger.play_animation, parm);
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

