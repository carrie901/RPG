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

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace ResourceFormat
{
    public class AnimationInfo
    {
        public string Path = "Unknown";
        public int MemSize = 0;
        public string MemSizeT;
        public ModelImporterAnimationType AnimationType = ModelImporterAnimationType.None;
        public ModelImporterAnimationCompression AnimationCompression = ModelImporterAnimationCompression.Off;

        public static AnimationInfo CreateAnimationInfo(string assetPath)
        {
            AnimationInfo mInfo = null;
            if (!m_dictMatInfo.TryGetValue(assetPath, out mInfo))
            {
                mInfo = new AnimationInfo();
                m_dictMatInfo.Add(assetPath, mInfo);
            }

            ModelImporter tImporter = AssetImporter.GetAtPath(assetPath) as ModelImporter;
            if (tImporter == null || tImporter.clipAnimations == null)
                return null;

            mInfo.Path = assetPath;
            mInfo.AnimationType = tImporter.animationType;
            mInfo.AnimationCompression = tImporter.animationCompression;

            mInfo.MemSize = 0;
            mInfo.MemSizeT = "等候计算";
            if (++m_loadCount % 256 == 0)
            {
                Resources.UnloadUnusedAssets();
            }

            return mInfo;
        }

        private static int m_loadCount = 0;
        private static Dictionary<string, AnimationInfo> m_dictMatInfo = new Dictionary<string, AnimationInfo>();
    }
}
