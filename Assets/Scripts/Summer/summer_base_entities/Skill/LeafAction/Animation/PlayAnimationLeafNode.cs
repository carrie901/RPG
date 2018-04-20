
namespace Summer
{
    /// <summary>
    /// 播放角色动作
    /// </summary>
    public class PlayAnimationLeafNode : SkillLeafNode
    {
        public const string DES = "播放动作";
        public string animation_name;
        public override void OnEnter()
        {
            LogEnter();
            PlayAnimationEventData data = EventDataFactory.Pop<PlayAnimationEventData>();
            data.animation_name = animation_name;

            RaiseEvent(E_EntityInTrigger.play_animation, data);
            Finish();
        }

        public override void OnExit()
        {
            LogExit();
        }

        public override string ToDes()
        {
            return DES;
        }
    }

}
