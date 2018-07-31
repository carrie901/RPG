using System.Collections.Generic;
namespace Summer
{
    /// <summary>
    /// AssetBundle的依赖信息
    /// </summary>
    public class AssetBundleDepInfo
    {
        public string AssetBundleName { get { return _assetbundle_name; } }                         // 主ab的名字 full path res_bundle/../..
        public int dep_count;                                                                       // 依赖个数
        public Dictionary<string, int> child_ref = new Dictionary<string, int>();                   // 儿子有谁 依赖配置表已经确定了
        public string _assetbundle_name;

        public AssetBundleDepInfo(string[] infos)
        {
            _assetbundle_name = infos[0];
            dep_count = infos[1].ToInt();
            for (int i = 2; i < infos.Length; i++)
            {
                bool result = child_ref.ContainsKey(infos[i]);
                ResLog.Assert(!result, "初始化AssetBundlePackage的包信息失败，[{0}]", infos[i]);
                if (result) continue;
                child_ref.Add(infos[i], 0);
            }
        }
    }
}
