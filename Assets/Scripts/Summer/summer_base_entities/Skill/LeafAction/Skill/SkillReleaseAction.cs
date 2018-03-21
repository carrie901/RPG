
namespace Summer
{
    public class SkillReleaseAction : SkillNodeAction
    {
        public const string DES = "释放当前技能状态";
        public override void OnEnter()
        {
            LogEnter();
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

