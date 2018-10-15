
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

namespace Summer
{
    public class AssetBundleCompleteLoadOperation: ResLoadOpertion 
    {
        public AssetBundlePackageInfo _packageInfo;
        public AssetBundleCompleteLoadOperation(AssetBundlePackageInfo info)
        {
            _packageInfo = info;
        }

        #region 生命周期

        protected override void Init() { }

        protected override bool Update()
        {
            return true;
        }

        protected override void Complete() { }

        public override AssetInfo GetAsset<T>(string resPath)
        {
            AssetInfo assetInfo = _packageInfo.GetAsset<T>(resPath);
            return assetInfo;
        }

        #endregion
    }
}
