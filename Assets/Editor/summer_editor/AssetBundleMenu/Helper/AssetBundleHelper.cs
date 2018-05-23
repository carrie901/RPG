using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// 想解决
    ///  A.Prefab
    ///    / \    
    ///   m1  fbx1
    ///   /    \
    ///  /      m1   这种情况
    ///  
    /// Yes  ---> A.Prefab的Deps  fbx1
    /// No   ---> A.Prefab的Deps  fbx1 m1
    /// </summary>
    public class AssetBundleHelper
    {

        public static void FindRealDep(string asset_path, List<string> deps)
        {
            deps.Clear();

            string[] top_deps = AssetDatabase.GetDependencies(asset_path, false);
            string[] all_deps = AssetDatabase.GetDependencies(asset_path, true);

            Dictionary<string, int> no_top_deps = new Dictionary<string, int>();
            no_top_deps.Add(asset_path, 1);

            for (int i = top_deps.Length - 1; i >= 0; i--)
            {
                _find_child_deps(no_top_deps, top_deps[i]);
            }

            // 剔除了一些重复性质的资源
            deps.AddRange(top_deps);
            for (int i = deps.Count - 1; i >= 0; i--)
            {
                if (no_top_deps.ContainsKey(deps[i]))
                {
                    deps.RemoveAt(i);
                }
            }

            Debug.AssertFormat(deps.Count + no_top_deps.Count == all_deps.Length, "[{0}]依赖计算失败", asset_path);
        }

        // 资源的top依赖
        public static void _find_child_deps(Dictionary<string, int> dep_map, string asset_path)
        {
            string[] top_deps = AssetDatabase.GetDependencies(asset_path, false);
            for (int i = 0; i < top_deps.Length; i++)
            {
                if (dep_map.ContainsKey(top_deps[i]))
                {
                    dep_map[top_deps[i]]++;
                }
                else
                {
                    dep_map.Add(top_deps[i], 1);
                }

                _find_child_deps(dep_map, top_deps[i]);
            }
        }
    }



}

