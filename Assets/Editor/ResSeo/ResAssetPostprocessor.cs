
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace SummerEditor
{
    /*public class ResAssetPostprocessor : AssetPostprocessor
    {
        public static bool open = true;
        void OnPostprocessModel(GameObject g)
        {
            if (!open) return;
            List<AnimationClip> animation_clip_list = new List<AnimationClip>(AnimationUtility.GetAnimationClips(g));
            if (animation_clip_list.Count == 0)
            {
                AnimationClip[] object_list = UnityEngine.Object.FindObjectsOfType(typeof(AnimationClip)) as AnimationClip[];
                animation_clip_list.AddRange(object_list);
            }

            foreach (AnimationClip animation_clip in animation_clip_list)
            {

               
            }
        }
    }*/
}

