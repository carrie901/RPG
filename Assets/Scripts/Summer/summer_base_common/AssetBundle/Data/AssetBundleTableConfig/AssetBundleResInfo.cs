

namespace Summer
{
    public class AssetBundleResInfo
    {
        /// <summary>
        /// 资源的路径 .prefab /.Animation /.bytes
        /// </summary>
        public string _resPath;
        /// <summary>
        /// 对应的包的路径.ab 路径-->StreamingAssets/rpg
        /// </summary>
        public string _packagePath;
        /// <summary>
        /// 资源的名称
        /// </summary>
        public string _resName;

        public AssetBundleResInfo(string[] infos)
        {
            _resPath = infos[0];
            _packagePath = infos[1];
            Init();
        }

        // TODO 是否可以优化
        public void Init()
        {
            int index = _resPath.LastIndexOf('/');
            if (index < 0)
                _resName = _resPath;
            else
                _resName = _resPath.Substring(index + 1);
        }
    }
}

