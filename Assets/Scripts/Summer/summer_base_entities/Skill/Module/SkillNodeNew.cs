using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summer
{
    public class SkillNodeNew : TreeNode
    {

        public PreconditionNode _precondition;
        public bool Evaluate(BlackBorad blackborad)
        {
            bool result = (_precondition == null || _precondition.IsTrue(blackborad)) && OnEvaluate(blackborad);
            return result;
        }

        #region

        public virtual bool OnEvaluate(BlackBorad blackborad)
        {
            return true;
        }

        #endregion
    }
}
