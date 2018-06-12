
namespace Summer
{
    public class EntityFsmFactory
    {
        /// <summary>
        /// 这里的状态不是指的AI，而是不同的状态下，播放不同的动作,接受不同的输入，有不同的行为
        /// </summary>
        /// <returns></returns>
        public static FsmSystem CreateFsmSystem(BaseEntity entity)
        {
            FsmSystem fsm_system = new FsmSystem(entity);

            AddState<EntityIdleState>(entity, fsm_system, E_StateId.idle);
            AddState<EntityAttackState>(entity, fsm_system, E_StateId.attack);
            AddState<EntitySleepState>(entity, fsm_system, E_StateId.sleep);
            AddState<EntityMoveState>(entity, fsm_system, E_StateId.move);
            AddState<EntityDieState>(entity, fsm_system, E_StateId.die);
            AddState<EntitySkillState>(entity, fsm_system, E_StateId.skill);
            AddState<EntityHurtState>(entity, fsm_system, E_StateId.hurt);
            fsm_system.Start();
            return fsm_system;
        }

        public static void AddState<T>(BaseEntity entity, FsmSystem system, E_StateId state_id)
            where T : FsmState, new()
        {
            T t = new T();
            t.entity = entity;
            system.AddState(t);
        }

    }

}
