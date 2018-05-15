using System.Collections.Generic;
using System.Text;
using Summer;
using UnityEditor;
using Object = UnityEngine.Object;
using UnityEngine;
namespace SummerEditor
{
    /// <summary>
    /// 主依赖资源包 一个资源不可以是主资源又属于依赖资源，目前不解决如此复杂的情况
    /// </summary>
    public class EabMainVbo
    {
        public string asset_path;                                                                       // 资源的名称
        public float size;                                                                              // 大小是kb
        //public float file_size;
        public Dictionary<string, EabDepVbo> _dep_map = new Dictionary<string, EabDepVbo>();            // 它所依赖的资源
        public EabMainVbo(string path)
        {
            asset_path = path;
            string[] deps = AssetDatabase.GetDependencies(path);
            string[] deps_recursive = AssetDatabase.GetDependencies(path, true);// 递归资源
            if (deps.Length != deps_recursive.Length)
            {
                Debug.LogError("递归资源不一致：" + path);
            }
            int length = deps.Length;
            for (int i = 0; i < length; i++)
            {
                if (deps[i].Contains(EAssetBundleConst.IGNORE_SUFFIX)) continue;
                EabDepVbo dep_ab = AssetAnalysisE.FindDep(deps[i]);
                if (_dep_map.ContainsKey(dep_ab.asset_name))
                {
                    Debug.LogError(string.Format("主资源[{0}],已经存在了依赖资源了[{1}]", asset_path, deps[i]));
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
            sb.AppendLine(tab + str_tab + "asset_path = " + asset_path);
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
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(asset_path);
            if (obj == null)
            {
                Debug.LogError(string.Format("找不到主资源,路径:[{0}]", asset_path));
                return;
            }
            long t_size = EMemorySizeHelper.GetRuntimeMemorySize(obj);

            float all_size = t_size / 1024f;
            foreach (var dep in _dep_map)
            {
                all_size += dep.Value.size;
            }

            size = all_size;
            //Debug.Log("主资源:" + asset_path + "占用内存size:" + size + " Kb");
            //Debug.Log("================================================================================");
        }

    }
}

