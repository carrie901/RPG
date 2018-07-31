namespace Summer
{
    public class W3Loader : I_ResourceLoad
    {
        public static W3Loader instance = new W3Loader();

        public AssetInfo LoadAsset(string path)
        {
            return null;
        }

        public void LoadSyncChildRes(string res_path) { }

        public LoadOpertion LoadAssetAsync(string path)
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
        public bool UnLoadChildRes(AssetInfo asset_info)
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

