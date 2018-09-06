
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
using System.IO;
using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    ///  默认走ETC2路线(RBG+A分离)，Read/Write不开启/设置Sprite_Atlas名字 名字加前缀tex_
    /// </summary>
    public class ArgbPotTexturteRule : I_AssetRule
    {
        public void ApplySettings<T>(AssetImporter assetImport, T obj) where T : UnityEngine.Object
        {
            TextureImporter importer = assetImport as TextureImporter;
            if (importer == null) return;

            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;

            //自动设置打包tag;
            string dirName = Path.GetDirectoryName(assetImport.assetPath);
            string folderStr = Path.GetFileName(dirName);
            importer.spritePackingTag = AssetImportConst.PREFIX_SPRITE_TAG_AGRB_POT + folderStr;

            // 关闭Read/Write和Mipmap
            importer.mipmapEnabled = false;
            importer.isReadable = false;
            importer.alphaIsTransparency = true;

            AssetImportHelper.OverrideAndroid(importer, TextureImporterFormat.ETC2_RGBA8);
            AssetImportHelper.OverrideIos(importer, TextureImporterFormat.PVRTC_RGBA4);
            importer.SaveAndReimport();
        }
    }
}