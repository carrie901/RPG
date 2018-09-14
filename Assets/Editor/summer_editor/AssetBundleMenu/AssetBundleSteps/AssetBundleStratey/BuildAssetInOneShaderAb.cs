using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 所有shader打包到一个ab中
    /// </summary>
    public class BuildAssetInOneShaderAb : I_AssetBundleStratey
    {
        public Dictionary<string, int> _assetsMap = new Dictionary<string, int>();
        public BuildAssetInOneShaderAb()
        {
        }


        public bool IsBundleStratey(EAssetObjectInfo info)
        {
            if (info.AssetPath.EndsWith(".shader"))
            {
                return true;
            }

            return false;
        }

        public void AddAssetBundleFileInfo(EAssetObjectInfo info)
        {
            _assetsMap.Add(info.AssetPath, 1);
        }

        public void SetAssetBundleName()
        {
            foreach (var info in _assetsMap)
            {
                AssetBundleSetNameE.SetAbNameByParam(info.Key, EAssetBundleConst.SHADER_BUNDLE_NAME);
            }
        }
    }
}
