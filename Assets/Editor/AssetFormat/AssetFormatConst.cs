
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


namespace SummerEditor
{
    public class AssetFormatConst
    {
        #region 平台

        public const string PLATFORM_ANDROID = "Android";
        public const string PLATFORM_IOS = "iPhone";
        public const string PLATFORM_STANDALONES = "Standalones";

        #endregion

        #region 资源格式后缀

        public static string[] _textureExts = { ".tga", ".png", ".jpg", ".tif", ".psd", ".exr" };
        public static string[] _materialExts = { ".mat" };
        public static string[] _modelExts = { ".fbx", ".asset", ".obj" };
        public static string[] _animationExts = { ".anim" };
        public static string[] _metaExts = { ".meta" };
        public static string[] _shaderExts = { ".shader" };
        public static string[] _scriptExts = { ".cs" };
        public static string[] _prefabExts = { ".prefab" };

        #endregion

        public const string RULE_ASSET_PATH = "Assets/Editor/AssetFormat/AssetFormatRule.txt";                  // 导入的规则文件
        public const string PREFIX_SPRITE_TAG_AGRB_POT = "tex_";                                                // 针对Argb_pot的非UI图集图片的前缀
    }
}