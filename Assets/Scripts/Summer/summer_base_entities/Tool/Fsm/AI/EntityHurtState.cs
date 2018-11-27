

namespace Summer
{
    public class EntityHurtState : FsmState
    {
        public float _hurt_time = 1.5f;                // 受伤的时间


        public EntityHurtState()
        {
            _state_id = E_StateId.hurt;
        }
        #region override fsm

        public override void DoBeforeEntering()
        {
            //entity.CanMovement = false;
            _hurt_time = 0;
            EntityEventFactory.PlayAnimation(entity, AnimationNameConst.HIT);
        }

        public override void DoBeforeLeaving()
        {

        }

        public override void OnUpdate(float dt)
        {
            _hurt_time -= dt;
            if (_hurt_time <= 0)
            {
                EntityEventFactory.ChangeInEntityState(entity, E_StateId.idle);
            }
        }

        #endregion
    }
}
