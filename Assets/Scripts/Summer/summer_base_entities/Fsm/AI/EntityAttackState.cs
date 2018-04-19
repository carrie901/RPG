namespace Summer
{
    public class EntityAttackState : EntityState
    {
      
        public EntityAttackState()
        {
            _state_id = E_StateId.attack;
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

        #region private

        public void _check_trsnstion()
        {

        }

        #endregion
    }
}

