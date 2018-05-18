using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// 快速出效果的办法，等候后续的修改
    /// </summary>
    public class EAssetBundleItem
    {
        public ExcelAbInfo _info;

        public ELabel _asset_name_lab;
        public ELabel _ref_lab;
        public ELabel _be_ref_lab;

        public EAssetBundleItem(EAbTreeNodeData child_data)
        {
            _info = child_data.info;

            _asset_name_lab = new ELabel(100, _info.asset_path);
            _ref_lab = new ELabel(100, _info.ref_count.ToString());
            _be_ref_lab = new ELabel(100, _info.be_ref_count.ToString());
        }

        public void OnDraw(float parent_x, float parent_y, float left_x)
        {
            
        }
    }

}

