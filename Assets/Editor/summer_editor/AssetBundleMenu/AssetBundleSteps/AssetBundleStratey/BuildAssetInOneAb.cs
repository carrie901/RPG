using System.Collections.Generic;
using UnityEditor;

namespace SummerEditor
{
    /// <summary>
    /// 这个文件夹下所有资源的达成一个包
    /// </summary>
    public class BuildAssetInOneAb : I_AssetBundleStratey
    {
        public string _directory;
        public Dictionary<string, int> _assetsMap = new Dictionary<string, int>();
        public string _abName = string.Empty;
        public BuildAssetInOneAb(string path)
        {
            _directory = path;
            _abName = EPathHelper.AbsoluteToRelativePathRemoveAssets(_directory);
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

        public void SetAssetBundleName()
        {
            int count = _assetsMap.Count;
            int index = 0;
            foreach (var info in _assetsMap)
            {
                index++;
                EditorUtility.DisplayProgressBar("设置主AssetBundle名字", info.Key, (float)(index) / count);
                AssetBundleSetNameE.SetAbNameByParam(info.Key, _abName);
            }
            EditorUtility.ClearProgressBar();
        }

        private void _init()
        {
            List<string> assetsPath = EPathHelper.GetAssetsPath(_directory, true);
            int length = assetsPath.Count;
            for (int i = 0; i < length; i++)
            {
                _assetsMap.Add(assetsPath[i], 1);
            }
        }
    }
}
