namespace Summer
{
    public class EntityAttackState : FsmState
    {
      
        public EntityAttackState()
        {
            _state_id = E_StateId.attack;
        }

        #region override fsm

        public override void DoBeforeEntering()
        {
            entity.CanMovement = false;
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

