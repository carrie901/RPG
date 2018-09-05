namespace Summer.Sequence
{
    /// <summary>
    /// 找到离我们最近的的一个目标
    /// </summary>
    public class GetTargetNearLeafNode : SequenceLeafNode
    {
        public const string DES = "找到离我们最近的的一个目标";
        public float ditance;

        public override void OnEnter(BlackBoard blackboard)
        {
            LogEnter();
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


