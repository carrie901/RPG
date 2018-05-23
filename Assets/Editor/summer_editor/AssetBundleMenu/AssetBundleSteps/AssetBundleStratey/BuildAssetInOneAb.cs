using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 这个文件夹下所有资源的达成一个包
    /// </summary>
    public class BuildAssetInOneAb : I_AssetBundleStratey
    {
        public string _directory;
        public Dictionary<string, int> _assets_map = new Dictionary<string, int>();
        public BuildAssetInOneAb(string path)
        {
            _directory = path;
            _init();
        }

        public bool IsBundleStratey(EAssetObjectInfo info)
        {
            if (_assets_map.ContainsKey(info.AssetPath))
            {
                return true;
            }
            return false;
        }

        public void AddAssetBundleFileInfo(EAssetObjectInfo info)
        {

        }

        public void SetAssetBundleName()
        {
            foreach (var info in _assets_map)
            {
                string asset_path = info.Key;
                AssetBundleSetNameE.SetAbNameByParam(asset_path, _directory);
            }
        }

        public void _init()
        {
            List<string> assets_path = EPathHelper.GetAssetsPath(_directory, true);
            for (int i = 0; i < assets_path.Count; i++)
            {
                _assets_map.Add(assets_path[i], 1);
            }
        }
    }
}
