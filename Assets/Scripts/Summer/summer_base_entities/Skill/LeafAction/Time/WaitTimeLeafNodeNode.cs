namespace Summer
{
    /// <summary>
    /// 等待一定的时间之后发出时间
    /// </summary>
    public class WaitTimeLeafNodeNode : PlayBaseTimeLeafNode
    {
        public const string DES = "等待一定的时间之后发出时间";
        public string finish_event;
        public override void DoAction()
        {

        }

        public override void ReAction()
        {

        }

        public override string ToDes()
        {
            return DES;
        }
    }

}
