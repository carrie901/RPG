using Object = UnityEngine.Object;
namespace Summer
{
    public class W3Loader : I_ResourceLoad
    {
        public static W3Loader instance = new W3Loader();

        public AssetInfo LoadAsset<T>(string resPath) where T : Object
        {
            return null;
        }

        public ResLoadOpertion LoadAssetAsync<T>(string resPath) where T : Object
        {
            return null;
        }

        public bool UnloadAll()
        {
            return false;
        }

        public bool UnloadAssetBundle(AssetInfo assetInfo)
        {
            return false;
        }
        public bool UnLoadChildRes(AssetInfo assetInfo)
        {
            return true;
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

