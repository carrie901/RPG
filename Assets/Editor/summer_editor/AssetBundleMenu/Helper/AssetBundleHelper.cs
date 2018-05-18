using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 想解决
    ///  A.Prefab
    ///    / \    
    ///   m1  fbx1
    ///   /    \
    ///  /      m1   这种情况
    /// </summary>
    public class AssetBundleHelper
    {
        /*public static string[] GetDependencies(string asset_path, bool recursive = true)
        {
            if (!recursive)
            {
                string[] deps = AssetDatabase.GetDependencies(asset_path, false);
                return deps;
            }
            else
            {
                string[] deps = AssetDatabase.GetDependencies(asset_path, true);
                List<string> dep_list = new List<string>();
                dep_list.AddRange(deps);

                string[] tmp_deps = dep_list.ToArray();

                return tmp_deps;

            }
        }


        public static void GetDepsDeep(string asset_path, string[] deps)
        {
            string[] tmp_dep = AssetDatabase.GetDependencies(asset_path, false);
            int length = tmp_dep.Length;
            for (int i = 0; i < length; i++)
            {
                GetDepsDeep(tmp_dep[i], deps);
            }

        }*/
    }



}

