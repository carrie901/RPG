using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.AI
{

    public abstract class TbPrecondition : TbTreeNode
    {

        public TbPrecondition(int max_child_count)
            : base(max_child_count)
        {
        }

        public abstract bool IsTrue(TbWorkingData work_data);
    }
}