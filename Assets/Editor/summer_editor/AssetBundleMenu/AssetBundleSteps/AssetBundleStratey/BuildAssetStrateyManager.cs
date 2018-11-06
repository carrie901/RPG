
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

namespace SummerEditor
{
    public class BuildAssetStrateyManager
    {
        public static List<I_AssetBundleStratey> _strateys = new List<I_AssetBundleStratey>();

        public static void Init()
        {
            _strateys.Clear();
            _strateys.Add(new BuildAssetInSprite());
            _strateys.Add(new BuildAssetInOneShaderAb());
        }

        public static bool IsBundleStratey(EAssetObjectInfo info)
        {
            bool result = false;
            int count = 0;
            int length = _strateys.Count;
            for (int i = 0; i < length; i++)
            {
                if (_strateys[i].IsBundleStratey(info))
                {
                    result = true;
                    count++;
                }
            }
            if (count >= 1)
                UnityEngine.Debug.Assert(count != 1, "Build AssetBunlde 策略失败:" + info.AssetPath);
            return result;
        }

        public static void SetAssetBundleName()
        {
            int length = _strateys.Count;
            for (int i = 0; i < length; i++)
            {
                _strateys[i].SetAssetBundleName();
            }
        }
    }

}

