
namespace Summer.AI
{
    /// <summary>
    /// 前置节点
    /// </summary>
    public abstract class BtPreconditionNode : BtTreeNode
    {

        public BtPreconditionNode(int maxChildCount)
            : base(maxChildCount)
        {
        }

        public abstract bool IsTrue(BtWorkingData workData);
    }
}