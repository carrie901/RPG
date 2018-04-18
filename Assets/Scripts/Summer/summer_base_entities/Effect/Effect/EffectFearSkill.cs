using UnityEngine;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2018-1-9 15:9:23
// FileName : EffectFearSkill.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 恐惧
    /// </summary>
    public class EffectFearSkill : EffectState
    {
        public override void _on_change_state()
        {
           /* _owner.inAfraid = true;
            Log("状态改变--->恐惧");*/
        }

        public override void _on_on_reverse_state()
        {
           /* _owner.inAfraid = false;
            Log("状态改变--->解除恐惧");*/
        }
    }
}
