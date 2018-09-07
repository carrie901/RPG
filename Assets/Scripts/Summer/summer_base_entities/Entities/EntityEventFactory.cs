

namespace Summer
{
    public class EntityEventFactory
    {
        /// <summary>
        /// 内部触发事件，改变人物状态
        /// </summary>
        public static void ChangeInEntityState(I_EntityInTrigger entity, E_StateId state, bool force = false)
        {
            ChangeEntityStateEventData data = EventDataFactory.Pop<ChangeEntityStateEventData>();
            data.state_id = state;
            data.force = force;
            entity.RaiseEvent(E_EntityInTrigger.change_state, data);
        }


        public static void PlayAnimation(I_EntityInTrigger entity,string animationName)
        {
            PlayAnimationEventData data = EventDataFactory.Pop<PlayAnimationEventData>();
            data.animation_name = animationName;
            entity.RaiseEvent(E_EntityInTrigger.play_animation, data);
        }
    }
}

