using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public abstract class EAssetInfo
    {
        public string asset_path;                                                                       // 资源名称
        public int ref_count;                                                                           // 引用次数
        public float size;                                                                              // 资源大小                                         
        public Dictionary<string, EAssetInfo> _child_dep_map = new Dictionary<string, EAssetInfo>();       // 依赖资源

        protected EAssetInfo(string path)
        {
            asset_path = path;
            ref_count = 0;
        }

        public void Init()
        {
            string[] deps = AssetDatabase.GetDependencies(asset_path, false);
            int length = deps.Length;
            for (int i = 0; i < length; i++)
            {
                string asset_dep_path = deps[i];
                EAssetInfo dep_ab = AssetBundleAnalysisE.FindDep(asset_dep_path);
                if (dep_ab == null) continue;

                if (_child_dep_map.ContainsKey(dep_ab.asset_path))
                {
                    Debug.LogError(string.Format("主资源[{0}],已经存在了依赖资源了[{1}]", asset_path, deps[i]));
                    continue;
                }
                _child_dep_map.Add(dep_ab.asset_path, dep_ab);
                ref_count++;
                dep_ab.RefParent(this);
            }
        }

        public abstract void RefParent(EAssetInfo par_info);
        public abstract bool IsMainAsset();
    }

    public class EAssetMainInfo : EAssetInfo
    {
        public EAssetMainInfo(string path) : base(path)
        {
            string[] deps = AssetDatabase.GetDependencies(asset_path, true);
            for (int i = 0; i < deps.Length; i++)
                AssetBundleAnalysisE.AddDepAsset(deps[i]);
        }

        public override bool IsMainAsset()
        {
            return true;
        }

        public override void RefParent(EAssetInfo par_info)
        {
            Debug.LogErrorFormat("主资源[{0}]的上层主资源[{1}]ERROR", par_info.asset_path, par_info.asset_path);
        }
    }

    public class EAssetDepInfo : EAssetInfo
    {
        public Dictionary<string, EAssetInfo> _parent_dep_map = new Dictionary<string, EAssetInfo>();   // 依赖资源
        public EAssetDepInfo(string path) : base(path)
        {
        }

        public override void RefParent(EAssetInfo par_info)
        {
            if (_parent_dep_map.ContainsKey(par_info.asset_path))
            {
                Debug.LogErrorFormat("上层资源[{0}]已经被依赖了:", par_info.asset_path);
                return;
            }
            _parent_dep_map.Add(par_info.asset_path, par_info);
        }

        public override bool IsMainAsset()
        {
            return false;
        }
    }

}
