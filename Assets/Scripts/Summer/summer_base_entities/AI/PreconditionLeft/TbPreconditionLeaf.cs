using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.AI
{
    public abstract class TbPreconditionLeaf : BtPreconditionNode
    {
        public TbPreconditionLeaf() : base(0) { }
    }

    #region 一元

    /// <summary>
    /// 一元节点
    /// </summary>
    public abstract class TbPreconditionUnary : BtPreconditionNode
    {
        public TbPreconditionUnary(BtPreconditionNode lhs)
            : base(1)
        {
            AddChild(lhs);
        }
    }

    #endregion

    #region 二元

    /// <summary>
    /// 二元节点
    /// </summary>
    public abstract class TbPreconditionBinary : BtPreconditionNode
    {
        public TbPreconditionBinary(BtPreconditionNode lhs, BtPreconditionNode rhs)
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
    public class TbPreconditionTrue : TbPreconditionLeaf
    {
        public override bool IsTrue(BtWorkingData work_data)
        {
            return true;
        }
    }

    #endregion

    #region Not

    /// <summary>
    /// 结果取反
    /// </summary>
    public class TbPreconditionNot : TbPreconditionUnary
    {
        public BtPreconditionNode _first = null;
        public TbPreconditionNot(BtPreconditionNode lhs)
            : base(lhs)
        { }
        public override bool IsTrue(BtWorkingData word_data)
        {
            return !GetFirst().IsTrue(word_data);
        }

        public BtPreconditionNode GetFirst()
        {
            if (_first == null)
                _first = GetChild<BtPreconditionNode>(0);
            return _first;
        }
    }

    #endregion

    #region false

    /// <summary>
    /// 强制为假
    /// </summary>
    public class TbPreconditionFalse : TbPreconditionLeaf
    {
        public override bool IsTrue(BtWorkingData work_data)
        {
            return false;
        }
    }

    #endregion

    #region and

    public class TbPreconditionAnd : TbPreconditionBinary
    {
        public TbPreconditionAnd(BtPreconditionNode lhs, BtPreconditionNode rhs)
            : base(lhs, rhs) { }

        public override bool IsTrue(BtWorkingData work_data)
        {
            return GetChild<BtPreconditionNode>(0).IsTrue(work_data) &&
                   GetChild<BtPreconditionNode>(1).IsTrue(work_data);
        }
    }

    #endregion

    #region or

    public class TbPreconditionOr : TbPreconditionBinary
    {
        public TbPreconditionOr(BtPreconditionNode lhs, BtPreconditionNode rhs)
            : base(lhs, rhs) { }

        public override bool IsTrue(BtWorkingData work_data)
        {
            return GetChild<BtPreconditionNode>(0).IsTrue(work_data) ||
                   GetChild<BtPreconditionNode>(1).IsTrue(work_data);
        }
    }

    #endregion

    #region Xor 参加运算的两个二进位同号，则结果为0（假）；异号则为1（真）。即0∧0＝0，0∧1＝1，1∧1＝0

    public class TbPreconditionXor : TbPreconditionBinary
    {
        public TbPreconditionXor(BtPreconditionNode lhs, BtPreconditionNode rhs)
            : base(lhs, rhs) { }

        public override bool IsTrue(BtWorkingData work_data)
        {
            return GetChild<BtPreconditionNode>(0).IsTrue(work_data) ^
                   GetChild<BtPreconditionNode>(1).IsTrue(work_data);
        }
    }

    #endregion
}

