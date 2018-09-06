
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

namespace SummerEditor
{
    /// <summary>
    /// 路径/NPOT/ARGB
    /// </summary>
    public class ArgbNpotTextureFilter : PathTextureFilter
    {
        public override bool IsInternalMatch(AssetImporter assetImport, Texture2D tex)
        {
            if (AssetImportHelper.IsPot(tex)) return false;

            if (!AssetImportHelper.HasAlphaChannel(tex)) return false;
            
            return true;
        }
    }
}