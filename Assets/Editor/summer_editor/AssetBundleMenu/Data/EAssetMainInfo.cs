

using UnityEditor;
using UnityEngine;
namespace SummerEditor
{
    public class EAssetMainInfo : EAssetInfo
    {
        public EAssetMainInfo(string path) : base(path)
        {
            string[] deps = AssetDatabase.GetDependencies(_asset_path, true);
            int length = deps.Length;
            for (int i = 0; i < length; i++)
                AssetBundleAnalysisE.AddDepAsset(deps[i]);
        }

        public override bool IsMainAsset()
        {
            return true;
        }
    }
}
