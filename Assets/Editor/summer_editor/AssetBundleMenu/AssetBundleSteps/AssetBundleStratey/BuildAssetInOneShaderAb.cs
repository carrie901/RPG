using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 所有shader打包到一个ab中
    /// 
    /// 如何搜索到所有的Shader
    /// </summary>
    public class BuildAssetInOneShaderAb : I_AssetBundleStratey
    {
        public List<string> _assetsMap = new List<string>();
        //public Dictionary<string, int> _assetsMap = new Dictionary<string, int>();
        public BuildAssetInOneShaderAb()
        {
            AddAssetBundleShader();
        }


        public bool IsBundleStratey(EAssetObjectInfo info)
        {
            if (info.AssetPath.EndsWith(".shader"))
            {
                return true;
            }

            return false;
        }

        public void SetAssetBundleName()
        {
            foreach (var info in _assetsMap)
            {
                AssetBundleSetNameE.SetAbNameByParam(info, EAssetBundleConst.SHADER_BUNDLE_NAME);
            }
        }

        private void AddAssetBundleShader()
        {
            Dictionary<string, EAssetObjectInfo> allAssets = EAssetBundleAnalysis._allAssets;
            foreach (var info in allAssets)
            {
                if (info.Key.EndsWith(".shader"))
                {
                    _assetsMap.Add(info.Key);
                }
            }
        }
    }
}
