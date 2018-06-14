namespace Summer
{
    /// <summary>
    /// 找到离我们最近的的一个目标
    /// </summary>
    public class GetTargetNearLeafNode : SkillLeafNode
    {
        public const string DES = "找到离我们最近的的一个目标";
        public float ditance;

        public override void OnEnter(EntityBlackBoard blackboard)
        {
            LogEnter();
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


