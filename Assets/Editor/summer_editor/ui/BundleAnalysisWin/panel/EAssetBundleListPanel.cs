using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 资源列表
    /// </summary>
    public class EAssetBundleListPanel : EScrollView
    {
        public EAssetBundleListPanel(float width, float height)
            : base(width, height)
        {
            _init();
        }

        public void _init()
        {
            /*Dictionary<string, EabMainVbo> main_ab_map = AssetAnalysisE._main_ab_map;
            bool color_tmp = false;
            foreach (var info in main_ab_map)
            {
                EabMainVbo ab = info.Value;
                EAssetInfoItem ab_item = new EAssetInfoItem(ab);
                ab_item.SetBgColor(color_tmp);
                color_tmp = !color_tmp;
                AddItem(ab_item);
            }*/
        }

    }
}

