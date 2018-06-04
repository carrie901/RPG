using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// 打包策略
    /// </summary>
    public class EAssetBundleStrateyManager
    {

        #region private

        public void OnComplete()
        {
            EditorUtility.ClearProgressBar();
            Resources.UnloadUnusedAssets();
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();

            EditorUtility.DisplayDialog("设置名字完成", "请查看log日志", "Ok");
        }

        public void CalRef(Dictionary<string, EAssetObjectInfo> all_assets)
        {
            Dictionary<string, EAssetObjectInfo> ref_map = new Dictionary<string, EAssetObjectInfo>();
            foreach (var info in all_assets)
            {
                if (!info.Value.IsMainAsset)
                {
                    ref_map.Add(info.Key, info.Value);
                }
            }

            foreach (var first in all_assets)
            {
                foreach (var second in all_assets)
                {

                }
            }

        }


        #endregion
    }
}
