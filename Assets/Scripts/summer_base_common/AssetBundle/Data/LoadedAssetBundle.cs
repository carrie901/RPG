using System;
using UnityEngine;

namespace Summer
{
    public class LoadedAssetBundle
    {

        public AssetBundle m_asset_bundle;
        public int m_referenced_count;

        internal event Action Unload;

        internal void OnUnload()
        {
            m_asset_bundle.Unload(false);
            if (Unload != null)
                Unload();
        }

        public LoadedAssetBundle(AssetBundle asset_bundle)
        {
            m_asset_bundle = asset_bundle;
            m_referenced_count = 1;
        }
    }

}
