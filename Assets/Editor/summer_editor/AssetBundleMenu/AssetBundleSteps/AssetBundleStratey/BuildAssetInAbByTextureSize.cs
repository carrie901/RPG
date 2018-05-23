using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 这个文件夹下的纹理资源按照大小，A+B+C小于指定大小达成一个包
    /// </summary>
    public class BuildAssetInAbByTextureSize : I_AssetBundleStratey
    {
        public string _directory;
        public Dictionary<string, int> _assets_map = new Dictionary<string, int>();
        public BuildAssetInAbByTextureSize(string path)
        {
            _directory = path;
        }

        public void SetAbName(string file_path, string directory, int index)
        {
            AssetBundleSetNameE.SetAbNameByParam(file_path, string.Format("{0}/{1:0000}", directory, index));
        }

        public bool IsBundleStratey(EAssetObjectInfo info)
        {
            if (_assets_map.ContainsKey(info.AssetPath))
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
            float left_size = EAssetBundleConst.ASSETBUILD_MAX_SIZE;
            string directory = EPathHelper.AbsoluteToRelativePathRemoveAssets(_directory);
            int name_index = 1;

            foreach (var info in _assets_map)
            {
                string asset_path = info.Key;
                float file_size = EMemorySizeHelper.CalculateTextureSizeBytes(asset_path);
                if (file_size >= EAssetBundleConst.ASSETBUILD_MAX_SIZE)
                {
                    SetAbName(info.Key, directory, name_index);
                    name_index++;
                }
                else
                {
                    if (left_size >= file_size)
                    {
                        SetAbName(asset_path, directory, name_index);
                        left_size = left_size - file_size;
                    }
                    else
                    {
                        name_index++;
                        left_size = EAssetBundleConst.ASSETBUILD_MAX_SIZE - file_size;

                        SetAbName(asset_path, directory, name_index);
                    }
                }
            }
        }
    }
}
