
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
    /// <summary>
    /// 对AssetBundleManifest 模拟
    /// 把相关的信息包含在这里，同时对外提供接口，可以介入不同程度的数据
    /// 
    /// 1.通过资源路径找到对应的AssetBundle的路径
    /// 2.通过AssetBundle的路径得到AssetBundlePackageInfo
    /// </summary>
    public class AssetBundleManifestInfo
    {
        public static Dictionary<string, AssetBundleDepInfo> _depMap                                // 依赖表信息
              = new Dictionary<string, AssetBundleDepInfo>();
        public static Dictionary<string, ResToAssetBundleCnf> _resMap                              // 资源-->Ab包 通过资源名得到对应的Ab包
           = new Dictionary<string, ResToAssetBundleCnf>();
        public static Dictionary<string, AssetBundlePackageCnf> _packageMap                        // Ab包信息
          = new Dictionary<string, AssetBundlePackageCnf>();

        /// <summary>
        /// 通过资源路径找到对应的AssetBundle的路径
        /// </summary>
        public ResToAssetBundleCnf GetResToAb(string resPath)
        {
            ResToAssetBundleCnf info;
            _resMap.TryGetValue(resPath, out info);
            ResLog.Assert((info != null), "找不到资源路径:[{0}]找不到对应的包", resPath);
            return info;
        }

        public AssetBundleDepInfo GetDepsInfo(string packagePath)
        {
            AssetBundleDepInfo info;
            _depMap.TryGetValue(packagePath, out info);
            ResLog.Assert((info != null), "找不到路径:[{0}]的包的依赖信息", packagePath);
            return info;
        }

        public AssetBundlePackageCnf GetPackageCnf(string packagePath)
        {
            AssetBundlePackageCnf info;
            _packageMap.TryGetValue(packagePath, out info);
            ResLog.Assert((info != null), "找不到路径:[{0}]的包的PackageInfo", packagePath);
            return info;
        }

        public Dictionary<string, int> GetDeps(string assetbundlePackagePath)
        {
            if (_depMap.ContainsKey(assetbundlePackagePath))
                return _depMap[assetbundlePackagePath]._childRef;

            ResLog.Error("dep_map不可能出现的情况，尼玛居然出现了[{0}]______", assetbundlePackagePath);
            return null;
        }


    }


}
