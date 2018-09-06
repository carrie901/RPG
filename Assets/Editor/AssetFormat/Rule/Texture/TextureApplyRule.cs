
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

namespace SummerEditor
{
    public static class TextureApplyRule
    {
        public static void ApplyFormat(TextureFormatInfo data)
        {
            TextureImporter importer = data._importer;
            if (importer == null) return;

            if (importer.textureType != data.TexType)
                importer.textureType = data.TexType;

            if (importer.textureShape != data.ShapeType)
                importer.textureShape = data.ShapeType;

            importer.isReadable = data.ReadWriteEnable;
            importer.mipmapEnabled = data.MipmapEnable;

            if (data.MaxSize > 0)
                importer.maxTextureSize = data.MaxSize;

            TextureImporterPlatformSettings settingAndroid = importer.GetPlatformTextureSettings(AssetImportConst.PLATFORM_ANDROID);
            settingAndroid.overridden = true;
            settingAndroid.format = data.AndroidFormat;
            settingAndroid.maxTextureSize = importer.maxTextureSize;
            importer.SetPlatformTextureSettings(settingAndroid);

            TextureImporterPlatformSettings settingIos = importer.GetPlatformTextureSettings(AssetImportConst.PLATFORM_IOS);
            settingIos.overridden = true;
            settingIos.format = data.IosFormat;
            settingIos.maxTextureSize = importer.maxTextureSize;
            importer.SetPlatformTextureSettings(settingIos);
            importer.SaveAndReimport();
        }
    }


    public class TextureFormatInfo
    {
        public TextureImporterType TexType { get; set; }                    // 图片类型
        public TextureImporterShape ShapeType { get; set; }                 // 
        public bool ReadWriteEnable { get; set; }                           // 可以读写
        public bool MipmapEnable { get; set; }                              // true=开启mip
        public int MaxSize { get; set; }
        public TextureImporterFormat IosFormat;
        public TextureImporterFormat AndroidFormat;

        public string _path;
        public TextureImporter _importer;



    }
}