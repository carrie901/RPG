using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    /// <summary>
    /// AssetBundle包信息
    /// 这个AssetBundle
    ///     1.实际加载路径
    ///     2.相对路径
    ///     3.AssetBundle 资源
    ///     4.这个AssetBundle下的所有的AssetInfo
    /// </summary>
    public class AssetBundlePackageCnf
    {
        public static string _fullPath = Application.streamingAssetsPath + "/rpg/";

        #region 属性

        public string FullPath { get; private set; }                                            // 实际加载路径
        public string PackagePath { get; private set; }                              // 相对路径                                                     // 包的路径
        public bool IsComplete { get; private set; }                                            // 是否加载完成

        public AssetBundle _assetbundle;
        public Dictionary<string, string> _resPathMap = new Dictionary<string, string>();       // 资源的实际路径 Key=资源路径，value=资源的名称
        public Dictionary<string, string> _resNames = new Dictionary<string, string>();         // 资源的名称 key的资源的名称，Value=资源的路径
        public List<AssetInfo> _assetMap = new List<AssetInfo>();

        #region 引用信息

        public int RefCount { get; private set; }
        public List<string> _refList = new List<string>(4);

        #endregion

        #endregion

        #region 构造

        public AssetBundlePackageCnf(string[] infos)
        {
            PackagePath = infos[0];
            IsComplete = false;
            // TODO 可以优化
            FullPath = _fullPath + PackagePath;
            for (int i = 1; i < infos.Length; i = i + 2)
            {
                bool result = _resPathMap.ContainsKey(infos[i]);
                LogManager.Assert(!result, "初始化AssetBundle的依赖信息失败，[{0}]", infos[i]);
                if (result) continue;

                _resPathMap.Add(infos[i], infos[i + 1]);
                _resNames.Add(infos[i + 1], infos[i]);
            }
        }

        #endregion

        #region public 

        public void InitAssetBundle(AssetBundle ab, Object[] objs)
        {
            if (ab == null) return;
            ResLog.Assert(!IsComplete, "[{0}]已经初始化过了", PackagePath);
            _assetbundle = ab;
            IsComplete = true;

            _assetMap.Clear();
            int length = objs.Length;
            for (int i = 0; i < length; i++)
            {
                if (_resNames.ContainsKey(objs[i].name))
                {
                    AssetInfo info = new AssetInfo(objs[i], _resNames[objs[i].name]);
                    _assetMap.Add(info);
                }
                else
                {
                    Debug.LogErrorFormat("遗留问题，我也忘记了：[{0}]", objs[i].name);
                    //_fbx.Add(objs[i]);
                }
            }
        }

        public AssetInfo GetAsset(string assetName)
        {
            if (_assetbundle == null) return null;
            AssetInfo reAssetInfo = null;

            int length = _assetMap.Count;
            for (int i = 0; i < length; i++)
            {
                AssetInfo assetInfo = _assetMap[i];
                if (assetInfo.ResPath != assetName) continue;
                reAssetInfo = _assetMap[i];
            }
            ResLog.Assert((reAssetInfo != null), "从资源主包[{0}]中找不到对应的AssetInfo:[{1}]的资源", PackagePath, assetName);
            return reAssetInfo;
        }

        public bool HasAssetBundle(string resPath)
        {
            if (_resPathMap.ContainsKey(resPath))
                return true;
            return false;
        }

        public void UnLoad()
        {
            ResLog.Log("[{0}]引用--,Ref:[{1}]", PackagePath, RefCount);
            if (RefCount > 0) return;

            ResLog.Assert(RefCount == 0, "ab package ref error:[{0}]", RefCount);

            IsComplete = false;
            LogManager.Assert(_assetbundle != null, "不能为空[{0}]", PackagePath);
            if (_assetbundle == null) return;

            int length = _assetMap.Count;
            for (int i = 0; i < length; i++)
            {
                _assetMap[i].UnLoad();
            }
            _assetMap.Clear();
            _assetbundle.Unload(true);
            _assetbundle = null;
        }

        public bool IsDone()
        {
            return true;
        }

        #region

        public void RefParent(string parent_path)
        {
            if (string.IsNullOrEmpty(parent_path)) return;
            if (_refList.Contains(parent_path)) return;
            //bool result = _ref_list.Contains(parent_path);
            //LogManager.Assert(!result, "已经包含了相关资源,[{0}],爸爸是:[{1}]", PackagePath, parent_path);
            //if (result) return;
            _refList.Add(parent_path);
            RefCount++;
        }

        public void UnRef(string parent_path)
        {
            if (_refList.Contains(parent_path))
            {
                _refList.Remove(parent_path);
                RefCount--;
            }
        }

        #endregion

        #endregion

        #region

        public bool _loading_complete()
        {
            if (!IsComplete || _assetbundle == null || _assetMap.Count == 0) return false;
            return true;

        }

        #endregion
    }
}

