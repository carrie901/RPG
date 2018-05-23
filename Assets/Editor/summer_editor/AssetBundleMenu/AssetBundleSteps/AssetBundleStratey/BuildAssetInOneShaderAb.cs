using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 所有shader打包到一个ab中
    /// </summary>
    public class BuildAssetInOneShaderAb : I_AssetBundleStratey
    {
        public Dictionary<string, int> _assets_map = new Dictionary<string, int>();
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
            _assets_map.Add(info.AssetPath, 1);
        }

        public void SetAssetBundleName()
        {
            foreach (var info in _assets_map)
            {
                AssetBundleSetNameE.SetAbNameByParam(info.Key, EAssetBundleConst.shader_bundle_name);
            }
        }
    }
}
