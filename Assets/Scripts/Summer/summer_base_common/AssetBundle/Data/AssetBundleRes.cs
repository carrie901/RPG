

namespace Summer
{
    public class AssetBundleRes
    {
        public string res_path;
        public string pack_path;

        public AssetBundleRes(string[] infos)
        {
            res_path = infos[0];
            pack_path = infos[1];
        }
    }
}

