namespace Summer
{
    public class W3Loader : I_ResourceLoad
    {
        public static W3Loader instance = new W3Loader();

        public AssetInfo LoadAsset(string resPath)
        {
            return null;
        }

        public void LoadSyncChildRes(string resPath) { }

        public LoadOpertion LoadAssetAsync(string resPath)
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

