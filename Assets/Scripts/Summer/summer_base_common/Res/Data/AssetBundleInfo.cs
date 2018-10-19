
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

using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    public class AssetBundleInfo : I_ObjectInfo
    {
        /// <summary>
        /// 这个路径
        ///     是资源路径比如 res_bundle/xxx/xxx.png 
        ///     //也可是资源包路径StreamingAssets/xxx/xxx.ab  
        ///     同样是ResName xxx
        /// </summary>
        public string Path { get { return _cnf.PackagePath; } }
        public int RefCount { get; private set; }
        public AssetBundlePackageCnf _cnf;
        public AssetBundle _assetbundle;
        public List<ObjectInfo> _abList = new List<ObjectInfo>(8);

        public AssetBundleInfo(AssetBundlePackageCnf cnf)
        {
            _cnf = cnf;
            RefCount = 0;
        }

        public bool InitAssetBundle(AssetBundle ab, Object[] objs)
        {
            ResLog.Assert(ab != null, "AssetBundleInfo InitAssetBundle 初始化失败:ab为空");
            if (ab == null) return false;
            _assetbundle = ab;
            _abList.Clear();
            int length = objs.Length;
            for (int i = 0; i < length; i++)
            {
                if (!_cnf._resNames.ContainsKey(objs[i].name))
                {
                    ResLog.Error("AssetBundleInfo InitAssetBundle 失败. 配置文件:[{0}]", objs[i].name);
                    continue;
                }
                ObjectInfo info = new ObjectInfo(objs[i], _cnf._resNames[objs[i].name]);
                _abList.Add(info);
            }
            return true;
        }

        public T GetAsset<T>(string resPath) where T : Object
        {
            ResLog.Assert(!(_assetbundle == null || string.IsNullOrEmpty(resPath)),
                "AssetBundleInfo GetAsset 失败。PackagePath:[{2}],AssetBundel:[{0}](true代表空),名字:[{1}]", _assetbundle == null, resPath, Path);
            if (_assetbundle == null || string.IsNullOrEmpty(resPath)) return null;


            ResToAssetBundleCnf resToAbCnf = AssetBundleManifestInfo.Instance.GetResToAb(resPath);
            if (resToAbCnf == null) return null;

            int length = _abList.Count;
            for (int i = 0; i < length; i++)
            {
                ObjectInfo tmpInfo = _abList[i];
                if (tmpInfo.Path != resToAbCnf.ResPath) continue;
                if (!tmpInfo.CheckType<T>()) continue;
                RefCount++;
                return tmpInfo.GetAsset<T>(String.Empty);
            }
            ResLog.Assert(false, "从资源主包[{0}]中找不到对应类型的AssetInfo:[{1}]的资源", _cnf.PackagePath, resToAbCnf.ResPath);
            return null;
        }

        public bool CheckObject(Object obj)
        {
            int length = _abList.Count;
            for (int i = 0; i < length; i++)
            {
                ObjectInfo tmpInfo = _abList[i];
                if (tmpInfo.CheckObject(obj)) return true;
            }

            return false;
        }
        public void UnRef(Object obj)
        {
            int length = _abList.Count;
            for (int i = 0; i < length; i++)
            {
                if (!_abList[i].CheckObject(obj)) continue;
                _abList[i].UnRef(null);
                RefCount--;
                break;
            }
        }

        public void UnRefByPath(string resPath)
        {
            int length = _abList.Count;
            for (int i = 0; i < length; i++)
            {
                if (_abList[i].Path != resPath) continue;
                _abList[i].UnRefByPath(null);
                RefCount--;
                break;
            }
        }

        public bool IsEmptyRef()
        {
            int length = _abList.Count;
            for (int i = 0; i < length; i++)
            {
                if (_abList[i].RefCount > 0)
                    return false;
            }
            return true;
        }

        public void UnLoad()
        {
            _abList.Clear();
            LogManager.Assert(_assetbundle != null, "AssetBundleInfo UnLoad. AssetBundle不能为空:[{0}]", _cnf.PackagePath);
            if (_assetbundle != null)
                _assetbundle.Unload(true);
            _assetbundle = null;
            _cnf = null;
        }
    }
}

