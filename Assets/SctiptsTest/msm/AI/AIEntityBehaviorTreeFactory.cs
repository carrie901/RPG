using System.Collections;
using System.Collections.Generic;
using Summer.AI;
using UnityEngine;

namespace Summer.Test { 
public class AIEntityBehaviorTreeFactory  {

        private static TbAction bev_tree_demo1;
        static public TbAction GetBehaviorTreeDemo1()
        {
            if (bev_tree_demo1 != null)
            {
                return bev_tree_demo1;
            }
            bev_tree_demo1 = new TbActionPrioritizedSelector();
            bev_tree_demo1
                .AddChild(new TbActionSequence()
                    .SetPrecondition(new TbPreconditionNot(new CON_HasReachedTarget()))
                    .AddChild(new NOD_TurnTo())
                    .AddChild(new NOD_MoveTo()))
                .AddChild(new NOD_Attack());
            return bev_tree_demo1;
        }
    }
}