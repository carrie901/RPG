namespace Summer
{
    public class EntityDieState : FsmState
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
            EntityEventFactory.PlayAnimation(entity, AnimationNameConst.DIE);
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