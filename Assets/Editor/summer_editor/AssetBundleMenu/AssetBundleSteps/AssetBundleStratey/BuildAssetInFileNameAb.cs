

using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 资源按照自己的文件名字设置Bundle Name
    /// </summary>
    public class BuildAssetInFileNameAb : I_AssetBundleStratey
    {
        public Dictionary<string, int> _assetsMap = new Dictionary<string, int>();
        public BuildAssetInFileNameAb()
        {

        }


        public bool IsBundleStratey(EAssetObjectInfo info)  
        {
            return true;
        }

        public void SetAssetBundleName()
        {
            foreach (var info in _assetsMap)
            {
                AssetBundleSetNameE.SetAbNameByPath(info.Key);
            }

        }
    }
}
