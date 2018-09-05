
namespace Summer
{
    public abstract class PreConditionLeaf : PreconditionNode
    {
        public PreConditionLeaf() : base(0) { }
    }

    #region 一元

    /// <summary>
    /// 一元节点
    /// </summary>
    public abstract class PreconditionUnary : PreconditionNode
    {
        public PreconditionUnary(PreconditionNode lhs)
            : base(1) { AddChild(lhs); }
    }

    #endregion

    #region 二元

    /// <summary>
    /// 二元节点
    /// </summary>
    public abstract class PreconditionBinary : PreconditionNode
    {
        public PreconditionBinary(PreconditionNode lhs, PreconditionNode rhs)
            : base(2)
        {
            AddChild(lhs);
            AddChild(rhs);
        }
    }

    #endregion

    #region true

    /// <summary>
    /// 强制为真
    /// </summary>
    public class PreconditionTrue : PreconditionNode
    {
        public override bool IsTrue(BlackBoard blackboard)
        {
            return true;
        }
    }

    #endregion

    #region Not

    /// <summary>
    /// 结果取反
    /// </summary>
    public class PreconditionNot : PreconditionUnary
    {
        public PreconditionNode _first = null;
        public PreconditionNot(PreconditionNode lhs)
            : base(lhs)
        { }
        public override bool IsTrue(BlackBoard blackboard)
        {
            return !GetFirst().IsTrue(blackboard);
        }

        public PreconditionNode GetFirst()
        {
            if (_first == null)
                _first = GetChild<PreconditionNode>(0);
            return _first;
        }
    }

    #endregion

    #region false

    /// <summary>
    /// 强制为假
    /// </summary>
    public class PreconditionFalse : PreConditionLeaf
    {
        public override bool IsTrue(BlackBoard blackboard)
        {
            return false;
        }
    }

    #endregion

    #region and

    public class PreconditionAnd : PreconditionBinary
    {
        public PreconditionAnd(PreconditionNode lhs, PreconditionNode rhs)
            : base(lhs, rhs) { }

        public override bool IsTrue(BlackBoard blackboard)
        {
            return GetChild<PreconditionNode>(0).IsTrue(blackboard) &&
                   GetChild<PreconditionNode>(1).IsTrue(blackboard);
        }
    }

    #endregion

    #region or

    public class PreconditionOr : PreconditionBinary
    {
        public PreconditionOr(PreconditionNode lhs, PreconditionNode rhs)
            : base(lhs, rhs) { }

        public override bool IsTrue(BlackBoard blackboard)
        {
            return GetChild<PreconditionNode>(0).IsTrue(blackboard) ||
                   GetChild<PreconditionNode>(1).IsTrue(blackboard);
        }
    }

    #endregion

    #region Xor 参加运算的两个二进位同号，则结果为0（假）；异号则为1（真）。即0∧0＝0，0∧1＝1，1∧1＝0

    public class PreconditionXor : PreconditionBinary
    {
        public PreconditionXor(PreconditionNode lhs, PreconditionNode rhs)
            : base(lhs, rhs) { }

        public override bool IsTrue(BlackBoard blackboard)
        {
            return GetChild<PreconditionNode>(0).IsTrue(blackboard) ^
                   GetChild<PreconditionNode>(1).IsTrue(blackboard);
        }
    }

    #endregion

}
