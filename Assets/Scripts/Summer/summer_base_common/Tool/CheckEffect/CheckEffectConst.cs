using UnityEngine;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2018-2-1 11:30:0
// FileName : CheckEffectConst.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 检测特效性能
    /// 特效的一些路径信息
    /// </summary>
    public class CheckEffectConst
    {
        public static string effect_scene = @"Assets/Scene/Check/CheckEffect.unity";
        public static string effect_texture_report_path = "..\\three_check\\Effect_Texture_Report.csv";
        public static string effect_report_path = "..\\three_check\\Effect_Report.csv";
        // 特效路径
        public static string all_effect_path = Application.dataPath + "\\Resources\\GameObjectRes\\PrefabObject\\EffectPrefab\\Prefabs";
    }
}
