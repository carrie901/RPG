
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
            FsmSystem fsmSystem = new FsmSystem(entity);

            AddState<EntityIdleState>(entity, fsmSystem, E_StateId.idle);
            AddState<EntityAttackState>(entity, fsmSystem, E_StateId.attack);
            AddState<EntitySleepState>(entity, fsmSystem, E_StateId.sleep);
            AddState<EntityMoveState>(entity, fsmSystem, E_StateId.move);
            AddState<EntityDieState>(entity, fsmSystem, E_StateId.die);
            AddState<EntitySkillState>(entity, fsmSystem, E_StateId.skill);
            AddState<EntityHurtState>(entity, fsmSystem, E_StateId.hurt);
            fsmSystem.Start();
            return fsmSystem;
        }

        public static void AddState<T>(BaseEntity entity, FsmSystem system, E_StateId stateId)
            where T : FsmState, new()
        {
            T t = new T();
            t.entity = entity;
            system.AddState(t);
        }

    }

}
