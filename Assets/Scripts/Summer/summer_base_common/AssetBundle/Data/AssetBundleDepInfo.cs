using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// AssetBundle的依赖信息
    /// </summary>
    public class AssetBundleDepInfo
    {
        //private int ref_count = 0;
        //public int RefCount { get { return ref_count; } set { ref_count = value; } }
        public string AssetBundleName { get { return _assetbundle_name; } }
        public Dictionary<string, int> parent_ref = new Dictionary<string, int>();                  // 爸爸有谁
        public Dictionary<string, int> child_ref = new Dictionary<string, int>();                   // 儿子有谁
        public string _assetbundle_name;

        public AssetBundleDepInfo(string[] infos)
        {

            _assetbundle_name = infos[0];

            for (int i = 1; i < infos.Length; i++)
            {
                bool result = child_ref.ContainsKey(infos[i]);
                LogManager.Assert(result, "初始化AssetBundlePackage的包信息失败，[{0}]", infos[i]);
                if (result) continue;
                child_ref.Add(infos[i], 0);
            }
            //ref_count = 0;
        }

    }

}
