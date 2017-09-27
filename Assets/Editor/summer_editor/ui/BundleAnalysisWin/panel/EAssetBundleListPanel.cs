using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SummerEditor
{
    public class EAssetBundleListPanel : EScrollView
    {
        public EAssetBundleListPanel(float width, float height)
            : base(width, height)
        {
            _init();
        }

        public void _init()
        {
            Dictionary<string, EabMainVbo> main_ab_map = EabAnalysisTool._main_ab_map;
            foreach (var info in main_ab_map)
            {
                EabMainVbo ab = info.Value;
                EAssetInfoItem ab_item = new EAssetInfoItem(ab);
                AddItem(ab_item);
            }
        }

    }
}

