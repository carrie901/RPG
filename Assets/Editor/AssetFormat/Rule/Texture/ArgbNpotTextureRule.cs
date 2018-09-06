
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
    public class ArgbNpotTextureRule : I_AssetRule
    {
        public void ApplySettings<T>(AssetImporter assetImport, T obj) where T : UnityEngine.Object
        {
            TextureImporter importer = assetImport as TextureImporter;

            if (importer == null) return;

            importer.textureType = TextureImporterType.Default;
            importer.textureShape = TextureImporterShape.Texture2D;
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.mipmapEnabled = false;
            importer.isReadable = false;
            
            AssetImportHelper.OverrideAndroid(importer, TextureImporterFormat.ARGB16);
            AssetImportHelper.OverrideIos(importer, TextureImporterFormat.PVRTC_RGBA2);

            importer.SaveAndReimport();
        }
    }
}