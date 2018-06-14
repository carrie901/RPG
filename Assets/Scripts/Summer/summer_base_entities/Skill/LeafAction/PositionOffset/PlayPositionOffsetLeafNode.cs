using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 位置相关的如：瞬移、冲撞、击退、跳跃等
    /// </summary>
    public class PlayPositionOffsetLeafNode : SkillLeafNode
    {
        public const string DES = "位置偏移";
        public Vector3 _target_post;
        public override void OnEnter(EntityBlackBoard blackboard)
        {

        }

        public override void OnExit(EntityBlackBoard blackboard)
        {

        }

        public override void OnUpdate(float dt, EntityBlackBoard blackboard)
        {
            //Finish();
            // 如果到达目标地点
            
        }

        public override string ToDes() { return DES; }
    }
}

