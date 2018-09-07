
namespace Summer.AI
{
    /// <summary>
    /// 前置节点
    /// </summary>
    public abstract class BtPreconditionNode : BtTreeNode
    {

        public BtPreconditionNode(int max_child_count)
            : base(max_child_count)
        {
        }

        public abstract bool IsTrue(BtWorkingData workData);
    }
}