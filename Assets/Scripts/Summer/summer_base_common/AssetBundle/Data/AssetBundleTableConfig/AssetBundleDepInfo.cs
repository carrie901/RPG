﻿using System.Collections.Generic;
namespace Summer
{
    /// <summary>
    /// AssetBundle的依赖信息
    /// </summary>
    public class AssetBundleDepInfo
    {
        public string AssetBundleName { get { return _assetbundleName; } }                          // 主ab的名字 full path res_bundle/../..
        public int _depCount;                                                                       // 依赖个数
        public Dictionary<string, int> _childRef = new Dictionary<string, int>();                   // 儿子有谁 依赖配置表已经确定了
        public string _assetbundleName;

        public AssetBundleDepInfo(string[] infos)
        {
            _assetbundleName = infos[0];
            _depCount = infos[1].ToInt();
            for (int i = 2; i < infos.Length; i++)
            {
                bool result = _childRef.ContainsKey(infos[i]);
                ResLog.Assert(!result, "初始化AssetBundlePackage的包信息失败，[{0}]", infos[i]);
                if (result) continue;
                _childRef.Add(infos[i], 0);
            }
        }
    }
}
