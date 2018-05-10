using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// 动画文件内容优化
    /// </summary>
    public class AnimationSeoE
    {
        public const string FLOAT_FORMAT = "f3";                            // 裁剪精度
        public static string anim_directory = "Assets/Raw/Animation/";      // 优化的目录
        public const string SUFFIX_ANIM = "*.anim";                          // 文件后缀名

        //[MenuItem("Tools/优化/动画内存优化")]
        public static void AllSeoAnimation()
        {

            List<string> anim_paths = EPathHelper.GetAssetPathList(anim_directory, true, SUFFIX_ANIM);

            int length = anim_paths.Count;
            for (int i = 0; i < length; i++)
            {
                AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(anim_paths[i]);
                if (clip == null) continue;
                _seo_animation(clip);
                EditorUtility.DisplayProgressBar(anim_paths[i], string.Format("动画内存优化({0}/{1})", i + 1, length), (float)(i + 1) / length);
            }
            ShowDialog();
        }

        //[MenuItem("Assets/优化/动画内存优化")]
        public static void SeoAnimation()
        {
            AnimationClip clip = Selection.activeObject as AnimationClip;
            if (clip == null) return;
            _seo_animation(clip);
            ShowDialog();
        }

        #region private

        // 保存信息，并且弹出提示框
        public static void ShowDialog()
        {
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("", "动画内存优化完毕", "OK");
        }

        // 剔除Scale曲线和裁剪精度
        public static bool _seo_animation(AnimationClip anim_clip)
        {
            if (anim_clip == null) return false;
            bool result = false;
            try
            {
                // 去除scale曲线
                SeoAnimationScaleCurve(anim_clip);
                // 浮点数精度压缩到f3
                SeoAnimationFloat3(anim_clip);
                result = true;
                // 通过EditorCurveBinding得到的 Path 会不一样

            }
            catch (System.Exception e)
            {
                Debug.LogError(string.Format("剔除Scale数据并且浮点数精度压缩到f3失败: {0} error: {1}", anim_clip.name, e));
            }
            return result;
        }

        // 去除scale曲线
        public static void SeoAnimationScaleCurve(AnimationClip anim_clip)
        {
            if (anim_clip == null) return;
            //去除scale曲线
            foreach (EditorCurveBinding the_curve_binding in AnimationUtility.GetCurveBindings(anim_clip))
            {
                string name = the_curve_binding.propertyName.ToLower();
                if (name.Contains("scale"))
                {
                    AnimationUtility.SetEditorCurve(anim_clip, the_curve_binding, null);
                }
            }
        }

        // 浮点数精度压缩到f3
        public static void SeoAnimationFloat3(AnimationClip anim_clip)
        {
            AnimationUtility.GetCurveBindings(anim_clip);
            AnimationClipCurveData[] curves = AnimationUtility.GetAllCurves(anim_clip);

            int length = curves.Length;
            for (int k = 0; k < length; k++)
            {
                AnimationClipCurveData curve_data = curves[k];
                if (curve_data == null || curve_data.curve.keys == null) continue;
                Keyframe[] key_frames = curve_data.curve.keys;
                for (int i = 0; i < key_frames.Length; i++)
                {
                    Keyframe key_frame = key_frames[i];

                    key_frame.value = float.Parse(key_frame.value.ToString(FLOAT_FORMAT));
                    key_frame.inTangent = float.Parse(key_frame.inTangent.ToString(FLOAT_FORMAT));
                    key_frame.outTangent = float.Parse(key_frame.outTangent.ToString(FLOAT_FORMAT));

                    key_frames[i] = key_frame;
                }
                curve_data.curve.keys = key_frames;

                anim_clip.SetCurve(curve_data.path, curve_data.type, curve_data.propertyName, curve_data.curve);
            }
        }

        #endregion
    }
}

