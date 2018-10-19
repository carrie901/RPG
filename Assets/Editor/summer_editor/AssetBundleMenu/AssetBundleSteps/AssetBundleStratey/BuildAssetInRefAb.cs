using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 根据引用，不知道如何用，分析了下引用合并的情况下 能合并多少东西，以及复杂引用导致出现的更加复杂情况
    /// </summary>
    public class BuildAssetInRefAb : I_AssetBundleStratey
    {
        public static Dictionary<string, int> _refMap = new Dictionary<string, int>();
        public static string _commonAb = "Assets/common/ref/";
        public Dictionary<string, int> _assetsMap = new Dictionary<string, int>();

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
            foreach (var info in _assetsMap)
            {
                result += EPathHelper.GetName(info.Key) + "_";
            }

            if (_refMap.ContainsKey(result))
            {
                UnityEngine.Debug.LogError("重复的名字" + result);

            }
            foreach (var info in _assetsMap)
            {
                AssetBundleSetNameE.SetAbNameByParam(info.Key, result);
            }

        }
    }
}
