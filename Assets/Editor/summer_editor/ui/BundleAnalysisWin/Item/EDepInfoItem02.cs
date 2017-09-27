using UnityEngine;
using System.Collections;

namespace SummerEditor
{
    /// <summary>
    /// 主资源的依赖资源的简略信息
    /// </summary>
    public class EDepInfoItem02 : EComponent
    {
        public static float e_width = 760;
        public static float e_height = 30;
        public ELabel _lab_dep_path = new ELabel(600, e_height, "");
        public ELabel _lab_dep_size = new ELabel(50, e_height, "");
        public ELabel _lab_dep_ref_count = new ELabel(50, e_height, "");
        public EabDepVbo _dep_info;
        public EDepInfoItem02(EabDepVbo dep_info) : base(e_width, e_height)
        {
            _dep_info = dep_info;
            _init();
        }

        public void _init()
        {
            if (_dep_info == null) return;
            _lab_dep_path.text = _dep_info.asset_name;
            _lab_dep_size.text = _dep_info.size.ToString();
            _lab_dep_ref_count.text = "引用:" + _dep_info.ref_count;
            float left = 10;
            AddComponent(_lab_dep_path, left, 0);
            left += _lab_dep_path.Size.x;

            AddComponent(_lab_dep_size, left, 0);
            left += _lab_dep_size.Size.x;

            AddComponent(_lab_dep_ref_count, left, 0);
        }
    }
}

