using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using Object = UnityEngine.Object;

namespace SummerEditor
{
    public class EMemorySizeHelper
    {

        public static long GetMemorySize(Object o)
        {
            return Profiler.GetRuntimeMemorySizeLong(o);
        }

        public static float GetFileSize(string path)
        {
            if (!File.Exists(path)) return 0;
            FileInfo info = new FileInfo(path);
            return info.Length;
        }

        public static Assembly asm;
        public static MethodInfo get_animation_clip_stats;
        public static Type aniclipstats;
        public static FieldInfo size_info;

        public static long GetBlobSize(AnimationClip anim_clip)
        {
            if (asm == null)
                asm = Assembly.GetAssembly(typeof(Editor));
            if (get_animation_clip_stats == null)
                get_animation_clip_stats = typeof(AnimationUtility).GetMethod("GetAnimationClipStats", BindingFlags.Static | BindingFlags.NonPublic);
            if (aniclipstats == null)
                aniclipstats = asm.GetType("UnityEditor.AnimationClipStats");
            if (size_info == null)
                size_info = aniclipstats.GetField("size", BindingFlags.Public | BindingFlags.Instance);

            if (get_animation_clip_stats == null || size_info == null) return 0;
            var stats = get_animation_clip_stats.Invoke(null, new object[] { anim_clip });
            int blob_size = (int)(size_info.GetValue(stats));
            return blob_size;
        }

        public static string GetKb(int bytes)
        {
            string size = (bytes / 1024).ToString("f2") + " Kb";
            if ((bytes / 1024) > 1024)
                size = ((float)bytes / (1024 * 1024)).ToString("f2") + " Mb";
            return size;
        }


    }

}
