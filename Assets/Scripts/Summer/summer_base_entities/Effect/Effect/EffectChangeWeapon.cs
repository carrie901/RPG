using UnityEngine;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2018-1-10 19:34:43
// FileName : EffectChangeWeapon.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 武器模型修改
    /// </summary>
	public class EffectChangeWeapon : EffectModelState
    {
        public override void _on_change_state()
        {
            Log("武器模型修改");
        }

        public override void _on_on_reverse_state()
        {
            Log("武器模型修改还原");
        }
    }
}
