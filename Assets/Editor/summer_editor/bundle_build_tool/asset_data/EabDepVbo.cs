using UnityEngine;
using System.Collections.Generic;
using System.Text;
using UnityEditor;

namespace SummerEditor
{


    public class EabDepVbo
    {
        public string asset_name;
        public int ref_count;
        public float size;

        public Dictionary<string, EabMainVbo> _ref_main_ab = new Dictionary<string, EabMainVbo>();
        public EabDepVbo(string path)
        {
            asset_name = path;
            ref_count = 0;
            _init();

        }

        public void RefMainAb(EabMainVbo main_ab)
        {
            if (main_ab == null) return;

            if (_ref_main_ab.ContainsKey(main_ab.asset_name))
            {
                Debug.Log(string.Format("已经引用了这个资源，[{0}]", main_ab.asset_name));
                return;
            }
            ref_count++;
            _ref_main_ab.Add(main_ab.asset_name, main_ab);
        }

        public string GetString(string tab)
        {
            string str_tab = "\t";
            tab = tab + str_tab;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tab + "<DepAb>");
            sb.AppendLine(tab + str_tab + "asset_name = " + asset_name);
            sb.AppendLine(tab + str_tab + "size = " + size);
            sb.AppendLine(tab + str_tab + "ref_count=" + ref_count);
            sb.AppendLine(tab + "</DepAb>");
            return sb.ToString();
        }

        public void _init()
        {
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(asset_name);
            if (obj == null) return;

            int tsize = UnityEngine.Profiling.Profiler.GetRuntimeMemorySize(obj);
            //string size_memory = EditorUtility.FormatBytes(tsize);
            if (EditorPath.IsTexture(asset_name))
                tsize = tsize / 2;
            size = (float)tsize / 1024;
            Debug.Log("依赖资源:" + asset_name + "内存占用:  " + size + "kb");
        }




    }
}

