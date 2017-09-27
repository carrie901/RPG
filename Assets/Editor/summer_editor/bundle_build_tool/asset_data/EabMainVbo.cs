using System.Collections.Generic;
using System.Text;
using UnityEditor;
using Object = UnityEngine.Object;
using UnityEngine;
namespace SummerEditor
{
    public class EabMainVbo
    {
        public string asset_name;
        public float size;              //大小是kb
        public Dictionary<string, EabDepVbo> _dep_map = new Dictionary<string, EabDepVbo>();
        public EabMainVbo(string path)
        {
            asset_name = path;
            string[] deps = AssetDatabase.GetDependencies(path);
            for (int i = 0; i < deps.Length; i++)
            {
                if (deps[i].Contains(".cs")) continue;
                EabDepVbo dep_ab = EabAnalysisTool.FindDep(deps[i]);
                if (_dep_map.ContainsKey(dep_ab.asset_name))
                {
                    Debug.LogError(string.Format("主资源[{0}],已经存在了依赖资源了[{1}]", asset_name, deps[i]));
                    continue;
                }
                _dep_map.Add(dep_ab.asset_name, dep_ab);
                dep_ab.RefMainAb(this);
            }
            _init();
        }

        public string GetString(string tab)
        {
            string str_tab = "\t";
            tab = tab + str_tab;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(str_tab + "<MainAb>");
            sb.AppendLine(tab + str_tab + "asset_name = " + asset_name);
            sb.AppendLine(tab + str_tab + "size = " + size);
            sb.AppendLine(tab + str_tab + "<deps>");
            foreach (var info in _dep_map)
            {
                string str_dep = info.Value.GetString(tab + str_tab);
                sb.AppendLine(str_dep);
            }
            sb.AppendLine(tab + "</deps>");
            sb.AppendLine(str_tab + "</MainAb>");
            return sb.ToString();
        }

        public void ParseNode(EdNode node)
        {

        }
        public void _init()
        {
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(asset_name);
            if (obj == null)
            {
                Debug.LogError(string.Format("找不到主资源,路径:[{0}]", asset_name));
                return;
            }
            int t_size = UnityEngine.Profiling.Profiler.GetRuntimeMemorySize(obj);
            //string size_memory = EditorUtility.FormatBytes(t_size);
            float all_size = t_size / 1024f;
            foreach (var dep in _dep_map)
            {
                all_size += dep.Value.size;
            }

            size = all_size;
            Debug.Log("主资源:" + asset_name + "占用内存size:" + size);
            Debug.Log("================================================================================");

        }

    }
}

