using System;
using Object = UnityEngine.Object;

namespace Summer
{
    public class W3Loader : I_ResourceLoad
    {
        public static W3Loader instance = new W3Loader();

        public AssetInfo LoadAsset<T>(string path) where T : UnityEngine.Object
        {
            throw new NotImplementedException();
        }

        public LoadOpertion LoadAssetAsync<T>(string path) where T : UnityEngine.Object
        {
            throw new NotImplementedException();
        }

        public bool UnloadAll()
        {
            throw new NotImplementedException();
        }

        public bool UnloadAssetBundle(string assetbundle_path)
        {
            throw new NotImplementedException();
        }

        public void OnUpdate()
        {
            throw new NotImplementedException();
        }

        public bool HasInLoading(string name)
        {
            return true;
        }
    }
}

