using UnityEngine;

//=============================================================================
// Author : mashao
// CreateTime : 2018-2-1 15:21:0
// FileName : SpriteUnUseConst.cs
//=============================================================================

namespace SummerEditor
{
    /// <summary>
    /// Sprite冗余扫描的Const
    /// </summary>
    public class SpriteUnUseConst
    {
        public static string[] ui_prefab_root_dirs = {
            Application.dataPath + "/Resources/prefab/ui",
            Application.dataPath + "/Resources/prefab/ui_eff",
            Application.dataPath + "/Resources/Item"
        };
    }
}
