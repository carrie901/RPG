using System;
using Object = UnityEngine.Object;

namespace Summer
{
    public class W3Loader : I_ResourceLoad
    {
        public static W3Loader instance = new W3Loader();

        public AssetInfo LoadAsset<T>(string path) where T : Object
        {
            return null;
        }

        public LoadOpertion LoadAssetAsync<T>(string path) where T : Object
        {
            return null;
        }

        public bool UnloadAll()
        {
            return false;
        }

        public bool UnloadAssetBundle(AssetInfo asset_info)
        {
            return false;
        }

        public void OnUpdate()
        {

        }

        public bool HasInLoading(string name)
        {
            return true;
        }
    }
}

