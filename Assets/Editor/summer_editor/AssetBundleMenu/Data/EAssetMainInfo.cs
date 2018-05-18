

using UnityEditor;
using UnityEngine;
namespace SummerEditor
{
    public class EAssetMainInfo : EAssetInfo
    {
        public EAssetMainInfo(string path) : base(path)
        {
            int index = 0;
            string[] deps = AssetDatabase.GetDependencies(_asset_path, true);
            int length = deps.Length;
            for (int i = 0; i < length; i++)
            {
                // 有疑问
                _mem_size += EMemorySizeHelper.CalculateRuntimeMemorySize(deps[i]);
                if (deps[i] == path)
                {
                    index++;
                    continue;
                }

                
                AssetBundleAnalysisE.AddDepAsset(deps[i]);
            }

            if (index > 1)
                Debug.Log("Error");

        }

        public override bool IsMainAsset()
        {
            return true;
        }
    }
}
