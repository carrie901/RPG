
namespace Summer
{
    public class SkillFinishAction : SkillNodeAction
    {
        public const string DES = "==释放当前技能状态==";
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

