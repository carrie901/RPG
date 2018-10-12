
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

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class TextureFormatInfo
    {
        public string Path;
        public int MemSize { get; set; }
        public TextureImporterType ImportType { get; set; }                 // 图片类型
        public TextureImporterShape ImportShape { get; set; }               // 
        public bool ReadWriteEnable { get; set; }                           // 可以读写
        public bool MipmapEnable { get; set; }                              // true=开启mip
        public int MaxSize { get; set; }
        public bool IsSpriteTag { get; set; }
        public TextureImporterFormat IosFormat;
        public TextureImporterFormat AndroidFormat;
        public int Width;
        public int Height;
        public float AndroidSize;
        public float IosSize;
        private static int LoadCount;
        public static Dictionary<string, TextureFormatInfo> _dictTexInfo = new Dictionary<string, TextureFormatInfo>();
        public static TextureFormatInfo Create(string assetPath)
        {
            TextureFormatInfo info = GetInfo(assetPath);
            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;

            Debug.Assert(!(importer == null), "错误的地址:" + assetPath);
            if (importer == null) return null;

            // 1.初始化纹理格式化信息
            info.Path = importer.assetPath;
            info.ImportType = importer.textureType;
            info.ImportShape = importer.textureShape;
            info.ReadWriteEnable = importer.isReadable;
            info.MipmapEnable = importer.mipmapEnabled;
            info.IsSpriteTag = !string.IsNullOrEmpty(importer.spritePackingTag);
            info.AndroidFormat = GetPlatformTextureSettings(importer, AssetFormatConst.PLATFORM_ANDROID);
            info.IosFormat = GetPlatformTextureSettings(importer, AssetFormatConst.PLATFORM_IOS);

            // 计算纹理的大小内存占用
            Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(assetPath);
            info.Width = texture.width;
            info.Height = texture.height;
            info.AndroidSize = EMemorySizeHelper.CalculateTextureSizeBytes(texture, info.AndroidFormat);
            info.IosSize = EMemorySizeHelper.CalculateTextureSizeBytes(texture, info.IosFormat);
            info.MemSize = (int)(Mathf.Max(info.AndroidSize, info.IosSize));

            if (Selection.activeObject != texture)
            {
                Resources.UnloadAsset(texture);
            }

            if (++LoadCount % 256 == 0)
            {
                Resources.UnloadUnusedAssets();
            }

            return info;
        }

        public static TextureImporterFormat GetPlatformTextureSettings(TextureImporter importer, string platform)
        {
            TextureImporterPlatformSettings settingAndroid = importer.GetPlatformTextureSettings(platform);
            return settingAndroid.format;
        }

        public static TextureFormatInfo GetInfo(string assetPath)
        {
            TextureFormatInfo tInfo;
            if (!_dictTexInfo.TryGetValue(assetPath, out tInfo))
            {
                tInfo = new TextureFormatInfo();
                _dictTexInfo.Add(assetPath, tInfo);
            }
            return tInfo;
        }
    }
}
