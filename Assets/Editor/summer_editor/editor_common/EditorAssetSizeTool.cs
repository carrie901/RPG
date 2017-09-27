using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace SummerEditor
{
    /// <summary>
    /// 计算Asset的大小
    /// </summary>
    public class EditorAssetSizeTool
    {
        //查找某一个类型的Object
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


        //计算纹理内存大小
        public static int CalculateTextureSizeBytes(string path)
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
            int ret_size = CalculateTextureSizeBytes(texture, importer.textureFormat);
            Resources.UnloadAsset(texture);

            return ret_size;
        }
        //计算纹理内存大小
        public static int CalculateTextureSizeBytes(Texture texture, TextureImporterFormat format)
        {
            int width = texture.width;
            int height = texture.height;
            if (texture is Texture2D)
            {
                Texture2D tex2_d = texture as Texture2D;
                int bits_per_pixel = GetBitsPerPixel(format);
                int mip_map_count = tex2_d.mipmapCount;

                int tmp_size = 0;
                if (mip_map_count != 1)
                    Debug.LogError(string.Format("图片的mip_map的等级:[{0}],计算内存有误", mip_map_count));

                tmp_size += width * height * bits_per_pixel / 8;
                return tmp_size;
            }
            else
            {
                Debug.LogError("计算的内容类型不是图片");
            }

            if (texture is Cubemap)
            {
                var bits_per_pixel = GetBitsPerPixel(format);
                return width * height * 6 * bits_per_pixel / 8;
            }
            return 0;
        }
        //纹理格式
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
                default:
                    return 32;
            }
        }
        //计算Asset的内存大小
        public static int GetRuntimeMemorySize(Object asset)
        {
            return UnityEngine.Profiling.Profiler.GetRuntimeMemorySize(asset);
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
        //计算模型Mesh大小
        public static int CalculateModelSizeBytes(string path)
        {
            int size = 0;
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
    }
}

