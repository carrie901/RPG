
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
    public abstract class PathTextureFilter : I_AssetFilter
    {
        public string _filterPath;
        public virtual void SetFilter(EdNode node)
        {
            _filterPath = node.GetAttribute(AssetImportConst.FILTER_PATH).ToStr();
        }

        public virtual bool IsMatch<T>(AssetImporter assetImport, T obj) where T : Object
        {
            string assetPath = assetImport.assetPath;
            bool mathPathResult = AssetImportHelper.IsMath(assetPath, _filterPath);
            if (!mathPathResult) return false;

            TextureImporter texImport = assetImport as TextureImporter;
            if (texImport == null) return false;

            Texture2D tex = obj as Texture2D;

            return IsInternalMatch(assetImport, tex);
        }

        public abstract bool IsInternalMatch(AssetImporter assetImport, Texture2D tex);


    }
}

