

namespace Summer
{
    /// <summary>
    /// 释放技能控制权，表示这时候，技能可以控制左右方向，
    /// </summary>
    public class ReleaseSkillLeafNode : SkillLeafNode
    {
        public const string DES = "释放普攻攻击的控制";
        public override void OnEnter(EntityBlackBoard blackboard)
        {
            LogEnter();

            RaiseEvent(E_EntityInTrigger.skill_release, null);
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