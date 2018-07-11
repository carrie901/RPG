
namespace Summer
{
    /// <summary>
    /// 触发的前置条件
    /// </summary>
    public /*abstract*/ class SkillPrecondition
    {
        public E_SkillTransition _precondition;
        public SkillPrecondition(E_SkillTransition precondition)
        {
            SetPrecondition(precondition);
        }

        public void SetPrecondition(E_SkillTransition precondition)
        {
            _precondition = precondition;
        }

        public /*abstract*/ bool IsTrue(E_SkillTransition node_event)
        {
            if (_precondition == node_event)
                return true;
            return false;
        }
    }
}
