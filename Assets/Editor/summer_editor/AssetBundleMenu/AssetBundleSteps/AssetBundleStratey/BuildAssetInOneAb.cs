using System.Collections.Generic;
using UnityEditor;

namespace SummerEditor
{
    /// <summary>
    /// 这个文件夹下所有资源的达成一个包
    /// 名字是这个文件夹的名字
    /// </summary>
    public class BuildAssetInOneAb : I_AssetBundleStratey
    {
        public string _directory;
        public List<string> _assetsMap = new List<string>();
        public string _abName;
        public BuildAssetInOneAb(string path)
        {
            _directory = path;
            _abName = EPathHelper.AbsoluteToRelativePathRemoveAssets(_directory);
            _init();
        }

        public bool IsBundleStratey(EAssetObjectInfo info)
        {
            if (_assetsMap.Contains(info.AssetPath))
            {
                return true;
            }
            return false;
        }

        public void SetAssetBundleName()
        {
            int count = _assetsMap.Count;
            for (int i = 0; i < count; i++)
            {
                EditorUtility.DisplayProgressBar("设置主AssetBundle名字", _assetsMap[i], (float)(i + 1) / count);
                AssetBundleSetNameE.SetAbNameByParam(_assetsMap[i], _abName);
            }
            EditorUtility.ClearProgressBar();
        }

        private void _init()
        {
            List<string> assetsPath = EPathHelper.GetAssetsPath(_directory, true);
            int length = assetsPath.Count;
            for (int i = 0; i < length; i++)
            {
                _assetsMap.Add(assetsPath[i]);
            }
        }
    }
}
