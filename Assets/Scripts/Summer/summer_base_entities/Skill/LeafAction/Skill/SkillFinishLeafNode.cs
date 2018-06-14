
namespace Summer
{
    public class SkillFinishLeafNode : SkillLeafNode
    {
        public const string DES = "==技能结束==";
        public override void OnEnter(EntityBlackBoard blackboard)
        {
            LogEnter();
            RaiseEvent(E_EntityInTrigger.skill_finish, null);
            Finish();
        }

        public override void OnExit(EntityBlackBoard blackboard)
        {
            LogExit();
        }

        public override string ToDes()
        {
            return DES;
        }
    }
}

