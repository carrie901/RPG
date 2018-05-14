using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class AssetBundlePackage
    {
        public string PackageName { get { return _package_name; } }

        public string _package_name;
        public Dictionary<string, int> _main_ab = new Dictionary<string, int>();

        public Dictionary<string, MainBundleInfo> _ab_map = new Dictionary<string, MainBundleInfo>();


        public bool HasAssetBundle()
        {
            return false;
        }

    }
}

