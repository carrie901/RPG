using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summer
{
    public abstract class PreconditionNode : TreeNode
    {
        public PreconditionNode(int max_child_count = -1)
            : base(max_child_count) { }

        public abstract bool IsTrue(BlackBoard blackboard);
    }
}
