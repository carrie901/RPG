using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.AI
{
    public abstract class TbPreconditionLeaf : TbPrecondition
    {
        public TbPreconditionLeaf() : base(0) { }
    }

    #region 一元

    public abstract class TbPreconditionUnary : TbPrecondition
    {
        public TbPreconditionUnary(TbPrecondition lhs)
            : base(1)
        {
            AddChild(lhs);
        }
    }

    #endregion

    #region 二元

    public abstract class TbPreconditionBinary : TbPrecondition
    {
        public TbPreconditionBinary(TbPrecondition lhs, TbPrecondition rhs)
            : base(2)
        {
            AddChild(lhs);
            AddChild(rhs);
        }
    }

    #endregion

    #region true

    public class TbPreconditionTrue : TbPreconditionLeaf
    {
        public override bool IsTrue( TbWorkingData work_data)
        {
            return true;
        }
    }

    #endregion

    #region Not

    public class TbPreconditionNot : TbPreconditionUnary
    {
        public TbPreconditionNot(TbPrecondition lhs)
            : base(lhs)
        { }
        public override bool IsTrue(TbWorkingData word_data)
        {
            return !GetChild<TbPrecondition>(0).IsTrue(word_data);
        }
    }

    #endregion

    #region false

    public class TbPreconditionFalse : TbPreconditionLeaf
    {
        public override bool IsTrue(TbWorkingData work_data)
        {
            return false;
        }
    }

    #endregion

    #region and

    public class TbPreconditionAnd : TbPreconditionBinary
    {
        public TbPreconditionAnd(TbPrecondition lhs, TbPrecondition rhs)
            : base(lhs, rhs){ }

        public override bool IsTrue(TbWorkingData work_data)
        {
            return GetChild<TbPrecondition>(0).IsTrue(work_data) &&
                   GetChild<TbPrecondition>(1).IsTrue(work_data);
        }
    }

    #endregion

    #region or

    public class TbPreconditionOr : TbPreconditionBinary
    {
        public TbPreconditionOr(TbPrecondition lhs, TbPrecondition rhs)
            : base(lhs, rhs){ }

        public override bool IsTrue(TbWorkingData work_data)
        {
            return GetChild<TbPrecondition>(0).IsTrue(work_data) ||
                   GetChild<TbPrecondition>(1).IsTrue(work_data);
        }
    }

    #endregion

    #region Xor 参加运算的两个二进位同号，则结果为0（假）；异号则为1（真）。即0∧0＝0，0∧1＝1，1∧1＝0

    public class TbPreconditionXor : TbPreconditionBinary
    {
        public TbPreconditionXor(TbPrecondition lhs, TbPrecondition rhs)
            : base(lhs, rhs){ }

        public override bool IsTrue(TbWorkingData work_data)
        {
            return GetChild<TbPrecondition>(0).IsTrue(work_data) ^
                   GetChild<TbPrecondition>(1).IsTrue(work_data);
        }
    }

    #endregion
}

