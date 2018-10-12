
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
    public class SpriteTagSet
    {
        protected static string[] _spritePath = new string[]
        {
            "Assets/UIResources/UITexture"
        };                                                              // 需要设置Sprite的Tag的地址

        private static string PrefixName = "Altas";
        private static string SuffixName = "";

        [MenuItem("策划/SpriteTag")]
        public static void Excute()
        {
            // 1.得到当前的Img路径
            int length = _spritePath.Length;
            List<string> imgFiles = new List<string>();
            for (int i = 0; i < length; i++)
            {
                List<string> tmpFiles = EPathHelper.GetAssetsPath(_spritePath[i], true);
                imgFiles.AddRange(tmpFiles);
            }

            // 2.根据路径设置Img的SpriteTag
            length = imgFiles.Count;
            for (int i = 0; i < length; i++)
            {
                EditorUtility.DisplayProgressBar("设置图片的SpriteTag", imgFiles[i], (float)(i) / length);
                SetImgTag(imgFiles[i]);
            }

            // 3.保存
            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
        }

        public static void SetImgTag(string assetPath)
        {
            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer == null) return;
            string str = EPathHelper.GetDirectoryLast(assetPath);
            importer.spritePackingTag = PrefixName + str + SuffixName;
        }


    }
}
