

namespace Summer
{
    public abstract class PreconditionNode : TreeNode
    {
        public PreconditionNode(int maxChildCount = -1)
            : base(maxChildCount) { }

        public abstract bool IsTrue(BlackBoard blackboard);
    }
}
