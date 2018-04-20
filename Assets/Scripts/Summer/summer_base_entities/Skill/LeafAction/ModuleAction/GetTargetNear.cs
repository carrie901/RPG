using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 找到离我们最近的的一个目标
    /// </summary>
    public class GetTargetNear : SkillLeafNode
    {
        public float ditance;
        public override void OnEnter()
        {
            LogEnter();
        }

        public override void OnExit()
        {
            LogExit();
        }

        public override string ToDes()
        {
            throw new System.NotImplementedException();
        }
    }
}


