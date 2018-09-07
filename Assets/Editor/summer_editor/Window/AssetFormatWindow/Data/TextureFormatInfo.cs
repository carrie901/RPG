
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
        public float MemSize { get; set; }
        public string MemSizeT { get; set; }
        public TextureImporterType ImportType { get; set; }                 // 图片类型
        public TextureImporterShape ImportShape { get; set; }               // 
        public bool ReadWriteEnable { get; set; }                           // 可以读写
        public bool MipmapEnable { get; set; }                              // true=开启mip
        public int MaxSize { get; set; }
        public TextureImporterFormat IosFormat;
        public TextureImporterFormat AndroidFormat;
        public int Width;
        public int Height;
        public float AndroidSize;
        public float IosSize;
        private static int m_load_count;
        public static Dictionary<string, TextureFormatInfo> _dictTexInfo = new Dictionary<string, TextureFormatInfo>();
        public static TextureFormatInfo Create(string assetPath)
        {

            TextureFormatInfo tInfo;
            if (!_dictTexInfo.TryGetValue(assetPath, out tInfo))
            {
                tInfo = new TextureFormatInfo();
                _dictTexInfo.Add(assetPath, tInfo);
            }
            TextureImporter tImport = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(assetPath);
            if (tImport == null || texture == null)
                return null;

            tInfo.Path = tImport.assetPath;
            tInfo.ImportType = tImport.textureType;
            tInfo.ImportShape = tImport.textureShape;
            tInfo.ReadWriteEnable = tImport.isReadable;
            tInfo.MipmapEnable = tImport.mipmapEnabled;
            TextureImporterPlatformSettings settingAndroid = tImport.GetPlatformTextureSettings(AssetFormatConst.PLATFORM_ANDROID);
            tInfo.AndroidFormat = settingAndroid.format;
            TextureImporterPlatformSettings settingIos = tImport.GetPlatformTextureSettings(AssetFormatConst.PLATFORM_IOS);
            tInfo.IosFormat = settingIos.format;
            tInfo.Width = texture.width;
            tInfo.Height = texture.height;
            tInfo.AndroidSize = EMemorySizeHelper.CalculateTextureSizeBytes(texture, tInfo.AndroidFormat);
            tInfo.IosSize = EMemorySizeHelper.CalculateTextureSizeBytes(texture, tInfo.IosFormat);
            tInfo.MemSize = Mathf.Max(tInfo.AndroidSize, tInfo.IosSize);
            tInfo.MemSizeT = EMemorySizeHelper.GetKb(tInfo.MemSize, true);
            if (Selection.activeObject != texture)
            {
                Resources.UnloadAsset(texture);
            }

            if (++m_load_count % 256 == 0)
            {
                Resources.UnloadUnusedAssets();
            }

            return tInfo;
        }
    }
}
