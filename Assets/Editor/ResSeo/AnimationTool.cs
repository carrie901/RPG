using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine.Profiling;

//=============================================================================
/// Author : mashao
/// CreateTime : 2018-5-8 21:0:40
/// FileName : AnimationTool.cs
//=============================================================================

namespace SummerEditor
{

    public class EAnimationHelper
    {
        public static void GetSize(string path)
        {
            AnimationClip ani_clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
            var file_info = new System.IO.FileInfo(path);
            Debug.Log("FileSize:" + GetKb((int)file_info.Length));//FileSize
            Debug.Log("MemorySize:" + GetKb((int)GetMemorySize(ani_clip)));//MemorySize

            Assembly asm = Assembly.GetAssembly(typeof(Editor));
            MethodInfo get_animation_clip_stats = typeof(AnimationUtility).GetMethod("GetAnimationClipStats", BindingFlags.Static | BindingFlags.NonPublic);
            Type aniclipstats = asm.GetType("UnityEditor.AnimationClipStats");
            FieldInfo size_info = aniclipstats.GetField("size", BindingFlags.Public | BindingFlags.Instance);

            if (get_animation_clip_stats == null || size_info == null) return;
            var stats = get_animation_clip_stats.Invoke(null, new object[] { ani_clip });
            Debug.Log("BlobSize:" + GetKb((int)size_info.GetValue(stats)));//BlobSize
            
        }

        public static long GetMemorySize(AnimationClip anim_clip)
        {
            return Profiler.GetRuntimeMemorySizeLong(anim_clip);
        }

        public static string GetKb(int bytes)
        {
            string size = (bytes / 1024).ToString("f2") + " Kb";
            if ((bytes / 1024) > 1024)
                size = ((float)bytes / (1024 * 1024)).ToString("f2") + " Mb";
            return size;
            //string result = EditorUtility.FormatBytes(bytes);
            //return result;
        }

    }

    public class AnimationTool
    {
        public static float size = 0;
    }

    class AnimationOpt
    {
        static Dictionary<uint, string> _FLOAT_FORMAT;
        static MethodInfo getAnimationClipStats;
        static FieldInfo sizeInfo;
        static object[] _param = new object[1];

        static AnimationOpt()
        {
            _FLOAT_FORMAT = new Dictionary<uint, string>();
            for (uint i = 1; i < 6; i++)
            {
                _FLOAT_FORMAT.Add(i, "f" + i.ToString());
            }
            Assembly asm = Assembly.GetAssembly(typeof(Editor));
            getAnimationClipStats = typeof(AnimationUtility).GetMethod("GetAnimationClipStats", BindingFlags.Static | BindingFlags.NonPublic);
            Type aniclipstats = asm.GetType("UnityEditor.AnimationClipStats");
            sizeInfo = aniclipstats.GetField("size", BindingFlags.Public | BindingFlags.Instance);
        }

        public AnimationClip _clip;
        public string _anim_path;

        public string AnimPath { get { return _anim_path; } }
        public long OriginFileSize { get; private set; }
        public int OriginMemorySize { get; private set; }
        public int OriginInspectorSize { get; private set; }
        public long OptimizeFileSize { get; private set; }
        public int OptimizeMemorySize { get; private set; }
        public int OptimizeInspectorSize { get; private set; }

        public AnimationOpt(string path, AnimationClip clip)
        {
            _anim_path = path;
            _clip = clip;
            _GetOriginSize();
        }

        void _GetOriginSize()
        {
            OriginFileSize = _GetFileZie();
            OriginMemorySize = _GetMemSize();
            OriginInspectorSize = _GetInspectorSize();
            //LogOrigin();
        }

        void _GetOptSize()
        {
            OptimizeFileSize = _GetFileZie();
            OptimizeMemorySize = _GetMemSize();
            OptimizeInspectorSize = _GetInspectorSize();
            //LogOpt();
            LogDelta();
        }

        long _GetFileZie()
        {
            FileInfo fi = new FileInfo(_anim_path);
            return fi.Length;
        }

        int _GetMemSize()
        {
            return UnityEngine.Profiling.Profiler.GetRuntimeMemorySize(_clip);
        }

        int _GetInspectorSize()
        {
            _param[0] = _clip;
            var stats = getAnimationClipStats.Invoke(null, _param);
            return (int)sizeInfo.GetValue(stats);
        }

        void _OptmizeAnimationScaleCurve()
        {
            if (_clip != null)
            {
                //去除scale曲线
                foreach (EditorCurveBinding theCurveBinding in AnimationUtility.GetCurveBindings(_clip))
                {
                    string name = theCurveBinding.propertyName.ToLower();
                    if (name.Contains("scale"))
                    {
                        AnimationUtility.SetEditorCurve(_clip, theCurveBinding, null);
                        //Debug.LogFormat("关闭{0}的scale curve", _clip.name);
                    }
                }
            }
        }

        void _OptmizeAnimationFloat_X(uint x)
        {
            if (_clip != null && x > 0)
            {
                //浮点数精度压缩到f3
                AnimationClipCurveData[] curves = null;
                curves = AnimationUtility.GetAllCurves(_clip);
                Keyframe key;
                Keyframe[] keyFrames;
                string floatFormat;
                if (_FLOAT_FORMAT.TryGetValue(x, out floatFormat))
                {
                    if (curves != null && curves.Length > 0)
                    {
                        for (int ii = 0; ii < curves.Length; ++ii)
                        {
                            AnimationClipCurveData curveDate = curves[ii];
                            if (curveDate.curve == null || curveDate.curve.keys == null)
                            {
                                //Debug.LogWarning(string.Format("AnimationClipCurveData {0} don't have curve; Animation name {1} ", curveDate, animationPath));
                                continue;
                            }
                            keyFrames = curveDate.curve.keys;
                            for (int i = 0; i < keyFrames.Length; i++)
                            {
                                key = keyFrames[i];
                                key.value = float.Parse(key.value.ToString(floatFormat));
                                key.inTangent = float.Parse(key.inTangent.ToString(floatFormat));
                                key.outTangent = float.Parse(key.outTangent.ToString(floatFormat));
                                keyFrames[i] = key;
                            }
                            curveDate.curve.keys = keyFrames;
                            _clip.SetCurve(curveDate.path, curveDate.type, curveDate.propertyName, curveDate.curve);
                        }
                    }
                }
                else
                {
                    Debug.LogErrorFormat("目前不支持{0}位浮点", x);
                }
            }
        }

