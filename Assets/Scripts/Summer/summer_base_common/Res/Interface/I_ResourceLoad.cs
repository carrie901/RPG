
namespace Summer
{
    #region 资源加载

    public interface I_ResourceLoad
    {
        /// <summary>
        /// 同步加载
        /// </summary>
        AssetInfo LoadAsset(string resPath);
        /// <summary>
        /// 异步加载
        /// </summary>
        LoadOpertion LoadAssetAsync(string resPath);

        bool UnloadAssetBundle(AssetInfo assetInfo);

        void OnUpdate();
    }

    #endregion
}

