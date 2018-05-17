using UnityEngine;
using System.Collections.Generic;
using System.Text;
using UnityEditor;

namespace SummerEditor
{
    /// <summary>
    /// 依赖资源，一个资源不可以是主资源又属于依赖资源，目前不解决如此复杂的情况
    /// </summary>
    public class EabDepVbo
    {
        public string asset_name;                                                                       // 资源名称
        public int ref_count;                                                                           // 引用次数
        public float size;                                                                              // 资源大小

        public Dictionary<string, EabMainVbo> _ref_main_ab = new Dictionary<string, EabMainVbo>();      // 所属的主资源
        public EabDepVbo(string path)
        {
            asset_name = path;
            ref_count = 0;
            _init();

        }

        public void RefMainAb(EabMainVbo main_ab)
        {
            /*if (main_ab == null) return;

            if (_ref_main_ab.ContainsKey(main_ab._asset_path))
            {
                Debug.Log(string.Format("已经引用了这个资源，[{0}]", main_ab._asset_path));
                return;
            }
            _ref_count++;
            _ref_main_ab.Add(main_ab._asset_path, main_ab);*/
        }

        public string GetString(string tab)
        {
            string str_tab = "\t";
            tab = tab + str_tab;
            StringBuilder sb = new StringBuilder();
            /*sb.AppendLine(tab + "<DepAb>");
            sb.AppendLine(tab + str_tab + "_asset_path = " + asset_name);
            sb.AppendLine(tab + str_tab + "size = " + size);
            sb.AppendLine(tab + str_tab + "_ref_count=" + _ref_count);
            sb.AppendLine(tab + "</DepAb>");*/
            return sb.ToString();
        }

        public void _init()
        {
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(asset_name);
            if (obj == null) return;

            long tsize = EMemorySizeHelper.GetRuntimeMemorySize(obj);
            if (EPathHelper.IsTexture(asset_name))
                tsize = tsize / 2;
            size = (float)tsize / 1024;
            //Debug.Log("依赖资源:" + _asset_path + "内存占用:  " + size + "kb");
        }
    }
}

