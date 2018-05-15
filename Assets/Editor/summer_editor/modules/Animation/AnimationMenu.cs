
using UnityEditor;

namespace SummerEditor
{
    /// <summary>
    /// 后缀带Menu的基本属于编辑器入口 
    /// Helper 工具
    /// EXxxx 代表实质内容
    /// 
    /// 原则上是为了内聚，清晰的知道入口
    /// </summary>
    public class AnimationMenu
    {
        [MenuItem("Tools/Animation/分离动画(针对Fbx)")]
        public static void AllSeparationAnimationByFbx()
        {
            AnimationSeparationE.AllSeparationAnimationByFbx();
        }

        [MenuItem("Assets/Animation/分离动画(针对Fbx)")]
        public static void SeparationAnimationByFbx()
        {
            AnimationSeparationE.SeparationAnimationByFbx();
        }

        [MenuItem("Tools/Animation/动画内存优化(针对AnimationClip)")]
        public static void AllSeoAnimation()
        {
            AnimationSeoE.AllSeoAnimation();
        }

        [MenuItem("Assets/Animation/动画内存优化(针对AnimationClip)")]
        public static void SeoAnimation()
        {
            AnimationSeoE.SeoAnimation();
        }

        [MenuItem("Assets/Animation/动画预处理(针对角色Prefab")]
        public static void AnimationPreTreatment()
        {
            AnimationPreTreatmentE.AnimationPreTreatment();
        }
    }
}

