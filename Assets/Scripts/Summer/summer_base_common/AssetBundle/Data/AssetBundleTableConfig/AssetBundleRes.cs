

namespace Summer
{
    public class AssetBundleRes
    {
        /// <summary>
        /// 资源的路径 .prefab /.Animation /.bytes
        /// </summary>
        public string res_path;
        /// <summary>
        /// 对应的包的路径.ab 路径 StreamingAssets/rpg
        /// </summary>
        public string package_path;
        /// <summary>
        /// 资源的名称
        /// </summary>
        public string res_name;

        public AssetBundleRes(string[] infos)
        {
            res_path = infos[0];
            package_path = infos[1];
            Init();
        }

        // TODO 是否可以优化
        public void Init()
        {
            int index = res_path.LastIndexOf('/');
            if (index < 0)
                res_name = res_path;
            else
                res_name = res_path.Substring(index + 1);
        }
    }
}

