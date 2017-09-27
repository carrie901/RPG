using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace SummerEditor
{
    public static class EditorTool
    {
        public static List<object> ToObjectList<T>(List<T> data)
        {
            if (data == null) return null;
            List<object> ret = new List<object>();
            for (int i = 0; i < data.Count; ++i)
            {
                ret.Add(data[i]);
            }
            return ret;
        }

        public static string GetCurrentBuildPlatform()
        {
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {
                return EditorConst.platform_android;
            }
            else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
            {
                return EditorConst.platform_ios;
            }
            else
            {
                return EditorConst.platform_android;
            }
        }

        public static int GetBitsPerPixel(TextureImporterFormat format)
        {
            switch (format)
            {
                case TextureImporterFormat.Alpha8: //	 Alpha-only texture format.
                    return 8;
                case TextureImporterFormat.RGB24: // A color texture format.
                    return 24;
                case TextureImporterFormat.RGBA32: //Color with an alpha channel texture format.
                    return 32;
                case TextureImporterFormat.ARGB32: //Color with an alpha channel texture format.
                    return 32;
                case TextureImporterFormat.DXT1: // Compressed color texture format.
                    return 4;
                case TextureImporterFormat.DXT5: // Compressed color with alpha channel texture format.
                    return 8;
                case TextureImporterFormat.PVRTC_RGB2: //	 PowerVR (iOS) 2 bits/pixel compressed color texture format.
                    return 2;
                case TextureImporterFormat.PVRTC_RGBA2: //	 PowerVR (iOS) 2 bits/pixel compressed with alpha channel texture format
                    return 2;
                case TextureImporterFormat.PVRTC_RGB4: //	 PowerVR (iOS) 4 bits/pixel compressed color texture format.
                    return 4;
                case TextureImporterFormat.PVRTC_RGBA4: //	 PowerVR (iOS) 4 bits/pixel compressed with alpha channel texture format
                    return 4;
                case TextureImporterFormat.ETC_RGB4: //	 ETC (GLES2.0) 4 bits/pixel compressed RGB texture format.
                    return 4;
                case TextureImporterFormat.ETC2_RGB4:
                    return 4;
                case TextureImporterFormat.ETC2_RGBA8:
                    return 8;
                case TextureImporterFormat.ATC_RGB4: //	 ATC (ATITC) 4 bits/pixel compressed RGB texture format.
                    return 4;
                case TextureImporterFormat.ATC_RGBA8: //	 ATC (ATITC) 8 bits/pixel compressed RGB texture format.
                    return 8;
#pragma warning disable 0618
                case TextureImporterFormat.AutomaticCompressed:
                    return 4;
                case TextureImporterFormat.AutomaticTruecolor:
                    return 32;
                default:
                    return 32;
#pragma warning restore 0618
            }
        }

        public static int CalculateTextureSizeBytes(Texture t_texture, TextureImporterFormat format)
        {
            var t_width = t_texture.width;
            var t_height = t_texture.height;
            if (t_texture is Texture2D)
            {
                var t_tex2_d = t_texture as Texture2D;
                var bits_per_pixel = GetBitsPerPixel(format);
                var mip_map_count = t_tex2_d.mipmapCount;
                var mip_level = 1;
                var t_size = 0;
                while (mip_level <= mip_map_count)
                {
                    t_size += t_width * t_height * bits_per_pixel / 8;
                    t_width = t_width / 2;
                    t_height = t_height / 2;
                    mip_level++;
                }
                return t_size;
            }

            if (t_texture is Cubemap)
            {
                var bits_per_pixel = GetBitsPerPixel(format);
                return t_width * t_height * 6 * bits_per_pixel / 8;
            }
            return 0;
        }

        //计算图片byte
        public static int CalculateTextureSizeBytes(string path)
        {
            int retSize = 0;
            Debug.Log("CalculateTextureSizeBytes Error");
            TextureImporter t_import = AssetImporter.GetAtPath(path) as TextureImporter;
            Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(path);
            if (t_import == null || texture == null) return 0;

            /*TextureImporterPlatformSettings setting = t_import.GetPlatformTextureSettings(GetCurrentBuildPlatform());


            if (!setting.overridden)
            {
                retSize = CalculateTextureSizeBytes(texture, tImport.textureFormat);
            }
            else
            {
                retSize = CalculateTextureSizeBytes(texture, setting.format);
            }

            Resources.UnloadAsset(texture); */

            return retSize;
        }
        //内存大小
        public static int GetRuntimeMemorySize(Object asset)
        {
            return UnityEngine.Profiling.Profiler.GetRuntimeMemorySize(asset);
        }
        //计算模型大小
        public static int CalculateModelSizeBytes(string path)
        {
            int size = 0;
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);
            for (int i = 0; i < assets.Length; ++i)
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
        //计算动作文件大小
        public static int CalculateAnimationSizeBytes(string path)
        {
            int size = 0;
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);
            for (int i = 0; i < assets.Length; ++i)
            {
                if ((assets[i] is AnimationClip) && assets[i].name != EditorConst.editor_aniclip_name)
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
        //创建文件夹
        public static void CreateDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public static T LoadJsonData<T>(string path)
        {
            /*try
            {
                if (!File.Exists(path))
                {
                    return default(T);
                }
                string str = File.ReadAllText(path);
                if (string.IsNullOrEmpty(str))
                {
                    return default(T);
                }
                T data = JsonMapper.ToObject<T>(str);
                if (data == null)
                {
                    Debug.LogError("Cannot read json data from " + path);
                }

                return data;
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                return default(T);
            }*/
            return default(T);
        }

        public static void SaveJsonData<T>(T data, string path)
        {
            Debug.Log("SaveJsonData Error");

            /*CreateDirectory(path);

            string jsonStr = JsonFormatter.PrettyPrint(JsonMapper.ToJson(data));
            File.WriteAllText(path, jsonStr);*/
        }

    }
}

