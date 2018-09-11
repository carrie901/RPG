using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 这个文件夹下所有资源的达成一个包
    /// </summary>
    public class BuildAssetInOneAb : I_AssetBundleStratey
    {
        public string _directory;
        public Dictionary<string, int> _assetsMap = new Dictionary<string, int>();
        public BuildAssetInOneAb(string path)
        {
            _directory = path;
            _init();
        }

        public bool IsBundleStratey(EAssetObjectInfo info)
        {
            if (_assetsMap.ContainsKey(info.AssetPath))
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
            foreach (var info in _assetsMap)
            {
                string assetPath = info.Key;
                AssetBundleSetNameE.SetAbNameByParam(assetPath, _directory);
            }
        }

        public void _init()
        {
            List<string> assetsPath = EPathHelper.GetAssetsPath(_directory, true);
            for (int i = 0; i < assetsPath.Count; i++)
            {
                _assetsMap.Add(assetsPath[i], 1);
            }
        }
    }
}
