using UnityEngine;
using System.Collections.Generic;

namespace Summer
{
    public class MainBundleInfo
    {
        public string _main_asset_name;
        public Dictionary<string, int> _dep_map;
        public int _load_count;
        public MainBundleInfo(string asset_name)
        {
            _main_asset_name = asset_name;
            string[] deps = AssetBundleLoader.instance._find_all_dependencies(_main_asset_name);

            if (deps != null)
            {
                _load_count = deps.Length;
                _dep_map = new Dictionary<string, int>(_load_count);
            }
        }

        public void Reset()
        {
            if (_dep_map == null) return;
            foreach (var info in _dep_map)
            {
                string name = info.Key;
                _dep_map[name] = 1;
            }
            _load_count = _dep_map.Count;
        }

        public bool IsDone()
        {
            return _load_count == 0;
        }

        public Dictionary<string, int> DepMap()
        {
            return _dep_map;
        }

        public void LoadComplete(string assetbundle_name)
        {
            if (_dep_map == null) return;
            if (_dep_map.ContainsKey(assetbundle_name))
            {
                _dep_map[assetbundle_name] = 0;
                _load_count--;
            }
        }
    }

    public class DepBundleInfo
    {
        private int ref_count = 0;
        public virtual int RefCount { get { return ref_count; } set { ref_count = value; } }
        public AssetBundle Bundle { get; set; }
        public DepBundleInfo(AssetBundle asset_bundle)
        {
            Bundle = asset_bundle;
            ref_count = 1;
        }
    }

}

