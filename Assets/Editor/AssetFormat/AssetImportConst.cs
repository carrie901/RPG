
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
    public class AssetImportConst
    {
        #region 平台

        public const string PLATFORM_ANDROID = "Android";
        public const string PLATFORM_IOS = "iPhone";
        public const string PLATFORM_STANDALONES = "Standalones";

        #endregion

        public const string RULE_ASSET_PATH = "Assets/Editor/AssetImport/rule.txt";                               // 导入的规则文件

        public const string PREFIX_SPRITE_TAG_AGRB_POT = "tex_";                        // 针对Argb_pot的非UI图集图片的前缀

        #region AssetImport 导入类型

        public const string TEXTURE = "Texture";
        public const string MODEL = "Model";
        #endregion

        #region EdNode


        #region Filter

        public const string FILTER_PATH = "Path";

        #endregion

        #endregion
    }
}