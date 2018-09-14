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
    public class EAssetBundleHelper
    {

        public static void FindRealDep(string assetPath, List<string> deps)
        {
            deps.Clear();

            string[] topDeps = AssetDatabase.GetDependencies(assetPath, false);
            string[] allDeps = AssetDatabase.GetDependencies(assetPath, true);

            Dictionary<string, int> noTopDeps = new Dictionary<string, int>();
            noTopDeps.Add(assetPath, 1);

            for (int i = topDeps.Length - 1; i >= 0; i--)
            {
                _find_child_deps(noTopDeps, topDeps[i]);
            }

            // 剔除了一些重复性质的资源
            deps.AddRange(topDeps);
            for (int i = deps.Count - 1; i >= 0; i--)
            {
                if (noTopDeps.ContainsKey(deps[i]))
                {
                    deps.RemoveAt(i);
                }
            }

            Debug.AssertFormat(deps.Count + noTopDeps.Count == allDeps.Length, "[{0}]依赖计算失败", assetPath);
        }

        // 资源的top依赖
        public static void _find_child_deps(Dictionary<string, int> depMap, string assetPath)
        {
            string[] topDeps = AssetDatabase.GetDependencies(assetPath, false);
            int length = topDeps.Length;
            for (int i = 0; i < length; i++)
            {
                if (depMap.ContainsKey(topDeps[i]))
                {
                    depMap[topDeps[i]]++;
                }
                else
                {
                    depMap.Add(topDeps[i], 1);
                }

                _find_child_deps(depMap, topDeps[i]);
            }
        }
    }



}

