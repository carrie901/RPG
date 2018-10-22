using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 这个文件夹下的纹理资源按照大小，A+B+C小于指定大小达成一个包
    /// </summary>
    public class BuildAssetInAbByTextureSize : I_AssetBundleStratey
    {
        public string _directory;
        public Dictionary<string, int> _assetsMap = new Dictionary<string, int>();
        public BuildAssetInAbByTextureSize(string path)
        {
            _directory = path;
        }

        public void SetAbName(string filePath, string directory, int index)
        {
            AssetBundleSetNameE.SetAbNameByParam(filePath, string.Format("{0}/{1:0000}", directory, index));
        }

        public bool IsBundleStratey(EAssetObjectInfo info)
        {
            if (_assetsMap.ContainsKey(info.AssetPath))
                return true;
            return false;
        }

        public void AddAssetBundleFileInfo(EAssetObjectInfo info)
        {

        }

        public void SetAssetBundleName()
        {
            // 脑子不好，直接粗糙算法，直接按照顺序加下去
            //int length = _assets_map.Count;
            float leftSize = EAssetBundleConst.ASSETBUILD_MAX_SIZE;
            string directory = EPathHelper.AbsoluteToRelativePathRemoveAssets(_directory);
            int nameIndex = 1;

            foreach (var info in _assetsMap)
            {
                string assetPath = info.Key;
                float fileSize = EMemorySizeHelper.CalculateTextureSizeBytes(assetPath);
                if (fileSize >= EAssetBundleConst.ASSETBUILD_MAX_SIZE)
                {
                    SetAbName(info.Key, directory, nameIndex);
                    nameIndex++;
                }
                else
                {
                    if (leftSize >= fileSize)
                    {
                        SetAbName(assetPath, directory, nameIndex);
                        leftSize = leftSize - fileSize;
                    }
                    else
                    {
                        nameIndex++;
                        leftSize = EAssetBundleConst.ASSETBUILD_MAX_SIZE - fileSize;

                        SetAbName(assetPath, directory, nameIndex);
                    }
                }
            }
        }
    }
}
