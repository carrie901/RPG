
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
namespace SummerEditor
{
    /// <summary>
    /// 应用规则
    /// </summary>
    public interface I_AssetRule
    {
        void ApplySettings<T>(AssetImporter assetImport, T obj) where T : UnityEngine.Object;
    }


    public interface I_AssetFilter
    {
        bool IsMatch<T>(T obj) where T : UnityEngine.Object;
    }

    public class AssetImportHelper
    {
        public static List<int> _pots = new List<int>() { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048 };
        public static bool IsMath(string assetPath, string filterPath)
        {
            if (string.IsNullOrEmpty(filterPath)) return true;
            if (assetPath.Contains(filterPath)) return true;
            return false;
        }

        // 拥有透明通道
        public static bool HasAlphaChannel(Texture2D tex_2_d)
        {
            for (int i = 0; i < tex_2_d.width; ++i)
            {
                for (int j = 0; j < tex_2_d.height; ++j)
                {
                    Color color = tex_2_d.GetPixel(i, j);
                    float alpha = color.a;
                    if (alpha < 1.0f - 0.001f)
                        return true;
                }
            }
            return false;
        }

        public static bool HasAlphaChannel(TextureImporter importer)
        {
            if (importer == null) return false;
            return importer.DoesSourceTextureHaveAlpha();
        }

        public static bool IsPot(Texture2D tex)
        {
            int width = tex.width, height = tex.height;
            if (!_pots.Contains(width) || !_pots.Contains(height)) return false;
            return true;
        }

        public static void ToRgba444(Texture2D texture)
        {
            /*            if (texture == null) return;
                        var texw = texture.width;
                        var texh = texture.height;

                        var pixels = texture.GetPixels();
                        var offs = 0;

                        var k1Per15 = 1.0f / 15.0f;
                        var k1Per16 = 1.0f / 16.0f;
                        var k3Per16 = 3.0f / 16.0f;
                        var k5Per16 = 5.0f / 16.0f;
                        var k7Per16 = 7.0f / 16.0f;

                        for (var y = 0; y < texh; y++)
                        {
                            for (var x = 0; x < texw; x++)
                            {
                                float a = pixels[offs].a;
                                float r = pixels[offs].r;
                                float g = pixels[offs].g;
                                float b = pixels[offs].b;

                                //裁剪颜色值从8bit到4bit
                                //设r=0.7843(200, 1100 1000)
                                //Mathf.Floor(a * 16) = 12即相当于r(1100 1000) >> 4 = 1100 = 0.8
                                //12 / 15 = 0.8 归一化
                                var a2 = Mathf.Clamp01(Mathf.Floor(a * 16) * k1Per15);
                                var r2 = Mathf.Clamp01(Mathf.Floor(r * 16) * k1Per15);
                                var g2 = Mathf.Clamp01(Mathf.Floor(g * 16) * k1Per15);
                                var b2 = Mathf.Clamp01(Mathf.Floor(b * 16) * k1Per15);

                                //得到差值-0.0157
                                var ae = a - a2;
                                var re = r - r2;
                                var ge = g - g2;
                                var be = b - b2;

                                //将当前点的颜色值改为裁剪后的
                                pixels[offs].a = a2;
                                pixels[offs].r = r2;
                                pixels[offs].g = g2;
                                pixels[offs].b = b2;

                                //由于pixels是一维数组, 所以+texw即往下移一行
                                //n1/2/3/4 分别为该点的左侧/左下/下方/右下的索引

                                var n1 = offs + 1;   // (x+1,y)
                                var n2 = offs + texw - 1; // (x-1 , y+1)
                                var n3 = offs + texw;  // (x, y+1)
                                var n4 = offs + texw + 1; // (x+1 , y+1)

                                //如果该点不是该行最后一个点，则左侧的点存在，乘以7/16修正之。
                                if (x < texw - 1)
                                {
                                    pixels[n1].a += ae * k7Per16;
                                    pixels[n1].r += re * k7Per16;
                                    pixels[n1].g += ge * k7Per16;
                                    pixels[n1].b += be * k7Per16;
                                }

                                //如果该点不是该列最后一个点，则下一行存在，下方的点存在，乘以5/16修正之。
                                if (y < texh - 1)
                                {
                                    pixels[n3].a += ae * k5Per16;
                                    pixels[n3].r += re * k5Per16;
                                    pixels[n3].g += ge * k5Per16;
                                    pixels[n3].b += be * k5Per16;

                                    //如果该点不是该行第一个点，则左下的点存在，乘以3/16修正之。
                                    if (x > 0)
                                    {
                                        pixels[n2].a += ae * k3Per16;
                                        pixels[n2].r += re * k3Per16;
                                        pixels[n2].g += ge * k3Per16;
                                        pixels[n2].b += be * k3Per16;
                                    }

                                    //如果该点不是该行最后一个点，则右下的点存在，乘以1/16修正之。
                                    if (x < texw - 1)
                                    {
                                        pixels[n4].a += ae * k1Per16;
                                        pixels[n4].r += re * k1Per16;
                                        pixels[n4].g += ge * k1Per16;
                                        pixels[n4].b += be * k1Per16;
                                    }
                                }

                                //读取下一个点
                                offs++;
                            }
                        }
                        texture.SetPixels(pixels);*/

            EditorUtility.CompressTexture(texture, TextureFormat.RGBA4444, TextureCompressionQuality.Best);
        }

        public static void OverrideAndroid(TextureImporter importer, TextureImporterFormat format)
        {
            TextureImporterPlatformSettings settingAndroid = importer.GetPlatformTextureSettings(AssetFormatConst.PLATFORM_ANDROID);
            settingAndroid.overridden = true;
            settingAndroid.format = format;
            settingAndroid.maxTextureSize = importer.maxTextureSize;
            importer.SetPlatformTextureSettings(settingAndroid);
        }

        public static void OverrideIos(TextureImporter importer, TextureImporterFormat format)
        {
            TextureImporterPlatformSettings settingIos = importer.GetPlatformTextureSettings(AssetFormatConst.PLATFORM_IOS);
            settingIos.overridden = true;
            settingIos.format = format;
            settingIos.maxTextureSize = importer.maxTextureSize;
            importer.SetPlatformTextureSettings(settingIos);
        }
    }

}