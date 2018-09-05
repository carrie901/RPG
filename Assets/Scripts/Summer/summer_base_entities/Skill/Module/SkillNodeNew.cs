
namespace Summer
{
    public class SkillNodeNew : TreeNode
    {

        public PreconditionNode _precondition;
        public bool Evaluate(BlackBoard blackborad)
        {
            bool result = (_precondition == null || _precondition.IsTrue(blackborad)) && OnEvaluate(blackborad);
            return result;
        }

        #region

        public virtual bool OnEvaluate(BlackBoard blackborad)
        {
            return true;
        }

        #endregion
    }
}
