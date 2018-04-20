

namespace Summer
{
    public class ReleaseSkillLeafNode : SkillLeafNode
    {
        public const string DES = "释放普攻攻击的控制";
        public override void OnEnter()
        {
            LogEnter();

            RaiseEvent(E_EntityInTrigger.skill_release, null);
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