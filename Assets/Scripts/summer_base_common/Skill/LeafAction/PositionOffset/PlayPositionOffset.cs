using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 位置相关的如：瞬移、冲撞、击退、跳跃等
    /// </summary>
    public class PlayPositionOffset : SkillNodeAction
    {
        public const string DES = "位置偏移";
        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }
        public override string ToDes() { return DES; }
    }
}

