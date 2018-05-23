using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 根据引用，不知道如何用，分析了下引用合并的情况下 能合并多少东西，以及复杂引用导致出现的更加复杂情况
    /// </summary>
    public class BuildAssetInRefAb : I_AssetBundleStratey
    {
        public static Dictionary<string, int> ref_map = new Dictionary<string, int>();
        public static string common_ab = "Assets/common/ref/";
        public Dictionary<string, int> _assets_map = new Dictionary<string, int>();

        public bool IsBundleStratey(EAssetObjectInfo info)
        {
            return true;
        }

        public void AddAssetBundleFileInfo(EAssetObjectInfo info)
        {

        }

        public void SetAssetBundleName()
        {
            string result = string.Empty;
            foreach (var info in _assets_map)
            {
                result += EPathHelper.GetName(info.Key) + "_";
            }

            if (ref_map.ContainsKey(result))
            {
                UnityEngine.Debug.LogError("重复的名字" + result);

            }
            foreach (var info in _assets_map)
            {
                AssetBundleSetNameE.SetAbNameByParam(info.Key, result);
            }

        }
    }
}