        public void Optimize(bool scale_opt, uint float_size)
        {
            if (scale_opt)
            {
                _OptmizeAnimationScaleCurve();
            }
            _OptmizeAnimationFloat_X(float_size);
            _GetOptSize();
        }

        public void Optimize_Scale_Float3()
        {
            Optimize(false, 3);
        }

        public void LogOrigin()
        {
            _logSize(OriginFileSize, OriginMemorySize, OriginInspectorSize);
        }

        public void LogOpt()
        {
            _logSize(OptimizeFileSize, OptimizeMemorySize, OptimizeInspectorSize);
        }

        public void LogDelta()
        {

            AnimationTool.size += (OriginInspectorSize - OptimizeInspectorSize);
            _logSize(OriginFileSize - OptimizeFileSize, OriginMemorySize - OptimizeMemorySize, OriginInspectorSize - OptimizeInspectorSize);
            Debug.Log("originInspectorSize:" + OriginInspectorSize + "optInspectorSize:" + OptimizeInspectorSize);
        }

        void _logSize(long file_size, int mem_size, int inspector_size)
        {
            Debug.LogFormat("{0} \nSize=[ {1} ]", _anim_path, string.Format("FSize={0} ; Mem->{1} ; inspector->{2}",
                file_size / 1024, mem_size / 1024, inspector_size / 1024));
        }
    }

    public class OptimizeAnimationClipTool
    {
        static List<AnimationOpt> _AnimOptList = new List<AnimationOpt>();
        static List<string> _Errors = new List<string>();
        static int _Index = 0;

        [MenuItem("Assets/Animation/裁剪浮点数去除Scale")]
        public static void Optimize()
        {
            AnimationTool.size = 0;
            _AnimOptList = FindAnims();
            if (_AnimOptList.Count > 0)
            {
                _Index = 0;
                _Errors.Clear();
                EditorApplication.update = ScanAnimationClip;
            }
        }

        private static void ScanAnimationClip()
        {
            AnimationOpt _AnimOpt = _AnimOptList[_Index];
            bool isCancel = EditorUtility.DisplayCancelableProgressBar("优化AnimationClip", _AnimOpt.AnimPath, (float)_Index / (float)_AnimOptList.Count);
            _AnimOpt.Optimize_Scale_Float3();
            _Index++;
            if (isCancel || _Index >= _AnimOptList.Count)
            {
                EditorUtility.ClearProgressBar();
                Debug.Log("AnimationTool.size :" + AnimationTool.size / 1024);
                Debug.Log(string.Format("--优化完成--    错误数量: {0}    总数量: {1}/{2}    错误信息↓:\n{3}\n----------输出完毕----------", _Errors.Count, _Index, _AnimOptList.Count, string.Join(string.Empty, _Errors.ToArray())));
                Resources.UnloadUnusedAssets();
                GC.Collect();
                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
                EditorApplication.update = null;
                _AnimOptList.Clear();
                _cachedOpts.Clear();
                _Index = 0;
            }
        }

        static Dictionary<string, AnimationOpt> _cachedOpts = new Dictionary<string, AnimationOpt>();

        static AnimationOpt _GetNewAOpt(string path)
        {
            AnimationOpt opt = null;
            if (!_cachedOpts.ContainsKey(path))
            {
                AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
                if (clip != null)
                {
                    opt = new AnimationOpt(path, clip);
                    _cachedOpts[path] = opt;
                }
            }
            return opt;
        }

        static List<AnimationOpt> FindAnims()
        {
            string[] guids = null;
            List<string> path = new List<string>();
            List<AnimationOpt> assets = new List<AnimationOpt>();
            UnityEngine.Object[] objs = Selection.GetFiltered(typeof(object), SelectionMode.Assets);
            if (objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (objs[i].GetType() == typeof(AnimationClip))
                    {
                        string p = AssetDatabase.GetAssetPath(objs[i]);
                        AnimationOpt animopt = _GetNewAOpt(p);
                        if (animopt != null)
                            assets.Add(animopt);
                    }
                    else
                        path.Add(AssetDatabase.GetAssetPath(objs[i]));
                }
                if (path.Count > 0)
                    guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(AnimationClip).ToString().Replace("UnityEngine.", "")), path.ToArray());
                else
                    guids = new string[] { };
            }
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                AnimationOpt animopt = _GetNewAOpt(assetPath);
                if (animopt != null)
                    assets.Add(animopt);
            }
            return assets;
        }
    }

    public class EAnimationMenu
    {
        [MenuItem("Assets/内存/Animation内存")]
        public static void CalMemSize()
        {
            /* GameObject go = Selection.activeGameObject;
             if (go == null) return;*/

            string go_path = AssetDatabase.GetAssetPath(Selection.activeObject);
            EAnimationHelper.GetSize(go_path);
            Resources.UnloadUnusedAssets();
        }

        [MenuItem("Assets/内存/刷新")]
        public static void Refresh()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Resources.UnloadUnusedAssets();

        }
    }
}
