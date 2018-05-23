

using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 资源按照自己的文件名字设置Bundle Name
    /// </summary>
    public class BuildAssetInFileNameAb : I_AssetBundleStratey
    {
        public Dictionary<string, int> _assets_map = new Dictionary<string, int>();
        public BuildAssetInFileNameAb()
        {

        }


        public bool IsBundleStratey(EAssetObjectInfo info)
        {
            return true;
        }

        public void AddAssetBundleFileInfo(EAssetObjectInfo info)
        {
            if (_assets_map.ContainsKey(info.AssetPath))
                return;
            _assets_map.Add(info.AssetPath, 1);
        }

        public void SetAssetBundleName()
        {
            foreach (var info in _assets_map)
            {
                AssetBundleSetNameE.SetAbNameByPath(info.Key);
            }

        }
    }
}
