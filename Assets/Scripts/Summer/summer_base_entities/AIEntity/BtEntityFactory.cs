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

        private static BtAction bev_tree_demo2;
        public static BtAction GetBtDemo2()
        {
            if (bev_tree_demo2 != null)
            {
                return bev_tree_demo2;
            }

            // root
            bev_tree_demo2 = new BtActionPrioritizedSelector();
            {

                BtActionPrioritizedSelector patrol = new BtActionPrioritizedSelector();
                patrol.TbName = "巡逻";
                bev_tree_demo2.AddChild(patrol);
                {
                    // 视野内没有目标
                    // 随机走路
                }


                BtActionSequence attack_sequence = new BtActionSequence();
                attack_sequence.TbName = "攻击";
                bev_tree_demo2.AddChild(attack_sequence);
                {
                    BtActionSequence violent_attack = new BtActionSequence();
                    violent_attack.TbName = "狂暴攻击";
                    {
                        // 自身处于狂暴状态
                        // 移动
                        // 普攻
                    }

                    BtActionSequence normal_attack = new BtActionSequence();
                    normal_attack.TbName = "常规攻击";
                    // 视野内发现目标
                    // 目标是玩家

                    {
                        // 移动
                        // 普攻
                    }
                }

                BtActionPrioritizedSelector self_defense = new BtActionPrioritizedSelector();
                self_defense.TbName = "反击";
                bev_tree_demo2.AddChild(self_defense);
                {
                    // 反击
                    {
                        // 玩家被攻击
                        // 生命高于500
                        // 移动 攻击玩家
                    }
                    // 逃跑
                    {
                        // 玩家被攻击
                        // hp 低于500
                        // 在警戒线外
                    }
                }
            }

            return bev_tree_demo2;
        }
    }
}