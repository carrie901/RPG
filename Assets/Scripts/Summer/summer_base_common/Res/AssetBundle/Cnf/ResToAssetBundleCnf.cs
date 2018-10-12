

namespace Summer
{
    /// <summary>
    /// 通过资源路径得到对应的AssetBundle包的路径
    /// </summary>
    public class ResToAssetBundleCnf
    {
        /// <summary>
        /// 资源的路径 res_bundle/xx/xx.prefab /.Animation /.bytes
        /// </summary>
        public string ResPath { get; private set; }
        /// <summary>
        /// 资源的名称
        /// </summary>
        public string ResName { get; private set; }
        /// <summary>
        /// 对应的包的路径.ab 路径-->StreamingAssets/rpg
        /// </summary
        public string PackagePath { get; private set; }

        public ResToAssetBundleCnf(string[] infos)
        {
            ResPath = infos[0];
            PackagePath = infos[1];
            Init();
        }

        // TODO 是否可以优化
        public void Init()
        {
            int index = ResPath.LastIndexOf('/');
            if (index < 0)
                ResName = ResPath;
            else
                ResName = ResPath.Substring(index + 1);
        }
    }
}

