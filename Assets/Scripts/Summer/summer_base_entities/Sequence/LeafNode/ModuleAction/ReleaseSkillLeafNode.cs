

namespace Summer.Sequence
{
    /// <summary>
    /// 释放技能控制权，表示这时候，技能可以控制左右方向，
    /// </summary>
    public class ReleaseSkillLeafNode : SequenceLeafNode
    {
        public const string DES = "释放普攻攻击的控制";
        public override void OnEnter(BlackBoard blackboard)
        {
            LogEnter();

            RaiseEvent(E_EntityInTrigger.SKILL_RELEASE, null);
            Finish();
        }

        public override void OnExit(BlackBoard blackboard)
        {
            LogExit();
        }
        public override void SetConfigInfo(EdNode cnf)
        {

        }
        public override string ToDes()
        {
            return DES;
        }
    }
}