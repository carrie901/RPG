using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace SummerEditor
{
    public class EAssetBundleRefPanel : EComponent
    {
        public static float e_width = 800;
        public static float e_height = 500;
        public ELabel _asset_name = new ELabel(e_width, 30f, "");
        public ELabel _asset_size = new ELabel(e_width, 30f, "");
        public EScrollView _deps_view = new EScrollView(e_width - 20, e_height - 100);
        public EAssetBundleRefPanel() : base(e_width, e_height)
        {
            _init();
        }

        public void _init()
        {
            AddComponent(_asset_name, 0, 0);
            AddComponent(_asset_size, 0, 30);
            AddComponent(_deps_view, 10, 70);

            //查看信息
            EAbAnalysisEvent.Instance.RegisterHandler(E_Editor_Analysis.check_asset, ResetAssetInfo);
        }

        public void ResetAssetInfo(System.Object obj)
        {
            EabMainVbo main_vbo = obj as EabMainVbo;
            if (main_vbo == null) return;
            _asset_name.text = "路径:" + main_vbo.asset_name;
            _asset_size.text = "大小:" + main_vbo.size + "kb";
            _deps_view.Clear();


            List<EabDepVbo> dep_map = main_vbo._dep_map.Values.ToList();
            for (int i = 0; i < dep_map.Count; i++)
            {
                EDepInfoItem02 dep_item = new EDepInfoItem02(dep_map[i]);
                _deps_view.AddItem(dep_item);
            }
        }
    }
}


