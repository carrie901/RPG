
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
    public class AltasTextureRule : I_AssetRule
    {
        private static string PrefixName = "Altas";
        private static string SuffixName = "";
        public void ApplySettings<T>(AssetImporter assetImport, T obj) where T : Object
        {
            TextureImporter importer = assetImport as TextureImporter;

            if (importer == null) return;

            importer.textureType = TextureImporterType.Sprite;
            importer.textureShape = TextureImporterShape.Texture2D;
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.spriteImportMode = SpriteImportMode.Single;
            importer.sRGBTexture = true;
            importer.alphaIsTransparency = true;
            importer.wrapMode = TextureWrapMode.Repeat;
            importer.filterMode = FilterMode.Bilinear;
            importer.mipmapEnabled = false;
            importer.isReadable = false;
            string assetPath = assetImport.assetPath;
            string str = EPathHelper.GetDirectoryLast(assetPath);
            importer.spritePackingTag = PrefixName + str + SuffixName;
            AssetImportHelper.OverrideAndroid(importer, TextureImporterFormat.RGBA32);
            AssetImportHelper.OverrideIos(importer, TextureImporterFormat.RGBA32);

            importer.SaveAndReimport();
        }
    }
}