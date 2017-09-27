using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    /// <summary>
    /// 
    /// </summary>
    public class EAssetInfoItem : EComponent
    {
        public ELabel _lab_asset_path;
        public ELabel _lab_asset_size;
        public EButton _btn_asset_path;
        public EButton _btn_asset_ref;
        public EabMainVbo _ab_main;
        public EAssetInfoItem(EabMainVbo ab_main) : base(1000, 32)
        {
            _ab_main = ab_main;
            InitComponent();
        }

        //TODO最好做出改变
        public void InitComponent()
        {
            _lab_asset_path = new ELabel(750, 30, _ab_main.asset_name);
            _lab_asset_size = new ELabel(80, 30, _ab_main.size.ToString());
            _btn_asset_path = new EButton(50, 30, "查看资源");
            _btn_asset_ref = new EButton(50, 30, "引用资源");

            float left = 10;
            AddComponent(_lab_asset_path, left, 0);
            left += _lab_asset_path.Size.x + 5;

            AddComponent(_lab_asset_size, left, 0);
            left += _lab_asset_size.Size.x + 5;

            AddComponent(_btn_asset_path, left, 0);
            left += _btn_asset_path.Size.x + 5;

            AddComponent(_btn_asset_ref, left, 0);

            _lab_asset_size.SetPositionRight(_lab_asset_path);
            _btn_asset_path.SetPositionRight(_lab_asset_size);
            _btn_asset_ref.SetPositionRight(_btn_asset_path);

            _btn_asset_path.on_click += CheckAsset;
        }

        public void CheckAsset(EButton button)
        {
            EAbAnalysisEvent.Instance.RaiseEvent(E_Editor_Analysis.check_asset, _ab_main);
        }
    }
}

