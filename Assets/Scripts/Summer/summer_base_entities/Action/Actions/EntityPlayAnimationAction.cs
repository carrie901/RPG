namespace Summer
{
    public class EntityPlayAnimationAction : I_EntityAction
    {
        public void OnAction(BaseEntity entity, EventSetData param)
        {
            PlayAnimationEventData data = param as PlayAnimationEventData;
            if (data == null) return;
            entity.EntityController.anim_group.PlayAnimation(data.animation_name);
        }
    }
}

