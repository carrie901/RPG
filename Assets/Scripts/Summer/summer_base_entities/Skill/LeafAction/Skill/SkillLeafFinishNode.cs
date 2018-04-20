
namespace Summer
{
    public class SkillLeafFinishNode : SkillLeafNode
    {
        public const string DES = "==技能结束==";
        public override void OnEnter()
        {
            LogEnter();
            RaiseEvent(E_EntityInTrigger.skill_finish, null);
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

