
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
        public static AssetBundleManifestInfo Instance = new AssetBundleManifestInfo();
        public static Dictionary<string, AssetBundleDepCnf> _depMap                         // 依赖表信息
              = new Dictionary<string, AssetBundleDepCnf>();

        public Dictionary<string, List<string>> _reverseDepMap
            = new Dictionary<string, List<string>>();                                       // 反依赖信息  知道自己的爸爸有谁
        public Dictionary<string, ResToAssetBundleCnf> _resMap                              // 资源-->Ab包 通过资源名得到对应的Ab包
           = new Dictionary<string, ResToAssetBundleCnf>();
        public Dictionary<string, AssetBundlePackageCnf> _packageMap                        // Ab包信息
          = new Dictionary<string, AssetBundlePackageCnf>();

        private AssetBundleManifestInfo() { }

        #region static public 

        // 得到文本List
        public static List<string[]> GetAbInfo(string assetName)
        {
            string text = LoadAsset(assetName);
            List<string[]> result = StringHelper.ParseData(text);
            return result;
        }

        // 加载指定路径下的文本
        private static string LoadAsset(string assetName)
        {
            string configPath = AssetBundleConst.GetAbResDirectory() + assetName;
            AssetBundle ab = AssetBundle.LoadFromFile(configPath);
            Object obj = ab.LoadAllAssets()[0];
            TextAsset textasset = obj as TextAsset;
            string result = textasset.text;
            ab.Unload(true);
            ab = null;
            return result;
        }

        #endregion

        #region public 
        /// <summary>
        /// 通过资源路径找到对应的AssetBundle的路径
        /// </summary>
        public ResToAssetBundleCnf GetResToAb(string resPath)
        {
            ResToAssetBundleCnf info;
            _resMap.TryGetValue(resPath, out info);
            ResLog.Assert(info != null, "根据包路径:[{0}]查找相关依赖包失败", resPath);
            return info;
        }

        public AssetBundleDepCnf GetDepsInfo(string packagePath)
        {
            AssetBundleDepCnf cnf;
            _depMap.TryGetValue(packagePath, out cnf);
            ResLog.Assert((cnf != null), "找不到路径:[{0}]的包的依赖信息(关于他儿子的)", packagePath);
            return cnf;
        }

        /// <summary>
        /// 通过包的路径找到他爸爸门的包路径
        /// </summary>
        public List<string> GetParentPaths(string packagePath)
        {
            List<string> parents;
            _reverseDepMap.TryGetValue(packagePath, out parents);
            //ResLog.Assert((parents != null), "找不到路径:[{0}]的包的依赖信息(关于他爸爸的、)", packagePath);
            return parents;
        }

        public AssetBundlePackageCnf GetPackageCnf(string packagePath)
        {
            AssetBundlePackageCnf info;
            _packageMap.TryGetValue(packagePath, out info);
            ResLog.Assert((info != null), "找不到路径:[{0}]的包的PackageInfo", packagePath);
            return info;
        }

        public AssetBundlePackageCnf GetPackageCnfByResPath(string resPath)
        {
            ResToAssetBundleCnf resInfo = GetResToAb(resPath);
            if (resInfo == null) return null;
            return GetPackageCnf(resInfo.PackagePath);
        }

        public void InitInfo()
        {
            _depMap.Clear();
            _packageMap.Clear();
            _resMap.Clear();

            List<string[]> depResult = GetAbInfo(AssetBundleConst.AssetbundleDepPath);
            int length = depResult.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleDepCnf dep = new AssetBundleDepCnf();
                dep.InitInfo(depResult[i]);
                _depMap.Add(dep.AbName, dep);
            }
            InitReverseDep();

            List<string[]> packageResult = GetAbInfo(AssetBundleConst.AssetbundlePackagePath);
            length = packageResult.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundlePackageCnf packageInfo = new AssetBundlePackageCnf(packageResult[i]);

                _packageMap.Add(packageInfo.PackagePath, packageInfo);
            }

            List<string[]> resResult = GetAbInfo(AssetBundleConst.AssetbundleResPath);
            length = resResult.Count;
            for (int i = 0; i < length; i++)
            {
                ResToAssetBundleCnf res = new ResToAssetBundleCnf(resResult[i]);
                _resMap.Add(res.ResPath, res);
            }
        }

        #endregion

        #region private 

        private void InitReverseDep()
        {
            _reverseDepMap.Clear();
            foreach (var info in _depMap)
            {
                AssetBundleDepCnf depInfo = info.Value;
                Dictionary<string, int> depMap = depInfo._childRef;
                foreach (var child in depMap)
                {
                    // 儿子路径
                    string childPath = child.Key;

                    if (!_reverseDepMap.ContainsKey(childPath))
                    {
                        List<string> parents = new List<string>(8);
                        _reverseDepMap.Add(childPath, parents);
                    }
                    _reverseDepMap[childPath].Add(depInfo.AbName);
                }
            }
        }

        #endregion

    }


}
