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
            EntityEventFactory.PlayAnimation(entity, "run");
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

