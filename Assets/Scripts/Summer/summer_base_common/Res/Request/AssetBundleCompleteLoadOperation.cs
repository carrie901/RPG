
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
using UnityEngine;

namespace Summer
{
    public class AssetBundleCompleteLoadOperation<T> : ResLoadOpertion where T : Object
    {
        public string _resPath;
        public string _parentPath;
        public AssetBundle _assetbundle;
        public AssetBundleCompleteLoadOperation(AssetBundle ab, string resPath, string parentPath)
        {
            _assetbundle = ab;
            _resPath = resPath;
            _parentPath = parentPath;
        }

        protected override void Init()
        {

        }

        protected override bool Update()
        {
            return true;
        }

        protected override void Complete()
        {
            throw new System.NotImplementedException();
        }
    }


}
