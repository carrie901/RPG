using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace SummerEditor
{
    public class FbxAnimationSeo
    {

        public static void Seo(GameObject go)
        {
         
            List<AnimationClip> animation_clip_list = new List<AnimationClip>(AnimationUtility.GetAnimationClips(go));
            if (animation_clip_list.Count == 0)
            {
                AnimationClip[] object_list = UnityEngine.Object.FindObjectsOfType(typeof(AnimationClip)) as AnimationClip[];
                animation_clip_list.AddRange(object_list);
            }

            foreach (AnimationClip animation_clip in animation_clip_list)
            {

                try
                {
                    EditorCurveBinding[] anim_curves = AnimationUtility.GetCurveBindings(animation_clip);
                    //去除scale曲线
                    foreach (EditorCurveBinding the_curve_binding in anim_curves)
                    {
                        string name = the_curve_binding.propertyName.ToLower();
                        if (name.Contains("scale"))
                        {
                            AnimationUtility.SetEditorCurve(animation_clip, the_curve_binding, null);
                        }
                    }

                    //浮点数精度压缩到f3
                    foreach (EditorCurveBinding curve_binding in anim_curves)
                    {
                        AnimationCurve curve_data = AnimationUtility.GetEditorCurve(animation_clip, curve_binding);

                        if (curve_data == null) continue;
                        Keyframe[] key_frames = curve_data.keys;
                        if (key_frames == null || key_frames.Length == 0) continue;
                        for (int i = 0; i < key_frames.Length; i++)
                        {
                            Keyframe key_frame = key_frames[i];
                            key_frame.value = float.Parse(key_frame.value.ToString("f3"));
                            key_frame.inTangent = float.Parse(key_frame.inTangent.ToString("f3"));
                            key_frame.outTangent = float.Parse(key_frame.outTangent.ToString("f3"));
                            key_frames[i] = key_frame;
                        }
                        curve_data.keys = key_frames;

                        animation_clip.SetCurve(curve_binding.path, curve_binding.type, curve_binding.propertyName, curve_data);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(string.Format("剔除Scale数据并且浮点数精度压缩到f3失败: {0} error: {1}", "", e));
                }
            }
        }
    }
}

