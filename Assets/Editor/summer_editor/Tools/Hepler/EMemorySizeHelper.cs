using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using Object = UnityEngine.Object;
using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 计算内存占用大小，有待优化，动作文件，网络 ，纹理，音乐，文本 
    /// TODO 
    ///     通过EPathHelper的类型判断，然后计算内存占用大小
    /// </summary>
    public class EMemorySizeHelper
    {
        /// <summary>
        /// 计算内存占用计算Asset的内存大小
        /// </summary>
        public static long GetRuntimeMemorySize(Object o)
        {
            return Profiler.GetRuntimeMemorySizeLong(o);
        }

        /// <summary>
        /// 计算指定路径的文件大小 硬盘占用
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static float GetFileSize(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogError("找不到对应的文件，路径:" + path);
                return 0;
            }
            FileInfo info = new FileInfo(path);
            return info.Length;
        }

        #region 计算 AnimationClip内存

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

        #endregion 
        public static string GetKb(long bytes)
        {
            float size = bytes;
            return GetKb(size);
        }

        public static string GetKb(float bytes, bool show = true)
        {
            string size = string.Empty;
            if (show)
            {
                size = (bytes / 1024).ToString("f2") + " Kb";
                if ((bytes / 1024) > 1024)
                    size = ((float)bytes / (1024 * 1024)).ToString("f2") + " Mb";
            }
            else
            {
                size = (bytes / 1024).ToString("f2");
                if ((bytes / 1024) > 1024)
                    size = ((float)bytes / (1024 * 1024)).ToString("f2");
            }

            return size;
        }


        #region public

        #region 计算 纹理/模型/动作文件内存占用大小

        public static float CalculateRuntimeMemorySize(string assetPath)
        {
            Object o = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
            float mem = GetRuntimeMemorySize(o);
            //Resources.UnloadUnusedAssets();
            //Resources.UnloadAsset(o);
            return mem;
        }

        /// <summary>
        /// 计算纹理内存大小
        /// </summary>
        public static float CalculateTextureSizeBytes(string path)
        {
            // 1.得到纹理的设置类型
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            // 2.得到纹理
            Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(path);
            if (importer == null || texture == null)
            {
                Debug.Log(string.Format("路径:[{0}]不是纹理", path));
                return 0;
            }
            float ret_size = CalculateTextureSizeBytes(texture, importer.textureFormat);
            Resources.UnloadAsset(texture);

            return ret_size;
        }

        /// <summary>
        /// 计算动作文件大小
        /// </summary>
        public static float CalculateAnimationSizeBytes(string path)
        {
            float size = 0;
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);
            for (int i = 0; i < assets.Length; ++i)
            {
                if ((assets[i] is AnimationClip) && assets[i].name != EditorConst._editorAniclipName)
                {
                    size += GetRuntimeMemorySize(assets[i]);
                }
                if ((!(assets[i] is GameObject)) && (!(assets[i] is Component)))
                {
                    Resources.UnloadAsset(assets[i]);
                }
            }
            return size;
        }

        /// <summary>
        /// 计算模型Mesh大小
        /// </summary>
        public static float CalculateModelSizeBytes(string path)
        {
            float size = 0;
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);
            int length = assets.Length;
            for (int i = 0; i < length; ++i)
            {
                if (assets[i] is Mesh)
                {
                    size += GetRuntimeMemorySize(assets[i]);
                }
                if ((!(assets[i] is GameObject)) && (!(assets[i] is Component)))
                {
                    Resources.UnloadAsset(assets[i]);
                }
            }
            return size;
        }

        #endregion

        /// <summary>
        /// 查找某一个类型的Object
        /// </summary>
        public static List<System.Object> ToObjectList<T>(List<T> data)
        {
            if (data == null) return null;
            List<System.Object> ret = new List<System.Object>();
            int length = data.Count;
            for (int i = 0; i < length; ++i)
            {
                ret.Add(data[i]);
            }
            return ret;
        }
        #endregion

        #region private

        /// <summary>
        /// 纹理格式
        /// </summary>
        public static int GetBitsPerPixel(TextureImporterFormat format)
        {
            switch (format)
            {
                case TextureImporterFormat.Alpha8:          // Alpha-only texture format.
                    return 8;
                case TextureImporterFormat.RGB24:           // A color texture format.
                    return 24;
                case TextureImporterFormat.RGBA32:          // Color with an alpha channel texture format.
                    return 32;
                case TextureImporterFormat.ARGB32:          // Color with an alpha channel texture format.
                    return 32;
                case TextureImporterFormat.DXT1:            // Compressed color texture format.
                    return 4;
                case TextureImporterFormat.DXT5:            // Compressed color with alpha channel texture format.
                    return 8;
                case TextureImporterFormat.PVRTC_RGB2:      // PowerVR (iOS) 2 bits/pixel compressed color texture format.
                    return 2;
                case TextureImporterFormat.PVRTC_RGBA2:     // PowerVR (iOS) 2 bits/pixel compressed with alpha channel texture format
                    return 2;
                case TextureImporterFormat.PVRTC_RGB4:      // PowerVR (iOS) 4 bits/pixel compressed color texture format.
                    return 4;
                case TextureImporterFormat.PVRTC_RGBA4:     // PowerVR (iOS) 4 bits/pixel compressed with alpha channel texture format
                    return 4;
                case TextureImporterFormat.ETC_RGB4:        // ETC (GLES2.0) 4 bits/pixel compressed RGB texture format.
                    return 4;
                case TextureImporterFormat.ETC2_RGB4:
                    return 4;
                case TextureImporterFormat.ETC2_RGBA8:
                    return 8;
                case TextureImporterFormat.ATC_RGB4:        // ATC (ATITC) 4 bits/pixel compressed RGB texture format.
                    return 4;
                case TextureImporterFormat.ATC_RGBA8:       // ATC (ATITC) 8 bits/pixel compressed RGB texture format.
                    return 8;
                case TextureImporterFormat.AutomaticCompressed:
                    return 4;
                case TextureImporterFormat.AutomaticTruecolor:
                    return 32;
                case TextureImporterFormat.RGBA16:
                    return 16;
                default:
                    Debug.LogError("输出默认纹理格式" + format);
                    return 32;
            }
        }

        /// <summary>
        /// 计算纹理内存大小
        /// </summary>
        public static float CalculateTextureSizeBytes(Texture tTexture, TextureImporterFormat format)
        {
            var tWidth = tTexture.width;
            var tHeight = tTexture.height;
            if (tTexture is Texture2D)
            {
                var tTex2D = tTexture as Texture2D;
                var bitsPerPixel = GetBitsPerPixel(format);
                var mipMapCount = tTex2D.mipmapCount;
                var mipLevel = 1;
                var tSize = 0;
                while (mipLevel <= mipMapCount)
                {
                    tSize += tWidth * tHeight * bitsPerPixel / 8;
                    tWidth = tWidth / 2;
                    tHeight = tHeight / 2;
                    mipLevel++;
                }
                return tSize;
            }

            if (tTexture is Cubemap)
            {
                var bitsPerPixel = GetBitsPerPixel(format);
                return tWidth * tHeight * 6 * bitsPerPixel / 8f;
            }
            return 0;
        }

        #endregion

    }

}
