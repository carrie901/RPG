using System.Collections;
using System.Collections.Generic;
using Summer.AI;
using Summer.Test;
using UnityEngine;

namespace Summer.AI
{
    public class BtEntityFactory
    {

        private static BtAction bev_tree_demo1;
        static public BtAction GetBehaviorTreeDemo1()
        {
            if (bev_tree_demo1 != null)
            {
                return bev_tree_demo1;
            }
            // root
            bev_tree_demo1 = new BtActionPrioritizedSelector();
            {
                // 序列节点
                BtActionSequence sequence_01 = new BtActionSequence();
                bev_tree_demo1.AddChild(sequence_01);
                {
                    // 
                    TbPreconditionNot not = new TbPreconditionNot(new BtHasReachedTargetCondition());
                    sequence_01.SetPrecondition(not);
                    {
                        sequence_01.AddChild(new BtTurnToLeaf());
                        sequence_01.AddChild(new BtMoveToLeaf());
                    }
                }

                bev_tree_demo1.AddChild(new BtAttackLeaf());
            }
            return bev_tree_demo1;
        }
    }
}